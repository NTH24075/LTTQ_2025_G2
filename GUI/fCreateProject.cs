using LTTQ_G2_2025.BLL;
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

namespace LTTQ_G2_2025.GUI
{
    public partial class fCreateProject : Form
    {
        

        
        private readonly long _teamId;
        private readonly ProjectBLL _bll;

        public fCreateProject(long teamId, ProjectBLL bll)
        {
            InitializeComponent();
            _teamId = teamId;
            _bll = bll;
        }
        private void fCreateProject_Load(object sender, EventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string content = txtContent.Text.Trim();
            string img = txtImg.Text.Trim();               // đường dẫn ảnh (nếu dùng)
            string desc = txtDescription.Text.Trim();
            bool status = false;
            //int? semId = string.IsNullOrWhiteSpace(txtSemesterId.Text) ? (int?)null : int.Parse(txtSemesterId.Text);
            int ? semId = null; // Giả sử không có semester_id
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên đề tài không được để trống."); return;
            }

            bool ok = _bll.CreateProjectForTeam(_teamId, name, content, img, desc, status, semId);
            if (ok)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Tạo đề tài thất bại.");
            }
        }

        private void btnBrowseImg_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog() { Filter = "Images|*.png;*.jpg;*.jpeg;*.gif" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtImg.Text = ofd.FileName;
                }
            }
        }
    }
}
