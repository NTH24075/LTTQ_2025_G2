using LTTQ_G2_2025.BLL;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace LTTQ_G2_2025.GUI
{
    public partial class fTeacherGui : Form
    {
        TeacherBLL teacherBLL = new TeacherBLL();
        private TienDoBLL tienDoBLL;
        private long _teacherId;
        public fTeacherGui(long teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
            teacherBLL = new TeacherBLL(teacherId);
            tienDoBLL = new TienDoBLL(teacherId);
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

            fProjectDetail f = new fProjectDetail(id);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();

            // Reload lại danh sách sau khi duyệt / hủy
            LoadDanhSachDoAnTheoTrangThai(Convert.ToInt32(cboTrangThai.SelectedValue));
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
        private void dgvTienDo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
