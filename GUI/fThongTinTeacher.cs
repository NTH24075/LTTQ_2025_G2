using LTTQ_G2_2025.BLL;
using LTTQ_G2_2025.DAL;
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
    public partial class fThongTinTeacher : Form
    {
        private readonly long _teacherId;
        private readonly TeacherBLL _teacherBLL = new TeacherBLL();
        private readonly DegreeDAL _degreeBLL = new DegreeDAL();
        private readonly FacultyDAL _facultyBLL = new FacultyDAL();

        public fThongTinTeacher(long teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Load += fThongTinTeacher_Load;

            this.button2.Click += button2_Update_Click; 
            this.button1.Click += button1_Close_Click; 
        }

        private void fThongTinTeacher_Load(object sender, EventArgs e)
        {
            var dtDegree = _degreeBLL.GetAll();
            comboBox1.DataSource = dtDegree;
            comboBox1.DisplayMember = "degreeName";
            comboBox1.ValueMember = "degree_id";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            var t = _teacherBLL.GetTeacherById(_teacherId);
            if (t == null)
            {
                MessageBox.Show("Không tìm thấy giảng viên.", "Lỗi");
                this.Close();
                return;
            }

            txtName.Text = t.TeacherName;
            txtMSV.Text = t.TeacherCode;
            txtBirth.Text = t.DateOfBirth;
            txtEmail.Text = t.Email;
            txtNumber.Text = t.PhoneNumber;
            txtAddress.Text = t.TeacherAddress;

            txtFaculty.ReadOnly = true;
            txtFaculty.Text = _facultyBLL.GetNameById(t.FacultyId);

            if (t.DegreeId.HasValue)
                comboBox1.SelectedValue = t.DegreeId.Value;
            else
                comboBox1.SelectedIndex = -1;

            TryLoadImage(t.Img);
        }

        private void TryLoadImage(string path)
        {
            try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                if (string.IsNullOrWhiteSpace(path)) return;
                if (File.Exists(path))
                {
                    pictureBox1.Image = System.Drawing.Image.FromFile(path);
                    return;
                }
                var local = Path.Combine(Application.StartupPath, path);
                if (File.Exists(local))
                {
                    pictureBox1.Image = System.Drawing.Image.FromFile(local);
                }
            }
            catch { /* ignore */ }
        }

        private void button2_Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Tên không được trống.", "Cảnh báo");
                return;
            }

            var dto = _teacherBLL.GetTeacherById(_teacherId);
            if (dto == null)
            {
                MessageBox.Show("Không tìm thấy giảng viên.", "Lỗi");
                return;
            }

            dto.TeacherName = txtName.Text.Trim();
            dto.TeacherCode = txtMSV.Text.Trim();
            dto.DateOfBirth = txtBirth.Text.Trim();
            dto.Email = txtEmail.Text.Trim();
            dto.PhoneNumber = txtNumber.Text.Trim();
            dto.TeacherAddress = txtAddress.Text.Trim();

            // Học vị lấy từ comboBox1
            int? degreeId = null;
            if (comboBox1.SelectedValue != null &&
                int.TryParse(comboBox1.SelectedValue.ToString(), out int deg))
            {
                degreeId = deg;
            }
            dto.DegreeId = degreeId;

            // Update
            bool ok = _teacherBLL.UpdateBasicInfo(dto);
            if (ok)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.", "Lỗi");
            }
        }

        private void button1_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
