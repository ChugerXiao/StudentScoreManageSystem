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

        private bool allFilled()
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("请输入学号！");
                return false;
            }
            if (comboBox1.Text == "教师")
            {
                if (textBox5.Text.Length != 6)
                {
                    MessageBox.Show("教工号由六位数字组成！");
                    return false;
                }
            }
            else
            {
                if (textBox5.Text.Length != 10)
                {
                    MessageBox.Show("学号由十位数字组成！");
                    return false;
                }
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入姓名！");
                return false;
            }
            if (textBox1.Text.Length < 1 || textBox1.Text.Length > 4)
            {
                MessageBox.Show("请输入真实姓名！");
                return false;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("请输入账号！");
                return false;
            }
            if (label8.Text != "")
            {
                MessageBox.Show("该账号已被注册！");
                return false;
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("请输入密码！");
                return false;
            }
            if (textBox4.Text != textBox3.Text)
            {
                MessageBox.Show("两次密码不一致！");
                return false;
            }
            return true;
        }

        private string getTable()
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

        private void ExecuteSQL(string sql)
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=usersInfo.accdb;Persist Security Info=False;";
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误！\n\n" + ex);
                Dispose();
            }
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            connection.Close();
        }

        private object QuerySQL(string sql)
        {
            object obj = null;
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=usersInfo.accdb;Persist Security Info=False;";
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误！\n\n" + ex);
                Dispose();
            }
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = sql;
            obj = command.ExecuteScalar();
            connection.Close();
            return obj;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!allFilled())
                return;
            string sql;
            sql = "SELECT * FROM " + getTable() + " WHERE id = '" + textBox5.Text + "'";
            if (QuerySQL(sql) != null)
            {
                MessageBox.Show("id(" + textBox5.Text + ")已被注册。");
                textBox5.Clear();
                return;
            }
            sql = "SELECT * FROM " + getTable() + " WHERE username = '" + textBox2.Text + "'";
            if (QuerySQL(sql) != null)
            {
                MessageBox.Show("该用户名已被注册。");
                textBox2.Clear();
                return;
            }
            sql = "INSERT INTO " + getTable() + "(fullName, class, username, [password], id) VALUES ('" + textBox1.Text.Trim() + "', '" + comboBox2.Text.Trim() + "', '" + textBox2.Text.Trim() + "', '" + textBox3.Text.Trim() + "', '" + textBox5.Text.Trim() + "')";
            ExecuteSQL(sql);
            MessageBox.Show("注册成功！");
            Form Login = new Login();
            Login.Show();
            Dispose(false);
        }
    }
}
