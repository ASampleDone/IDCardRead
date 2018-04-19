using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IDCard idCard = new IDCard();
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                int result = CVRSDK.ReadCard();
                if (result == -1) label6.Text = "卡放置位置不对";
                else if (result == 1)
                {
                    label6.Text = "读卡成功";
                    //将读取数据存入相对的文字框中
                    fillData();
                    //MessageBox.Show(idCard.tPhotoBox1);
                }
                else label6.Text = "读卡失败";
        
        }

        private void fillData()
        {
            idCard = CVRSDK.getFarmerCard();
            tName.Text = idCard.tName.Trim();
            tSex.Text = idCard.tSex.Trim();
            tNation.Text = idCard.tNation.Trim();
            tCode.Text = idCard.tCode.Trim();
            tAddress.Text = idCard.tAddress.Trim();
            tBrith.Text = GetTime(idCard.tBirth.ToString()).ToString().Substring(0, 10);
            tInstitution.Text = idCard.tInstitution.Trim();
            tValidity.Text = idCard.tValidity.Trim();
            //pictureBox1.Image.Tag = idCard.tPhotoBox1.Trim();
            
           // pictureBox1.Image = idCard.tPhotoBox1;
        }
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string reuslt = CVRSDK.Init();
            label6.Text = reuslt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
