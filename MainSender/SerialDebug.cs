using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;   // 导入输入输出文件框
using System.IO.Ports;   // 串口模块
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CCWin;
using Library;
using Modbus.Device;

namespace MainSender
{
    // 解决线程访问问题  zhj-SerialPortEventArgs为自定义类 sender为被监控对象
    public delegate void SerialPortEventHandler(Object sender, SerialPortEventArgs e);    // zhj-声明委托

    
    public partial class SerialDebug : Form //CCSkinMain
    {
        private string FilePath = null;    // 打开文件路径

        private static object thisLock = new object();    // 锁住线程

        public static event SerialPortEventHandler comReceiveDataEvent = null;  // 定义串口接收数据响应事件 //zhj-声明事件
        // 数据状态
        private static int sendCount = 0;    // 发送数据量
        private static int receCount = 0;    // 接收数据量

        
        public  static RingBuffer ringBuffer = new RingBuffer(1024);    //接收缓冲区 输入寄存器
        public  static RingBuffer holdBuffer = new RingBuffer(200);    //保持寄存器

        static MobusRTU modusRtu = new MobusRTU();
        //private static IModbusMaster master; //定义ModbusRTU主站字段

        //static Thread thread = new Thread(ReadInput);
        public static Thread thread = null;                 //modbus查询线程
        public static volatile bool signalStopFlag = false;

        public static SerialPort serialPort1 = null;

        public static System.Windows.Forms.Timer modbusTimer = null;

        public SerialDebug()
        {
            InitializeComponent();

            serialPort1 = new SerialPort();

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPortMonitor_DataReceived);

            InitializeSerialSet(); // 初始化串口设置

            //定时器初始化
            modbusTimer = new System.Windows.Forms.Timer();

            modbusTimer.Interval = 50;
            modbusTimer.Tick += new System.EventHandler(timerSend_Tick);
            modbusTimer.Enabled = false;


        }

