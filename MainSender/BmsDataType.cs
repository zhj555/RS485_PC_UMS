using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSender
{
    public class BmsDataType //internal
    {
        public class BmsData
        {
            public float voltage1;
            public float voltage2;
            public float current1;
            public float current2;
            public ushort res_insulation_posi;
            public ushort res_insulation_neg;
            public float pcb_temp;
            public float soc;
            public float soh;
            public ushort softwareVersion;
            public ushort capacity;
            public ushort sysState;
            public ushort subState;
            public ushort HighModuleVolt_Number; //最高电压模组编号
            public float LowModuleVolt;         //模组最低电压
            public ushort LowModuleVolt_Number;  //最低电压模组编号
            public float moduleAvgVolt;        //模组平均电压
            public float moduleHighTemp;       //模组最高温度
            public ushort HighModuleTmep_Number; //最高温度模组编号
            public float moduleLowTemp;        //模组最低温度
            public ushort LowModuleTmep_Number; //最低温度模组编号
            public float moduleAvgTemp;        //模组平均温度
            public float insulationVBusPos;    //绝缘检测VBus+
            public float insulationVBusNeg;    //绝缘检测VBus-
            public ushort reserve1;
            public ushort reserve2;
            public ushort reserve3;
            public ushort reserve4;

            public float ntc_temp1;
            public float ntc_temp2;
            public float ntc_temp3;
            public float ntc_temp4;
            public float ntc_temp5;
            public float ntc_temp6;

            public BmsData()
            {
                voltage1 = 0;
                voltage2 = 0;
                //current = 0;
                res_insulation_posi = 0;
                res_insulation_neg = 0;
                pcb_temp = 0;
                soc = 0;
                soh = 0;
                ntc_temp1 = 0;
                ntc_temp2 = 0;
                ntc_temp3 = 0;
                ntc_temp4 = 0;
                ntc_temp5 = 0;
                ntc_temp6 = 0;
            }
        }

        public class BmsStatus
        {
            /*DO输出状态 - 线圈状态*/
            public byte DO1_Status = 0;
            public byte DO2_Status = 0;
            public byte DO3_Status = 0;
            public byte DO4_Status = 0;
            public byte DO5_Status = 0;
            public byte DO6_Status = 0;
            public byte DO7_Status = 0;
            public byte DO8_Status = 0;


            /*DI输入状态*/
            public byte DI1_Status = 0;
            public byte DI2_Status = 0;
            public byte DI3_Status = 0;
            public byte DI4_Status = 0;
            public byte DI5_Status = 0;
            public byte DI6_Status = 0;
            public byte DI7_Status = 0;
            public byte DI8_Status = 0;

            
            /*为了兼容modbus之前版本协议，保持顺序一样*/
            /*总压1过压状态*/
            public byte TotalOverVol1_AlarmStatus1;
            public byte TotalOverVol1_AlarmStatus2;

            /*总压1欠压状态*/
            public byte TotalUnderVol1_AlarmStatus1;
            public byte TotalUnderVol1_AlarmStatus2;

            /*总压2过压状态*/
            public byte TotalOverVol2_AlarmStatus1;
            public byte TotalOverVol2_AlarmStatus2;

            /*总压2欠压状态*/
            public byte TotalUnderVol2_AlarmStatus1;
            public byte TotalUnderVol2_AlarmStatus2;

            /*总电流1过流状态*/
            public byte OverCurrent1_AlarmStatus1;
            public byte OverCurrent1_AlarmStatus2;

            /*总电流2过流状态*/
            public byte OverCurrent2_AlarmStatus1;
            public byte OverCurrent2_AlarmStatus2;

            /*模组过温状态*/
            public byte OverTemp_AlarmStatus1;
            public byte OverTemp_AlarmStatus2;

            /*模组欠温状态*/
            public byte UnderTemp_AlarmStatus1;
            public byte UnderTemp_AlarmStatus2;

            /*绝缘状态*/
            public byte insulation_AlarmStatus1;
            public byte insulation_AlarmStatus2;

            /*单体过压*/
            public byte cellOverVolt_AlarmStatus1;
            public byte cellOverVolt_AlarmStatus2;

            /*模组POL告警*/
            public byte modulePol_AlarmStatus;

            /*模组OTP告警*/
            public byte moduleOtp_AlarmStatus;

            /*can 通讯故障*/
            public byte can0_FaultStatus;
            public byte can1_FaultStatus;
            public byte can2_FaultStatus;

            /*正负极接触器状态*/
            public byte relayFaultStatus;

            /*熔断器状态*/
            public byte fuseFaultStatus;

            /*ums自身异常*/
            public byte umsFaultStatus;


            
        }


    }
}
