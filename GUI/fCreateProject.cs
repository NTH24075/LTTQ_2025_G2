using LTTQ_G2_2025.BLL;
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
using System.Xml.Linq;

namespace LTTQ_G2_2025.GUI
{
    public partial class fCreateProject : Form
    {


        private List<TeacherDTO> _teachers;
        private readonly long _teamId;
        private readonly ProjectBLL _bll;

        public fCreateProject(long teamId, ProjectBLL bll)
        {
            InitializeComponent();
            _teamId = teamId;
            _bll = bll;
        }
        private void fCreateProject_Load(object sender, EventArgs e)
        {
            LoadTeachers();
            this.BackColor = Color.FromArgb(135, 206, 235);
            dgvTeacher.BackgroundColor = Color.White;
        }
        private void LoadTeachers()
        {
            try
            {
                _teachers = ShowTeacherBLL.Instance.GetAllTeachers();

                // Chỉ lấy các cột cần hiển thị
                var viewData = _teachers.Select(t => new
                {
                    Teacher_Name = t.TeacherName,
                    Date_Of_Birth = t.DateOfBirth,
                    Email = t.Email,
                    Phone_Number = t.PhoneNumber,
                    Gender = t.TeacherGender ? "Nam" : "Nữ",
                    Address = t.TeacherAddress
                }).ToList();

                dgvTeacher.DataSource = viewData;

                // Giãn đầy DataGridView
                dgvTeacher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Đặt HeaderText thân thiện
                dgvTeacher.Columns["Teacher_Name"].HeaderText = "Teacher Name";
                dgvTeacher.Columns["Date_Of_Birth"].HeaderText = "Date of Birth";
                dgvTeacher.Columns["Email"].HeaderText = "Email";
                dgvTeacher.Columns["Phone_Number"].HeaderText = "Phone Number";
                dgvTeacher.Columns["Gender"].HeaderText = "Gender";
                dgvTeacher.Columns["Address"].HeaderText = "Address";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teachers: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // --- VALIDATE TEACHER ID ---
            if (lblTeacherName.Text == "...")
            {
                MessageBox.Show("Vui lòng chọn một giảng viên bằng cách nhấn vào giảng viên đó!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- LẤY DỮ LIỆU ---
            long teacherId = long.Parse(lblTeacherId.Text);
            string name = txtName.Text.Trim();
            string content = txtContent.Text.Trim();
            string img = txtImg.Text.Trim();
            string desc = txtDescription.Text.Trim();
            bool status = false;
            int? semId = null; // nếu chưa dùng semester_id

            // --- VALIDATE CÁC Ô KHÔNG ĐƯỢC ĐỂ TRỐNG ---
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên đề tài không được để trống.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Nội dung đề tài không được để trống.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("Mô tả đề tài không được để trống.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nếu bạn bắt buộc phải có đường dẫn ảnh
            if (string.IsNullOrWhiteSpace(img))
            {
                MessageBox.Show("Vui lòng chọn ảnh đề tài.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- TẠO PROJECT ---
            bool ok = _bll.CreateProjectForTeam(_teamId, name, content, img, desc, status, semId, teacherId);
            if (ok)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Tạo đề tài thất bại.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void dgvTeacher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var teacher = _teachers[e.RowIndex];

            lblEmail.Text = teacher.Email;
            lblTeacherName.Text = teacher.TeacherName;
            lblTeacherId.Text = teacher.TeacherId.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtImg_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog() { Filter = "Images|*.png;*.jpg;*.jpeg;*.gif" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtImg.Text = ofd.FileName;
                }
            }
        }
    }
}