        public static void subscriptionSerialRecEvent(bool value)
        {
            if(value)
            {
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPortMonitor_DataReceived);
            }
            else
            {
                serialPort1.DataReceived -= serialPortMonitor_DataReceived;
            }
            
        }

        public static void ControlModbusTimer(bool value)
        {
            
            if(value)
            {
                if (modbusTimer != null)
                {
                    modbusTimer.Enabled = true;
                    modbusTimer.Start();
                }
            }
               
            else
            {
                if (modbusTimer != null)
                {
                    modbusTimer.Enabled = false;
                    modbusTimer.Stop();
                }
                    
            }
        }

        /// <summary>
        /// 串口初始化设置
        /// </summary>

        public void InitializeSerialSet()
        {
            InitializePorts();   // 初始化串口号
            // 初始化波特率
            comboBox_BandRate.Text = comboBox_BandRate.Items[1].ToString();
            // 初始化校验位
            comboBox_Check.Text = comboBox_Check.Items[0].ToString();
            // 初始化数据位
            comboBox_Data.Text = comboBox_Data.Items[0].ToString();
            // 初始化停止位
            comboBox_Stop.Text = comboBox_Stop.Items[0].ToString();

            //modusRtu.master = ModbusSerialMaster.CreateRtu(serialPort1);
            //实例化Modbus RTU主站
            /*            modusRtu.master = ModbusSerialMaster.CreateRtu(serialPort1);

                        thread = new Thread(ReadInput);
                        thread.IsBackground = true;*/


        }

        /// <summary>
        /// 可用串口扫描，并且显示
        /// </summary>
        public void InitializePorts()
        {
            comboBox_Serial.Items.Clear();   // 清空原来的信息
            // 返回可用串口号，形式：COM3
            string[] arraysPostsNames = SerialPort.GetPortNames();  // 获取所有可用的串口号

            // 检查串口号是否正确
            if (arraysPostsNames.Length > 0)
            {

                Array.Sort(arraysPostsNames);  // 使用默认进行排序，从小到大排序
                for (int i = 0; i < arraysPostsNames.Length; i++)
                {
                    comboBox_Serial.Items.Add(arraysPostsNames[i]);  // 将所有可用串口加载到串口显示框当中
                }
                comboBox_Serial.Text = arraysPostsNames[0];   // 默认选择第一个串口

                comboBox_Serial.Enabled = true;   // 打开选择框
                // 设置状态栏属性
                toolStripStatus_Port.Text = "串口号：" + comboBox_Serial.Text;  // 设置状态栏的情况                   
                toolStripStatus_Port.ForeColor = Color.Black; // 设置为红色

            }
            else
            {
                toolStripStatus_Port.Text = "没有可用串口";  // 设置状态栏的情况                   
                toolStripStatus_Port.ForeColor = Color.Red; // 设置为红色
                comboBox_Serial.Text = "None";   // 提示没有可用串口
                comboBox_Serial.Enabled = false;   // 禁止打开串口选择框
            }
        }

        /// <summary>
        /// 串口读取数据响应方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void serialPortMonitor_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //ReceiveData(sender,e);

            try
            {
                //等待数据接收完成
                //Thread.Sleep(10);

                int len = serialPort1.BytesToRead;
                byte[] data = new Byte[len];
                try
                {
                    serialPort1.Read(data, 0, len);   // 向串口中读取数据
                }
                catch (Exception)
                {
                    MessageBox.Show("串口数据读取失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                ringBuffer.WriteBuffer(data);
            }
            catch(Exception)
            {
                MessageBox.Show("数据接收中异常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SerialDebug_Load(object sender, EventArgs e) //zhj-加载窗体时调用
        {

            comReceiveDataEvent += new SerialPortEventHandler(ComReceiveDataEvent);  // 订阅事件
            toolStripStatus_Time.Text = "时间:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");  // 显示当前时间
        }

        public void ComReceiveDataEvent(Object sender, SerialPortEventArgs e)
        {
            /*zhj-  C#中禁止跨线程直接访问控件
                    InvokeRequired是为了解决这个问题而产生的，当一个控件InvokeRequired的值为true时，说明有一个创建它以外的线程想访问它*/
            /*if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<Object, SerialPortEventArgs>(ComReceiveDataEvent), sender, e);
                }
                catch (Exception)
                {

                }
                return;
            }


            string context = "[" + DateTime.Now.ToString("hh:mm:ss") + "] ";
            context += tool.byteToHexStr(e.receivedBytes);

            richTextBox_Receive.AppendText(context + "\r\n");


            // 更新状态显示框
            receCount += e.receivedBytes.Length;
            toolStripStatus_recestatus.Text = "收到数据: " + receCount.ToString();*/


            /*压入接收缓冲区*/
            ringBuffer.WriteBuffer(e.receivedBytes);


        }
        /// <summary>
        /// 串口选择框改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select_Ports_Change(object sender, EventArgs e)
        {
            // 设置状态栏属性
            toolStripStatus_Port.Text = "串口号：" + comboBox_Serial.Text;  // 设置状态栏的情况                   
            toolStripStatus_Port.ForeColor = Color.Black; // 设置为黑色
        }
        /// <summary>
        /// 更新状态栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_StatusTime(object sender, EventArgs e)
        {
            
            toolStripStatus_Time.Text = "时间:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");  // 显示当前时间 
        }
        /// <summary>
        /// 保存文件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveData_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDataSend = new SaveFileDialog();
            saveDataSend.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);   // 获取文件路径
            saveDataSend.Filter = "*.txt|txt file";   // 文本文件
            saveDataSend.DefaultExt = ".txt";   // 默认文件的形式
            saveDataSend.FileName = "SendData.txt";   // 文件默认名

            if (saveDataSend.ShowDialog() == DialogResult.OK)   // 显示文件框，并且选择文件
            {
                FilePath = saveDataSend.FileName;   // 获取文件名
                // 参数1：写入文件的文件名；参数2：写入文件的内容
                try
                {
                    System.IO.File.WriteAllText(FilePath, richTextBox_Send.Text);   // 向文件中写入内容
                }
                catch
                {
                    MessageBox.Show("保存文件失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }

            }

        }
        /// <summary>
        /// 选择发送文件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Filepath_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;   // 是否可以选择多个文件
            fileDialog.Title = "请选择文件";     // 标题
            fileDialog.Filter = "所有文件(*.*)|*.*";   // 显示所有文件
            if (fileDialog.ShowDialog() == DialogResult.OK)   // 打开文件选择框
            {
                FilePath = fileDialog.FileName;   // 绝对路径
                textBox_Filepath.Text = FilePath;   // 在窗口中显示文件路径
            }
            checkBox_Sendfile.Checked = true;   // 设置发送文件框选项状态
            ReadFile(FilePath);   // 将文件内容显示在发送框当中
        }

        /// <summary>
        /// 将文件内容显示在发送数据显示框中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReadFile(string filepath)
        {
            try
            {
                richTextBox_Send.Text = "";  // 清空显示框

                StreamReader sr = new StreamReader(filepath, Encoding.Default);
                string content;
                while ((content = sr.ReadLine()) != null)   // 按行读取显示在发送数据框中
                {
                    richTextBox_Send.Text += (content.ToString() + "\r\n");   // ReadLine默认不会读取换行符
                }
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// 清除发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ClcSendData_Click(object sender, EventArgs e)
        {
            richTextBox_Send.Text = "";  // 清空显示框
            sendCount = 0;
            toolStripStatus_sendstatus.Text = "发送数据：" + sendCount.ToString();
        }
        /// <summary>
        /// 清除接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ClcReceData_Click(object sender, EventArgs e)
        {
            richTextBox_Receive.Text = "";   // 清空接收数据
            receCount = 0;
            toolStripStatus_recestatus.Text = "收到数据：" + receCount.ToString();
        }



        
        







        /// <summary>
        /// 发送数据按键点击响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SendData_Click(object sender, EventArgs e)
        {
            string senddata = richTextBox_Send.Text;
            //byte[] data = System.Text.Encoding.Default.GetBytes(senddata);   // 将发送的数据转化为字节数组
            byte[] data = tool.strToToHexByte(senddata);
            SendData(data);    // 发送数据
            sendCount += senddata.Length;
            toolStripStatus_sendstatus.Text = "发送数据：" + sendCount.ToString();

        }
        /// <summary>
        /// 保存发送数据，保存发送数据菜单响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_ReceData_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDataSend = new SaveFileDialog();
            saveDataSend.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);   // 获取文件路径
            saveDataSend.Filter = "*.txt|txt file";   // 文本文件
            saveDataSend.DefaultExt = ".txt";   // 默认文件的形式
            saveDataSend.FileName = "ReceData.txt";   // 文件默认名

            if (saveDataSend.ShowDialog() == DialogResult.OK)   // 显示文件框，并且选择文件
            {
                FilePath = saveDataSend.FileName;   // 获取文件名
                // 参数1：写入文件的文件名；参数2：写入文件的内容
                try
                {
                    System.IO.File.WriteAllText(FilePath, richTextBox_Receive.Text);   // 向文件中写入内容
                }
                catch
                {
                    MessageBox.Show("保存文件失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }

            }
        }
        /// <summary>
        /// 串口状态响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (serialPort1 == null)
            {
                return;
            }

            if (serialPort1.IsOpen == false)
            {
                serialPort1.PortName = comboBox_Serial.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox_BandRate.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox_Check.Text);   // 强制类型转换
                serialPort1.DataBits = Convert.ToInt32(comboBox_Data.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox_Stop.Text);
                try
                {
                    serialPort1.Open();

                    // 设置按键的使用权限
                    comboBox_Serial.Enabled = false;
                    comboBox_BandRate.Enabled = false;
                    comboBox_Check.Enabled = false;
                    comboBox_Data.Enabled = false;
                    comboBox_Stop.Enabled = false;
                    button_Refresh.Enabled = false;

                    button_SendData.Enabled = true;
                    checkBox_SendData.Enabled = true;
                    textBox_selectTime.Enabled = true;
                    checkBox_Sendfile.Enabled = true;
                    textBox_Filepath.Enabled = true;

                    // 打开属性变为关闭属性
                    button_OK.Text = "关闭串口";

                    toolStripStatus_Port.Text = "连接串口:" + comboBox_Serial.Text;
                    toolStripStatus_Port.ForeColor = Color.Green; // 设置为绿色

                    //timerSend.Enabled = true;
                    modbusTimer.Enabled = true;
                    modbusTimer.Start();

/*                    modusRtu.master = ModbusSerialMaster.CreateRtu(serialPort1);

                    thread = new Thread(ReadInput);
                    thread.IsBackground = true;


                    thread.Start();*/

                }
                catch (Exception)
                {
                    toolStripStatus_Port.Text = "连接失败:" + comboBox_Serial.Text;
                    toolStripStatus_Port.ForeColor = Color.Red; // 设置为红色
                    MessageBox.Show("串口连接失败！\r\n可能原因：串口被占用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    timerSend.Enabled = false;
                    //thread.Abort();
                }


            }
            else
            {
                serialPort1.Close();   // 关闭串口

                timerSend.Enabled = false;

                // 设置按键的使用权限
                comboBox_Serial.Enabled = true;
                comboBox_BandRate.Enabled = true;
                comboBox_Check.Enabled = true;
                comboBox_Data.Enabled = true;
                comboBox_Stop.Enabled = true;
                button_Refresh.Enabled = true;

                button_SendData.Enabled = false;
                checkBox_SendData.Enabled = false;
                textBox_selectTime.Enabled = false;
                checkBox_Sendfile.Enabled = false;
                textBox_Filepath.Enabled = false;

                // 打开属性变为关闭属性
                button_OK.Text = "打开串口";
                toolStripStatus_Port.Text = "断开连接:" + comboBox_Serial.Text;
                toolStripStatus_Port.ForeColor = Color.Red; // 设置为红色

                //thread.Abort();
            }

        }
        /// <summary>
        /// 向串口中发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static bool SendData(byte[] data)
        {
            if (serialPort1 == null)
            {
                return false;
            }
            if (serialPort1.IsOpen == false)
            {
                return false;
            }

            try
            {
                serialPort1.Write(data, 0, data.Length);
            }
            catch (Exception)
            {
                //提示信息
                MessageBox.Show("数据发送失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        /// <summary>
        /// 串口接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static bool ReceiveData(object sender, EventArgs e)
        {

            if (serialPort1 == null)
            {
                return false;
            }
            if (serialPort1.IsOpen == false)
            {
                return false;
            }
            if (serialPort1.BytesToRead <= 0)   // 串口中没有数据
            {
                return false;
            }
            /*lock (thisLock)   // 锁住串口
            {
                int len = serialPort1.BytesToRead;
                byte[] data = new Byte[len];
                try
                {
                    serialPort1.Read(data, 0, len);   // 向串口中读取数据
                }
                catch (Exception)
                {
                    MessageBox.Show("数据接收失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }


                SerialPortEventArgs args = new SerialPortEventArgs();
                args.receivedBytes = data;
                if (comReceiveDataEvent != null)
                {
                    comReceiveDataEvent.Invoke(sender, args);
                    //comReceiveDataEvent.Invoke(this, args);
                }

            }*/


            int len = serialPort1.BytesToRead;
            byte[] data = new Byte[len];
            try
            {
                serialPort1.Read(data, 0, len);   // 向串口中读取数据
            }
            catch (Exception)
            {
                MessageBox.Show("数据接收失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            ringBuffer.WriteBuffer(data);


            return true;
        }
        /// <summary>
        /// 刷新串口参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Refresh_Click(object sender, EventArgs e)
        {
            InitializeSerialSet(); // 刷新串口设置
        }



        #region  modbus查询帧

        void Mobus_ReadInput()
        {
            byte[] data = new byte[8];
            data[0] = 0x01;
            data[1] = 0x04;
            data[2] = 0x00;  //寄存器基地址
            data[3] = 0x00;
            data[4] = 0x00;  //长度
            data[5] = 0x1C;

            byte[] crcValue = CRC.CRCCalc(data, 0, 6);

            data[6] = crcValue[0];  //校验码
            data[7] = crcValue[1];

            SendData(data);    // 发送数据
        }

        void Modbus_ReadHold()
        {
            byte[] data = new byte[8];
            data[0] = 0x01;
            data[1] = 0x03;
            data[2] = 0x00;  //寄存器基地址
            data[3] = 0x00;
            data[4] = 0x00;  //长度
            data[5] = 0x26;

            byte[] crcValue = CRC.CRCCalc(data, 0, 6);

            data[6] = crcValue[0];  //校验码
            data[7] = crcValue[1];

            SendData(data);    // 发送数据
        }

        void Mobus_ReadCoil()
        {
            byte[] data = new byte[8];
            data[0] = 0x01;
            data[1] = 0x01;
            data[2] = 0x00;  //寄存器基地址
            data[3] = 0x00;
            data[4] = 0x00;  //线圈数量
            data[5] = 0x08;

            byte[] crcValue = CRC.CRCCalc(data, 0, 6);

            data[6] = crcValue[0];  //校验码
            data[7] = crcValue[1];

            SendData(data);    // 发送数据
        }

        void Mobus_ReadInputState()
        {
            byte[] data = new byte[8];
            data[0] = 0x01;
            data[1] = 0x02;
            data[2] = 0x00;  //寄存器基地址
            data[3] = 0x00;
            data[4] = 0x00;  //输入状态数量
            data[5] = 0x26;

            byte[] crcValue = CRC.CRCCalc(data, 0, 6);

            data[6] = crcValue[0];  //校验码
            data[7] = crcValue[1];

            SendData(data);    // 发送数据
        }

        #endregion


        public static volatile int SendCommand = 0;
        public static bool immediatelySendFlag = false;
        /// <summary>
        /// 定时发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSend_Tick(object sender, EventArgs e)
        {
            if(immediatelySendFlag == false)
            {
                //定时发送
                switch (SendCommand)
                {
                    case 0:
                        {
                            if (mainForm.sendValid[0] == true)
                            {
                                Mobus_ReadInput();
                            }

                            SendCommand = 1;
                            break;
                        }
                    case 1:
                        {
                            if (mainForm.sendValid[0] == true)
                            {
                                Mobus_ReadCoil();
                            }

                            SendCommand = 2;
                            break;
                        }

                    case 2:
                        {
                            if (mainForm.sendValid[1] == true && ControlForm.holdRegistersQuery == true)
                            {
                                ControlForm.holdRegistersQuery = false;
                                Modbus_ReadHold();
                            }

                            SendCommand = 3;
                            break;
                        }
                    case 3:
                        {
                            if (mainForm.sendValid[0] == true)
                            {
                                Mobus_ReadInputState();
                            }

                            SendCommand = 0;
                            break;
                        }

                    default:
                        SendCommand = 0;
                        break;

                }

                Thread.Sleep(150); //等待接收
            }
            

        }


        public static void ReadInput() //静态函数只能调用static 修饰的外部变量和外部静态函数
        {
            ushort[] inputRegValue = new ushort[200];
            ushort[] holdRegValue = new ushort[200];


            //while (!signalStopFlag)
            {
                //debug 测试调用时间
                DateTime before = DateTime.Now;

                try
                {
                    //使用modbus库会阻塞线程，不清楚原因
                    inputRegValue = modusRtu.ReadInputRegisters(0x01, 0x01, 28);
                    holdRegValue = modusRtu.ReadHoldingRegisters(0x01, 0x00, 38);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("端口可能被关闭或占用！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //serialPort1.Close();
                    //break;


                }


                DateTime after = DateTime.Now;

                TimeSpan afterTimeSpan = after.Subtract(before);

                double time = afterTimeSpan.TotalSeconds; //阻塞100ms左右

                //显示界面-- - 1、将数据传到窗口 2、直接在这里做显示
    
                //压入接收缓冲区
                byte[] byteArray = tool.toByteArray(inputRegValue);
                byte[] byteArray2 = tool.toByteArray(holdRegValue);


                ringBuffer.WriteBuffer(byteArray);
                holdBuffer.WriteBuffer(byteArray2);

                //Thread.Sleep(50);
            }


        }
        /*        public static void ReadInput() //静态函数只能调用static 修饰的外部变量和外部静态函数
                {
                    //modbus 查询主线程
                }*/
        /// <summary>
        /// 退出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Quit_Click(object sender, EventArgs e)
        {
            this.Close();   // 关闭窗体，然后退出
        }
        /// <summary>
        /// 关于菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_About_Click(object sender, EventArgs e)
        {
            //About about = new About();
            //about.Show();   // 显示关于窗体

        }

        private void processDatatimer1_Tick(object sender, EventArgs e)
        {
            //定时处理数据
            byte[] rbytes = new byte[200];
            int readBytes = ringBuffer.GetDataCount();

            if(readBytes > 0)
            {
                ringBuffer.ReadBuffer(rbytes, 0, readBytes);
                ringBuffer.Clear(readBytes);
            }
            
            
        }
    }

    public class SerialPortEventArgs : EventArgs
    {
        public byte[] receivedBytes = null;   // 用来接收串口读取的数据
    }



}
