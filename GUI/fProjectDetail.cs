using LTTQ_G2_2025.BLL;
using LTTQ_G2_2025.DAL;
using LTTQ_G2_2025.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO;
using System.Windows.Forms;
using LTTQ_G2_2025.BLL;
using LTTQ_G2_2025.DTO;
using System.Diagnostics;

namespace LTTQ_G2_2025.GUI
{
    public partial class fProjectDetail : Form
    {
        private readonly ProjectDetailDTO _p;
        private long _projectId;
        private InforStageBLL stageBLL = new InforStageBLL();
        private string selectedFilePath = string.Empty; // Đường dẫn file gốc người dùng chọn

        // Định nghĩa thư mục lưu trữ file upload
        private readonly string UploadFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
        public fProjectDetail(ProjectDetailDTO p)
        {
            InitializeComponent();
            _p = p;
            _projectId = p.ProjectId;
        }

        private void fProjectDetail_Load(object sender, EventArgs e)
        {
            labelName.Text = _p.Name;
            labelContent.Text = _p.Content;
            labelDescription.Text = _p.Description;
            //label6.Text = _p.SemesterId.ToString();
            labelTeamName.Text = _p.TeamName;
            labelTeacherName.Text = _p.TeacherName;
            labelSemesterName.Text = _p.SemesterName;
            LoadStages();
            this.BackColor = Color.FromArgb(135, 206, 235);
            dgvStage.BackgroundColor = Color.White;
        }
        private void LoadStages()
        {
            InforStageBLL stageBLL = new InforStageBLL();
            try
            {
                var stages = stageBLL.GetStagesForDisplay(_projectId);

                // Chỉ lấy các cột muốn hiển thị
                var viewData = stages.Select(x => new
                {
                    Stage_Name = x.StageName,
                    Report_Content = x.ProgressReportContent,
                    Milestone_Name = x.MilestoneName,
                    Report_File = x.ProgressReportFile,
                    Milestone_Description = x.MilestoneDescription,
                    Weight_Percent = x.WeightPercent
                }).ToList();

                dgvStage.DataSource = viewData;

                // Giãn full width DataGridView
                dgvStage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Đặt HeaderText rõ ràng
                dgvStage.Columns["Stage_Name"].HeaderText = "Stage Name";
                
                dgvStage.Columns["Report_Content"].HeaderText = "Report Content";
                dgvStage.Columns["Milestone_Name"].HeaderText = "Milestone";
                dgvStage.Columns["Report_File"].HeaderText = "File";
                dgvStage.Columns["Milestone_Description"].HeaderText = "Description";
                dgvStage.Columns["Weight_Percent"].HeaderText = "Weight (%)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
                ViewNameFile.Text = Path.GetFileName(selectedFilePath);
                //MessageBox.Show("File selected: " + Path.GetFileName(selectedFilePath));
                // Bạn có thể hiển thị tên file trong một TextBox khác nếu muốn
            }
        }

        // Xử lý nút Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            // *** ĐÃ THÊM: Kiểm tra validation trước khi xử lý ***
            // Giả định tên controls là txtStageName, txtMilestoneName, txtDescription
            if (string.IsNullOrEmpty(txtStageName.Text) || string.IsNullOrEmpty(txtMilestoneName.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên giai đoạn và Tên mốc quan trọng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng xử lý
            }

            string fileNameInDb = null;

            // --- Logic Xử lý Upload File Vật Lý ---
            if (!string.IsNullOrEmpty(selectedFilePath) && File.Exists(selectedFilePath))
            {
                try
                {
                    if (!Directory.Exists(UploadFolder))
                    {
                        Directory.CreateDirectory(UploadFolder);
                    }

                    fileNameInDb = Path.GetFileName(selectedFilePath);
                    string destinationPath = Path.Combine(UploadFolder, fileNameInDb);

                    File.Copy(selectedFilePath, destinationPath, true);
                    ViewNameFile.Text = fileNameInDb;
                    //MessageBox.Show($"File đã được sao chép tới: {destinationPath}");
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Lỗi khi sao chép file: " + ex.Message);
                    return;
                }
            }
            // ----------------------------------------

            InforStage newStage = new InforStage
            {
                ProjectId = _projectId, // Sử dụng project ID hiện tại
                StageName = txtStageName.Text,
                MilestoneName = txtMilestoneName.Text,
                ProgressReportContent = txtDescription.Text, // Giả định TextBox tên txtDescription
                ProgressReportFile = fileNameInDb,
                // ... (các thuộc tính khác nếu có controls nhập liệu) ...
            };

            if (stageBLL.SaveNewStage(newStage))
            {
                MessageBox.Show("Lưu giai đoạn và file thành công!");
                LoadStages(); // Gọi lại LoadStages (đã đổi tên từ LoadStagesData)
                // Xóa các TextBox sau khi lưu thành công nếu cần
                txtStageName.Clear();
                txtMilestoneName.Clear();
                txtDescription.Clear();
                selectedFilePath = string.Empty;
            }
            else
            {
                // Nếu BLL trả về false do lỗi DB hoặc validation (nếu chưa chặn ở trên)
                MessageBox.Show("Lưu thất bại. Vui lòng kiểm tra log lỗi chi tiết.");
            }
        }


        
        

        private void dgvStage_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải cột chứa tên file và bấm vào ô không rỗng
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStage.Rows[e.RowIndex];
                string fileName = row.Cells["Report_File"].Value?.ToString();

                if (!string.IsNullOrEmpty(fileName))
                {
                    string fullPath = Path.Combine(UploadFolder, fileName);

                    if (File.Exists(fullPath))
                    {
                        try
                        {
                            // Mở file bằng ứng dụng mặc định của hệ thống
                            Process.Start(fullPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không thể mở file: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tệp tin không tồn tại tại đường dẫn: " + fullPath);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
