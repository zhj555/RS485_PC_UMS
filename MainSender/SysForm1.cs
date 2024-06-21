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
using CCWin.SkinClass;
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
        public BmsDataType.BmsStatus realBmsStatus = new BmsDataType.BmsStatus();

        
        public static System.Windows.Forms.Timer SysRecTimer = null;

        public Library.Richbox richbox_AlarmMsg;                              //告警消息框

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

            SysRecTimer.Interval = 10;
            SysRecTimer.Tick += new System.EventHandler(timerRec_Tick);
            SysRecTimer.Enabled = true;
            SysRecTimer.Start();

            richbox_AlarmMsg = new Library.Richbox(richTextBox1);
        }

        public static void ControlSysRecTimer(bool value)
        {
            //SysForm1.Invoke(handTimer1);

            if (value)
            {
                if(SysRecTimer != null)
                {
                    SysRecTimer.Enabled = true;
                    SysRecTimer.Start();
                }
               
            }
            else
            {
                if (SysRecTimer != null)
                {
                    SysRecTimer.Enabled = false;
                    SysRecTimer.Stop();
                }
                
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
            bms_status.Columns.Add("数值", typeof(byte));
            bms_status.Columns.Add("状态", typeof(string));

            //将数据源绑定到控件上
            skinDataGridView1.DataSource = bms_data;
            skinDataGridView2.DataSource = bms_status;


            /*数据初始化*/
            bms_data.Rows.Add("电容电压（总电压1）", 0, "V");
            bms_data.Rows.Add("母线电压（总电压2）", 0, "V");
            bms_data.Rows.Add("电流1",   0, "A");
            bms_data.Rows.Add("电流2", 0, "A");
            bms_data.Rows.Add("正极绝缘电阻", 0, "KR");
            bms_data.Rows.Add("负极绝缘电阻", 0, "KR");
            bms_data.Rows.Add("PCB温度", 0, "°C");
            bms_data.Rows.Add("SOC", 0, "%");
            bms_data.Rows.Add("SOH", 0, "%");
            bms_data.Rows.Add("软件版本", 0, " ");
            bms_data.Rows.Add("电容组容量", 0, " ");
            bms_data.Rows.Add("系统运行状态", 0, " ");
            bms_data.Rows.Add("运行1和运行2子状态", 0, " ");
            bms_data.Rows.Add("最高电压模组编号", 0, " ");
            bms_data.Rows.Add("模组最低电压", 0, " ");
            bms_data.Rows.Add("最低电压模组编号", 0, " ");
            bms_data.Rows.Add("模组平均电压", 0, " ");
            bms_data.Rows.Add("模组最高温度", 0, " ");
            bms_data.Rows.Add("最高温度模组编号", 0, " ");
            bms_data.Rows.Add("模组最低温度", 0, " ");
            bms_data.Rows.Add("最低温度模组编号", 0, " ");
            bms_data.Rows.Add("模组平均温度", 0, " ");
            bms_data.Rows.Add("绝缘检测VBus+", 0, " ");
            bms_data.Rows.Add("绝缘检测VBus-", 0, " ");
            bms_data.Rows.Add("备用模拟量1", 0, " ");
            bms_data.Rows.Add("备用模拟量2", 0, " ");
            bms_data.Rows.Add("备用模拟量3", 0, " ");
            bms_data.Rows.Add("备用模拟量4", 0, " ");


            bms_data.Rows.Add("NTC温度1", 0, "°C");
            bms_data.Rows.Add("NTC温度2", 0, "°C");
            bms_data.Rows.Add("NTC温度3", 0, "°C");
            bms_data.Rows.Add("NTC温度4", 0, "°C");
            bms_data.Rows.Add("NTC温度5", 0, "°C");
            bms_data.Rows.Add("NTC温度6", 0, "°C");

            /*状态初始化*/
            bms_status.Rows.Add("Input1-OVP", 0, "断开");
            bms_status.Rows.Add("Input2-OTP", 0, "断开");
            bms_status.Rows.Add("Input3-KM2辅助触点状态", 0, "断开");
            bms_status.Rows.Add("Input4-KM1辅助触点状态", 0, "断开");
            bms_status.Rows.Add("Input5-Reserve", 0, "断开");
            bms_status.Rows.Add("Input6-Reserve", 0, "断开");
            bms_status.Rows.Add("Input7-Reserve", 0, "断开");
            bms_status.Rows.Add("Input8-Reserve", 0, "断开");

            //作为空距进行隔开
            bms_status.Rows.Add();

            bms_status.Rows.Add("Output1-KM2", 0, "断开");
            bms_status.Rows.Add("Output2-DCDC启动(常开)", 0, "断开");
            bms_status.Rows.Add("Output3-KM2缓冲接触器", 0, "断开");
            bms_status.Rows.Add("Output4-绝缘仪供电(常闭)", 0, "断开");
            bms_status.Rows.Add("Output5-Ready灯", 0, "断开");
            bms_status.Rows.Add("Output6-Alarm灯", 0, "断开");
            bms_status.Rows.Add("Output7-Reserve", 0, "断开");
            bms_status.Rows.Add("Output8-Reserve", 0, "断开");
        }

        public void convertBmsData(ushort[] data,byte len)
        {
            realBmsData.voltage1 = (data[0] / 10.0f);
            realBmsData.voltage2 = (data[1] / 10.0f);
            realBmsData.current1 = data[2];
            realBmsData.current2 = data[3];
            realBmsData.res_insulation_posi = data[4];
            realBmsData.res_insulation_neg = data[5];
            realBmsData.pcb_temp = ((data[6]-500)/10.0f);
            realBmsData.soc = data[7]; 
            realBmsData.soh = data[8];
            realBmsData.softwareVersion = data[9];
            realBmsData.capacity = data[10];
            realBmsData.sysState = data[11];
            realBmsData.subState = data[12];
            realBmsData.HighModuleVolt_Number = data[13];
            realBmsData.LowModuleVolt = (data[14] / 10.0f);
            realBmsData.LowModuleVolt_Number = data[15];
            realBmsData.moduleAvgVolt = (data[16] / 10.0f);
            realBmsData.moduleHighTemp = ((data[17]-500)/10.0f);
            realBmsData.HighModuleTmep_Number = data[18];
            realBmsData.moduleLowTemp = ((data[19]-500)/10.0f);
            realBmsData.LowModuleTmep_Number = data[20];
            realBmsData.moduleAvgTemp = ((data[21] - 500) / 10.0f);
            realBmsData.insulationVBusPos = (data[22]/10.0f);
            realBmsData.insulationVBusNeg = (data[23]/10.0f);
            realBmsData.reserve1 = data[24];
            realBmsData.reserve2 = data[25];
            realBmsData.reserve3 = data[26];
            realBmsData.reserve4 = data[27];

            bms_data.Rows[0]["数值"] = realBmsData.voltage1;
            bms_data.Rows[1]["数值"] = realBmsData.voltage2;
            bms_data.Rows[2]["数值"] = realBmsData.current1;
            bms_data.Rows[3]["数值"] = realBmsData.current2;
            bms_data.Rows[4]["数值"] = realBmsData.res_insulation_posi;
            bms_data.Rows[5]["数值"] = realBmsData.res_insulation_neg;
            bms_data.Rows[6]["数值"] = realBmsData.pcb_temp;
            bms_data.Rows[7]["数值"] = realBmsData.soc;
            bms_data.Rows[8]["数值"] = realBmsData.soh;
            bms_data.Rows[9]["数值"] = realBmsData.softwareVersion;
            bms_data.Rows[10]["数值"] = realBmsData.capacity;
            bms_data.Rows[11]["数值"] = realBmsData.sysState;
            bms_data.Rows[12]["数值"] = realBmsData.subState;
            bms_data.Rows[13]["数值"] = realBmsData.HighModuleVolt_Number;
            bms_data.Rows[14]["数值"] = realBmsData.LowModuleVolt;
            bms_data.Rows[15]["数值"] = realBmsData.LowModuleVolt_Number;
            bms_data.Rows[16]["数值"] = realBmsData.moduleAvgVolt;
            bms_data.Rows[17]["数值"] = realBmsData.moduleHighTemp;
            bms_data.Rows[18]["数值"] = realBmsData.HighModuleTmep_Number;
            bms_data.Rows[19]["数值"] = realBmsData.moduleLowTemp;
            bms_data.Rows[20]["数值"] = realBmsData.LowModuleTmep_Number;
            bms_data.Rows[21]["数值"] = realBmsData.moduleAvgTemp;
            bms_data.Rows[22]["数值"] = realBmsData.insulationVBusPos;
            bms_data.Rows[23]["数值"] = realBmsData.insulationVBusNeg;
            bms_data.Rows[24]["数值"] = realBmsData.reserve1;
            bms_data.Rows[25]["数值"] = realBmsData.reserve2;
            bms_data.Rows[26]["数值"] = realBmsData.reserve3;
            bms_data.Rows[27]["数值"] = realBmsData.reserve4;


            /*            for (int i = 0;i< len; i++)
                        {
                            bms_data.Rows[i]["数值"] = data[i];
                        }*/
        }

        public void convertBmsOutStatus(byte[] data, byte len)
        {
            bms_status.Rows[9]["数值"] = realBmsStatus.DO1_Status = (byte)((data[0] & 0x01) >> 0);
            if(realBmsStatus.DO1_Status == 0)
            {
                bms_status.Rows[9]["状态"] = "断开";
              
            }
            else
            {
                bms_status.Rows[9]["状态"] = "闭合";
            }

            bms_status.Rows[10]["数值"] = realBmsStatus.DO2_Status = (byte)((data[0] & 0x02) >> 1);
            if (realBmsStatus.DO2_Status == 0)
            {
                bms_status.Rows[10]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[10]["状态"] = "闭合";
            }

            bms_status.Rows[11]["数值"] = realBmsStatus.DO3_Status = (byte)((data[0] & 0x04) >> 2);
            if (realBmsStatus.DO3_Status == 0)
            {
                bms_status.Rows[11]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[11]["状态"] = "闭合";
            }

            bms_status.Rows[12]["数值"] = realBmsStatus.DO4_Status = (byte)((data[0] & 0x08) >> 3);
            if (realBmsStatus.DO4_Status == 0)
            {
                bms_status.Rows[12]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[12]["状态"] = "闭合";
            }

            bms_status.Rows[13]["数值"] = realBmsStatus.DO5_Status = (byte)((data[0] & 0x10) >> 4);
            if (realBmsStatus.DO5_Status == 0)
            {
                bms_status.Rows[13]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[13]["状态"] = "闭合";
            }

            bms_status.Rows[14]["数值"] = realBmsStatus.DO6_Status = (byte)((data[0] & 0x20) >> 5);
            if (realBmsStatus.DO6_Status == 0)
            {
                bms_status.Rows[14]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[14]["状态"] = "闭合";
            }

            bms_status.Rows[15]["数值"] = realBmsStatus.DO7_Status = (byte)((data[0] & 0x40) >> 6);
            if (realBmsStatus.DO7_Status == 0)
            {
                bms_status.Rows[15]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[15]["状态"] = "闭合";
            }

            bms_status.Rows[16]["数值"] = realBmsStatus.DO8_Status = (byte)((data[0] & 0x80) >> 7);
            if (realBmsStatus.DO8_Status == 0)
            {
                bms_status.Rows[16]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[16]["状态"] = "闭合";
            }


        }

        void AlarmMsgShow()
        {
            richbox_AlarmMsg.Clear();

            //测试
            //if (realBmsData.voltage1 == 0)
            //{
            //    realBmsData.voltage1 = 1;
            //    richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss"+" -> 触发总电压1过压1级告警"));
                
            //}
            //else
            //{
            //    realBmsData.voltage1 = 0;
            //}

            if (realBmsStatus.TotalOverVol1_AlarmStatus1 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss"+" -> 触发总电压1过压1级告警"));
            }
            if (realBmsStatus.TotalOverVol1_AlarmStatus2 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电压1过压2级告警"));
            }
            if (realBmsStatus.TotalUnderVol1_AlarmStatus1 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电压1欠压1级告警"));
            }
            if (realBmsStatus.TotalUnderVol1_AlarmStatus2 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电压1欠压2级告警"));
            }
            if (realBmsStatus.TotalOverVol2_AlarmStatus1 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电压2欠压1级告警"));
            }
            if (realBmsStatus.TotalOverVol2_AlarmStatus2 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电压2欠压2级告警"));
            }


            if(realBmsStatus.TotalUnderVol2_AlarmStatus1 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电压2欠压1级告警"));
            }
            if(realBmsStatus.TotalUnderVol2_AlarmStatus2 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电压2欠压2级告警"));
            }
            if(realBmsStatus.OverCurrent1_AlarmStatus1 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电流1过流1级告警"));
            }
            if (realBmsStatus.OverCurrent1_AlarmStatus2 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电流1过流2级告警"));
            }
            if (realBmsStatus.OverCurrent2_AlarmStatus1 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电流2过流1级告警"));
            }
            if (realBmsStatus.OverCurrent2_AlarmStatus2 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发总电流2过流2级告警"));
            }
            if (realBmsStatus.OverTemp_AlarmStatus1 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发模组过温1级告警"));
            }
            if (realBmsStatus.OverTemp_AlarmStatus2 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发模组过温2级告警"));
            }


            if (realBmsStatus.UnderTemp_AlarmStatus1 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发模组欠温1级告警"));
            }
            if (realBmsStatus.UnderTemp_AlarmStatus2 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发模组欠温2级告警"));
            }
            if(realBmsStatus.insulation_AlarmStatus1 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发绝缘1级告警"));
            }
            if(realBmsStatus.insulation_AlarmStatus2 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发绝缘2级告警"));
            }
            if(realBmsStatus.cellOverVolt_AlarmStatus1 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发单体过压1级告警"));
            }
            if(realBmsStatus.cellOverVolt_AlarmStatus2 == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发单体过压2级告警"));
            }
            if(realBmsStatus.modulePol_AlarmStatus == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发模组POL告警"));
            }
            if(realBmsStatus.moduleOtp_AlarmStatus == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发模组OTP告警"));
            }



            if(realBmsStatus.can0_FaultStatus == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发绝缘仪通讯故障"));
            }
            if(realBmsStatus.can1_FaultStatus == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发DCDC通讯故障"));
            }
            if(realBmsStatus.can2_FaultStatus == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发发电机通讯故障"));
            }
            if(realBmsStatus.relayFaultStatus == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发正负接触器故障"));
            }
            if(realBmsStatus.fuseFaultStatus == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发熔断器故障"));
            }
            if(realBmsStatus.umsFaultStatus == 1)
            {
                richbox_AlarmMsg.Msg(Color.Red, DateTime.Now.ToString("HH:mm:ss" + " -> 触发ums自身异常"));
            }

            

        }

        public void convertBmsInStatus(byte[] data, byte len)
        {
            bms_status.Rows[0]["数值"] = realBmsStatus.DI1_Status = (byte)((data[0] & 0x01) >> 0);
            if (realBmsStatus.DI1_Status == 0)
            {
                bms_status.Rows[0]["状态"] = "断开";

            }
            else
            {
                bms_status.Rows[0]["状态"] = "闭合";
            }

            bms_status.Rows[1]["数值"] = realBmsStatus.DI2_Status = (byte)((data[0] & 0x02) >> 1);
            if (realBmsStatus.DI2_Status == 0)
            {
                bms_status.Rows[1]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[1]["状态"] = "闭合";
            }

            bms_status.Rows[2]["数值"] = realBmsStatus.DI3_Status = (byte)((data[0] & 0x04) >> 2);
            if (realBmsStatus.DI3_Status == 0)
            {
                bms_status.Rows[2]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[2]["状态"] = "闭合";
            }

            bms_status.Rows[3]["数值"] = realBmsStatus.DI4_Status = (byte)((data[0] & 0x08) >> 3);
            if (realBmsStatus.DI4_Status == 0)
            {
                bms_status.Rows[3]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[3]["状态"] = "闭合";
            }

            bms_status.Rows[4]["数值"] = realBmsStatus.DI5_Status = (byte)((data[0] & 0x10) >> 4);
            if (realBmsStatus.DI5_Status == 0)
            {
                bms_status.Rows[4]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[4]["状态"] = "闭合";
            }

            bms_status.Rows[5]["数值"] = realBmsStatus.DI6_Status = (byte)((data[0] & 0x20) >> 5);
            if (realBmsStatus.DI6_Status == 0)
            {
                bms_status.Rows[5]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[5]["状态"] = "闭合";
            }

            bms_status.Rows[6]["数值"] = realBmsStatus.DI7_Status = (byte)((data[0] & 0x40) >> 6);
            if (realBmsStatus.DI7_Status == 0)
            {
                bms_status.Rows[6]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[6]["状态"] = "闭合";
            }

            bms_status.Rows[7]["数值"] = realBmsStatus.DI8_Status = (byte)((data[0] & 0x80) >> 7);
            if (realBmsStatus.DI8_Status == 0)
            {
                bms_status.Rows[7]["状态"] = "断开";
            }
            else
            {
                bms_status.Rows[7]["状态"] = "闭合";
            }


            /*告警状态*/
            realBmsStatus.TotalOverVol1_AlarmStatus1 = (byte)((data[1] & 0x04) >> 2);
            realBmsStatus.TotalOverVol1_AlarmStatus2 = (byte)((data[1] & 0x08) >> 3);
            realBmsStatus.TotalUnderVol1_AlarmStatus1 = (byte)((data[1] & 0x10) >> 4);
            realBmsStatus.TotalUnderVol1_AlarmStatus2 = (Byte)((data[1] & 0x20) >> 5);
            realBmsStatus.TotalOverVol2_AlarmStatus1 = (byte)((data[1] & 0x40) >> 6);
            realBmsStatus.TotalOverVol2_AlarmStatus2 = (byte)((data[1] & 0x80) >> 7);

            realBmsStatus.TotalUnderVol2_AlarmStatus1 = (byte)((data[2] & 0x01) >> 0);
            realBmsStatus.TotalUnderVol2_AlarmStatus2 = (byte)((data[2] & 0x02) >> 1);
            realBmsStatus.OverCurrent1_AlarmStatus1 = (byte)((data[2] & 0x04) >> 2);
            realBmsStatus.OverCurrent1_AlarmStatus2 = (byte)((data[2] & 0x08) >> 3);
            realBmsStatus.OverCurrent2_AlarmStatus1 = (byte)((data[2] & 0x10) >> 4);
            realBmsStatus.OverCurrent2_AlarmStatus2 = (byte)((data[2] & 0x20) >> 5);
            realBmsStatus.OverTemp_AlarmStatus1 = (byte)((data[2] & 0x40) >> 6);
            realBmsStatus.OverTemp_AlarmStatus2 = (byte)((data[2] & 0x80) >> 7);

            realBmsStatus.UnderTemp_AlarmStatus1 = (byte)((data[3] & 0x01) >> 0);
            realBmsStatus.UnderTemp_AlarmStatus2 = (byte)((data[3] & 0x02) >> 1);
            realBmsStatus.insulation_AlarmStatus1 = (byte)((data[3] & 0x04) >> 2);
            realBmsStatus.insulation_AlarmStatus2 = (byte)((data[3] & 0x08) >> 3);
            realBmsStatus.cellOverVolt_AlarmStatus1 = (byte)((data[3] & 0x10) >> 4);
            realBmsStatus.cellOverVolt_AlarmStatus2 = (byte)((data[3] & 0x20) >> 5);
            realBmsStatus.modulePol_AlarmStatus = (byte)((data[3] & 0x40) >> 6);
            realBmsStatus.moduleOtp_AlarmStatus = (byte)((data[3] & 0x80) >> 7);

            realBmsStatus.can0_FaultStatus = (byte)((data[4] & 0x01) >> 0);
            realBmsStatus.can1_FaultStatus = (byte)((data[4] & 0x02) >> 1);
            realBmsStatus.can2_FaultStatus = (byte)((data[4] & 0x04) >> 2);
            //realBmsStatus.relayFaultStatus = (byte)((data[4] & 0x08) >> 3);
            realBmsStatus.fuseFaultStatus = (byte)((data[4] & 0x10) >> 4);
            realBmsStatus.umsFaultStatus = (byte)((data[4] & 0x20) >> 5);

            AlarmMsgShow();
           

        }



        /*串口接收数据全部在这里进行处理*/
        private void timerRec_Tick(object sender, EventArgs e)
        {
            //定时轮询
            byte[] rbytes = new byte[1024];
            byte[] sysdata = new byte[1024];

            byte addr,funcode,len;


            int readBytes = SerialDebug.ringBuffer.GetDataCount();

            if (readBytes > 3)
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

                switch (funcode)
                {
                    case 0x01:   //读取线圈状态

                        SerialDebug.ringBuffer.Clear(readBytes);

                        convertBmsOutStatus(sysdata, len);
                        
                        break;

                    case 0x02:   //读取输入状态

                        SerialDebug.ringBuffer.Clear(readBytes);

                        convertBmsInStatus(sysdata, len);

                        break;

                    case 0x04:   //读取输入寄存器

                        //转换后数据
                        ushort[] data1 = tool.toShortArray(sysdata);

                        SerialDebug.ringBuffer.Clear(readBytes);

                        convertBmsData(data1, (byte)(len/2));
                        break;

                    case 0x03:   //读取保持寄存器

                        //转换后数据
                        ushort[] data2 = tool.toShortArray(sysdata);

                        SerialDebug.ringBuffer.Clear(readBytes);

                        ControlForm.convertBmsData(data2, (byte)(len / 2));
                        break;


                    default:
                        SerialDebug.ringBuffer.Clear(readBytes);
                        break;

                }


               

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
