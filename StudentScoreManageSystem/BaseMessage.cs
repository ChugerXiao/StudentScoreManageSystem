using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace StudentScoreManageSystem
{
    public partial class BaseMessage : Form
    {
        public string mid;

        public BaseMessage(string id)
        {
            InitializeComponent();
            mid = id;
        }

        private string getIdentity(string id)
        {
            switch (id.Length)
            {
                case 10:
                    return "stuLogin";
                case 6:
                    return "teaLogin";
                default:
                    return " ";
            }
        }

        private void BaseMessage_Load(object sender, EventArgs e)
        {
            if (getIdentity(mid) == "teaLogin")
            {
                label3.Text = "教工号";
            }
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
            command.CommandText = "SELECT fullName, class FROM " + getIdentity(mid) + " WHERE id='" + mid + "'";
            OleDbDataReader reader = command.ExecuteReader();
            bool exist = reader.Read();
            if (exist)
            {
                textBox1.Text = reader.GetString(0);
                textBox2.Text = reader.GetString(1);
                textBox3.Text = mid;
            }
            else
            {
                MessageBox.Show("没有您的信息！");
            }
            connection.Close();
            reader.Close();
        }
    }
}
