using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSender
{
    internal class tool
    {
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name=”bytes”></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                    returnStr += " ";  //增加空格
                }
            }
            return returnStr;
        }

        //byte[] 转为 ushort[]
        public static ushort[] toShortArray(byte[] src)
        {

            int count = src.Length >> 1;
            ushort[] dest = new ushort[count];
            for (int i = 0; i < count; i++)
            {
                dest[i] = (ushort)(src[i * 2] << 8 | src[2 * i + 1] & 0xff);
            }
            return dest;
        }


        //ushort[] 转为 byte[]
        public static byte[] toByteArray(ushort[] src)
        {

            int count = src.Length;
            byte[] dest = new byte[count << 1];
            for (int i = 0; i < count; i++)
            {
                dest[i * 2] = (byte)(src[i] >> 8);  //取高8bit
                dest[i * 2 + 1] = (byte)(src[i] >> 0);
            }

            return dest;
        }



        public string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += "%" + Convert.ToString(b[i],16);
            }
            return result;
        }
        public string HexStringToString(string hs, Encoding encode)
        {
            //以%分割字符串，并去掉空字符
            string[] chars = hs.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] b = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                b[i] = Convert.ToByte(chars[i], 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }
    }
}
