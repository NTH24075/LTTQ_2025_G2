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
using LTTQ_G2_2025.BLL;

namespace LTTQ_G2_2025.GUI
{
    public partial class fDetailAccount : Form
    {
        private readonly long _accountId;
        private readonly string _roleName;
        private readonly StudentBLL _studentBLL = new StudentBLL(); 
        private readonly TeacherBLL _teacherBLL = new TeacherBLL();
        public fDetailAccount(long accountId, string roleName)
        {
            InitializeComponent();
            _accountId = accountId;
            _roleName = roleName;
            this.StartPosition = FormStartPosition.CenterParent;

            // gán sự kiện
            this.Load += fDetailAccount_Load;
        }

        private void fDetailAccount_Load(object sender, EventArgs e)
        {
            LoadDetail();

        }
        private void LoadDetail()
        {
            if (_roleName == "ROLE_STUDENT")
            {
                var sv = _studentBLL.GetStudentByAccountId(_accountId);
                if (sv == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên.", "Lỗi");
                    return;
                }

                txtName.Text = sv.StudentName;
                txtMSV.Text = sv.StudentCode;
                txtBirth.Text = sv.DateOfBirth;
                txtEmail.Text = sv.Email;
                txtNumber.Text = sv.PhoneNumber;
                txtAddress.Text = sv.StudentAddress;
                txtFaculty.Text = sv.FacultyName;
                LoadImageToPictureBox(sv.Img);
            }
            else if (_roleName == "ROLE_TEACHER")
            {
                var tc = _teacherBLL.ViewDetailDTO(_accountId);
                if (tc == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên.", "Lỗi");
                    return;
                }

                txtName.Text = tc.TeacherName;
                txtMSV.Text = tc.TeacherCode;
                txtBirth.Text = tc.DateOfBirth;
                txtEmail.Text = tc.Email;
                txtNumber.Text = tc.PhoneNumber;
                txtAddress.Text = tc.TeacherAddress;
                txtFaculty.Text = tc.FacultyName;
                LoadImageToPictureBox(tc.Img);
            }
            else if (_roleName == "ROLE_ADMIN")
            {
                // TODO: adminBLL.GetAdminByAccountId(...)
            }
        }
        private void LoadImageToPictureBox(string imgPath)
        {
            try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                if (string.IsNullOrWhiteSpace(imgPath))
                    return;

                if (File.Exists(imgPath))
                {
                    pictureBox1.Image = Image.FromFile(imgPath);
                    return;
                }

                string combined = Path.Combine(Application.StartupPath, imgPath);
                if (File.Exists(combined))
                {
                    pictureBox1.Image = Image.FromFile(combined);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được ảnh: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
