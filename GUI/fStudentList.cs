using LTTQ_G2_2025.BLL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LTTQ_G2_2025.GUI
{
    public partial class fStudentList : Form
    {
        private long _accountId;
        private string _imageFolder;          // KHÔNG khởi tạo ở đây nữa
        private List<StudentDTO> _teamMembers;

        // ✅ Constructor mặc định – dùng cho Designer
        public fStudentList()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                _imageFolder = Path.Combine(Application.StartupPath, "Images", "Students");
                picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        // ✅ Constructor dùng khi chạy chương trình
        public fStudentList(long accountId) : this()   // gọi lại ctor trên
        {
            _accountId = accountId;
        }

        private void fStudentList_Load(object sender, EventArgs e)
        {
            // Không load dữ liệu khi đang mở Designer
            if (!DesignMode)
            {
                LoadTeamMembers();
            }
        }

        private void LoadTeamMembers()
        {
            // Lấy dữ liệu sinh viên cùng nhóm
            _teamMembers = StudentBLL.Instance.GetTeamMembersByAccountId(_accountId);

            var view = _teamMembers.Select(s => new
            {
                StudentName = s.StudentName,
                StudentCode = s.StudentCode,
                DateOfBirth = s.DateOfBirth,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                StudentGender = s.StudentGender ? "Nam" : "Nữ",
                StudentAddress = s.StudentAddress
            }).ToList();

            dgvStudent.AutoGenerateColumns = true;
            dgvStudent.Columns.Clear();

            dgvStudent.DataSource = view;
            dgvStudent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvStudent.Columns["StudentName"].HeaderText = "Họ và tên";
            dgvStudent.Columns["StudentCode"].HeaderText = "Mã sinh viên";
            dgvStudent.Columns["DateOfBirth"].HeaderText = "Ngày sinh";
            dgvStudent.Columns["Email"].HeaderText = "Email";
            dgvStudent.Columns["PhoneNumber"].HeaderText = "SĐT";
            dgvStudent.Columns["StudentGender"].HeaderText = "Giới tính";
            dgvStudent.Columns["StudentAddress"].HeaderText = "Địa chỉ";
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Đảm bảo đã có danh sách teamMembers
            if (_teamMembers == null || _teamMembers.Count == 0)
            {
                _teamMembers = StudentBLL.Instance.GetTeamMembersByAccountId(_accountId);
            }

            if (e.RowIndex >= _teamMembers.Count) return;

            var student = _teamMembers[e.RowIndex];

            // Lấy tên file ảnh từ DB
            string fileName = student.Img;
            string imgPath = Path.Combine(_imageFolder, fileName ?? "");

            // Load ảnh
            if (!string.IsNullOrEmpty(fileName) && File.Exists(imgPath))
            {
                // Optional: giải phóng ảnh cũ để tránh khóa file
                if (picAvatar.Image != null)
                {
                    picAvatar.Image.Dispose();
                    picAvatar.Image = null;
                }

                picAvatar.Image = Image.FromFile(imgPath);
            }
            else
            {
                picAvatar.Image = null;  // hoặc gán ảnh mặc định
            }

            // Đổ dữ liệu ra TextBox
            DataGridViewRow row = dgvStudent.Rows[e.RowIndex];

            txtName.Text = row.Cells["StudentName"].Value?.ToString();
            txtCode.Text = row.Cells["StudentCode"].Value?.ToString();
            txtDOB.Text = row.Cells["DateOfBirth"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtPhone.Text = row.Cells["PhoneNumber"].Value?.ToString();
            txtGender.Text = row.Cells["StudentGender"].Value?.ToString();
            txtAddress.Text = row.Cells["StudentAddress"].Value?.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
