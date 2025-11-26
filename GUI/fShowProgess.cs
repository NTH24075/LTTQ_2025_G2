using LTTQ_G2_2025.BLL;
using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace LTTQ_G2_2025.GUI
{
    public partial class fShowProgess : Form
    {
        private readonly long _projectId;
        private readonly long _teacherId;
        private readonly TienDoBLL _tienDoBLL;
        private readonly TeacherBLL _teacherBLL;

        public fShowProgess(long projectId, long teacherId)
        {
            InitializeComponent();
            _projectId = projectId;
            _teacherId = teacherId;

            _tienDoBLL = new TienDoBLL(_teacherId);
            _teacherBLL = new TeacherBLL();
        }

        private void fShowProgess_Load(object sender, EventArgs e)
        {
            LoadProjectName();
            LoadStageStatus();
        }

        private void LoadProjectName()
        {
            // Lấy tên đồ án từ TeacherBLL → GetProjectDetail
            var row = _teacherBLL.GetProjectDetail(_projectId);

            if (row != null)
            {
                lblTenDoAn.Text = "Đồ án: " + row["name"].ToString();
            }
            else
            {
                lblTenDoAn.Text = "Đồ án: (Không tìm thấy)";
            }
        }

        private void LoadStageStatus()
        {
            DataTable dt = _tienDoBLL.GetStageCompletionStatus(_projectId);

            lblStatus1.Text = "Chưa có dữ liệu";
            lblStatus2.Text = "Chưa có dữ liệu";
            lblStatus3.Text = "Chưa có dữ liệu";

            for (int i = 0; i < dt.Rows.Count && i < 3; i++)
            {
                DataRow r = dt.Rows[i];
                string stageName = r["Giai đoạn"].ToString();
                string status = r["Trạng thái"].ToString();

                switch (i)
                {
                    case 0:
                        lblStage1.Text = stageName;
                        lblStatus1.Text = status;
                        break;
                    case 1:
                        lblStage2.Text = stageName;
                        lblStatus2.Text = status;
                        break;
                    case 2:
                        lblStage3.Text = stageName;
                        lblStatus3.Text = status;
                        break;
                }
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
