using CCWin;
using CCWin.SkinControl;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MainSender.BmsDataType;

namespace MainSender
{
    public partial class ControlForm : Form
    {

        public static DataTable bms_thresholdValue = new DataTable();    //定义一个DataTable作为数据源

        public static bool holdRegistersQuery = false;

        public static bool pcControl = false;
        

        public ControlForm()
        {
            InitializeComponent();

            InitializeDataGridView();

            SysForm1.conformhandle += OutputStatus_Display;
        }

        //初始化DataGridView控件
        public void InitializeDataGridView()
        {
            bms_thresholdValue.Columns.Add("内容", typeof(string));
            bms_thresholdValue.Columns.Add("数值", typeof(ushort));
            bms_thresholdValue.Columns.Add("单位", typeof(string));



            //将数据源绑定到控件上
            skinDataGridView1.DataSource = bms_thresholdValue;

            //设置各列的相对宽度
            skinDataGridView1.Columns[0].FillWeight = 50;  //第一列的相对宽度为50%
            skinDataGridView1.Columns[1].FillWeight = 25;  //第二列的相对宽度为25%
            skinDataGridView1.Columns[2].FillWeight = 25;  //第三列的相对宽度为25%


            /*数据初始化*/
            bms_thresholdValue.Rows.Add("存储有效标识", 0, " ");
            bms_thresholdValue.Rows.Add("绝缘检测校准使能", 0, " ");
            bms_thresholdValue.Rows.Add("电容组额定容量", 0, " ");
            bms_thresholdValue.Rows.Add("硬件版本", 0, " ");
            bms_thresholdValue.Rows.Add("从板数量", 0, " ");
            bms_thresholdValue.Rows.Add("风机开启温度", 0, "°C");
            bms_thresholdValue.Rows.Add("风机关闭温度", 0, "°C");
            bms_thresholdValue.Rows.Add("加热开启温度", 0, "°C");
            bms_thresholdValue.Rows.Add("加热关闭温度", 0, "°C");
            bms_thresholdValue.Rows.Add("总电压1级过压阈值1", 0, "V");
            bms_thresholdValue.Rows.Add("总电压2级过压阈值1", 0, "V");
            bms_thresholdValue.Rows.Add("总电压1级欠压阈值1", 0, "V");
            bms_thresholdValue.Rows.Add("总电压2级欠压阈值1", 0, "V");
            bms_thresholdValue.Rows.Add("总电压1级过压阈值2", 0, "V");
            bms_thresholdValue.Rows.Add("总电压2级过压阈值2", 0, "V");
            bms_thresholdValue.Rows.Add("总电压1级欠压阈值2", 0, "V");
            bms_thresholdValue.Rows.Add("总电压2级欠压阈值2", 0, "V");
            bms_thresholdValue.Rows.Add("总电流1级过流阈值1", 0, "V");
            bms_thresholdValue.Rows.Add("总电流2级过流阈值1", 0, "V");
            bms_thresholdValue.Rows.Add("模组OTP1级时间", 0, "V");
            bms_thresholdValue.Rows.Add("模组OTP2级时间", 0, "V");
            bms_thresholdValue.Rows.Add("模组1级过温阈值", 0, "°C");
            bms_thresholdValue.Rows.Add("模组2级过温阈值", 0, "°C");
            bms_thresholdValue.Rows.Add("模组1级欠温阈值", 0, "°C");
            bms_thresholdValue.Rows.Add("绝缘检测电压阈值", 0, "V");
            bms_thresholdValue.Rows.Add("绝缘1级故障阈值", 0, "KR");
            bms_thresholdValue.Rows.Add("绝缘2级故障阈值", 0, "KR");
            bms_thresholdValue.Rows.Add("单体OVP1级时间", 0, " ");
            bms_thresholdValue.Rows.Add("单体OVP2级时间", 0, " ");
            bms_thresholdValue.Rows.Add("最高允许充电总电压", 0, "V");
            bms_thresholdValue.Rows.Add("最低允许充电总电压", 0, "V");
            bms_thresholdValue.Rows.Add("最高允许充电电流", 0, "A");
            bms_thresholdValue.Rows.Add("最高允许放电电流", 0, "A");
            bms_thresholdValue.Rows.Add("绝缘检测等压误差", 0, " ");
            bms_thresholdValue.Rows.Add("跳转指令", 0, " ");
            bms_thresholdValue.Rows.Add("系统时钟年月", 0, " ");
            bms_thresholdValue.Rows.Add("系统时钟日时", 0, " ");
            bms_thresholdValue.Rows.Add("系统时钟分秒", 0, " ");

        }


