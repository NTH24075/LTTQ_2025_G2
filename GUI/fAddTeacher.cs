using LTTQ_G2_2025.BLL;
using LTTQ_G2_2025.DAL;
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
    public partial class AddTeacher : Form
    {
        private readonly FacultyDAL _facultyBLL = new FacultyDAL();
        private readonly DegreeDAL _degreeBLL = new DegreeDAL();
        private readonly TeacherBLL _teacherBLL = new TeacherBLL();
        private string _selectedImagePath = "";
        private string _storedImgPath = null;
        public AddTeacher()
        {
            InitializeComponent();
            this.btCancel.Click += (s, e) => this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddTeacher_Load(object sender, EventArgs e)
        {
            LoadFaculties();
            LoadDegrees();

            // Mặc định chọn Nam
            rdbMale.Checked = true;

            // Định dạng ngày sinh
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }
        private void LoadFaculties()
        {
            var dt = _facultyBLL.GetAll();
            cbFaculty.DataSource = dt;
            cbFaculty.DisplayMember = "facultyName";
            cbFaculty.ValueMember = "faculty_id";
            cbFaculty.SelectedIndex = dt.Rows.Count > 0 ? 0 : -1;
        }
        private void LoadDegrees()
        {
            var dt = _degreeBLL.GetAll();
            cbDegree.DataSource = dt;
            cbDegree.DisplayMember = "degreeName";
            cbDegree.ValueMember = "degree_id";
            cbDegree.SelectedIndex = dt.Rows.Count > 0 ? 0 : -1;
        }

        private void buttonSelectImage_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _selectedImagePath = ofd.FileName;
                    MessageBox.Show("Đã chọn ảnh: " + Path.GetFileName(_selectedImagePath));
                }
            }
        }

        private void btAcp_Click(object sender, EventArgs e)
        {
            int? facultyId = null;
            int? degreeId = null;

            if (cbFaculty.SelectedValue != null && int.TryParse(cbFaculty.SelectedValue.ToString(), out int fId))
                facultyId = fId;

            if (cbDegree.SelectedValue != null && int.TryParse(cbDegree.SelectedValue.ToString(), out int dId))
                degreeId = dId;

            // Giới tính
            bool isMale = rdbMale.Checked;

            // Ngày sinh: DB bạn đang để VARCHAR(50), ta để format yyyy-MM-dd
            string dob = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            var dto = new TeacherDTO
            {
                TeacherCode = txtMSV.Text.Trim(),
                TeacherName = textBox1.Text.Trim(),
                DateOfBirth = dob,
                Email = textBox2.Text.Trim(),
                PhoneNumber = textBox3.Text.Trim(),
                TeacherGender = isMale,
                TeacherAddress = textBox4.Text.Trim(),
                Img = _selectedImagePath, // lưu đường dẫn local hoặc tương đối như bạn đã chọn
                FlagDelete = false,
                FacultyId = facultyId,
                DegreeId = degreeId,
                AccountId = null // sẽ gán trong BLL sau khi tạo Account
            };

            // Validate đơn giản
            if (string.IsNullOrWhiteSpace(dto.TeacherCode) ||
                string.IsNullOrWhiteSpace(dto.TeacherName) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                MessageBox.Show("Mã, Tên và Email là bắt buộc.");
                return;
            }

            // Gọi BLL: tạo Account(email, 12345678) + gán ROLE_TEACHER + insert Teacher
            if (_teacherBLL.AddTeacherWithAutoAccount(dto, out string error))
            {
                MessageBox.Show("Thêm giảng viên + tạo tài khoản thành công! Mật khẩu mặc định: 12345678");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm thất bại: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
