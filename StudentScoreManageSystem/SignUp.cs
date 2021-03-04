using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StudentScoreManageSystem
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            login.Show();
            Dispose(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入姓名！");
                return;
            }
            if (textBox1.Text.Length < 1 || textBox1.Text.Length > 4)
            {
                MessageBox.Show("请输入真实姓名！");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("请输入账号！");
                return;
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("请输入密码！");
                return;
            }
            if (textBox4.Text != textBox3.Text)
            {
                MessageBox.Show("两次密码不一致！");
                return;
            }
            // users.info format -> 教师 张三 高三（1） zhangsan 123456 \n
            string personInfo = comboBox1.Text + ' ' + textBox1.Text + ' ' + comboBox2.Text + ' ' + textBox2.Text + ' ' + textBox3.Text + " \n";
            StreamWriter sw = new StreamWriter("users.info", true);
            sw.Write(personInfo);
            sw.Flush();
            sw.Close();
            MessageBox.Show("注册成功！");
            Form login = new Login();
            login.Show();
            Dispose(false);
        }
    }
}
