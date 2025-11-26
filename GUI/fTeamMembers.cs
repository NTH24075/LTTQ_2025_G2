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
    public partial class fTeamMembers : Form
    {
        private readonly long _projectId;
        private readonly long _teacherId;
        private readonly TienDoBLL _tienDoBLL;
        public fTeamMembers(long projectId, long teacherId)
        {
            InitializeComponent();
            _projectId = projectId;
            _teacherId = teacherId;
            _tienDoBLL = new TienDoBLL(_teacherId);
        }

        private void fTeamMembers_Load(object sender, EventArgs e)
        {
            LoadThanhVien();
        }
        private void LoadThanhVien()
        {
            DataTable dt = _tienDoBLL.GetTeamMembersByProject(_projectId);
            dgvThanhVien.DataSource = dt;

            dgvThanhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThanhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThanhVien.MultiSelect = false;
            dgvThanhVien.ReadOnly = true;
            dgvThanhVien.AllowUserToAddRows = false;
            dgvThanhVien.RowTemplate.Height = 30;

            // Ẩn các cột không muốn show trên grid
            if (dgvThanhVien.Columns["IdSV"] != null)
                dgvThanhVien.Columns["IdSV"].Visible = false;
            if (dgvThanhVien.Columns["Email"] != null)
                dgvThanhVien.Columns["Email"].Visible = false;
            if (dgvThanhVien.Columns["Nhóm"] != null)
                dgvThanhVien.Columns["Nhóm"].Visible = false;

            // Nếu có dữ liệu thì chọn dòng đầu tiên và hiển thị chi tiết
            if (dgvThanhVien.Rows.Count > 0)
            {
                dgvThanhVien.Rows[0].Selected = true;
                HienChiTietSinhVien();
            }
        }
        private void dgvThanhVien_SelectionChanged(object sender, EventArgs e)
        {
            HienChiTietSinhVien();
        }
        private void HienChiTietSinhVien()
        {
            if (dgvThanhVien.SelectedRows.Count == 0)
                return;

            DataGridViewRow row = dgvThanhVien.SelectedRows[0];

            txtMaSV.Text = row.Cells["Mã SV"].Value?.ToString();
            txtHoTen.Text = row.Cells["Họ tên"].Value?.ToString();
            txtLop.Text = row.Cells["Lớp"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtNhom.Text = row.Cells["Nhóm"].Value?.ToString();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
