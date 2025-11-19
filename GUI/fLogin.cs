using LTTQ_G2_2025.BLL;
using LTTQ_G2_2025.DTO;
using LTTQ_G2_2025.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTTQ_G2_2025
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //fManagerAMinGUI f = new fManagerAMinGUI();
            //this.Hide();
            //f.ShowDialog();
            string username = txtAccount.Text.Trim();
            string password = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AccountBLL accountBLL = new AccountBLL();
            AccountDTO account = accountBLL.LoginAccount(username, password);

            if (account != null)
            {
                // Hiển thị thông tin roles (optional)
                string rolesInfo = string.Join(", ", account.Roles);
                //MessageBox.Show($"Đăng nhập thành công!\nRoles: {rolesInfo}",
                //    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Phân quyền dựa vào role cao nhất
                string primaryRole = account.GetPrimaryRole();

                switch (primaryRole.ToLower())
                {
                    case "admin":
                        fManagerAMinGUI adminForm = new fManagerAMinGUI();
                        adminForm.ShowDialog();
                        break;

                    case "manager":
                        //FormManager managerForm = new FormManager(account);
                        //managerForm.ShowDialog();
                        //break;

                    case "user":
                    default:
                        //FormUser userForm = new FormUser(account);
                        //userForm.ShowDialog();
                        //break;
                        fManagerAMinGUI s = new fManagerAMinGUI();
                        s.ShowDialog();
                        break;
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn muốn đóng chương trình ?","Thông báo",MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel= true;
            }
        }
    }
}
