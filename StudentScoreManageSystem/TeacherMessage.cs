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
    public partial class TeacherMessage : Form
    {
        string mid;
        int index = 0;
        List<string> fullName = new List<string>();
        List<string> sex = new List<string>();
        List<string> yearOld = new List<string>();
        List<string> teachSubject = new List<string>();
        List<string> partyOrNot = new List<string>();

        public TeacherMessage(string id)
        {
            InitializeComponent();
            button1.Enabled = false;
            mid = id;
        }

        private void TeacherMessage_Load(object sender, EventArgs e)
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
            command.CommandText = "SELECT teaLogin.fullName, sex, yearOld, teachSubject, partyOrNot FROM (stuLogin INNER JOIN teaLogin ON stuLogin.class = teaLogin.class) INNER JOIN teacher ON teaLogin.id = teacher.id WHERE stuLogin.id='" + mid + "'";
            OleDbDataReader reader = command.ExecuteReader(); 
            while (reader.Read())
            {
                fullName.Add(reader.GetString(0).ToString());
                sex.Add(reader.GetString(1).ToString());
                yearOld.Add(reader.GetString(2).ToString());
                teachSubject.Add(reader.GetString(3).ToString());
                partyOrNot.Add(reader.GetString(4).ToString());
            }
            if (fullName.Count() == 0)
            {
                button2.Enabled = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                MessageBox.Show("未查询到您班级教师情况！");
            }
            else if (fullName.Count() == 1)
            {
                button2.Enabled = false;
                textBox1.Text = fullName[index];
                textBox2.Text = sex[index];
                textBox3.Text = yearOld[index];
                textBox4.Text = teachSubject[index];
                textBox5.Text = partyOrNot[index];
            }
            else
            {
                button2.Enabled = true;
                textBox1.Text = fullName[index];
                textBox2.Text = sex[index];
                textBox3.Text = yearOld[index];
                textBox4.Text = teachSubject[index];
                textBox5.Text = partyOrNot[index];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            index--;
            textBox1.Text = fullName[index];
            textBox2.Text = sex[index];
            textBox3.Text = yearOld[index];
            textBox4.Text = teachSubject[index];
            textBox5.Text = partyOrNot[index];
            button2.Enabled = true;
            if (index == 0)
                button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index++;
            textBox1.Text = fullName[index];
            textBox2.Text = sex[index];
            textBox3.Text = yearOld[index];
            textBox4.Text = teachSubject[index];
            textBox5.Text = partyOrNot[index];
            button1.Enabled = true;
            if (index + 1 == fullName.Count())
                button2.Enabled = false;
        }
    }
}
