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
        private readonly long _currentAccountId;
        private readonly ProjectBLL _projectBll = new ProjectBLL();
        public fStudentList(long accountId)
        {
            InitializeComponent();
            _accountId = accountId;
            _currentAccountId = accountId;
        }
        private void fStudentList_Load(object sender, EventArgs e)
        {
            LoadTeamMembers();
            this.BackColor = Color.FromArgb(135, 206, 235);
            dgvStudent.BackgroundColor = Color.White;
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
            SetPictureNoLock(picAvatar, imgPath);

            DataGridViewRow row = dgvStudent.Rows[e.RowIndex];

            txtName.Text = row.Cells["StudentName"].Value?.ToString();
            txtCode.Text = row.Cells["StudentCode"].Value?.ToString();
            txtDOB.Text = row.Cells["DateOfBirth"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtPhone.Text = row.Cells["PhoneNumber"].Value?.ToString();
            txtGender.Text = row.Cells["StudentGender"].Value?.ToString();
            txtAddress.Text = row.Cells["StudentAddress"].Value?.ToString();
        }
        private void SetPictureNoLock(PictureBox pb, string path)
        {
            // Giải phóng ảnh cũ nếu đang giữ
            if (pb.Image != null)
            {
                var old = pb.Image;
                pb.Image = null;
                old.Dispose();
            }

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                pb.Image = null; // hoặc ảnh mặc định
                return;
            }

            // Load mà không khóa file gốc:
            // Mở stream -> tạo Bitmap clone -> đóng stream
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var img = Image.FromStream(fs))
            {
                pb.Image = new Bitmap(img);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ViewProject_Click(object sender, EventArgs e)
        {
            // 1) Thử lấy Project của SV
            ProjectDetailDTO p = _projectBll.GetProjectDetail(_currentAccountId);

            if (p == null)
            {
                // 2) Chưa có Project (có thể SV chưa có team hoặc team chưa gán project)
                long? teamId = _projectBll.GetTeamIdOfStudent(_currentAccountId);
                if (teamId == null)
                {
                    MessageBox.Show("Bạn chưa thuộc nhóm (team). Hãy liên hệ GV/Quản trị để được thêm vào nhóm trước khi tạo đề tài.",
                                    "Chưa có nhóm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3) Hỏi có muốn tạo Project mới
                var ask = MessageBox.Show("Bạn chưa có đồ án. Bạn có muốn tạo đề tài (Project) mới cho nhóm không?",
                                          "Tạo Project", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ask == DialogResult.Yes)
                {
                    // Mở form tạo Project (điền minimal info)
                    using (var f = new fCreateProject((long)teamId, _projectBll))
                    {
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show("Tạo đề tài thành công.");
                        }
                    }
                }
                return;
            }

            // 4) Đã có Project → mở form xem chi tiết
            using (var f = new fProjectDetail(p))
            {
                f.ShowDialog();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            fLogin loginForm = new fLogin();
            loginForm.Show();
            this.Close();
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            GroupBox gb = (GroupBox)sender;
            e.Graphics.FillRectangle(new SolidBrush(Color.White),
                             0, 10, gb.Width, gb.Height - 10);
        }
    }
}
