using LTTQ_G2_2025.BLL;
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

namespace LTTQ_G2_2025.GUI
{
    public partial class cboRoleDetail : Form
    {
        private AccountBLL _accountBLL;
        public cboRoleDetail()
        {
            InitializeComponent();
            _accountBLL = new AccountBLL();
        }

       

       

        private void button4_Click(object sender, EventArgs e)
        {

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
            using (var f = new fThongTinTaiKhoan_Admin(acc.AccountId,acc.Email, acc.DisplayName))
            {
                f.ShowDialog();
            }

            // Nếu muốn, sau khi đổi mật khẩu xong có thể load lại danh sách
            // LoadAccounts();
        }
        

        private void btAddTeacher_Click(object sender, EventArgs e)
        {

        }

        private void fManagerAMinGUI_Load(object sender, EventArgs e)
        {
            LoadAccountsToGrid();
            LoadAccounts();
            LoadRoleFilter();
        }
        private void LoadAccounts()
        {
            var data = _accountBLL.GetAllAccountsForView();
            dgvAcc.DataSource = data;
            dgvAcc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ẩn cột DisplayName, chỉ dùng để đổ sang ô bên phải
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

            // Truyền accountId + roleName vào form chi tiết
            using (var f = new fDetailAccount(acc.AccountId, acc.RoleName))
            {
                f.ShowDialog();
            }
        }
    }
}
