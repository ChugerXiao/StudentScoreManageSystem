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
    public partial class HandUpScore : Form
    {
        DataSet dataSet;
        string mid;

        public HandUpScore(string id)
        {
            InitializeComponent();
            mid = id;
        }

        private void ExecuteSql(string sql)
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

        private void HandUpScore_Load(object sender, EventArgs e)
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
            sql = "SELECT stuLogin.id, stuLogin.fullName FROM teaLogin INNER JOIN stuLogin ON teaLogin.class = stuLogin.class WHERE teaLogin.id='" + mid + "'";
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable table = dataSet.Tables[0];
            table.Columns.Add("score");
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请输入考试时间与科目！");
                return;
            }
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                string score = dataGridView1.Rows[index].Cells[2].Value.ToString();
                string sql = "INSERT INTO scores(id, subject, score, [time]) VALUES ('" + id + "', '" + textBox2.Text + "', '" + score + "', '" + textBox1.Text + "')";
                ExecuteSql(sql);
            }
            MessageBox.Show("成绩提交成功！");
            Dispose();
            // INSERT INTO scores(id, subject, score, [time]) VALUES ('2021010101', '政治', '77', '2021-3-12')
        }
    }
}
