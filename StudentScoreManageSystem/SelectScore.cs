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
    public partial class SelectScore : Form
    {
        private string mid;
        private string sName,sClass;
        DataSet dataSet;

        public SelectScore(string id)
        {
            InitializeComponent();
            mid = id;
        }

        private string getIdentity(string id)
        {
            switch (id.Length)
            {
                case 10:
                    return "student";
                case 6:
                    return "teacher";
                default:
                    return "unknow";
            }
        }

        private void onChangeLinstenster()
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
            string sql;
            if (getIdentity(mid) == "student")
            {
                sql = "SELECT subject, score, time FROM scores WHERE id='" + mid + "'";
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT fullName, class FROM stuLogin WHERE id='" + mid + "'";
                OleDbDataReader reader = command.ExecuteReader();
                bool exist = reader.Read();
                if (!exist)
                {
                    MessageBox.Show("您的数据不存在，请联系老师处理。");
                    Dispose();
                }
                else
                {
                    sName = reader.GetString(0);
                    sClass = reader.GetString(1);
                }
                reader.Close();
            }
            else
            {
                sql = "SELECT stuLogin.fullName, stuLogin.class, subject, score, time FROM (scores INNER JOIN stuLogin ON scores.id = stuLogin.id) LEFT JOIN teaLogin ON stuLogin.class = teaLogin.class WHERE teaLogin.id='" + mid + "'";
            }
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            onChangeLinstenster();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (getIdentity(mid) == "teacher")
            {
                sName = dataSet.Tables[0].Rows[e.RowIndex]["fullName"].ToString();
                sClass = dataSet.Tables[0].Rows[e.RowIndex]["class"].ToString();
            }
            textBox1.Text = sName;
            textBox2.Text = sClass;
            textBox3.Text = dataSet.Tables[0].Rows[e.RowIndex]["subject"].ToString();
            textBox4.Text = dataSet.Tables[0].Rows[e.RowIndex]["score"].ToString();
            textBox5.Text = dataSet.Tables[0].Rows[e.RowIndex]["time"].ToString();
        }
    }
}
