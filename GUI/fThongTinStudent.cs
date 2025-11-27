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
    public partial class fThongTinStudent : Form
    {
        private readonly long _accountId;
        private readonly StudentBLL _studentBLL = new StudentBLL();
        private long _studentId = 0;
        private string _currentImgPath = null;
        public fThongTinStudent()
        {
            InitializeComponent();
        }
        public fThongTinStudent(long accountId)
        {
            InitializeComponent();
            _accountId = accountId;

            StartPosition = FormStartPosition.CenterParent;

        }

        private void fThongTinStudent_Load(object sender, EventArgs e)
        {
            LoadDetail();
            txtFaculty.ReadOnly = true;
        }
        private void LoadDetail()
        {
            var sv = _studentBLL.GetStudentDetailByAccountId(_accountId); // dùng hàm bạn đã có
            if (sv == null)
            {
                MessageBox.Show("Không tìm thấy thông tin sinh viên.", "Thông báo");
                return;
            }

            _studentId = sv.StudentId;
            txtName.Text = sv.StudentName;
            txtMSV.Text = sv.StudentCode;
            txtBirth.Text = sv.DateOfBirth;            // bạn đang lưu DOB dạng string -> giữ nguyên
            txtEmail.Text = sv.Email;
            txtNumber.Text = sv.PhoneNumber;
            txtAddress.Text = sv.StudentAddress;
            txtFaculty.Text = sv.FacultyName ?? "";

            _currentImgPath = sv.Img;
            LoadImageToPictureBox(_currentImgPath);
        }
        private void LoadImageToPictureBox(string imgPath)
        {
            try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = null;
                if (string.IsNullOrWhiteSpace(imgPath)) return;

                // 1) nếu là đường dẫn tuyệt đối và tồn tại
                if (File.Exists(imgPath))
                {
                    using (var fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(fs);
                    }
                    return;
                }

                // 2) thử combine tương đối cùng thư mục app
                var combined = Path.Combine(Application.StartupPath, imgPath);
                if (File.Exists(combined))
                {
                    using (var fs = new FileStream(combined, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được ảnh: " + ex.Message);
            }
        }

       

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()
            {
                Filter = "Image files|*.png;*.jpg;*.jpeg;*.gif;*.bmp",
                Title = "Chọn ảnh đại diện"
            })
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    _currentImgPath = ofd.FileName; // lưu tạm đường dẫn
                    LoadImageToPictureBox(_currentImgPath);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_studentId <= 0)
            {
                MessageBox.Show("Không xác định được StudentId.", "Lỗi");
                return;
            }

            // Validate đơn giản
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Họ tên không được để trống.", "Lỗi");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMSV.Text))
            {
                MessageBox.Show("Mã sinh viên không được để trống.", "Lỗi");
                return;
            }

            // Quy ước lưu ảnh:
            // - Nếu là đường dẫn tuyệt đối ở máy, ta copy ảnh vào thư mục /GUI/Resource/ và lưu CSDL đường dẫn tương đối "GUI\\Resource\\file.png"
            string imgToSave = _currentImgPath;
            try
            {
                if (!string.IsNullOrWhiteSpace(_currentImgPath) && File.Exists(_currentImgPath))
                {
                    var resDir = Path.Combine(Application.StartupPath, "GUI", "Resource");
                    if (!Directory.Exists(resDir)) Directory.CreateDirectory(resDir);

                    var fileName = Path.GetFileName(_currentImgPath);
                    var dest = Path.Combine(resDir, fileName);

                    // nếu khác file đích thì copy
                    if (!string.Equals(Path.GetFullPath(_currentImgPath), Path.GetFullPath(dest), StringComparison.OrdinalIgnoreCase))
                    {
                        File.Copy(_currentImgPath, dest, true);
                    }
                    // Lưu tương đối để khi build máy khác vẫn đọc được
                    imgToSave = Path.Combine("GUI", "Resource", fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xử lý ảnh: " + ex.Message, "Lỗi");
                return;
            }

            var dto = new StudentDTO
            {
                StudentId = _studentId,
                StudentName = txtName.Text.Trim(),
                StudentCode = txtMSV.Text.Trim(),
                DateOfBirth = txtBirth.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                PhoneNumber = txtNumber.Text.Trim(),
                StudentAddress = txtAddress.Text.Trim(),
                Img = imgToSave
                // Các cột khác (Gender, FlagDelete, ClassId, TeamId, …) không có control -> giữ nguyên (DAL sẽ không cập nhật chúng)
            };

            bool ok = _studentBLL.UpdateStudentBasic(dto);
            if (ok)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.", "Thông báo");
            }
        }
    }
}
