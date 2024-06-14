using System;
using System.Globalization;
using System.IO;

namespace Library
{
    public class HexToBin
    {
        /// <summary>
        /// Hex文件转Bin数组
        /// </summary>
        /// <param name="HexPath">Hex文件地址</param>
        /// <param name="StartAddress">偏移地址</param>
        /// <returns></returns>
        public byte[] ToBinByte(string HexPath, int StartAddress = 0)
        {
            Int32 i = 0;
            Int32 j = 0;
            Int32 maxAddr = 0;        //HEX文件的最大地址
            Int32 segAddr = 0;        //段地址
            try
            {
                String szLine = "";
                if (HexPath == "")
                {
                    Console.Write("请选择需要转换的目标文件!");
                    return null;
                }
                StreamReader HexReader = new StreamReader(HexPath);
                //先找出HEX文件的最大地址
                while (true)
                {
                    szLine = HexReader.ReadLine(); //读取一行数据
                    i++;
                    if (szLine == null) //读完所有行
                    {
                        break;
                    }
                    if (szLine.Substring(0, 1) == ":") //判断第1字符是否是:
                    {
                        if (szLine.Substring(1, 8) == "00000001")//数据结束
                        {
                            break;
                        }
                        if (szLine.Substring(7, 2) == "02")
                        {
                            segAddr = Int32.Parse(szLine.Substring(9, 4), NumberStyles.HexNumber);
                            segAddr *= 16;
                        }
                        else if (szLine.Substring(7, 2) == "00")
                        {
                            Int32 tmpAddr = Int32.Parse(szLine.Substring(3, 4), NumberStyles.HexNumber);
                            tmpAddr += UInt16.Parse(szLine.Substring(1, 2), NumberStyles.HexNumber);
                            tmpAddr += segAddr;
                            if (tmpAddr > maxAddr)
                                maxAddr = tmpAddr;
                        }
                    }
                    else
                    {
                        Console.Write("错误:不是标准的hex文件!");
                        return null;
                    }
                }
                //新建一个二进制文件,填充为0XFF
                byte[] szBin = new byte[maxAddr];
                for (i = 0; i < maxAddr; i++)
                    szBin[i] = 0XFF;
                //返回文件开头
                HexReader.BaseStream.Seek(0, SeekOrigin.Begin);
                HexReader.DiscardBufferedData();//不加这句不能正确返回开头 
                segAddr = 0;
                //根据hex文件地址,填充bin文件
                while (true)
                {
                    szLine = HexReader.ReadLine(); //读取一行数据
                    if (szLine == null) //读完所有行
                    {
                        break;
                    }
                    if (szLine.Substring(0, 1) == ":") //判断第1字符是否是:
                    {
                        if (szLine.Substring(1, 8) == "00000001")//数据结束
                        {
                            break;
                        }
                        if (szLine.Substring(7, 2) == "02")
                        {
                            segAddr = Int32.Parse(szLine.Substring(9, 4), NumberStyles.HexNumber);
                            segAddr *= 16;
                        }
                        if (szLine.Substring(7, 2) == "00")
                        {
                            int tmpAddr = Int32.Parse(szLine.Substring(3, 4), NumberStyles.HexNumber);
                            int num = Int16.Parse(szLine.Substring(1, 2), NumberStyles.HexNumber);
                            tmpAddr += segAddr;
                            j = 0;
                            for (i = 0; i < num; i++)
                            {
                                szBin[tmpAddr++] = (byte)Int16.Parse(szLine.Substring(j + 9, 2), NumberStyles.HexNumber);
                                j += 2;
                            }

                        }
                    }
                }
                HexReader.Close(); //关闭目标文件

                //新建一个二进制文件
                byte[] Bin = new byte[maxAddr - StartAddress];
                Array.Copy(szBin, StartAddress, Bin, 0, maxAddr - StartAddress);
                return Bin;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Hex文件转Bin文件
        /// </summary>
        /// <param name="HexPath"></param>
        /// <param name="BinPath"></param>
        /// <param name="StartAddress"></param>
        /// <returns></returns>
        public bool ToBinFile(string HexPath, string BinPath, int StartAddress = 0)
        {
            try
            {
                byte[] Bin = ToBinByte(HexPath, StartAddress);
                if (BinPath == "")
                {
                    BinPath = Path.ChangeExtension(HexPath, "bin");
                }

                FileStream fBin = new FileStream(BinPath, FileMode.Create); //创建文件BIN文件
                BinaryWriter BinWrite = new BinaryWriter(fBin); //二进制方式打开文件
                BinWrite.Write(Bin, 0, Bin.Length); //写入数据
                BinWrite.Flush();//释放缓存
                BinWrite.Close();//关闭文件
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }
    }
}
