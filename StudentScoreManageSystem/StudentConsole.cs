using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentScoreManageSystem
{
    public partial class StudentConsole : Form
    {
        public string mid;

        public StudentConsole(string sid)
        {
            InitializeComponent();
            mid = sid;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form baseMsg = new BaseMessage(mid);
            baseMsg.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form selectScore = new SelectScore(mid);
            selectScore.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form teacherMsg = new TeacherMessage(mid);
            teacherMsg.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form classMsg = new ClassMessage(mid);
            classMsg.ShowDialog();
        }
    }
}
