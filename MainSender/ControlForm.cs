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

        public static DataTable bms_thresholdValue = new DataTable();    //定义一个DataTable作为数据源

        public static bool holdRegistersQuery = false;

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
    }
}
