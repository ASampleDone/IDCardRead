using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Demo2
{
    public class CVRSDK
    {
        static int RetUsb = 0, RetCom = 0;
        public static string Init()
        {
            for (int port = 1001; port <= 1016; port++)
            {
                RetUsb = CVRSDK.CVR_InitComm(port);
                if (RetUsb == 1) break;
            }
            for (int com = 1; com <= 4; com++)
            {
                RetCom = CVRSDK.CVR_InitComm(com);
                if (RetCom == 1) break;
            }
            if (RetCom == 1 || RetUsb == 1) return "初始化成功";
            else return "初始化失败";
        }
        public void CloseCommunite()
        {
            CVRSDK.CVR_CloseComm();
        }
        //实现IDisposable接口，要实现的方法
        public void Dispose()
        {
            Dispose();
            GC.Collect();//释放托管资源
            GC.SuppressFinalize(this);
            throw new NotImplementedException();
        }

        //读取身份证信息
        public static int ReadCard()
        {
            //用于读卡器与身份证建立连接
            int authen = CVRSDK.CVR_Authenticate();
            if (authen != 1) return -1;//卡片放置位置不正确
            else
            {
                int readContent = CVRSDK.CVR_Read_Content(4);
                if (readContent == 2)
                {
                    return 1;//"读卡操作成功！";
                }
                else
                {
                    return 0; //"读卡操作失败！";
                }
            }
        }

        public static IDCard getFarmerCard()
        {
            IDCard idCard = new IDCard();
            try
            {
                //pictureBox1.ImageLocation = Application.StartupPath + "\\zp.bmp";
                byte[] tName = new byte[30];
                int length = 20;
                CVRSDK.GetPeopleName(ref tName[0], ref length);

                byte[] idCode = new byte[30];
                length = 36;
                CVRSDK.GetPeopleIDCode(ref idCode[0], ref length);

                byte[] tNation = new byte[30];
                length = 3;
                CVRSDK.GetPeopleNation(ref tNation[0], ref length);

                byte[] tValidity = new byte[30];
                length = 16;
                CVRSDK.GetEndDate(ref tValidity[0], ref length);

                byte[] tBirth = new byte[30];
                length = 16;
                CVRSDK.GetPeopleBirthday(ref tBirth[0], ref length);

                byte[] tAddress = new byte[50];
                length = 70;
                CVRSDK.GetPeopleAddress(ref tAddress[0], ref length);

                byte[] tInstitution = new byte[30];
                length = 30;
                CVRSDK.GetDepartment(ref tInstitution[0], ref length);

                byte[] tSex = new byte[30];
                length = 3;
                CVRSDK.GetPeopleSex(ref tSex[0], ref length);

                //byte[] tPhoto = new byte[200];
                //length = 200;
                //CVRSDK.GetPhotoBMP(tPhoto[0], length);
               
                



                //对获取到的人的身份证信息字符串进行处理

                idCard.tName = Encoding.GetEncoding("GB2312").GetString(tName);
        
                idCard.tSex = Encoding.GetEncoding("GB2312").GetString(tSex);
             
                idCard.tCode = Encoding.GetEncoding("GB2312").GetString(idCode);
                
                idCard.tNation = Encoding.GetEncoding("GB2312").GetString(tNation);
             
                idCard.tValidity = Encoding.GetEncoding("GB2312").GetString(tValidity);

                string str = Encoding.GetEncoding("GB2312").GetString(tBirth);
                str = str.Substring(0, 10);
                idCard.tBirth = ConvertDateTimeInt(Convert.ToDateTime(str));

                idCard.tAddress = Encoding.GetEncoding("GB2312").GetString(tAddress);

                //string range = card.ValidTermOfStart + "-" + card.ValidTermOfEnd;
                //range += "- " + card.ValidTermOfEnd;
            
                idCard.tInstitution = Encoding.GetEncoding("GB2312").GetString(tInstitution);

                //idCard.tPhotoBox1 = Encoding.GetEncoding("GB2312").GetString(tPhoto);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return idCard;
        }
        //datetime转化为int
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        //初始化串口 参数：port 设置串口
        [DllImport("termb.dll", EntryPoint = "CVR_InitComm", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int CVR_InitComm(int Port);//声明外部的标准动态库, 跟Win32API是一样的
        //读卡器和卡片之间的合法身份确认
        [DllImport("termb.dll", EntryPoint = "CVR_Authenticate", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int CVR_Authenticate();
        //读卡器从非接触卡中读取相应信息 参数：active 读取信息类型
        [DllImport("termb.dll", EntryPoint = "CVR_Read_Content", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int CVR_Read_Content(int Active);

        [DllImport("termb.dll", EntryPoint = "CVR_ReadBaseMsg", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int CVR_ReadBaseMsg(char pucCHMsg, int puiCHMsgLen, char pucPHMsg, int puiPHMsgLen, int nMode);
        //关闭串口
        [DllImport("termb.dll", EntryPoint = "CVR_CloseComm", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int CVR_CloseComm();
        //获取姓名信息 参数：[out]strTmp 读到的信息    [in] strLen 表示strTmp参数分配的内存空间大小
        [DllImport("termb.dll", EntryPoint = "GetPeopleName", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern  int GetPeopleName(ref byte strTmp, ref int strLen);
        //获取民族信息
        [DllImport("termb.dll", EntryPoint = "GetPeopleNation", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetPeopleNation(ref byte strTmp, ref int strLen);
        //获取出生日期
        [DllImport("termb.dll", EntryPoint = "GetPeopleBirthday", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPeopleBirthday(ref byte strTmp, ref int strLen);
        //获取地址信息
        [DllImport("termb.dll", EntryPoint = "GetPeopleAddress", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPeopleAddress(ref byte strTmp, ref int strLen);
        //获取卡号信息
        [DllImport("termb.dll", EntryPoint = "GetPeopleIDCode", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPeopleIDCode(ref byte strTmp, ref int strLen);
        //获取发证机关信息
        [DllImport("termb.dll", EntryPoint = "GetDepartment", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetDepartment(ref byte strTmp, ref int strLen);
        //获取有效期开启日期
        [DllImport("termb.dll", EntryPoint = "GetStartDate", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetStartDate(ref byte strTmp, ref int strLen);
        //获取有效截止日期
        [DllImport("termb.dll", EntryPoint = "GetEndDate", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetEndDate(ref byte strTmp, ref int strLen);
        //获取性别信息
        [DllImport("termb.dll", EntryPoint = "GetPeopleSex", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPeopleSex(ref byte strTmp, ref int strLen);
        //[DllImport("termb.dll", EntryPoint = "GetPhotoBMP", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        //public static extern int GetPhotoBMP(byte Photo,  int Len);
       
    }
}
