using CCWin;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSender
{

    //public delegate void HandTimer(bool value);    // zhj-声明委托
    public partial class UpdateForm : Form //CCSkinMain
    {

        //HandTimer handTimer1 = new HandTimer(SysForm1.ControlSysRecTimer);          //定义委托
        //HandTimer handTimer2 = new HandTimer(SerialDebug.ControlModbusTimer);

        OpenFileDialog openfile = new OpenFileDialog();

        static int PackageSize = 1024;
        byte[] databuf;                                //读取文件缓冲区
        byte[] datatosend = new byte[PackageSize];     //定义数据发送缓冲区
        byte[] ResponseData = new byte[1024];

        int PackageNum = 0;                                      //总包数
        int packet_zheng = 0;                                    //整包数
        int packet_yu = 0;                                       //不足1包的剩余字节数

        //正在刷固件
        bool Running = false;

        int StartTimeOut = 50;//握手信号超时时间 ms
        //int StartTimeOut = 5000;//握手信号超时时间 ms
        int StartRepeat = 100;//握手信号重发次数 
        //int timeout = 2000;//刷固件超时时间 ms
        int timeout = 5000;//刷固件超时时间 ms


        public Library.Richbox richbox;                              //固件下载消息框

        Library.CRC CRC = new Library.CRC();

        public UpdateForm()
        {
            InitializeComponent();

            /*#region 初始化控件缩放

            x = Width;
            y = Height;
            setTag(this);

            #endregion*/
        }

        #region 控件大小随窗体大小等比例缩放

        private readonly float x; //定义当前窗体的宽度
        private readonly float y; //定义当前窗体的高度

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0) setTag(con);
            }
        }

        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    var mytag = con.Tag.ToString().Split(';');
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(Convert.ToSingle(mytag[0]) * newx); //宽度
                    con.Height = Convert.ToInt32(Convert.ToSingle(mytag[1]) * newy); //高度
                    con.Left = Convert.ToInt32(Convert.ToSingle(mytag[2]) * newx); //左边距
                    con.Top = Convert.ToInt32(Convert.ToSingle(mytag[3]) * newy); //顶边距
                    var currentSize = Convert.ToSingle(mytag[4]) * newy; //字体大小                   
                    if (currentSize > 0) con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    con.Focus();
                    if (con.Controls.Count > 0) setControls(newx, newy, con);
                }
        }


        /// <summary>
        /// 重置窗体布局
        /// </summary>
        private void ReWinformLayout()
        {
            var newx = Width / x;
            var newy = Height / y;
            setControls(newx, newy, this);

        }

        #endregion

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            //IAP升级
            richbox = new Library.Richbox(richTextBox1);
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            try
            {
                openfile.Filter = @"固件(*.bin) | *.bin|固件(*.hex) | *.hex";
                //显示打开文件对话框
                openfile.ShowDialog();
                //获取所选择固件的名称
                skinTextBox1.Text = openfile.FileName;
                LoadFile();
                skinButton2.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            if (skinButton2.Text == "升级")
            {
/*                if (serialPort1.IsOpen == false)
                {
                    MessageBox.Show("串口未打开");
                    return;
                }*/
                richTextBox1.Text = string.Empty;
                LoadFile();
                Running = true;
                skinButton2.Text = "取消";

                //启动刷固件进程

                //SerialDebug.thread.Suspend();
                //Thread.Sleep(1000);

                //SerialDebug.signalStopFlag = true;              //结束modbus线程
                //Thread.Sleep(1000);

                //SerialDebug.subscriptionSerialRecEvent(true); //开启串口接收事件

                //结束其他线程
                SerialDebug.ControlModbusTimer(false);
                SysForm1.ControlSysRecTimer(false);

                /*                Thread.Sleep(5000);

                                SysForm1.ControlSysRecTimer(true);*/

                Thread updateThread = new Thread(new ThreadStart(threadFun));
                updateThread.IsBackground = true;
                updateThread.Start();

                //结束其他子进程
                //方法1.置信号量    - 线程正常结束会置stop状态，需要重新创建实例
                //SerialDebug.signalStopFlag = true;

/*                while (SerialDebug.thread.ThreadState != ThreadState.Stopped)
                {
                    Thread.Sleep(1000);
                }*/
                //SerialDebug.subscriptionSerialRecEvent(true);

                //方法2.挂起线程
               // SerialDebug.thread.Suspend();

                //SerialDebug.thread.Abort();
                /*while (SerialDebug.thread.ThreadState != ThreadState.Stopped || SerialDebug.thread.ThreadState != ThreadState.Suspended ||
                    SerialDebug.thread.ThreadState != ThreadState.Aborted)
                {
                    
                    Thread.Sleep(1000);
                }*/
            }
            else
            {
                Running = false;
                skinButton2.Text = "升级";
                this.Invoke(new MethodInvoker(delegate
                {
                    richbox.Msg(string.Format("已取消！"));
                }));
                skinButton2.Enabled = true;
            }
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {

        }

        private void LoadFile()
        {
            string str = string.Empty;
            //判断文件是Bin还是Hex
            if (openfile.FileName.EndsWith(".bin"))
            {
                //获取文件流
                FileStream fs = new FileStream(openfile.FileName, FileMode.Open);
                databuf = new byte[fs.Length];
                fs.Read(databuf, 0, (int)fs.Length);
                fs.Close();
            }
           /* else if (openfile.FileName.EndsWith(".hex"))
            {
                Library.HexToBin hexToBin = new Library.HexToBin();
                //转换到bin并获取数组
                databuf = hexToBin.ToBinByte(openfile.FileName, int.Parse(this.btnAddress.Text));
            }*/
            if (databuf != null)
            {
                packet_zheng = (int)databuf.Length / PackageSize;                                   //获取文件的整K字节数
                packet_yu = (int)databuf.Length % PackageSize;                                      //获取不足1K的剩余字节数
                PackageNum = packet_zheng + (packet_yu == 0 ? 0 : 1);
                this.Invoke(new MethodInvoker(delegate
                {
                    //显示文件的总字节数
                    richbox.Msg(Color.Blue, "文件共" + databuf.Length.ToString() + "字节");
                    //显示文件的总字节数
                    richbox.Msg(Color.DeepPink, "包大小：" + PackageSize.ToString());
                    //显示文件的总字节数
                    richbox.Msg(Color.DeepPink, "包总数：" + PackageNum.ToString());
                    //预计耗时
                    richbox.Msg(Color.DeepPink, "预计需要：" + (int)(databuf.Length / (115200 / 8.0) * 4) + "秒");
                }));
            }
        }


        /// <summary>
        /// 发送整包数据
        /// </summary>
        private void send_data_zheng(int index)
        {
            //单次发送的总长度
            int data_len = 4 + PackageSize + 4;
            byte[] Data = new byte[data_len];
            Array.Copy(databuf, index * PackageSize, datatosend, 0, PackageSize);
            //桢头
            Data[0] = 0xF1;
            //功能码
            Data[1] = 0x02;
            //包索引
            Data[2] = (byte)(index >> 8);
            Data[3] = (byte)index;
            for (int i = 0; i < PackageSize; i++)
            {
                Data[4 + i] = datatosend[i];
            }
            //CRC
            byte[] crc = CRC.CRC32_byte(Data, data_len - 4);
            //追加CRC
            Data[data_len - 4] = crc[0];
            Data[data_len - 3] = crc[1];
            Data[data_len - 2] = crc[2];
            Data[data_len - 1] = crc[3];
            SerialDebug.SendData(Data);
            //string str = string.Empty;
            //for (int j = 0; j < Data.Length; j++)
            //{
            //    str += String.Format("{0:X2}", Data[j]) + " ";
            //}
            //this.Invoke(new MethodInvoker(delegate
            //{
            //    richbox.Msg(str);
            //}));
        }

        /// <summary>
        /// 发送不足1包部分
        /// </summary>
        private void send_data_yu()
        {
            //单次发送的总长度
            int data_len = 4 + packet_yu + 4;
            byte[] Data = new byte[data_len];
            Array.Copy(databuf, packet_zheng * PackageSize, datatosend, 0, packet_yu);
            //桢头
            Data[0] = 0xF1;
            //功能码
            Data[1] = 0x02;
            //包索引
            Data[2] = (byte)(packet_zheng >> 8);
            Data[3] = (byte)packet_zheng;
            for (int i = 0; i < packet_yu; i++)
            {
                Data[4 + i] = datatosend[i];
            }
            //CRC
            byte[] crc = CRC.CRC32_byte(Data, data_len - 4);
            //追加CRC
            Data[data_len - 4] = crc[0];
            Data[data_len - 3] = crc[1];
            Data[data_len - 2] = crc[2];
            Data[data_len - 1] = crc[3];
            SerialDebug.SendData(Data);
            //string str = string.Empty;
            //for (int j = 0; j < Data.Length; j++)
            //{
            //    str += String.Format("{0:X2}", Data[j]) + " ";
            //}
            //this.Invoke(new MethodInvoker(delegate
            //{
            //    richbox.Msg(str);
            //}));
        }



        //刷固件进程
        private void threadFun()
        {
            try
            {

                Thread.Sleep(100);  //等待modbus线程的结束
                //SerialDebug.subscriptionSerialRecEvent(true); //开启接收事件
                

                int timeCount = 0;
                Library.Time time = new Library.Time();
                long StartTime = time.NowTimeMS();
                while (Running)
                {
                    //1.请求刷固
                    for (int i = 0; i < StartRepeat; i++)
                    {
                        try
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                richbox.Msg(Color.Green, "{0} 第{1}次请求刷固...", DateTime.Now.ToString("HH:mm:ss fff"), i + 1);
                            }));
                        }
                        catch
                        {
                        }

                        byte[] Data = new byte[12];
                        //桢头
                        Data[0] = 0xF1;
                        //功能码
                        Data[1] = 0x01;
                        //固件长度
                        Data[2] = (byte)(databuf.Length >> 24);
                        Data[3] = (byte)(databuf.Length >> 16);
                        Data[4] = (byte)(databuf.Length >> 8);
                        Data[5] = (byte)(databuf.Length & 0xff);
                        //分包数
                        Data[6] = (byte)(PackageNum >> 8);
                        Data[7] = (byte)PackageNum;
                        //CRC
                        byte[] crc = CRC.CRC32_byte(Data, Data.Length - 4);
                        //追加CRC
                        Data[8] = crc[0];
                        Data[9] = crc[1];
                        Data[10] = crc[2];
                        Data[11] = crc[3];

                        //测试
                        /*                        byte[] Data = new byte[8];

                                                Data[0] = 0x01;                      
                                                Data[1] = 0x04;
                                                Data[2] = 0x00;
                                                Data[3] = 0x00;
                                                Data[4] = 0x00;
                                                Data[5] = 0x1E;
                                                Data[6] = 0x70;
                                                Data[7] = 0x02;*/

                        SerialDebug.SendData(Data);
                        //Thread.Sleep(100);



                        string str = string.Empty;
                        for (int j = 0; j < Data.Length; j++)
                        {
                            str += String.Format("{0:X2}", Data[j]) + " ";
                        }
                        this.Invoke(new MethodInvoker(delegate
                        {
                            richbox.Msg(str);
                        }));

                        //等待响应
                        while (Running)
                        {
                            if (timeCount * 10 > StartTimeOut)
                            {
                                if (i == StartRepeat - 1)
                                {
                                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        richbox.Msg(Color.Red, "{0} 等待超时，设备无响应，任务已取消！", DateTime.Now.ToString("HH:mm:ss fff"));
                                        Running = false;
                                        skinButton2.Text = "升级";
                                    }));
                                    goto stop;
                                }
                                else
                                    break;
                            }
                            //收到响应，开始刷固件
                            
                            int readBytes = SerialDebug.ringBuffer.GetDataCount();
                            if (readBytes >= 8)
                            {
                                SerialDebug.ringBuffer.ReadBuffer(ResponseData, 0, readBytes);               

                                SerialDebug.ringBuffer.Clear(readBytes);

                                //检验crc
                                byte[] bt = CRC.CRC32_byte(ResponseData, readBytes - 4);
                                if (ResponseData[readBytes - 4] == bt[0] &&
                                    ResponseData[readBytes - 3] == bt[1] &&
                                    ResponseData[readBytes - 2] == bt[2] &&
                                    ResponseData[readBytes - 1] == bt[3])
                                {
                                    if (ResponseData[1] == 0x81)
                                    {
                                        //清除接收缓存
                                        Array.Clear(ResponseData, 0, ResponseData.Length);
                                        goto AA;
                                    }
                                }

                               

                            }

                            
                            Thread.Sleep(10);
                            timeCount++;
                        }
                        if (Running == false)
                        {
                            goto stop;
                        }
                        timeCount = 0;
                    }
                AA:
                    //更新启动时间
                    StartTime = time.NowTimeMS();
                    //2.开始刷固
                    //发送整包数据
                    for (int i = 0; i < packet_zheng; i++)
                    {
                        if (Running == false)
                            break;
                        this.Invoke(new MethodInvoker(delegate
                        {
                            richbox.Msg(Color.Blue, "{0} 已发送第{1}包,共{2}包", DateTime.Now.ToString("HH:mm:ss fff"), i + 1, PackageNum);
                        }));
                        send_data_zheng(i);
                        //等待响应
                        timeCount = 0;
                        bool Sendflag = true;
                        while (Sendflag && Running)
                        {
                            //超时重发
                            if (timeCount * 10 > timeout)
                            {
                                this.Invoke(new MethodInvoker(delegate
                                {
                                    richbox.Msg(Color.Red, "{0} 等待超时，重发！", DateTime.Now.ToString("HH:mm:ss fff"));
                                    timeCount = 0;
                                    send_data_zheng(i);
                                    //Running = false;
                                    //btn_IAP_send.Text = "下载";
                                }));
                                //goto stop;
                            }
                            //收到了反馈
                            int readBytes = SerialDebug.ringBuffer.GetDataCount();
                            if (readBytes >= 8)
                            {
                                SerialDebug.ringBuffer.ReadBuffer(ResponseData, 0, readBytes);

                                SerialDebug.ringBuffer.Clear(readBytes);

                                //检验crc
                                byte[] bt = CRC.CRC32_byte(ResponseData, readBytes - 4);
                                if (ResponseData[readBytes - 4] == bt[0] &&
                                    ResponseData[readBytes - 3] == bt[1] &&
                                    ResponseData[readBytes - 2] == bt[2] &&
                                    ResponseData[readBytes - 1] == bt[3])
                                {
                                    if (ResponseData[1] == 0x82)
                                    {
                                        int idx = (ResponseData[2] << 8) | ResponseData[3];
                                        //包索引是否一致
                                        if (i == idx)
                                        {
                                            //判断结果
                                            switch (ResponseData[4])
                                            {
                                                case 0x01:
                                                    //正常
                                                    if ((idx + 1) == PackageNum)
                                                    {
                                                        //升级完成
                                                        this.Invoke(new MethodInvoker(delegate
                                                        {
                                                            richbox.Msg(Color.Green, "{0} Flash写入成功！", DateTime.Now.ToString("HH:mm:ss fff"));
                                                            richbox.Msg(Color.Red, "完成！");
                                                        }));
                                                        Running = false;
                                                        skinButton2.Text = "升级";
                                                        goto stop;
                                                    }
                                                    else
                                                    {
                                                        //退出循环，继续下一包发送
                                                        Sendflag = false;
                                                    }
                                                    break;
                                                case 0x02:
                                                case 0x03:
                                                    //数据错误，重新发送
                                                    this.Invoke(new MethodInvoker(delegate
                                                    {
                                                        richbox.Msg(Color.Red, "{0} 等待超时，重发！", DateTime.Now.ToString("HH:mm:ss fff"));
                                                    }));
                                                    timeCount = 0;
                                                    send_data_zheng(i);
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        //清除接收缓存
                                        Array.Clear(ResponseData, 0, ResponseData.Length);
                                    }
                                }

                            }
                            
                            Thread.Sleep(10);
                            timeCount++;
                        }
                    }

                    //发送剩余数据
                    if (Running)
                    {
                        send_data_yu();
                        this.Invoke(new MethodInvoker(delegate
                        {
                            richbox.Msg(Color.Blue, "{0} 已发送第{1}包,共{2}包", DateTime.Now.ToString("HH:mm:ss fff"), PackageNum, PackageNum);
                        }));
                    }
                    //等待响应
                    timeCount = 0;
                    while (Running)
                    {
                        //超时重发
                        if (timeCount * 10 > timeout)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                richbox.Msg(Color.Red, "{0} 等待超时，重发！", DateTime.Now.ToString("HH:mm:ss fff"));
                                timeCount = 0;
                                send_data_yu();
                            }));
                        }
                        //收到了反馈
                        int readBytes = SerialDebug.ringBuffer.GetDataCount();
                        if (readBytes >= 8)
                        {
                            SerialDebug.ringBuffer.ReadBuffer(ResponseData, 0, readBytes);

                            SerialDebug.ringBuffer.Clear(readBytes);

                            //检验crc
                            byte[] bt = CRC.CRC32_byte(ResponseData, readBytes - 4);
                            if (ResponseData[readBytes - 4] == bt[0] &&
                                ResponseData[readBytes - 3] == bt[1] &&
                                ResponseData[readBytes - 2] == bt[2] &&
                                ResponseData[readBytes - 1] == bt[3])
                            {
                                if (ResponseData[1] == 0x82)
                                {
                                    int idx = (ResponseData[2] << 8) | ResponseData[3];
                                    //包索引是否一致
                                    if ((PackageNum - 1) == idx)
                                    {
                                        //判断结果
                                        switch (ResponseData[4])
                                        {
                                            case 0x01:
                                                //升级完成
                                                this.Invoke(new MethodInvoker(delegate
                                                {
                                                    richbox.Msg(Color.Green, "{0} Flash写入成功！", DateTime.Now.ToString("HH:mm:ss fff"));
                                                    richbox.Msg(Color.Green, "完成！");
                                                    Running = false;
                                                    skinButton2.Text = "下载";
                                                }));
                                                goto stop;
                                            // break;
                                            case 0x02:
                                            case 0x03:
                                                //数据错误，重新发送
                                                this.Invoke(new MethodInvoker(delegate
                                                {
                                                    richbox.Msg(Color.Red, "{0} 等待超时，重发！", DateTime.Now.ToString("HH:mm:ss fff"));
                                                }));
                                                timeCount = 0;
                                                send_data_yu();
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    //清除接收缓存
                                    Array.Clear(ResponseData, 0, ResponseData.Length);
                                }
                            }

                        }

                        
                        Thread.Sleep(10);
                        timeCount++;
                    }
                }
            //stop:;
            stop: ;
                this.Invoke(new MethodInvoker(delegate
                {
                    //获取结束时间
                    long StopTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                    richbox.Msg(Color.Green, "耗时：{0}ms", StopTime - StartTime);
                }));

                

                Thread.Sleep(1100);

                //方法1：置信号量结束后，重新创建实例
                /* SerialDebug.signalStopFlag = false;
                 SerialDebug.subscriptionSerialRecEvent(false);

                 SerialDebug.thread = new Thread(SerialDebug.ReadInput);
                 SerialDebug.thread.IsBackground = true;
                 SerialDebug.thread.Start();*/

                //方法2：恢复线程
                //SerialDebug.thread.Resume();

                //重新开启其他线程
                //SerialDebug.ControlModbusTimer(true);
                //SysForm1.ControlSysRecTimer(true);

                this.Invoke(new Action(() =>
                {
                    SerialDebug.modbusTimer.Enabled = true;
                    SerialDebug.modbusTimer.Start();

                    SysForm1.SysRecTimer.Enabled = true;
                    SysForm1.SysRecTimer.Start();
                }));
            }
            catch(Exception ex)
            {
                MessageBox.Show("升级进程异常！"+ ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateForm_Resize(object sender, EventArgs e)
        {
            //ReWinformLayout();
        }
    }
}
