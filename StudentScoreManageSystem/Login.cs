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
using System.Data.OleDb;

namespace StudentScoreManageSystem
{
    public partial class Login : Form
    {
        OleDbConnection connection = new OleDbConnection();
        public Login()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=usersInfo.accdb;Persist Security Info=False;";
            comboBox1.SelectedIndex = 0;
        }

        private bool allFilled()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入账号！");
                return false;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("请输入密码！");
                return false;
            }
            return true;
        }

        public string getTable()
        {
            string table;
            switch (comboBox1.SelectedItem.ToString())
            {
                case "教师":
                    {
                        table = "teaLogin";
                        break;
                    }
                case "学生":
                    {
                        table = "stuLogin";
                        break;
                    }
                default:
                    { 
                        table = "";
                        break;
                    }
            }
            return table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!allFilled())
                return;
            try
            {
                connection.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show("数据库连接错误！\n\n" + ex);
                Dispose();
            }
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT password FROM " + getTable() + " WHERE username='" + textBox1.Text + "'";
            OleDbDataReader reader = command.ExecuteReader();
            bool exist = reader.Read();
            if (exist)
            {
                if (textBox2.Text == reader.GetString(0))
                {
                    MessageBox.Show("登录成功！");
                    // Goto => 管理界面
                    Dispose(false);
                }
                else
                {
                    textBox2.Clear();
                    MessageBox.Show("密码错误！");
                }
            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show("用户名错误！");
            }
            reader.Close();
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form signUp = new SignUp();
            signUp.Show();
            Dispose(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }


    }
}
