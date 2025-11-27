using LTTQ_G2_2025.BLL;
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

namespace LTTQ_G2_2025.GUI
{
    public partial class fMilestone_Teacher : Form
    {
        private long _projectId;
        private long _teacherId;
        private TienDoBLL _tienDoBLL;
        private List<InforStage> _stages;
        private long _selectedMilestoneId;
        private readonly string _uploadFolder = @"E:\ThesisUploads";
        public fMilestone_Teacher(long projectId, long teacherId)
        {
            InitializeComponent();
            _projectId = projectId;
            _teacherId = teacherId;
            _tienDoBLL = new TienDoBLL(teacherId);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void fMilestone_Teacher_Load(object sender, EventArgs e)
        {
            LoadHeader();
            LoadTienDo();
        }
        private void LoadHeader()
        {
            DataRow dr = _tienDoBLL.GetProjectHeader(_projectId);
            if (dr == null) return;

            string projectName = dr["projectName"].ToString();
            string teacherName = dr["teacherName"].ToString();

            lblTitle.Text = $"{projectName} - GVHD: {teacherName}";
        }
        private void LoadTienDo()
        {
            _stages = _tienDoBLL.GetStagesByProject(_projectId);

            // Nếu không có gì
            if (_stages == null || _stages.Count == 0)
            {
                dgvDanhSachTienDo.DataSource = null;
                ClearDetail();
                return;
            }

            // Chỉ bind vài cột cần hiển thị
            dgvDanhSachTienDo.DataSource = _stages
                .Select(x => new
                {
                    x.StageId,
                    GiaiDoan = x.StageName,
                    x.MilestoneId,
                    TenTienDo = x.MilestoneName,
                    TrongSo = x.WeightPercent,
                    HanNop = x.DueDate,
                })
                .ToList();

            // Ẩn id nội bộ
            dgvDanhSachTienDo.Columns["StageId"].Visible = false;
            dgvDanhSachTienDo.Columns["MilestoneId"].Visible = false;

            dgvDanhSachTienDo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachTienDo.RowTemplate.Height = 35;
            dgvDanhSachTienDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSachTienDo.MultiSelect = false;
            dgvDanhSachTienDo.ReadOnly = true;
            dgvDanhSachTienDo.AllowUserToAddRows = false;

            dgvDanhSachTienDo.ClearSelection();
            dgvDanhSachTienDo.CurrentCell = null;

            ClearDetail();
        }
        private void ClearDetail()
        {
            lblTenTienDo.Text = string.Empty;
            txtNoiDungBaoCao.Text = string.Empty;
            lblTenFileBaoCao.Text = string.Empty;

            txtPercent.Text = string.Empty;
            txtNhanXetTienDo.Text = string.Empty;   // <- thêm
            txtDiem.Text = string.Empty;
            txtNhanXet.Text = string.Empty;

            // cho phép nhập lại khi chưa chọn mốc
            txtPercent.ReadOnly = false;
            txtNhanXetTienDo.ReadOnly = false;
            txtDiem.ReadOnly = false;
            txtNhanXet.ReadOnly = false;
            btnXacNhan.Enabled = true;
        }

        private InforStage GetCurrentStage()
        {
            if (dgvDanhSachTienDo.SelectedRows.Count == 0) return null;

            var row = dgvDanhSachTienDo.SelectedRows[0];
            if (row.Cells["MilestoneId"].Value == null) return null;

            long milestoneId = Convert.ToInt64(row.Cells["MilestoneId"].Value);
            return _stages.FirstOrDefault(s => s.MilestoneId == milestoneId);
        }
        private void dgvDanhSachTienDo_SelectionChanged(object sender, EventArgs e)
        {
            var item = GetCurrentStage();
            if (item == null)
            {
                ClearDetail();
                return;
            }

            lblTenTienDo.Text = item.MilestoneName ?? "(Chưa đặt tên mốc)";
            txtNoiDungBaoCao.Text = item.ProgressReportContent
                                    ?? "(Sinh viên chưa nộp nội dung báo cáo)";
            lblTenFileBaoCao.Text = string.IsNullOrEmpty(item.ProgressReportFile)
                ? "Chưa có file báo cáo"
                : item.ProgressReportFile;

            LoadEvaluationStateForCurrentStage(item);
        }
        private void LoadEvaluationStateForCurrentStage(InforStage item)
        {
            // Reset mặc định (cho phép nhập)
            txtPercent.ReadOnly = false;
            txtNhanXetTienDo.ReadOnly = false;
            txtDiem.ReadOnly = false;
            txtNhanXet.ReadOnly = false;
            btnXacNhan.Enabled = true;

            // Đặt sẵn dữ liệu từ InforStage (nếu có)
            txtPercent.Text = item.WeightPercent.HasValue
                ? item.WeightPercent.Value.ToString()
                : string.Empty;

            txtNhanXetTienDo.Text = item.MilestoneDescription ?? string.Empty;
            txtDiem.Text = string.Empty;
            txtNhanXet.Text = string.Empty;

            // Gọi BLL lấy thông tin đã chấm trong Evaluation (nếu có)
            var dr = _tienDoBLL.GetMilestoneEvaluation(_projectId, item.MilestoneId);
            if (dr == null)
                return; // chưa có gì -> cho nhập bình thường

            bool hasPercent = !(dr["weightPercent"] is DBNull);
            bool hasTienDoCmt = !(dr["milestoneDescription"] is DBNull);
            bool hasScore = !(dr["totalScore"] is DBNull);
            bool hasComment = !(dr["comment"] is DBNull);

            if (hasPercent)
                txtPercent.Text = dr["weightPercent"].ToString();

            if (hasTienDoCmt)
                txtNhanXetTienDo.Text = dr["milestoneDescription"].ToString();

            if (hasScore)
                txtDiem.Text = dr["totalScore"].ToString();

            if (hasComment)
                txtNhanXet.Text = dr["comment"].ToString();

            // Nếu milestone đã có bất kỳ thông tin nào -> chỉ xem
            if (hasPercent || hasTienDoCmt || hasScore || hasComment)
            {
                txtPercent.ReadOnly = true;
                txtNhanXetTienDo.ReadOnly = true;
                txtDiem.ReadOnly = true;
                txtNhanXet.ReadOnly = true;
                btnXacNhan.Enabled = false;
            }
        }

        private void btnTaiFile_Click(object sender, EventArgs e)
        {
            var item = GetCurrentStage();
            if (item == null)
            {
                MessageBox.Show("Vui lòng chọn một tiến độ ở danh sách bên trái!", "Thông báo");
                return;
            }

            if (string.IsNullOrEmpty(item.ProgressReportFile))
            {
                MessageBox.Show("Sinh viên chưa nộp file báo cáo!", "Thông báo");
                return;
            }

            // GHÉP ĐƯỜNG DẪN THẬT TỪ FOLDER + TÊN FILE LƯU TRONG DB
            string fullPath = Path.Combine(_uploadFolder, item.ProgressReportFile);

            if (!File.Exists(fullPath))
            {
                MessageBox.Show("Không tìm thấy file báo cáo tại:\n" + fullPath,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Mở bằng app mặc định (Word, PDF viewer...)
                var psi = new ProcessStartInfo
                {
                    FileName = fullPath,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được file báo cáo: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            var item = GetCurrentStage();
            if (item == null)
            {
                MessageBox.Show("Vui lòng chọn một mốc tiến độ để chấm điểm!", "Thông báo");
                return;
            }

            if (item.MilestoneId == 0)
            {
                MessageBox.Show("Không xác định được milestone!", "Lỗi");
                return;
            }

            // =============================
            // 1. Validate % tiến độ (0-100)
            // =============================
            if (!int.TryParse(txtPercent.Text.Trim(), out int percent))
            {
                MessageBox.Show("% tiến độ phải là số nguyên!", "Lỗi");
                return;
            }

            if (percent < 0 || percent > 100)
            {
                MessageBox.Show("% tiến độ phải nằm trong khoảng 0 - 100!", "Lỗi");
                return;
            }

            // =============================
            // 2. Validate điểm (0–10)
            // =============================
            if (!decimal.TryParse(txtDiem.Text.Trim(), out decimal score))
            {
                MessageBox.Show("Điểm không hợp lệ!", "Lỗi");
                return;
            }

            if (score < 0 || score > 10)
            {
                MessageBox.Show("Điểm phải nằm trong khoảng 0 - 10!", "Lỗi");
                return;
            }

            string comment = txtNhanXet.Text.Trim();

            // =============================
            // 3. Xác nhận trước khi lưu
            // =============================
            var rs = MessageBox.Show(
                "Bạn có chắc muốn lưu điểm và nhận xét cho mốc tiến độ này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (rs != DialogResult.Yes)
                return;

            // =============================
            // 4. Lưu vào DB
            // =============================
            _tienDoBLL.UpdateMilestoneProgress(item.MilestoneId, percent, txtNhanXetTienDo.Text.Trim());

            bool ok = _tienDoBLL.SaveEvaluation(_projectId, item.MilestoneId, score, comment);

            if (ok)
            {
                MessageBox.Show("Đã lưu điểm/nhận xét thành công!", "Thông báo");

                // ==========> 5. Reload lại grid + lock luôn <==========
                long currentMid = item.MilestoneId;

                // reload lại list + grid từ DB
                LoadTienDo();

                // tìm lại dòng có MilestoneId vừa chấm để chọn + hiển thị
                foreach (DataGridViewRow row in dgvDanhSachTienDo.Rows)
                {
                    if (row.Cells["MilestoneId"].Value != null &&
                        Convert.ToInt64(row.Cells["MilestoneId"].Value) == currentMid)
                    {
                        row.Selected = true;
                        dgvDanhSachTienDo.CurrentCell = row.Cells["TenTienDo"];
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Lưu điểm thất bại!", "Lỗi");
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtNoiDungBaoCao_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
