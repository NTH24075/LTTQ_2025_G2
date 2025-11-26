namespace LTTQ_G2_2025.GUI
{
    partial class fTeacherGui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fTeacherGui));
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTenDoAn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDanhSachDoAn = new System.Windows.Forms.DataGridView();
            this.tabTienDo = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnChamDiem_TienDo = new System.Windows.Forms.Button();
            this.btnKiemTra_TienDo = new System.Windows.Forms.Button();
            this.btnXemNhom_TienDo = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLamMoi_TienDo = new System.Windows.Forms.Button();
            this.btnTimKiem_TienDo = new System.Windows.Forms.Button();
            this.txtTenDoAn_TienDo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvTienDo = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDoAn)).BeginInit();
            this.tabTienDo.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(460, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(489, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "UTC-Trường Đại Học Giao Thông Vận Tải";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabTienDo);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(2, 88);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1238, 654);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.dgvDanhSachDoAn);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1230, 621);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Đồ án";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnXuatExcel);
            this.panel2.Controls.Add(this.btnXemChiTiet);
            this.panel2.Location = new System.Drawing.Point(688, 266);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(525, 332);
            this.panel2.TabIndex = 7;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Location = new System.Drawing.Point(135, 206);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(242, 72);
            this.btnXuatExcel.TabIndex = 3;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.Location = new System.Drawing.Point(135, 69);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(242, 70);
            this.btnXemChiTiet.TabIndex = 0;
            this.btnXemChiTiet.Text = "Xem chi tiết";
            this.btnXemChiTiet.UseVisualStyleBackColor = true;
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnLamMoi);
            this.panel1.Controls.Add(this.cboTrangThai);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Controls.Add(this.txtTenDoAn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(688, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 222);
            this.panel1.TabIndex = 6;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(280, 150);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(242, 38);
            this.btnLamMoi.TabIndex = 6;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(135, 83);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(151, 28);
            this.cboTrangThai.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(23, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Trạng thái";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(13, 150);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(242, 38);
            this.btnTimKiem.TabIndex = 3;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTenDoAn
            // 
            this.txtTenDoAn.Location = new System.Drawing.Point(135, 23);
            this.txtTenDoAn.Name = "txtTenDoAn";
            this.txtTenDoAn.Size = new System.Drawing.Size(313, 26);
            this.txtTenDoAn.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(23, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên đồ án: ";
            // 
            // dgvDanhSachDoAn
            // 
            this.dgvDanhSachDoAn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachDoAn.Location = new System.Drawing.Point(6, 21);
            this.dgvDanhSachDoAn.Name = "dgvDanhSachDoAn";
            this.dgvDanhSachDoAn.RowHeadersWidth = 62;
            this.dgvDanhSachDoAn.RowTemplate.Height = 28;
            this.dgvDanhSachDoAn.Size = new System.Drawing.Size(672, 577);
            this.dgvDanhSachDoAn.TabIndex = 0;
            this.dgvDanhSachDoAn.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachDoAn_CellContentClick);
            // 
            // tabTienDo
            // 
            this.tabTienDo.Controls.Add(this.panel4);
            this.tabTienDo.Controls.Add(this.panel3);
            this.tabTienDo.Controls.Add(this.dgvTienDo);
            this.tabTienDo.Location = new System.Drawing.Point(4, 29);
            this.tabTienDo.Name = "tabTienDo";
            this.tabTienDo.Padding = new System.Windows.Forms.Padding(3);
            this.tabTienDo.Size = new System.Drawing.Size(1230, 621);
            this.tabTienDo.TabIndex = 1;
            this.tabTienDo.Text = "Tiến độ";
            this.tabTienDo.UseVisualStyleBackColor = true;
            this.tabTienDo.Click += new System.EventHandler(this.tabTienDo_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnChamDiem_TienDo);
            this.panel4.Controls.Add(this.btnKiemTra_TienDo);
            this.panel4.Controls.Add(this.btnXemNhom_TienDo);
            this.panel4.Location = new System.Drawing.Point(689, 224);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(470, 375);
            this.panel4.TabIndex = 2;
            // 
            // btnChamDiem_TienDo
            // 
            this.btnChamDiem_TienDo.Location = new System.Drawing.Point(111, 270);
            this.btnChamDiem_TienDo.Name = "btnChamDiem_TienDo";
            this.btnChamDiem_TienDo.Size = new System.Drawing.Size(211, 56);
            this.btnChamDiem_TienDo.TabIndex = 2;
            this.btnChamDiem_TienDo.Text = "Chấm điểm theo tiến độ";
            this.btnChamDiem_TienDo.UseVisualStyleBackColor = true;
            // 
            // btnKiemTra_TienDo
            // 
            this.btnKiemTra_TienDo.Location = new System.Drawing.Point(111, 151);
            this.btnKiemTra_TienDo.Name = "btnKiemTra_TienDo";
            this.btnKiemTra_TienDo.Size = new System.Drawing.Size(211, 56);
            this.btnKiemTra_TienDo.TabIndex = 1;
            this.btnKiemTra_TienDo.Text = "Kiểm tra tiến độ";
            this.btnKiemTra_TienDo.UseVisualStyleBackColor = true;
            // 
            // btnXemNhom_TienDo
            // 
            this.btnXemNhom_TienDo.Location = new System.Drawing.Point(111, 41);
            this.btnXemNhom_TienDo.Name = "btnXemNhom_TienDo";
            this.btnXemNhom_TienDo.Size = new System.Drawing.Size(211, 56);
            this.btnXemNhom_TienDo.TabIndex = 0;
            this.btnXemNhom_TienDo.Text = "Xem nhóm";
            this.btnXemNhom_TienDo.UseVisualStyleBackColor = true;
            this.btnXemNhom_TienDo.Click += new System.EventHandler(this.btnXemNhom_TienDo_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnLamMoi_TienDo);
            this.panel3.Controls.Add(this.btnTimKiem_TienDo);
            this.panel3.Controls.Add(this.txtTenDoAn_TienDo);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(689, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(471, 166);
            this.panel3.TabIndex = 1;
            // 
            // btnLamMoi_TienDo
            // 
            this.btnLamMoi_TienDo.Location = new System.Drawing.Point(248, 96);
            this.btnLamMoi_TienDo.Name = "btnLamMoi_TienDo";
            this.btnLamMoi_TienDo.Size = new System.Drawing.Size(162, 36);
            this.btnLamMoi_TienDo.TabIndex = 3;
            this.btnLamMoi_TienDo.Text = "Làm mới";
            this.btnLamMoi_TienDo.UseVisualStyleBackColor = true;
            this.btnLamMoi_TienDo.Click += new System.EventHandler(this.btnLamMoi_TienDo_Click);
            // 
            // btnTimKiem_TienDo
            // 
            this.btnTimKiem_TienDo.Location = new System.Drawing.Point(32, 96);
            this.btnTimKiem_TienDo.Name = "btnTimKiem_TienDo";
            this.btnTimKiem_TienDo.Size = new System.Drawing.Size(162, 36);
            this.btnTimKiem_TienDo.TabIndex = 2;
            this.btnTimKiem_TienDo.Text = "Tìm kiếm";
            this.btnTimKiem_TienDo.UseVisualStyleBackColor = true;
            this.btnTimKiem_TienDo.Click += new System.EventHandler(this.btnTimKiem_TienDo_Click);
            // 
            // txtTenDoAn_TienDo
            // 
            this.txtTenDoAn_TienDo.Location = new System.Drawing.Point(167, 32);
            this.txtTenDoAn_TienDo.Name = "txtTenDoAn_TienDo";
            this.txtTenDoAn_TienDo.Size = new System.Drawing.Size(243, 26);
            this.txtTenDoAn_TienDo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tên đồ án :";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // dgvTienDo
            // 
            this.dgvTienDo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTienDo.Location = new System.Drawing.Point(28, 30);
            this.dgvTienDo.Name = "dgvTienDo";
            this.dgvTienDo.RowHeadersWidth = 62;
            this.dgvTienDo.RowTemplate.Height = 28;
            this.dgvTienDo.Size = new System.Drawing.Size(591, 570);
            this.dgvTienDo.TabIndex = 0;
            this.dgvTienDo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTienDo_CellContentClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1230, 621);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Lớp&Khoa";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1230, 621);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Team";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1230, 621);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Thông tin";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(358, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // fTeacherGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 775);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "fTeacherGui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fTeacherGui";
            this.Load += new System.EventHandler(this.fTeacherGui_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDoAn)).EndInit();
            this.tabTienDo.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabTienDo;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvDanhSachDoAn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenDoAn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvTienDo;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLamMoi_TienDo;
        private System.Windows.Forms.Button btnTimKiem_TienDo;
        private System.Windows.Forms.TextBox txtTenDoAn_TienDo;
        private System.Windows.Forms.Button btnXemNhom_TienDo;
        private System.Windows.Forms.Button btnChamDiem_TienDo;
        private System.Windows.Forms.Button btnKiemTra_TienDo;
    }
}