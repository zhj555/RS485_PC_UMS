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
            public ushort voltage1;
            public ushort voltage2;
            public ushort current;
            public ushort res_insulation_posi;
            public ushort res_insulation_neg;
            public ushort pcb_temp;
            public ushort soc;
            public ushort soh;
            public ushort ntc_temp1;
            public ushort ntc_temp2;
            public ushort ntc_temp3;
            public ushort ntc_temp4;
            public ushort ntc_temp5;
            public ushort ntc_temp6;

            public BmsData()
            {
                voltage1 = 0;
                voltage2 = 0;
                current = 0;
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
            /*DI输入状态*/
            public byte DI1_Status;
            public byte DI2_Status;
            public byte DI3_Status;
            public byte DI4_Status;
            public byte DI5_Status;
            public byte DI6_Status;
            public byte DI7_Status;
            public byte DI8_Status;

            /*总压过压状态*/
            public byte TotalOverVol1_AlarmStatus;
            public byte TotalOverVol2_AlarmStatus;

            /*总压欠压状态*/
            public byte TotalUnderVol1_AlarmStatus;
            public byte TotalUnderVol2_AlarmStatus;

            /*过流状态*/
            public byte OverCurrent1_AlarmStatus;
            public byte OverCurrent2_AlarmStatus;

            /*模组过温状态*/
            public byte OverTemp1_AlarmStatus;
            public byte OverTemp2_AlarmStatus;

            /*模组欠温状态*/
            public byte UnderTemp1_AlarmStatus;
            public byte UnderTemp2_AlarmStatus;

            /*绝缘状态*/
            public byte insulation1_AlarmStatus;
            public byte insulation2_AlarmStatus;

            /*熔断器状态*/
            public byte Fuse_AlarmStatus;
        }


    }
}
