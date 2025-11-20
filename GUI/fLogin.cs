using LTTQ_G2_2025.BLL;
using LTTQ_G2_2025.DTO;
using LTTQ_G2_2025.GUI;
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

namespace LTTQ_G2_2025
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
            CustomizeUI();
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
                        long currentAccountId = account.AccountId;
                        
                        fStudentList f = new fStudentList(currentAccountId);
                        f.ShowDialog();
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
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        

        private void CustomizeUI()
        {
            // ===== Form =====
            this.BackColor = Color.FromArgb(108, 153, 246);     // cam nhẹ
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ===== Panel chính =====
            panel1.BackColor = Color.White;
            panel1.Padding = new Padding(20);

            int radius = 25;
            panel1.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, panel1.Width, panel1.Height, radius, radius));

            // Căn giữa panel
            CenterPanel();
            this.Resize += (s, e) =>
            {
                CenterPanel();
                panel1.Region = Region.FromHrgn(CreateRoundRectRgn(
                    0, 0, panel1.Width, panel1.Height, radius, radius));
            };

            // ===== Label =====
            label1.ForeColor = Color.FromArgb(40, 40, 40);
            label2.ForeColor = Color.FromArgb(40, 40, 40);

            // ===== TextBox =====
            txtAccount.Font = new Font("Segoe UI", 12F);
            txtPass.Font = new Font("Segoe UI", 12F);
            txtAccount.BorderStyle = BorderStyle.FixedSingle;
            txtPass.BorderStyle = BorderStyle.FixedSingle;

            // textbox password cho cao
            //txtAccount.Multiline = true;
            //txtAccount.Height = 38;
            //txtPass.Multiline = true;
            //txtPass.Height = 38;

            // ===== Button Đăng nhập =====
            button1.BackColor = Color.FromArgb(33, 150, 243);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Cursor = Cursors.Hand;

            // ===== Button Thoát =====
            button2.BackColor = Color.FromArgb(224, 224, 224);
            button2.ForeColor = Color.Black;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Cursor = Cursors.Hand;

            // Enter = Đăng nhập, Esc = Thoát
            this.AcceptButton = button1;
            this.CancelButton = button2;
        }

        private void CenterPanel()
        {
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
