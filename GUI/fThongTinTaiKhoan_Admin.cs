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

namespace LTTQ_G2_2025.GUI
{
    public partial class fThongTinTaiKhoan_Admin : Form
    {
        private readonly long _accountId;
        private readonly AccountBLL _accountBLL = new AccountBLL();
        private readonly RoleBLL _roleBLL = new RoleBLL();
        private List<RoleDTO> _roles;
        public fThongTinTaiKhoan_Admin(long AccountId, string email, string displayName)
        {
            InitializeComponent();
            _accountId = AccountId;
            txtEmail.Text = email;
            txtName.Text = displayName;
        }
        public fThongTinTaiKhoan_Admin(long accountId)
        {
            InitializeComponent();
            _accountId = accountId;
            LoadAccountInfo();
        }

        private void fThongTinTaiKhoan_Admin_Load(object sender, EventArgs e)
        {
            LoadRoles();
            LoadAccountInfo();
        }
        private void LoadRoles()
        {
            _roles = _roleBLL.GetAllRoles();
            cbRole.DataSource = _roles;
            cbRole.DisplayMember = "RoleName";
            cbRole.ValueMember = "RoleId";
        }
       
        private void LoadAccountInfo()
        {
            var acc = _accountBLL.GetAccountDetailById(_accountId);
            if (acc == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtEmail.Text = acc.Email;
            txtName.Text = acc.Name;
            cbRole.SelectedValue = acc.RoleId;
        }
        
        private void btAcp_Click(object sender, EventArgs e)
        {
            string newPass = txtNewPass.Text.Trim();
            string confirm = txtConfirmPass.Text.Trim();

            if (string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Mật khẩu mới không được để trống.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPass.Focus();
                return;
            }

            if (newPass.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải ít nhất 6 ký tự.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPass.Focus();
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPass.Focus();
                return;
            }

            try
            {
                bool ok = _accountBLL.UpdatePassword(_accountId, newPass);

                if (ok)
                {
                    MessageBox.Show("Đổi mật khẩu thành công.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đổi mật khẩu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
