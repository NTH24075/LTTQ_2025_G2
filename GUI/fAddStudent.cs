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
    public partial class fAddStudent : Form
    {
        private readonly StudentBLL _studentBLL = new StudentBLL();
        private readonly FacultyDAL _facultyBLL = new FacultyDAL();

        private string _selectedImageFullPath = null;     // đường dẫn ảnh người dùng chọn (absolute)
        private string _imageRelativePath = null;
        public fAddStudent()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }


        private void fAddStudent_Load(object sender, EventArgs e)
        {
            try
            {
                var faculties = _facultyBLL.GetAllFaculties(); // List<(int Id, string Name)>
                cbFaculty.DataSource = faculties;
                cbFaculty.DisplayMember = "Name";
                cbFaculty.ValueMember = "Id";
                cbFaculty.SelectedIndex = faculties.Count > 0 ? 0 : -1;
            }
            catch
            {
                // Không bắt buộc phải có Faculty khi thêm SV
            }

            rdbMale.Checked = true; // default
            buttonSelectImage.Text = "Chọn ảnh...";
            btAcp.Text = "Thêm";
        }

        private void buttonSelectImage_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh sinh viên";
                ofd.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp|All files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _selectedImageFullPath = ofd.FileName;

                    // Copy ảnh vào thư mục chuẩn của app (Images\Students)
                    try
                    {
                        string imagesDir = Path.Combine(Application.StartupPath, "Images", "Students");
                        Directory.CreateDirectory(imagesDir);

                        string fileName = Path.GetFileName(ofd.FileName);
                        string destFull = Path.Combine(imagesDir, fileName);

                        // Nếu trùng tên, tạo tên mới
                        destFull = MakeUniqueIfExists(destFull);

                        File.Copy(ofd.FileName, destFull, false);

                        // Lưu relative path để insert DB
                        _imageRelativePath = GetRelativePath(destFull, Application.StartupPath);
                        MessageBox.Show("Đã chọn ảnh: " + _imageRelativePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể lưu ảnh: " + ex.Message);
                        _imageRelativePath = null;
                    }
                }
            }
        }
        private string MakeUniqueIfExists(string fullPath)
        {
            if (!File.Exists(fullPath)) return fullPath;
            string dir = Path.GetDirectoryName(fullPath);
            string name = Path.GetFileNameWithoutExtension(fullPath);
            string ext = Path.GetExtension(fullPath);
            int i = 1;
            string cand;
            do
            {
                cand = Path.Combine(dir, $"{name}_{i}{ext}");
                i++;
            } while (File.Exists(cand));
            return cand;
        }

        private string GetRelativePath(string filespec, string folder)
        {
            Uri pathUri = new Uri(filespec);
            // Ensure folder ends with a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                folder += Path.DirectorySeparatorChar;
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

        private void btAcp_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Lấy dữ liệu từ controls
                string code = txtMSV.Text.Trim();
                string name = txtName.Text.Trim();
                string dob = dateTimePicker1.Value.ToString("yyyy-MM-dd"); // DB bạn đang để VARCHAR(50)
                string email = txtEmail.Text.Trim();
                string phone = txtPhoneNumber.Text.Trim();
                string addr = txtAddress.Text.Trim();
                string className = txtClass.Text.Trim(); // bạn thiết kế là TextBox

                bool gender = rdbMale.Checked; // true = Nam, false = Nữ

                // 2) Validate cơ bản
                if (string.IsNullOrWhiteSpace(code)) { MessageBox.Show("Vui lòng nhập Mã sinh viên."); return; }
                if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Vui lòng nhập Tên sinh viên."); return; }
                if (string.IsNullOrWhiteSpace(email)) { MessageBox.Show("Vui lòng nhập Email."); return; }
                if (string.IsNullOrWhiteSpace(phone)) { MessageBox.Show("Vui lòng nhập SĐT."); return; }

                // 3) Gọi BLL để thêm Account + Student
                long newId = _studentBLL.AddStudentWithAccount(
                    studentCode: code,
                    studentName: name,
                    dateOfBirth: dob,
                    gender: gender,
                    email: email,
                    phone: phone,
                    address: addr,
                    imageRelativePath: _imageRelativePath,   // có thể null
                    className: className                     // có thể rỗng -> không gán lớp
                );

                MessageBox.Show("Thêm sinh viên thành công. ID = " + newId);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm sinh viên: " + ex.Message);
            }
        }
    }

}
