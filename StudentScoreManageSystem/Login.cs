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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") 
            {
                MessageBox.Show("请输入账号！");
            }
            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("请输入密码！");
                }
                else
                {
                    string identity = comboBox1.SelectedItem.ToString();
                    try
                    {
                        // users.info format -> 教师 张三 高三（1） zhangsan 123456 \n
                        StreamReader sr = new StreamReader("users.info");
                        string usersInfo = sr.ReadToEnd();
                        sr.Close();
                        string[] persons = usersInfo.Split('\n');
                        foreach (string info in persons)
                        {
                            string[] personInfo = info.Split(' ');
                            if (personInfo[0] == identity && personInfo[3] == textBox1.Text)
                            {
                                if (personInfo[4] == textBox2.Text)
                                {
                                    MessageBox.Show("登录成功");
                                    // Goto => 管理界面
                                    Dispose();
                                }
                                else
                                {
                                    throw new IOException();
                                }
                            }
                        }
                        throw new IOException();
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("账号或密码错误！");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        
    }
}
