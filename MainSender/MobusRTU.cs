using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.Device;

namespace MainSender
{
    internal class MobusRTU
    {


        /// <summary>
        /// 私有ModbusRTU主站字段
        /// </summary>
        public  IModbusMaster master;

        public MobusRTU()
        {

        }

        /// <summary>
        /// 写入单个线圈
        /// </summary>
        public void WriteSingleCoil()
        {
/*            bool result = false;
            if (rbxRWMsg.Text.Equals("true", StringComparison.OrdinalIgnoreCase) || rbxRWMsg.Text.Equals("1", StringComparison.OrdinalIgnoreCase))
            {
                result = true;
            }
            master.WriteSingleCoil((byte)nudSlaveID.Value, (ushort)nudStartAdr.Value, result);*/
        }


        /// <summary>
        /// 批量写入线圈
        /// </summary>
        public void WriteArrayCoil()
        {
/*            List<string> strList = rbxRWMsg.Text.Split(',').ToList();

            List<bool> result = new List<bool>();

            strList.ForEach(m => result.Add(m.Equals("true", StringComparison.OrdinalIgnoreCase) || m.Equals("1", StringComparison.OrdinalIgnoreCase)));

            master.WriteMultipleCoils((byte)nudSlaveID.Value, (ushort)nudStartAdr.Value, result.ToArray());*/
        }

        /// <summary>
        /// 写入单个寄存器
        /// </summary>
        public void WriteSingleRegister()
        {
/*            ushort result = Convert.ToUInt16(rbxRWMsg.Text);

            master.WriteSingleRegister((byte)nudSlaveID.Value, (ushort)nudStartAdr.Value, result);*/
        }

        /// <summary>
        /// 批量写入寄存器
        /// </summary>
        public void WriteArrayRegister()
        {
/*            List<string> strList = rbxRWMsg.Text.Split(',').ToList();

            List<ushort> result = new List<ushort>();

            strList.ForEach(m => result.Add(Convert.ToUInt16(m)));

            master.WriteMultipleRegisters((byte)nudSlaveID.Value, (ushort)nudStartAdr.Value, result.ToArray());*/
        }

        /// <summary>
        /// 读取输出线圈
        /// </summary>
        /// <returns></returns>
/*        private bool[] ReadCoils()
        {
            //return master.ReadCoils((byte)nudSlaveID.Value, (ushort)nudStartAdr.Value, (ushort)nudLength.Value);
        }

        /// <summary>
        /// 读取输入线圈
        /// </summary>
        /// <returns></returns>
        private bool[] ReadInputs()
        {
            //return master.ReadInputs((byte)nudSlaveID.Value, (ushort)nudStartAdr.Value, (ushort)nudLength.Value);
        }*/

        /// <summary>
        /// 读取保持型寄存器
        /// </summary>
        /// <returns></returns>
        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            //return master.ReadHoldingRegisters((byte)nudSlaveID.Value, (ushort)nudStartAdr.Value, (ushort)nudLength.Value);
            return master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
        }

        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        /// <returns></returns>
        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            ushort[] inputRegData = new ushort[numberOfPoints];


/*            try
            {
                inputRegData= master.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取输入寄存器失败！"+ ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return inputRegData;

            }

            return inputRegData;*/
            //return master.ReadInputRegisters((byte)nudSlaveID.Value, (ushort)nudStartAdr.Value, (ushort)nudLength.Value);
            return master.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
        }








    }
}
