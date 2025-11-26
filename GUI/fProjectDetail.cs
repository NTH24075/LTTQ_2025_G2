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
    public partial class fProjectDetail : Form
    {
        private long projectId;
        private TeacherBLL teacherBLL = new TeacherBLL();
        private int currentStatus = 0;
        public fProjectDetail(long projectId)
        {
            InitializeComponent();
            this.projectId = projectId;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void fProjectDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            DataRow row = teacherBLL.GetProjectDetail(projectId);
            if (row == null)
            {
                MessageBox.Show("Không tìm thấy dữ liệu đề tài!", "Lỗi");
                this.Close();
                return;
            }

            // Hiển thị dữ liệu
            txtTenDoAn.Text = row["name"].ToString();
            txtNoiDung.Text = row["content"].ToString();
            txtMoTa.Text = row["description"].ToString();
            txtHocKy.Text = row["semester_id"].ToString();

            currentStatus = Convert.ToInt32(row["projectStatus"]);
            HienThiTrangThai(currentStatus);

            // Kiểm tra xem có được phép duyệt/hủy không
            DieuKhienButtonTheoTrangThai();
        }
        private void HienThiTrangThai(int status)
        {
            switch (status)
            {
                case 0:
                    lblTrangThai.Text = "Chưa duyệt";
                    lblTrangThai.ForeColor = Color.Goldenrod;
                    break;

                case 1:
                    lblTrangThai.Text = "Đã duyệt";
                    lblTrangThai.ForeColor = Color.Green;
                    break;

                case 2:
                    lblTrangThai.Text = "Bị hủy";
                    lblTrangThai.ForeColor = Color.Red;
                    break;
            }

            lblTrangThai.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }

        private void DieuKhienButtonTheoTrangThai()
        {
            // CHỈ cho phép thao tác khi status = 0 (Chưa duyệt)
            if (currentStatus == 0)
            {
                btnDuyet.Enabled = true;
                btnHuy.Enabled = true;
            }
            else
            {
                btnDuyet.Enabled = false;
                btnHuy.Enabled = false;
            }
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (currentStatus != 0)
            {
                MessageBox.Show("Chỉ có đề tài CHƯA duyệt mới được duyệt!", "Thông báo");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn DUYỆT đề tài này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (teacherBLL.UpdateProjectStatus(projectId, 1))
                {
                    MessageBox.Show("Đã duyệt đề tài!", "Thành công");
                    LoadData();  // Reload lại dữ liệu
                }
                else
                {
                    MessageBox.Show("Duyệt đề tài thất bại!", "Lỗi");
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (currentStatus != 0)
            {
                MessageBox.Show("Chỉ có đề tài CHƯA duyệt mới được hủy!", "Thông báo");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn HỦY đề tài này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (teacherBLL.UpdateProjectStatus(projectId, 2))
                {
                    MessageBox.Show("Đã hủy đề tài!", "Thành công");
                    LoadData();  // Reload lại dữ liệu
                }
                else
                {
                    MessageBox.Show("Hủy đề tài thất bại!", "Lỗi");
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
