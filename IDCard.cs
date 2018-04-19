using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2
{
    public class IDCard
    {
        public string tName { get; set; }//姓名 
        public string tSex { get; set; }//性别 
        public string tCode { get; set; }//身份证号 
        public string tNation { get; set; }//民族 
        public int tBirth { get; set; } //生日 
        public string tAddress { get; set; }//家庭住址 
        public string tInstitution { get; set; }//发证机关 
        public string tValidity { get; set; }//有效期结束 
        public string tPhotoBox1 { get; set; }//照片路径

        public static string InitCvrSdk { get; set; }//身份证阅读器初始状态
    }
}
