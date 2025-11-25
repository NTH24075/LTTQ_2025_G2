using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTTQ_G2_2025.GUI
{
    public partial class fProjectDetail : Form
    {
        private readonly ProjectDetailDTO _p;
        public fProjectDetail(ProjectDetailDTO p)
        {
            InitializeComponent();
            _p = p;
        }

        private void fProjectDetail_Load(object sender, EventArgs e)
        {
            label1.Text = _p.Name;
            label2.Text = _p.Content;
            label3.Text = _p.Description;
            label6.Text = _p.SemesterId.ToString();
            label4.Text = _p.TeamName;
            label5.Text = _p.TeacherName;
            label7.Text = _p.SemesterName;
        }
    }
}
