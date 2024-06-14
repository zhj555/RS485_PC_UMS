using CCWin;
using CCWin.SkinControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MainSender.BmsDataType;

namespace MainSender
{
    public partial class ControlForm : Form
    {

        public DataTable bms_thresholdValue = new DataTable();    //定义一个DataTable作为数据源
        public ControlForm()
        {
            InitializeComponent();

            InitializeDataGridView();
        }

        //初始化DataGridView控件
        public void InitializeDataGridView()
        {
            bms_thresholdValue.Columns.Add("内容", typeof(string));
            bms_thresholdValue.Columns.Add("数值", typeof(float));
            bms_thresholdValue.Columns.Add("单位", typeof(string));



            //将数据源绑定到控件上
            skinDataGridView1.DataSource = bms_thresholdValue;


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

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //定时轮询
            byte[] rbytes = new byte[200];
            int readBytes = SerialDebug.holdBuffer.GetDataCount();

            if (readBytes > 0)
            {
                SerialDebug.holdBuffer.ReadBuffer(rbytes, 0, readBytes);

                ushort[] data = tool.toShortArray(rbytes);

                SerialDebug.holdBuffer.Clear(readBytes);

                convertBmsData(data);

            }

        }

        public void convertBmsData(ushort[] data)
        {


            bms_thresholdValue.Rows[0]["数值"] = data[0];
            bms_thresholdValue.Rows[1]["数值"] = data[1];
            bms_thresholdValue.Rows[2]["数值"] = data[2];
            bms_thresholdValue.Rows[3]["数值"] = data[3];
            bms_thresholdValue.Rows[4]["数值"] = data[4];
            bms_thresholdValue.Rows[5]["数值"] = data[5];
            bms_thresholdValue.Rows[6]["数值"] = data[6];
            bms_thresholdValue.Rows[7]["数值"] = data[7];
        }
    }
}
