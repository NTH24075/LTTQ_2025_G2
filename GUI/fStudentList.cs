using LTTQ_G2_2025.BLL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTTQ_G2_2025.GUI
{
    public partial class fStudentList : Form
    {
        private long _accountId;
        private string _imageFolder = Path.Combine(Application.StartupPath, "Images", "Students");
        private List<StudentDTO> _teamMembers;

        public fStudentList(long accountId)
        {
            InitializeComponent();
            _accountId = accountId;
        }
        private void fStudentList_Load(object sender, EventArgs e)
        {
            LoadTeamMembers();
        }

        private void LoadTeamMembers()
        {
            var data = StudentBLL.Instance.GetTeamMembersByAccountId(_accountId);

            var view = data.Select(s => new
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
            _teamMembers = StudentBLL.Instance.GetTeamMembersByAccountId(_accountId);

            if (e.RowIndex < 0) return;
            var student = _teamMembers[e.RowIndex];

            // Lấy tên file ảnh từ DB
            string fileName = student.Img;

            // Ghép thành đường dẫn đầy đủ
            string imgPath = Path.Combine(_imageFolder, fileName);

            // Load ảnh
            if (File.Exists(imgPath))
            {
                picAvatar.Image = Image.FromFile(imgPath);
            }
            else
            {
                picAvatar.Image = null;  // hoặc ảnh mặc định
            }
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
