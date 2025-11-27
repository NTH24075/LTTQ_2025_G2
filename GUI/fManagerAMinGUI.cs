using LTTQ_G2_2025.BLL;
using LTTQ_G2_2025.DAL;
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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace LTTQ_G2_2025.GUI
{
    public partial class cboRoleDetail : Form
    {
        private AccountBLL _accountBLL;
        private readonly TeacherBLL _teacherBLL = new TeacherBLL();
        private readonly FacultyDAL _facultyDAL = new FacultyDAL();
        private StudentBLL studentBLL = new StudentBLL();
        private readonly ProjectBLL _bll = new ProjectBLL();
        private readonly TeamBLL _teamBLL = new TeamBLL();
        private DataTable _studentsDT;
        private DataTable _teachersDT;
        private DataTable _teamsDT;
        private DataTable _projectsDT;
        public cboRoleDetail()
        {
            InitializeComponent();
            _accountBLL = new AccountBLL();
        }





        private void button4_Click(object sender, EventArgs e)
        {
            ApplySearch();
        }
        private void ApplySearch()
        {
            string keyword = txtSearchTeacher.Text.Trim();
            int? facultyId = null;

            if (cbCategory.SelectedIndex > 0 && cbCategory.SelectedValue != null
                && int.TryParse(cbCategory.SelectedValue.ToString(), out int fid))
            {
                facultyId = fid;
            }

            var data = _teacherBLL.Search(keyword, facultyId, null);
            dgvTeacher.DataSource = data;

            foreach (var col in new[] { "TeacherId", "DateOfBirth", "Email", "PhoneNumber", "TeacherGender", "TeacherAddress", "Img", "FlagDelete", "AccountId", "FacultyId", "DegreeId" })
                if (dgvTeacher.Columns.Contains(col)) dgvTeacher.Columns[col].Visible = false;
            if (dgvTeacher.Columns.Contains("TeacherCode")) dgvTeacher.Columns["TeacherCode"].HeaderText = "Mã";
            if (dgvTeacher.Columns.Contains("TeacherName")) dgvTeacher.Columns["TeacherName"].HeaderText = "Tên giảng viên";
            if (dgvTeacher.Columns.Contains("FacultyName")) dgvTeacher.Columns["FacultyName"].HeaderText = "Khoa";
            if (dgvTeacher.Columns.Contains("DegreeName")) dgvTeacher.Columns["DegreeName"].HeaderText = "Học vị";
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void btResetPass_Click(object sender, EventArgs e)
        {
            if (dgvAcc.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 tài khoản.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var acc = dgvAcc.CurrentRow.DataBoundItem as AccountViewDTO;
            if (acc == null)
            {
                MessageBox.Show("Không lấy được thông tin tài khoản.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Mở form thông tin tài khoản
            using (var f = new fThongTinTaiKhoan_Admin(acc.AccountId, acc.Email, acc.DisplayName))
            {
                f.ShowDialog();
            }

            // Nếu muốn, sau khi đổi mật khẩu xong có thể load lại danh sách
            // LoadAccounts();
        }


        private void btAddTeacher_Click(object sender, EventArgs e)
        {
            using (var f = new AddTeacher())
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    LoadTeachers();
                    LoadFacultyFilter();
                    LoadAccountsToGrid();
                    LoadAccounts();
                    LoadRoleFilter();
                    LoadTeachers();
                }
            }

        }

        private void fManagerAMinGUI_Load(object sender, EventArgs e)
        {
            LoadFacultyFilter();
            LoadAccountsToGrid();
            LoadAccounts();
            LoadRoleFilter();
            LoadTeachers();
            LoadStudentList();
            LoadProjects();
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.CustomFormat = "yyyy-MM-dd";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.CustomFormat = "yyyy-MM-dd";
            dtFromTeam.Format = DateTimePickerFormat.Custom;
            dtFromTeam.CustomFormat = "yyyy-MM-dd";
            dtToTeam.Format = DateTimePickerFormat.Custom;
            dtToTeam.CustomFormat = "yyyy-MM-dd";
            // mặc định: đầu tháng -> hôm nay
            var today = DateTime.Today;
            dtpFrom.Value = new DateTime(today.Year - 1, today.Month, 1);
            dtFromTeam.Value = new DateTime(today.Year - 1, today.Month, 1);
            dtpTo.Value = today;
            dtToTeam.Value = today;
        }
        
        private void LoadStudentList()
        {
            dgvStudent.DataSource = studentBLL.GetAllStudents();

            dgvStudent.Columns["StudentId"].Visible = false; // Ẩn ID
        }
        private void LoadAccounts()
        {
            var data = _accountBLL.GetAllAccountsForView();
            dgvAcc.DataSource = data;
            dgvAcc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvAcc.Columns["DisplayName"] != null)
            {
                dgvAcc.Columns["DisplayName"].Visible = false;
            }
        }
        private void LoadRoleFilter()
        {
            cbRole.Items.Clear();
            cbRole.Items.Add("Tất cả");      // index 0
            cbRole.Items.Add("ROLE_ADMIN");
            cbRole.Items.Add("ROLE_TEACHER");
            cbRole.Items.Add("ROLE_STUDENT");
            cbRole.SelectedIndex = 0;
        }
        private void LoadAccountsToGrid()
        {
            var data = _accountBLL.GetAllAccounts();
            dgvAcc.DataSource = data;
            dgvAcc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvAcc.Columns["DisplayName"] != null)
            {
                dgvAcc.Columns["DisplayName"].Visible = false;
            }
        }

        private void dgvAcc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var acc = (AccountViewDTO)dgvAcc.Rows[e.RowIndex].DataBoundItem;
            if (acc == null) return;

            txtEmailDetail.Text = acc.Email;
            txtNameDetail.Text = acc.DisplayName;
            txtRoleDetail.Text = acc.RoleName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            fLogin s = new fLogin();
            s.Show();
        }

        private void btSearchAcc_Click(object sender, EventArgs e)
        {
            string emailKeyword = txtSearchAcc.Text.Trim();

            string roleName = null;
            if (cbRole.SelectedIndex > 0) // bỏ "Tất cả"
            {
                roleName = cbRole.SelectedItem.ToString();
            }

            var data = _accountBLL.SearchAccounts(emailKeyword, roleName);
            dgvAcc.DataSource = data;
            dgvAcc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ẩn DisplayName nếu bạn vẫn giữ trong DTO
            if (dgvAcc.Columns["DisplayName"] != null)
                dgvAcc.Columns["DisplayName"].Visible = false;
        }
        private void OpenUpdateFormForCurrentRow()
        {
            if (dgvAcc.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var accView = dgvAcc.CurrentRow.DataBoundItem as AccountViewDTO;
            if (accView == null)
            {
                MessageBox.Show("Không lấy được thông tin tài khoản.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var f = new fThongTinTaiKhoan_Admin(accView.AccountId))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadAccounts(); // load lại dgv sau khi cập nhật
                }
            }
        }
        private void btUpdateAcc_Click(object sender, EventArgs e)
        {
            OpenUpdateFormForCurrentRow();
        }

        private void btDetailAcc_Click(object sender, EventArgs e)
        {
            if (dgvAcc.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 tài khoản.", "Thông báo");
                return;
            }

            var acc = dgvAcc.CurrentRow.DataBoundItem as AccountViewDTO;
            if (acc == null) return;

            using (var f = new fDetailAccount(acc.AccountId, acc.RoleName))
            {
                f.ShowDialog();
            }
        }

        private void btDetailTeacher_Click(object sender, EventArgs e)
        {
            if (dgvTeacher.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một giảng viên trong bảng.", "Thông báo");
                return;
            }

            long accountId = 0;

            // Nếu DataSource là list< TeacherDTO > (đúng như mình đã set ở BLL/DAL)
            if (dgvTeacher.CurrentRow.DataBoundItem is LTTQ_G2_2025.DTO.TeacherDTO dto)
            {
                if (dto.AccountId.HasValue) accountId = dto.AccountId.Value;
            }
            else
            {
                if (dgvTeacher.Columns.Contains("AccountId"))
                {
                    var val = dgvTeacher.CurrentRow.Cells["AccountId"].Value;
                    if (val != null && long.TryParse(val.ToString(), out long tmp))
                        accountId = tmp;
                }
                else if (dgvTeacher.Columns.Contains("TeacherId"))
                {
                    var val = dgvTeacher.CurrentRow.Cells["TeacherId"].Value;
                    if (val != null && long.TryParse(val.ToString(), out long teacherId))
                    {
                        var t = _teacherBLL.GetTeacherById(teacherId);
                        if (t != null && t.AccountId.HasValue)
                            accountId = t.AccountId.Value;
                    }
                }
            }

            if (accountId <= 0)
            {
                MessageBox.Show("Không tìm thấy AccountId của giảng viên này.", "Thông báo");
                return;
            }

            using (var f = new fDetailAccount(accountId, "ROLE_TEACHER"))
            {
                f.ShowDialog(this);
            }
        }

        private void LoadTeachers()
        {
            var data = _teacherBLL.GetAll();
            dgvTeacher.DataSource = data;

            if (dgvTeacher.Columns.Contains("Img")) dgvTeacher.Columns["Img"].Visible = false;
            dgvTeacher.Columns["TeacherId"].Visible = false;
            //dgvTeacher.Columns["TeacherCode"].Visible = false;
            dgvTeacher.Columns["DateOfBirth"].Visible = false;
            dgvTeacher.Columns["Email"].Visible = false;
            dgvTeacher.Columns["PhoneNumber"].Visible = false;
            dgvTeacher.Columns["TeacherGender"].Visible = false;
            dgvTeacher.Columns["TeacherAddress"].Visible = false;
            dgvTeacher.Columns["Img"].Visible = false;
            dgvTeacher.Columns["FlagDelete"].Visible = false;
            dgvTeacher.Columns["AccountId"].Visible = false;
            dgvTeacher.Columns["FacultyId"].Visible = false;
            dgvTeacher.Columns["DegreeId"].Visible = false;

            dgvTeacher.Columns["TeacherCode"].HeaderText = "Mã";
            dgvTeacher.Columns["TeacherName"].HeaderText = "Tên giảng viên";
            dgvTeacher.Columns["FacultyName"].HeaderText = "Khoa";
            dgvTeacher.Columns["DegreeName"].HeaderText = "Học vị";
            dgvTeacher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTeacher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTeacher.MultiSelect = false;
        }

        private void dgvTeacher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            long teacherId = Convert.ToInt64(dgvTeacher.Rows[e.RowIndex].Cells["TeacherId"].Value);

            TeacherViewDetailDTO t = _teacherBLL.GetTeacherById(teacherId);

            if (t == null) return;

            txtNameTeacherr.Text = t.TeacherName;
            txtEmailTeacher.Text = t.Email;
            txtPhoneTeacher.Text = t.PhoneNumber;
            txtFacultyTeacher.Text = t.FacultyName;
            txtDegree.Text = t.DegreeName;
        }
        private void LoadFacultyFilter()
        {
            var dt = _facultyDAL.GetAll();

            var all = dt.NewRow();
            all["faculty_id"] = DBNull.Value;
            all["facultyName"] = "Tất cả khoa";
            dt.Rows.InsertAt(all, 0);

            cbCategory.DataSource = dt;
            cbCategory.DisplayMember = "facultyName";
            cbCategory.ValueMember = "faculty_id";
            cbCategory.SelectedIndex = 0;
        }

        private void button4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btUpdateInforTeacher_Click(object sender, EventArgs e)
        {
            if (dgvTeacher.CurrentRow == null)
            {
                MessageBox.Show("Hãy chọn một giảng viên.", "Thông báo");
                return;
            }

            long teacherId = 0;

            if (dgvTeacher.CurrentRow.DataBoundItem is LTTQ_G2_2025.DTO.TeacherDTO dto && dto.TeacherId > 0)
            {
                teacherId = dto.TeacherId;
            }
            else if (dgvTeacher.Columns.Contains("TeacherId"))
            {
                var val = dgvTeacher.CurrentRow.Cells["TeacherId"].Value;
                if (val != null) long.TryParse(val.ToString(), out teacherId);
            }

            if (teacherId <= 0)
            {
                MessageBox.Show("Không xác định được TeacherId.", "Lỗi");
                return;
            }

            using (var f = new fThongTinTeacher(teacherId))
            {
                var dr = f.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    ApplySearch(); // hoặc LoadTeachers();
                }
            }
        }

        private void dgvTeacher_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btDetailTeacher_Click(sender, EventArgs.Empty);
        }

        private void tbStudent_Click(object sender, EventArgs e)
        {

        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtNameStudent.Text = dgvStudent.Rows[e.RowIndex].Cells["StudentName"].Value.ToString();
            txtClassStudent.Text = dgvStudent.Rows[e.RowIndex].Cells["ClassName"].Value.ToString();
            txtEmailStudent.Text = dgvStudent.Rows[e.RowIndex].Cells["Email"].Value.ToString();
            txtPhoneStudent.Text = dgvStudent.Rows[e.RowIndex].Cells["PhoneNumber"].Value.ToString();
            txtFaculStudent.Text = dgvStudent.Rows[e.RowIndex].Cells["facultyName"].Value.ToString();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchStudent.Text.Trim();

            if (keyword == "")
            {
                LoadStudentList();
                return;
            }

            dgvStudent.DataSource = studentBLL.SearchStudents(keyword);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dgvStudent.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên trong bảng.", "Thông báo");
                return;
            }

            long accountId = -1;

            // Nếu DataGridView đang bind StudentViewDTO hoặc DTO khác có AccountId
            if (dgvStudent.CurrentRow.DataBoundItem is StudentViewDTO dto)
            {
                if (dto.AccountId.HasValue)
                    accountId = dto.AccountId.Value;
            }
            else
            {
                if (dgvStudent.Columns.Contains("AccountId"))
                {
                    var val = dgvStudent.CurrentRow.Cells["AccountId"].Value;
                    if (val != null && long.TryParse(val.ToString(), out long tmp))
                        accountId = tmp;
                }
            }

            if (accountId <= 0)
            {
                MessageBox.Show("Không tìm thấy AccountId của sinh viên.", "Thông báo");
                return;
            }

            using (var f = new fDetailAccount(accountId, "ROLE_STUDENT"))
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvStudent.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên.", "Thông báo");
                return;
            }

            long accountId = 0;

            if (dgvStudent.CurrentRow.DataBoundItem is StudentViewDTO dto && dto.AccountId.HasValue)
                accountId = dto.AccountId.Value;
            else if (dgvStudent.Columns.Contains("AccountId"))
            {
                var v = dgvStudent.CurrentRow.Cells["AccountId"].Value;
                if (v != null) long.TryParse(v.ToString(), out accountId);
            }

            if (accountId <= 0)
            {
                MessageBox.Show("Không tìm thấy AccountId.", "Thông báo");
                return;
            }

            using (var f = new fThongTinStudent(accountId))
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {

                    LoadStudentList();
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (var f = new LTTQ_G2_2025.GUI.fAddStudent())
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    LoadStudentList();
                    LoadAccounts();
                }
            }
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadProjects()
        {
            DateTime? from = dtpFrom.Checked ? dtpFrom.Value.Date : (DateTime?)null;
            DateTime? to = dtpTo.Checked ? dtpTo.Value.Date : (DateTime?)null;

            string kw = txtSearch?.Text?.Trim();

            var data = _bll.Search(from, to, kw);

            dgvProject.AutoGenerateColumns = false; // tự định nghĩa cột cho chuẩn
            if (dgvProject.Columns.Count == 0)
            {
                dgvProject.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ProjectName",
                    HeaderText = "Tên đề tài",
                    Width = 220
                });
                dgvProject.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TeamName",
                    HeaderText = "Nhóm",
                    Width = 140
                });
                dgvProject.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TeacherName",
                    HeaderText = "GV hướng dẫn",
                    Width = 160
                });
                dgvProject.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SemesterName",
                    HeaderText = "Học kỳ",
                    Width = 120
                });
            }

            dgvProject.DataSource = data;
            if (data.Count > 0) dgvProject.Rows[0].Selected = true;
            FillDetailFromCurrentRow();
        }

        private void dgvProject_SelectionChanged(object sender, EventArgs e)
        {
            FillDetailFromCurrentRow();
        }
        private void FillDetailFromCurrentRow()
        {
            txtNamePrj.Text = "";
            txtGroupPrj.Text = "";
            txtTeacherPrj.Text = "";
            txtDetailPrj.Text = "";

            if (dgvProject.CurrentRow?.DataBoundItem is ProjectListItemDTO item)
            {
                txtNamePrj.Text = item.ProjectName;
                txtGroupPrj.Text = item.TeamName;
                txtTeacherPrj.Text = item.TeacherName;
                txtDetailPrj.Text = item.Description;
            }
        }

        private void btSearchPrj_Click(object sender, EventArgs e)
        {
            LoadProjects();
        }
        private void BuildTeamGrid()
        {
            dgvTeam.Columns.Clear();

            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TeamId",
                HeaderText = "ID",
                Width = 60
            });
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TeamName",
                HeaderText = "Tên nhóm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProjectName",
                HeaderText = "Tên dự án",
                Width = 180
            });
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TeacherName",
                HeaderText = "GV hướng dẫn",
                Width = 150
            });
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FacultyName",
                HeaderText = "Khoa",
                Width = 120
            });
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SemesterName",
                HeaderText = "Kì học",
                Width = 120
            });

            // các field nội bộ không hiển thị vẫn có thể giữ trong datasource (không cần thêm cột)
        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void btSearchTeam_Click(object sender, EventArgs e)
        {
            DateTime? from = dtFromTeam.Checked ? dtFromTeam.Value.Date : (DateTime?)null;
            DateTime? to = dtToTeam.Checked ? dtToTeam.Value.Date : (DateTime?)null;
            string kw = txtSearch.Text?.Trim();

            var list = _teamBLL.Search(from, to, kw);
            dgvTeam.DataSource = list;

            // chọn dòng đầu tiên để đổ chi tiết
            if (list.Count > 0)
            {
                dgvTeam.ClearSelection();
                dgvTeam.Rows[0].Selected = true;
                FillDetail(list[0]);
            }
            else
            {
                ClearDetail();
            }
        }
        private void FillDetail(TeamViewDTO dto)
        {
            txtTeamId.Text = dto.TeamId.ToString();
            txtTeamName.Text = dto.TeamName;
            txtTeacherTeam.Text = dto.TeacherName;
            txtProjectTeam.Text = dto.ProjectName;
            txtFacultyTeam.Text = dto.FacultyName;
            txtSemesterTeam.Text = dto.SemesterName;

            // Nếu bạn có RichTextBox mô tả: rtbProjectDesc.Text = dto.ProjectDescription;
        }
        private void ClearDetail()
        {
            txtTeamId.Clear();
            txtTeamName.Clear();
            txtTeacherTeam.Clear();
            txtProjectTeam.Clear();
            txtFacultyTeam.Clear();
            txtSemesterTeam.Clear();
            // rtbProjectDesc.Clear();
        }

        private void dgvTeam_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        
        private bool PickSavePath(string suggestedBaseName, out string path)
        {
            var sfd = new SaveFileDialog();
            sfd.Title = "Chọn nơi lưu Excel";
            sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
            sfd.DefaultExt = "xlsx";
            sfd.AddExtension = true;
            sfd.OverwritePrompt = true;
            sfd.ValidateNames = true;
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var baseName = string.IsNullOrWhiteSpace(suggestedBaseName)
                ? "BaoCao"
                : suggestedBaseName.Trim();

            // Đừng thêm .xlsx ở đây; để người dùng thấy tên gợi ý và có thể sửa
            sfd.FileName = baseName;

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                // Bảo đảm có đuôi .xlsx nếu người dùng không gõ
                path = sfd.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)
                    ? sfd.FileName
                    : sfd.FileName + ".xlsx";
                return true;
            }

            path = string.Empty;
            return false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? fromSt = dtpFromSt.Checked ? (DateTime?)dtpFromSt.Value.Date : null;
                DateTime? toSt = dtpToSt.Checked ? (DateTime?)dtpToSt.Value.Date : null;

                DateTime? fromTeam = dtFromTeam.Checked ? (DateTime?)dtFromTeam.Value.Date : null;
                DateTime? toTeam = dtToTeam.Checked ? (DateTime?)dtToTeam.Value.Date : null;

                // Validate
                if (fromSt.HasValue && toSt.HasValue && fromSt.Value > toSt.Value)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc (Sinh viên/Giảng viên)!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (fromTeam.HasValue && toTeam.HasValue && fromTeam.Value > toTeam.Value)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc (Nhóm/Đề tài)!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _studentsDT = StatisticsDAL.Instance.GetStudentList(fromSt, toSt);
                _teachersDT = StatisticsDAL.Instance.GetTeacherList(fromSt, toSt);
                _teamsDT = StatisticsDAL.Instance.GetTeamList(fromSt, toSt);
                _projectsDT = StatisticsDAL.Instance.GetProjectList(fromSt, toSt);

                int total = _studentsDT.Rows.Count + _teachersDT.Rows.Count +
                            _teamsDT.Rows.Count + _projectsDT.Rows.Count;

                MessageBox.Show($"Đã lọc dữ liệu thành công!\n\n" +
                               $"Sinh viên: {_studentsDT.Rows.Count}\n" +
                               $"Giảng viên: {_teachersDT.Rows.Count}\n" +
                               $"Nhóm: {_teamsDT.Rows.Count}\n" +
                               $"Đề tài: {_projectsDT.Rows.Count}\n" +
                               $"Tổng: {total} bản ghi",
                               "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm dữ liệu:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_studentsDT == null)
            {
                MessageBox.Show("Vui lòng bấm Tìm kiếm trước!");
                return;
            }

            string suggest = "DanhSachSinhVien_" + DateTime.Now.ToString("yyyy-MM-dd");
            string path;
            if (!PickSavePath(suggest, out path)) return;
            ExcelExporter.ExportDataTable("Danh sách sinh viên", _studentsDT, path);

            MessageBox.Show("Xuất Excel thành công!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (_teachersDT == null)
            {
                MessageBox.Show("Vui lòng bấm Tìm kiếm trước!");
                return;
            }

            string suggest = "DanhSachGiangVien_" + DateTime.Now.ToString("yyyy-MM-dd");
            string path;
            if (!PickSavePath(suggest, out path)) return;


            ExcelExporter.ExportDataTable("Danh sách giảng viên", _teachersDT, path);
            MessageBox.Show("Xuất Excel thành công!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            

            string suggest = "DanhSachNhom_" + DateTime.Now.ToString("yyyy-MM-dd");
            string path;
            if (!PickSavePath(suggest, out path)) return;


            ExcelExporter.ExportDataTable("Danh sách nhóm", _teamsDT, path);
            MessageBox.Show("Xuất Excel thành công!");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (_projectsDT == null)
            {
                MessageBox.Show("Vui lòng bấm Tìm kiếm trước!");
                return;
            }

            string suggest = "DanhSachDeTai_" + DateTime.Now.ToString("yyyy-MM-dd");
            string path;
            if (!PickSavePath(suggest, out path)) return;


            ExcelExporter.ExportDataTable("Danh sách đề tài", _projectsDT, path);
            MessageBox.Show("Xuất Excel thành công!");
        }
    }
}
