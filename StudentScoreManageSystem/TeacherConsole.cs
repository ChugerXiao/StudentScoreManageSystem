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
    public partial class TeacherConsole : Form
    {
        public string mid;

        public TeacherConsole(string tid)
        {
            InitializeComponent();
            mid = tid;
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            Form baseMsg = new BaseMessage(mid);
            baseMsg.ShowDialog();
        }

        private void pictureBox2_Click(object sender, System.EventArgs e)
        {
            Form selectScore = new SelectScore(mid);
            selectScore.ShowDialog();
        }

        private void pictureBox3_Click(object sender, System.EventArgs e)
        {
            Form handUpScore = new HandUpScore(mid);
            handUpScore.ShowDialog();
        }

        private void pictureBox4_Click(object sender, System.EventArgs e)
        {
            Form classMsg = new ClassMessage(mid);
            classMsg.ShowDialog();
        }
    }
}
