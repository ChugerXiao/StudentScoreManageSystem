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
    public partial class ClassMessage : Form
    {
        string mid;
        DataSet dataSet;
        OleDbConnection connection = new OleDbConnection();

        private bool isTeacher(string id)
        {
            if (id.Length == 10)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

        public ClassMessage(string id)
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=usersInfo.accdb;Persist Security Info=False;";
            mid = id;
        }

        private void tryOpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误！\n\n" + ex);
                Dispose();
            }
        }

        private void ClassMessage_Load(object sender, EventArgs e)
        {
            tryOpenConnection();
            string sql;
            if (!isTeacher(mid))
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT class FROM stuLogin WHERE id='" + mid + "'";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                sql = "SELECT stuLogin.id, fullName, class, sex, yearOld, partyOrNot, address, phone FROM student INNER JOIN stuLogin ON student.id = stuLogin.id WHERE stuLogin.class='" + reader.GetString(0).ToString() + "'";
                reader.Close();
            }
            else
            {
                sql = "SELECT stuLogin.id, stuLogin.fullName, stuLogin.class, sex, yearOld, partyOrNot, address, phone FROM teaLogin INNER JOIN (student INNER JOIN stuLogin ON student.id = stuLogin.id) ON teaLogin.class = stuLogin.class WHERE teaLogin.id='" + mid + "'";
            }
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> items = new List<string>();
            string rows;
            dataGridView1.DataSource = dataSet.Tables[0];
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            switch (comboBox1.Text)
            {
                case "性别":
                    rows = "sex";
                    break;
                case "年龄":
                    rows = "yearOld";
                    break;
                case "党员否":
                    rows = "partyOrNot";
                    break;
                case "家庭住址":
                    rows = "address";
                    break;
                default:
                    return;
            }
            for (int index = 0; index < dataSet.Tables[0].Rows.Count; index++)
            {
                items.Add(dataSet.Tables[0].Rows[index][rows].ToString());
            }
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = items.Count - 1; j > i; j--)
                {
                    if (items[i] == items[j])
                    {
                        items.RemoveAt(j);
                    }
                }
            }
            comboBox2.Items.AddRange(items.ToArray());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet newSet = dataSet.Copy();
            string rows;
            switch (comboBox1.Text)
            {
                case "性别":
                    rows = "sex";
                    break;
                case "年龄":
                    rows = "yearOld";
                    break;
                case "党员否":
                    rows = "partyOrNot";
                    break;
                case "家庭住址":
                    rows = "address";
                    break;
                default:
                    return;
            }
            for (int index = 0; index < newSet.Tables[0].Rows.Count; index++)
            {
                if (newSet.Tables[0].Rows[index][rows].ToString() != comboBox2.Text)
                {
                    newSet.Tables[0].Rows.RemoveAt(index);
                }
                dataGridView1.DataSource = newSet.Tables[0];
            }
        }
    }
}
