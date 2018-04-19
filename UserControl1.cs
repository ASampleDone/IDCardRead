using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo2
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            //读取文字、信息自定义内存缓冲
            char a, b;
            a = '1';
            b = '2';
            int result = CVRSDK.CVR_ReadBaseMsg(a, 10, b, 10, 3);
            if (result == 1)
            {
                MessageBox.Show("成功");
            }
            else MessageBox.Show("失败");
        }
    }
}
