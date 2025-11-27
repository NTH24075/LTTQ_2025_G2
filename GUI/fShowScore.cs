using LTTQ_G2_2025.BLL;
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
    public partial class fShowScore : Form
    {
        private readonly long _projectId;
        private readonly DiemBLL _diemBLL;
        public fShowScore(long projectId, long teacherId)
        {
            InitializeComponent();
            _projectId = projectId;
            _diemBLL = new DiemBLL(teacherId);

            // Cố định form, không cho resize
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }
        private void fShowScore_Load(object sender, EventArgs e)
        {
            LoadScoreGrid();
        }

        private void LoadScoreGrid()
        {
            DataTable dt = _diemBLL.GetScoreMatrixByProject(_projectId);
            dgvScore.DataSource = dt;

            dgvScore.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvScore.RowTemplate.Height = 30;
            dgvScore.ReadOnly = true;

            // === ĐỔI TÊN CỘT (NẾU DATA TABLE CHƯA ĐÚNG HEADER) ===
            if (dgvScore.Columns.Contains("student_id"))
                dgvScore.Columns["student_id"].HeaderText = "Mã SV";

            if (dgvScore.Columns.Contains("studentCode"))
                dgvScore.Columns["studentCode"].HeaderText = "Mã sinh viên";

            if (dgvScore.Columns.Contains("studentName"))
                dgvScore.Columns["studentName"].HeaderText = "Họ và tên";

            if (dgvScore.Columns.Contains("milestoneName"))
                dgvScore.Columns["milestoneName"].HeaderText = "Tên mốc";

            if (dgvScore.Columns.Contains("totalScore"))
                dgvScore.Columns["totalScore"].HeaderText = "Điểm";
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