        public static void convertBmsData(ushort[] data,byte len)
        {


/*            bms_thresholdValue.Rows[0]["数值"] = data[0];
            bms_thresholdValue.Rows[1]["数值"] = data[1];
            bms_thresholdValue.Rows[2]["数值"] = data[2];
            bms_thresholdValue.Rows[3]["数值"] = data[3];
            bms_thresholdValue.Rows[4]["数值"] = data[4];
            bms_thresholdValue.Rows[5]["数值"] = data[5];
            bms_thresholdValue.Rows[6]["数值"] = data[6];
            bms_thresholdValue.Rows[7]["数值"] = data[7];*/

            for(int i = 0; i < len; i++)
            {
                bms_thresholdValue.Rows[i]["数值"] = data[i];
            }

        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            holdRegistersQuery = true;
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            //上锁，避免串口同时发送
            SerialDebug.immediatelySendFlag = true;

            SerialDebug.ringBuffer.Clear();

            //获取当前选中行的值，并发送写保持寄存器命令
            int index = skinDataGridView1.CurrentRow.Index; //获得当前选中行的索引
            ushort value = (ushort)skinDataGridView1.Rows[index].Cells[1].Value;

            byte[] data = new byte[8];
            data[0] = 0x01;  //从机地址

            data[1] = 0x06;  //功能码

            data[2] = (byte)(index >> 8);  //寄存器地址
            data[3] = (byte)(index & 0xff);

            data[4] = (byte)(value >> 8);  //数据
            data[5] = (byte)(value & 0xff);

            byte[] crcValue = CRC.CRCCalc(data, 0, 6);

            data[6] = crcValue[0];  //校验码
            data[7] = crcValue[1];

            SerialDebug.SendData(data);

            Thread.Sleep(100); //等待接收

            //释放锁
            SerialDebug.immediatelySendFlag = false;


        }

        public void WriteSingeCoils(ushort regAddr,ushort value)
        {
            //发送写线圈指令
            //上锁，避免串口同时发送
            SerialDebug.immediatelySendFlag = true;

            SerialDebug.ringBuffer.Clear();

            byte[] data = new byte[8];
            data[0] = 0x01;  //从机地址

            data[1] = 0x05;  //功能码

            data[2] = (byte)(regAddr >> 8);  //寄存器地址
            data[3] = (byte)(regAddr & 0xff);

            data[4] = (byte)(value >> 8);  //数据
            data[5] = (byte)(value & 0xff);

            byte[] crcValue = CRC.CRCCalc(data, 0, 6);

            data[6] = crcValue[0];  //校验码
            data[7] = crcValue[1];

            SerialDebug.SendData(data);

            Thread.Sleep(100); //等待接收

            //释放锁
            SerialDebug.immediatelySendFlag = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                checkBox1.Image = Properties.Resources.开关_开;

                if(PcControl_checkBox.Checked)
                {
                    WriteSingeCoils(0x0000, 0xFF00);
                }
                
            }
            else
            {
                checkBox1.Image = Properties.Resources.开关_关;

                if(PcControl_checkBox.Checked)
                {
                    WriteSingeCoils(0x0000, 0x0000);
                }
                
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox2.Image = Properties.Resources.开关_开;

                if(PcControl_checkBox.Checked)
                {
                    WriteSingeCoils(0x0001, 0xFF00);
                }
            }
            else
            {
                checkBox2.Image = Properties.Resources.开关_关;

                if (PcControl_checkBox.Checked)
                {
                    WriteSingeCoils(0x0001, 0x0000);
                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox3.Image = Properties.Resources.开关_开;

                if (PcControl_checkBox.Checked)
                {
                    WriteSingeCoils(0x0002, 0xFF00);
                }
            }
            else
            {
                checkBox3.Image = Properties.Resources.开关_关;

                if (PcControl_checkBox.Checked)
                {
                    WriteSingeCoils(0x0002, 0x0000);
                }
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox4.Image = Properties.Resources.开关_开;

                if (PcControl_checkBox.Checked)
                {
                    WriteSingeCoils(0x0003, 0xFF00);
                }
            }
            else
            {
                checkBox4.Image = Properties.Resources.开关_关;

                if (PcControl_checkBox.Checked)
                {
                    WriteSingeCoils(0x0003, 0x0000);
                }
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox5.Image = Properties.Resources.开关_开;

                if (PcControl_checkBox.Checked)
                {
                    WriteSingeCoils(0x0004, 0xFF00);
                }
            }
            else
            {
                checkBox5.Image = Properties.Resources.开关_关;

                if (PcControl_checkBox.Checked)
                {
                    WriteSingeCoils(0x0004, 0x0000);
                }
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                checkBox6.Image = Properties.Resources.开关_开;

                if (PcControl_checkBox.Checked)
                {

                    WriteSingeCoils(0x0005, 0xFF00);
                }

            }
            else
            {
                checkBox6.Image = Properties.Resources.开关_关;

                if (PcControl_checkBox.Checked)
                {

                    WriteSingeCoils(0x0005, 0x0000);
                }
            }
        }

        private void PcControl_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(PcControl_checkBox.Checked)
            {
                PcControl_checkBox.Image = Properties.Resources.开关_开;

                pcControl = true;
            }
            else
            {
                PcControl_checkBox.Image = Properties.Resources.开关_关;

                pcControl = false;
            }
        }

        public void OutputStatus_Display()
        {
            //开关量状态显示
            if (SysForm1.realBmsStatus.DO1_Status == 1)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

            if (SysForm1.realBmsStatus.DO2_Status == 1)
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }

            if (SysForm1.realBmsStatus.DO3_Status == 1)
            {
                checkBox3.Checked = true;
            }
            else
            {
                checkBox3.Checked = false;
            }

            if (SysForm1.realBmsStatus.DO4_Status == 1)
            {
                checkBox4.Checked = true;
            }
            else
            {
                checkBox4.Checked = false;
            }

            if (SysForm1.realBmsStatus.DO5_Status == 1)
            {
                checkBox5.Checked = true;
            }
            else
            {
                checkBox5.Checked = false;
            }

            if (SysForm1.realBmsStatus.DO6_Status == 1)
            {
                checkBox6.Checked = true;
            }
            else
            {
                checkBox6.Checked = false;
            }
        }
    }
}
