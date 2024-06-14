using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using CCWin.SkinControl;
using Library;

namespace MainSender
{
    public delegate void HandTimer(bool value);    // zhj-声明委托
    public partial class SysForm1 : Form //CCSkinMain
    {

        static HandTimer handTimer1 = new HandTimer(ControlSysRecTimer);          //定义委托

        public DataTable bms_data = new DataTable();    //定义一个DataTable作为数据源
        public DataTable bms_status = new DataTable();  //定义一个DataTable作为数据源

        public BmsDataType.BmsData realBmsData = new BmsDataType.BmsData();
        public BmsDataType.BmsStatus realBmsAlarmStatus = new BmsDataType.BmsStatus();

        
        public static System.Windows.Forms.Timer SysRecTimer = null;

        //SerialDebug serialClass = new SerialDebug();
        public SysForm1()
        {
            InitializeComponent();
            InitializeDataGridView();

            /*#region 初始化控件缩放

            x = Width;
            y = Height;
            setTag(this);

            #endregion*/

            //定时器初始化
            SysRecTimer = new System.Windows.Forms.Timer();

            SysRecTimer.Interval = 50;
            SysRecTimer.Tick += new System.EventHandler(timerRec_Tick);
            //SysRecTimer.Enabled = false;
            SysRecTimer.Enabled = true;
            SysRecTimer.Start();
        }

        public static void ControlSysRecTimer(bool value)
        {
            //SysForm1.Invoke(handTimer1);

            if (value)
            {
                SysRecTimer.Enabled = true;
                SysRecTimer.Start();
            }
            else
            {
                SysRecTimer.Enabled = false;
                SysRecTimer.Stop();
            }
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

        //初始化DataGridView控件
        public void InitializeDataGridView()
        {
            bms_data.Columns.Add("内容", typeof(string));
            bms_data.Columns.Add("数值",typeof(float));
            bms_data.Columns.Add("单位", typeof(string));

            bms_status.Columns.Add("内容",typeof(string));
            bms_status.Columns.Add("数值", typeof(ushort));
            bms_status.Columns.Add("状态", typeof(string));

            //将数据源绑定到控件上
            skinDataGridView1.DataSource = bms_data;
            skinDataGridView2.DataSource = bms_status;


            /*数据初始化*/
            bms_data.Rows.Add("电压1", 0, "V");
            bms_data.Rows.Add("电压2", 0, "V");
            bms_data.Rows.Add("电流",   0, "A");
            bms_data.Rows.Add("正极绝缘电阻", 0, "KR");
            bms_data.Rows.Add("负极绝缘电阻", 0, "KR");
            bms_data.Rows.Add("PCB温度", 0, "°C");
            bms_data.Rows.Add("SOC", 0, "%");
            bms_data.Rows.Add("SOH", 0, "%");
            bms_data.Rows.Add("NTC温度1", 0, "°C");
            bms_data.Rows.Add("NTC温度2", 0, "°C");
            bms_data.Rows.Add("NTC温度3", 0, "°C");
            bms_data.Rows.Add("NTC温度4", 0, "°C");
            bms_data.Rows.Add("NTC温度5", 0, "°C");
            bms_data.Rows.Add("NTC温度6", 0, "°C");

            /*状态初始化*/
            bms_status.Rows.Add("输入检测1", 0, "断开");
            bms_status.Rows.Add("输入检测2", 0, "断开");
            bms_status.Rows.Add("输入检测3", 0, "断开");
            bms_status.Rows.Add("输入检测4", 0, "断开");
            bms_status.Rows.Add("输入检测5", 0, "断开");
            bms_status.Rows.Add("输入检测6", 0, "断开");
            bms_status.Rows.Add("输入检测7", 0, "断开");
            bms_status.Rows.Add("输入检测8", 0, "断开");
            bms_status.Rows.Add("输出1", 0, "断开");
            bms_status.Rows.Add("输出2", 0, "断开");
            bms_status.Rows.Add("输出3", 0, "断开");
            bms_status.Rows.Add("输出4", 0, "断开");
            bms_status.Rows.Add("输出5", 0, "断开");
            bms_status.Rows.Add("输出6", 0, "断开");
            bms_status.Rows.Add("输出7", 0, "断开");
            bms_status.Rows.Add("输出8", 0, "断开");
        }

        public void convertBmsData(ushort[] data)
        {
            realBmsData.voltage1 = data[0];
            realBmsData.voltage2 = data[1];
            realBmsData.current = data[2];
            realBmsData.res_insulation_posi = data[3];
            realBmsData.res_insulation_neg = data[4];
            realBmsData.pcb_temp = data[5];
            realBmsData.soc = data[6]; 
            realBmsData.soh = data[7];

            bms_data.Rows[0]["数值"] = realBmsData.voltage1;
            bms_data.Rows[1]["数值"] = realBmsData.voltage2;
            bms_data.Rows[2]["数值"] = realBmsData.current;
            bms_data.Rows[3]["数值"] = realBmsData.res_insulation_posi;
            bms_data.Rows[4]["数值"] = realBmsData.res_insulation_neg;
            bms_data.Rows[5]["数值"] = realBmsData.pcb_temp;
            bms_data.Rows[6]["数值"] = realBmsData.soc;
            bms_data.Rows[7]["数值"] = realBmsData.soh;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //定时轮询
            byte[] rbytes = new byte[200];
            int readBytes = SerialDebug.ringBuffer.GetDataCount();

            if (readBytes > 0)
            {
                SerialDebug.ringBuffer.ReadBuffer(rbytes, 0, readBytes);

                ushort[] data = tool.toShortArray(rbytes);

                SerialDebug.ringBuffer.Clear(readBytes);

                convertBmsData(data);

            }
            
            
        }

        private void timerRec_Tick(object sender, EventArgs e)
        {
            //定时轮询
            byte[] rbytes = new byte[512];
            byte[] sysdata = new byte[512];

            byte addr,funcode,len;


            int readBytes = SerialDebug.ringBuffer.GetDataCount();

            if (readBytes > 8)
            {
                SerialDebug.ringBuffer.ReadBuffer(rbytes, 0, readBytes);

                addr = rbytes[0];
                funcode = rbytes[1];
                len = rbytes[2];

                //长度判断
                if(readBytes < (len + 5))
                {
                    return;
                }

                //校验数据
                byte[] crc16 = CRC.CRCCalc(rbytes, 0, readBytes - 2);
                if ((crc16[0] != rbytes[readBytes-2]) || (crc16[1] != rbytes[readBytes - 1]) )
                {
                    SerialDebug.ringBuffer.Clear(readBytes);
                    return;
                }

                Array.Copy(rbytes, 3, sysdata, 0, len);


                //转换后数据
                ushort[] data = tool.toShortArray(sysdata);

                SerialDebug.ringBuffer.Clear(readBytes);

                convertBmsData(data);

            }
        }

        private void SysForm1_Load(object sender, EventArgs e)
        {

        }

        private void SysForm1_Resize(object sender, EventArgs e)
        {
            //ReWinformLayout();
        }
    }
}
