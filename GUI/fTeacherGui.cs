using LTTQ_G2_2025.BLL;
using LTTQ_G2_2025.DAL;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace LTTQ_G2_2025.GUI
{
    public partial class fTeacherGui : Form
    {
        TeacherBLL teacherBLL = new TeacherBLL();
        private TienDoBLL tienDoBLL;
        private long _teacherId;
        private DiemBLL _diemBLL;
        public fTeacherGui(long teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
            teacherBLL = new TeacherBLL(teacherId);
            tienDoBLL = new TienDoBLL(teacherId);
            _diemBLL = new DiemBLL(teacherId);
        }
        private void LoadTrangThaiComboBox()
        {
            var list = new List<dynamic>()
    {
        new { Text = "Tất cả", Value = 3 },
        new { Text = "Chưa duyệt", Value = 0 },
        new { Text = "Đã duyệt", Value = 1 },
        new { Text = "Bị hủy", Value = 2 },
    };

            cboTrangThai.DataSource = list;
            cboTrangThai.DisplayMember = "Text";
            cboTrangThai.ValueMember = "Value";
        }
        private void LoadDanhSachDoAn()
        {
            dgvDanhSachDoAn.DataSource = teacherBLL.GetDanhSachDoAn();

            // chỉnh giao diện DataGridView cho đẹp
            dgvDanhSachDoAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachDoAn.RowTemplate.Height = 35;
            dgvDanhSachDoAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSachDoAn.MultiSelect = false;
            dgvDanhSachDoAn.ReadOnly = true;
            dgvDanhSachDoAn.AllowUserToAddRows = false;
            dgvDanhSachDoAn.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
        }
        private void LoadDanhSachDoAnTheoTrangThai(int status)
        {
            dgvDanhSachDoAn.DataSource = teacherBLL.GetProjectsByStatus(status);

            dgvDanhSachDoAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachDoAn.RowTemplate.Height = 35;
            dgvDanhSachDoAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSachDoAn.MultiSelect = false;
            dgvDanhSachDoAn.ReadOnly = true;
            dgvDanhSachDoAn.AllowUserToAddRows = false;
            dgvDanhSachDoAn.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
        }

        private void fTeacherGui_Load(object sender, EventArgs e)
        {
            LoadTrangThaiComboBox();
            LoadDanhSachDoAnTheoTrangThai(3);
            LoadDanhSachDoAn_TienDo();
            LoadTrangThaiComboBox_Diem();
            LoadDanhSachDoAn_Diem();
        }
        // Lấy projectId của đồ án được chọn trong DataGridView
        private long GetSelectedProjectId()
        {
            if (dgvDanhSachDoAn.SelectedRows.Count > 0)
            {
                return Convert.ToInt64(
                    dgvDanhSachDoAn.SelectedRows[0].Cells["Mã đồ án"].Value
                );
            }
            return -1;
        }
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            long id = GetSelectedProjectId();

            if (id == -1)
            {
                MessageBox.Show("Vui lòng chọn 1 đồ án!", "Thông báo");
                return;
            }

            fProjectDetail_Teacher f = new fProjectDetail_Teacher(id);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();

            // Reload lại danh sách sau khi duyệt / hủy
            LoadDanhSachDoAnTheoTrangThai(Convert.ToInt32(cboTrangThai.SelectedValue));
            LoadDanhSachDoAn_TienDo();
            dgvDanhSachDoAn.ClearSelection();
            dgvDanhSachDoAn.CurrentCell = null;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ten = txtTenDoAn.Text.Trim();
            int status = Convert.ToInt32(cboTrangThai.SelectedValue);

            dgvDanhSachDoAn.DataSource = teacherBLL.SearchProjects(ten, status);
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xóa text tìm kiếm
            txtTenDoAn.Text = "";

            // Đưa trạng thái về "Tất cả"
            cboTrangThai.SelectedValue = 3;

            // Load lại danh sách đầy đủ
            LoadDanhSachDoAnTheoTrangThai(3);
        }
        private void ExportExcel(DataGridView dgv)
        {
            try
            {
                // Bước 1: Tạo ứng dụng Excel
                Excel.Application exApp = new Excel.Application();
                Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                // Bước 2: Đặt tên cho sheet
                exSheet.Name = "DanhSachDoAn";

                // ======= TIÊU ĐỀ =======
                Excel.Range title = exSheet.get_Range("A1", "E1");
                title.Merge(true);
                title.Value = "DANH SÁCH ĐỒ ÁN";
                title.Font.Size = 18;
                title.Font.Bold = true;
                title.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                // ======= HEADER CỘT =======
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    exSheet.Cells[3, i + 1] = dgv.Columns[i].HeaderText;
                }

                Excel.Range header = exSheet.get_Range("A3", GetExcelColumnName(dgv.Columns.Count) + "3");
                header.Font.Bold = true;
                header.Interior.Color = Color.LightGray;
                header.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                // ======= ĐỔ DỮ LIỆU =======
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        exSheet.Cells[i + 4, j + 1] = dgv.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // AutoFit độ rộng các cột
                exSheet.Columns.AutoFit();

                // Bước 6: Lưu file qua SaveFileDialog
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Excel File|*.xlsx";
                save.Title = "Lưu file Excel";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    exBook.SaveAs(save.FileName);
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo");
                    System.Diagnostics.Process.Start(save.FileName);
                }

                // Bước 7: Thoát Excel
                exBook.Close();
                exApp.Quit();

                ReleaseObject(exSheet);
                ReleaseObject(exBook);
                ReleaseObject(exApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }
        private void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";
            while (columnNumber > 0)
            {
                int mod = (columnNumber - 1) % 26;
                columnName = Convert.ToChar(65 + mod) + columnName;
                columnNumber = (columnNumber - mod) / 26;
            }
            return columnName;
        }
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            ExportExcel(dgvDanhSachDoAn);
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Hỏi lại cho chắc
            var confirm = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            // Mở lại form đăng nhập
            fLogin f = new fLogin();

            // Khi form login đóng thì đóng luôn form hiện tại (tránh app chạy ngầm)
            f.FormClosed += (s, args) => this.Close();

            f.Show();

            // Ẩn form giảng viên
            this.Hide();
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvDanhSachDoAn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tabTienDo_Click(object sender, EventArgs e)
        {

        }
        //
        private void LoadDanhSachDoAn_TienDo()
        {
            dgvTienDo.DataSource = tienDoBLL.GetApprovedProjects();

            // Tuỳ bạn, mình format giống dgvDanhSachDoAn cho đẹp
            dgvTienDo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTienDo.RowTemplate.Height = 35;
            dgvTienDo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTienDo.MultiSelect = false;
            dgvTienDo.ReadOnly = true;
            dgvTienDo.AllowUserToAddRows = false;
            dgvTienDo.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
        }
        private void btnTimKiem_TienDo_Click(object sender, EventArgs e)
        {
            string keyword = txtTenDoAn_TienDo.Text.Trim();

            // Dùng BLL để lọc theo tên (trong chính các project đã duyệt của teacher)
            dgvTienDo.DataSource = tienDoBLL.SearchApprovedProjects(keyword);
        }
        private void btnLamMoi_TienDo_Click(object sender, EventArgs e)
        {
            txtTenDoAn_TienDo.Text = string.Empty;
            LoadDanhSachDoAn_TienDo();
        }
        private long GetSelectedProjectId_TienDo()
        {
            if (dgvTienDo.SelectedRows.Count > 0)
            {
                // Cột phải đúng alias [Mã đồ án] từ DAL
                object value = dgvTienDo.SelectedRows[0].Cells["Mã đồ án"].Value;

                if (value != null && long.TryParse(value.ToString(), out long id))
                {
                    return id;
                }
            }

            return -1; // không chọn
        }
        private void btnXemNhom_TienDo_Click(object sender, EventArgs e)
        {
            long projectId = GetSelectedProjectId_TienDo();

            if (projectId == -1)
            {
                MessageBox.Show(
                    "Vui lòng chọn một đồ án ở danh sách trước khi xem nhóm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Mở form xem nhóm, truyền projectId + teacherId
            fTeamMembers f = new fTeamMembers(projectId, _teacherId);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
        private void btnKiemTra_TienDo_Click(object sender, EventArgs e)
        {
            long projectId = GetSelectedProjectId_TienDo();

            if (projectId == -1)
            {
                MessageBox.Show("Vui lòng chọn một đồ án!", "Thông báo");
                return;
            }

            // Mở GUI mới kiểm tra tiến độ / chấm điểm milestone
            using (var f = new fMilestone_Teacher(projectId, _teacherId))
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
            }

            // Nếu muốn sau khi chấm xong load lại lưới:
            // LoadDanhSachDoAn_TienDo();
        }

        private void dgvTienDo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnChiTietDoAn_TienDo_Click(object sender, EventArgs e)
        {
            long projectId = GetSelectedProjectId_TienDo();
            if (projectId == -1)
            {
                MessageBox.Show("Vui lòng chọn một đồ án!", "Thông báo");
                return;
            }
            fProjectDetail_Teacher f = new fProjectDetail_Teacher(projectId);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvDanhSachDoAn_Diem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //
        private void LoadTrangThaiComboBox_Diem()
        {
            var list = new[]
            {
        new { Text = "Tất cả đồ án đã duyệt",      Value = 0 },
        new { Text = "Chưa hoàn thành chấm điểm", Value = 1 },
        new { Text = "Đã hoàn thành chấm điểm",   Value = 2 }
    };

            cboTrangThai_Diem.DataSource = list;
            cboTrangThai_Diem.DisplayMember = "Text";
            cboTrangThai_Diem.ValueMember = "Value";
        }
        private void LoadDanhSachDoAn_Diem()
        {
            int filter = (int)cboTrangThai_Diem.SelectedValue;
            string kw = txtTenDoAn_Diem.Text.Trim();

            dgvDanhSachDoAn_Diem.DataSource = _diemBLL.GetProjects(filter, kw);

            dgvDanhSachDoAn_Diem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachDoAn_Diem.RowTemplate.Height = 35;
            dgvDanhSachDoAn_Diem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSachDoAn_Diem.MultiSelect = false;
            dgvDanhSachDoAn_Diem.ReadOnly = true;
            dgvDanhSachDoAn_Diem.AllowUserToAddRows = false;
            dgvDanhSachDoAn_Diem.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);

            dgvDanhSachDoAn_Diem.ClearSelection();
            dgvDanhSachDoAn_Diem.CurrentCell = null;
        }
        private void btnTimKiem_Diem_Click(object sender, EventArgs e)
        {
            LoadDanhSachDoAn_Diem();
        }

        private void btnLamMoi_Diem_Click(object sender, EventArgs e)
        {
            txtTenDoAn_Diem.Text = string.Empty;
            cboTrangThai_Diem.SelectedValue = 0;
            LoadDanhSachDoAn_Diem();
        }

        private void cboTrangThai_Diem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTrangThai_Diem.Focused)
                LoadDanhSachDoAn_Diem();
        }
        private long GetSelectedProjectId_Diem()
        {
            if (dgvDanhSachDoAn_Diem.SelectedRows.Count == 0)
                return -1;

            var cell = dgvDanhSachDoAn_Diem.SelectedRows[0].Cells["Mã đồ án"];
            if (cell == null || cell.Value == null)
                return -1;

            if (long.TryParse(cell.Value.ToString(), out long id))
                return id;

            return -1;
        }
        private void btnChiTietDoAn_Diem_Click(object sender, EventArgs e)
        {
            long projectId = GetSelectedProjectId_Diem();
            if (projectId == -1)
            {
                MessageBox.Show("Vui lòng chọn một đồ án!", "Thông báo");
                return;
            }
            fProjectDetail_Teacher f = new fProjectDetail_Teacher(projectId);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnChiTietDiem_Diem_Click(object sender, EventArgs e)
        {
            long projectId = GetSelectedProjectId_Diem();
            if (projectId == -1)
            {
                MessageBox.Show("Vui lòng chọn một đồ án!", "Thông báo");
                return;
            }

            var f = new fShowScore(projectId, _teacherId);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
        private void ExportScoreToExcel(System.Data.DataTable dt)
        {
            try
            {
                Excel.Application exApp = new Excel.Application();
                Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                exSheet.Name = "BangDiem";

                // ====== TIÊU ĐỀ LỚN ======
                Excel.Range title = exSheet.get_Range("A1", "G1");
                title.Merge(true);
                title.Value = "BẢNG ĐIỂM TIẾN ĐỘ";
                title.Font.Size = 18;
                title.Font.Bold = true;
                title.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // ====== HEADER ======
                exSheet.Cells[3, 1] = "Mã đồ án";
                exSheet.Cells[3, 2] = "Tên đồ án";
                exSheet.Cells[3, 3] = "Tên nhóm";
                exSheet.Cells[3, 4] = "Mã SV";       // sttSV
                exSheet.Cells[3, 5] = "Mã số SV";
                exSheet.Cells[3, 6] = "Họ tên SV";
                exSheet.Cells[3, 7] = "Điểm trung bình";

                Excel.Range header = exSheet.get_Range("A3", "G3");
                header.Font.Bold = true;
                header.Interior.Color = Color.LightGray;
                header.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // ====== ĐỔ DỮ LIỆU ======
                string lastProjectId = null;
                string lastTeamName = null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    int excelRow = i + 4; // bắt đầu từ dòng 4

                    string projectId = row["project_id"].ToString();
                    string projectName = row["projectName"].ToString();
                    string teamName = row["teamName"].ToString();

                    // Chỉ ghi mã & tên đồ án khi đổi project
                    if (i == 0 || projectId != lastProjectId)
                    {
                        exSheet.Cells[excelRow, 1] = projectId;
                        exSheet.Cells[excelRow, 2] = projectName;
                    }

                    // Chỉ ghi tên nhóm khi đổi nhóm hoặc đổi project
                    if (i == 0 || projectId != lastProjectId || teamName != lastTeamName)
                    {
                        exSheet.Cells[excelRow, 3] = teamName;
                    }

                    exSheet.Cells[excelRow, 4] = row["sttSV"];          // Mã SV / STT trong nhóm
                    exSheet.Cells[excelRow, 5] = row["studentCode"];    // Mã số SV
                    exSheet.Cells[excelRow, 6] = row["studentName"];    // Họ tên SV

                    decimal avg = 0;
                    if (!(row["avgScore"] is DBNull))
                        avg = Convert.ToDecimal(row["avgScore"]);

                    exSheet.Cells[excelRow, 7] = avg;   // nếu chưa có => 0

                    lastProjectId = projectId;
                    lastTeamName = teamName;
                }

                exSheet.Columns.AutoFit();

                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Excel File|*.xlsx";
                save.Title = "Lưu bảng điểm";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    exBook.SaveAs(save.FileName);
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo");
                    System.Diagnostics.Process.Start(save.FileName);
                }

                exBook.Close();
                exApp.Quit();

                ReleaseObject(exSheet);
                ReleaseObject(exBook);
                ReleaseObject(exApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }
        private void btnXuatExcel_Diem_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = _diemBLL.GetScoreExportForTeacher();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu điểm để xuất!", "Thông báo");
                return;
            }

            ExportScoreToExcel(dt);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
