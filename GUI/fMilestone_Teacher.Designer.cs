namespace LTTQ_G2_2025.GUI
{
    partial class fMilestone_Teacher
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvDanhSachTienDo = new System.Windows.Forms.DataGridView();
            this.lblNoiDungBaoCaoTitle = new System.Windows.Forms.Label();
            this.lblFileBaoCaoTitle = new System.Windows.Forms.Label();
            this.lblTenFileBaoCao = new System.Windows.Forms.Label();
            this.txtNoiDungBaoCao = new System.Windows.Forms.TextBox();
            this.lblTenTienDoTitle = new System.Windows.Forms.Label();
            this.lblTenTienDo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDiem = new System.Windows.Forms.TextBox();
            this.txtPercent = new System.Windows.Forms.TextBox();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.txtNhanXet = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPercentTitle = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTaiFileBaoCao = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNhanXetTienDo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTienDo)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(172, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(269, 29);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Kiểm tra tiến độ đồ án";
            this.lblTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // dgvDanhSachTienDo
            // 
            this.dgvDanhSachTienDo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachTienDo.Location = new System.Drawing.Point(12, 89);
            this.dgvDanhSachTienDo.Name = "dgvDanhSachTienDo";
            this.dgvDanhSachTienDo.RowHeadersWidth = 62;
            this.dgvDanhSachTienDo.RowTemplate.Height = 28;
            this.dgvDanhSachTienDo.Size = new System.Drawing.Size(467, 474);
            this.dgvDanhSachTienDo.TabIndex = 0;
            this.dgvDanhSachTienDo.SelectionChanged += new System.EventHandler(this.dgvDanhSachTienDo_SelectionChanged);
            // 
            // lblNoiDungBaoCaoTitle
            // 
            this.lblNoiDungBaoCaoTitle.AutoSize = true;
            this.lblNoiDungBaoCaoTitle.Location = new System.Drawing.Point(33, 115);
            this.lblNoiDungBaoCaoTitle.Name = "lblNoiDungBaoCaoTitle";
            this.lblNoiDungBaoCaoTitle.Size = new System.Drawing.Size(133, 20);
            this.lblNoiDungBaoCaoTitle.TabIndex = 0;
            this.lblNoiDungBaoCaoTitle.Text = "Nội dung báo cáo";
            // 
            // lblFileBaoCaoTitle
            // 
            this.lblFileBaoCaoTitle.AutoSize = true;
            this.lblFileBaoCaoTitle.Location = new System.Drawing.Point(30, 233);
            this.lblFileBaoCaoTitle.Name = "lblFileBaoCaoTitle";
            this.lblFileBaoCaoTitle.Size = new System.Drawing.Size(99, 20);
            this.lblFileBaoCaoTitle.TabIndex = 1;
            this.lblFileBaoCaoTitle.Text = "File báo cáo:";
            // 
            // lblTenFileBaoCao
            // 
            this.lblTenFileBaoCao.AutoSize = true;
            this.lblTenFileBaoCao.Location = new System.Drawing.Point(144, 233);
            this.lblTenFileBaoCao.Name = "lblTenFileBaoCao";
            this.lblTenFileBaoCao.Size = new System.Drawing.Size(148, 20);
            this.lblTenFileBaoCao.TabIndex = 2;
            this.lblTenFileBaoCao.Text = "Hiển thị file báo cáo";
            // 
            // txtNoiDungBaoCao
            // 
            this.txtNoiDungBaoCao.Location = new System.Drawing.Point(34, 149);
            this.txtNoiDungBaoCao.Multiline = true;
            this.txtNoiDungBaoCao.Name = "txtNoiDungBaoCao";
            this.txtNoiDungBaoCao.ReadOnly = true;
            this.txtNoiDungBaoCao.Size = new System.Drawing.Size(367, 72);
            this.txtNoiDungBaoCao.TabIndex = 3;
            this.txtNoiDungBaoCao.TextChanged += new System.EventHandler(this.txtNoiDungBaoCao_TextChanged);
            // 
            // lblTenTienDoTitle
            // 
            this.lblTenTienDoTitle.AutoSize = true;
            this.lblTenTienDoTitle.Location = new System.Drawing.Point(30, 73);
            this.lblTenTienDoTitle.Name = "lblTenTienDoTitle";
            this.lblTenTienDoTitle.Size = new System.Drawing.Size(92, 20);
            this.lblTenTienDoTitle.TabIndex = 4;
            this.lblTenTienDoTitle.Text = "Tên tiến độ:";
            // 
            // lblTenTienDo
            // 
            this.lblTenTienDo.AutoSize = true;
            this.lblTenTienDo.Location = new System.Drawing.Point(128, 73);
            this.lblTenTienDo.Name = "lblTenTienDo";
            this.lblTenTienDo.Size = new System.Drawing.Size(90, 20);
            this.lblTenTienDo.TabIndex = 5;
            this.lblTenTienDo.Text = "Hiển thị tên";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtNhanXetTienDo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtDiem);
            this.panel1.Controls.Add(this.txtPercent);
            this.panel1.Controls.Add(this.btnThoat);
            this.panel1.Controls.Add(this.btnXacNhan);
            this.panel1.Controls.Add(this.txtNhanXet);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblPercentTitle);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(563, 440);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(479, 279);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtDiem
            // 
            this.txtDiem.Location = new System.Drawing.Point(163, 126);
            this.txtDiem.Name = "txtDiem";
            this.txtDiem.Size = new System.Drawing.Size(101, 26);
            this.txtDiem.TabIndex = 10;
            // 
            // txtPercent
            // 
            this.txtPercent.Location = new System.Drawing.Point(163, 46);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.Size = new System.Drawing.Size(105, 26);
            this.txtPercent.TabIndex = 9;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(282, 228);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(126, 37);
            this.btnThoat.TabIndex = 8;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Location = new System.Drawing.Point(111, 228);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(126, 37);
            this.btnXacNhan.TabIndex = 7;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // txtNhanXet
            // 
            this.txtNhanXet.Location = new System.Drawing.Point(163, 163);
            this.txtNhanXet.Multiline = true;
            this.txtNhanXet.Name = "txtNhanXet";
            this.txtNhanXet.Size = new System.Drawing.Size(279, 36);
            this.txtNhanXet.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(33, 177);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Nhận xét";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Điểm";
            // 
            // lblPercentTitle
            // 
            this.lblPercentTitle.AutoSize = true;
            this.lblPercentTitle.Location = new System.Drawing.Point(30, 49);
            this.lblPercentTitle.Name = "lblPercentTitle";
            this.lblPercentTitle.Size = new System.Drawing.Size(75, 20);
            this.lblPercentTitle.TabIndex = 1;
            this.lblPercentTitle.Text = "% tiến độ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(94, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(314, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "Chấm điểm/Nhận xét về tiến độ";
            // 
            // btnTaiFileBaoCao
            // 
            this.btnTaiFileBaoCao.Location = new System.Drawing.Point(88, 274);
            this.btnTaiFileBaoCao.Name = "btnTaiFileBaoCao";
            this.btnTaiFileBaoCao.Size = new System.Drawing.Size(126, 37);
            this.btnTaiFileBaoCao.TabIndex = 6;
            this.btnTaiFileBaoCao.Text = "Tải file";
            this.btnTaiFileBaoCao.UseVisualStyleBackColor = true;
            this.btnTaiFileBaoCao.Click += new System.EventHandler(this.btnTaiFile_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dgvDanhSachTienDo);
            this.panel2.Location = new System.Drawing.Point(16, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(517, 629);
            this.panel2.TabIndex = 4;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(75, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Danh sách tiến độ";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btnTaiFileBaoCao);
            this.panel3.Controls.Add(this.lblTenFileBaoCao);
            this.panel3.Controls.Add(this.lblTenTienDo);
            this.panel3.Controls.Add(this.txtNoiDungBaoCao);
            this.panel3.Controls.Add(this.lblFileBaoCaoTitle);
            this.panel3.Controls.Add(this.lblNoiDungBaoCaoTitle);
            this.panel3.Controls.Add(this.lblTenTienDoTitle);
            this.panel3.Location = new System.Drawing.Point(563, 76);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(478, 335);
            this.panel3.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(144, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Chi tiết tiến độ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Nhận xét tiến độ";
            // 
            // txtNhanXetTienDo
            // 
            this.txtNhanXetTienDo.Location = new System.Drawing.Point(163, 83);
            this.txtNhanXetTienDo.Name = "txtNhanXetTienDo";
            this.txtNhanXetTienDo.Size = new System.Drawing.Size(272, 26);
            this.txtNhanXetTienDo.TabIndex = 12;
            // 
            // fMilestone_Teacher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 731);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.MaximizeBox = false;
            this.Name = "fMilestone_Teacher";
            this.Text = "fMilestone_Teacher";
            this.Load += new System.EventHandler(this.fMilestone_Teacher_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTienDo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvDanhSachTienDo;
        private System.Windows.Forms.TextBox txtNoiDungBaoCao;
        private System.Windows.Forms.Label lblTenFileBaoCao;
        private System.Windows.Forms.Label lblFileBaoCaoTitle;
        private System.Windows.Forms.Label lblNoiDungBaoCaoTitle;
        private System.Windows.Forms.Button btnTaiFileBaoCao;
        private System.Windows.Forms.Label lblTenTienDo;
        private System.Windows.Forms.Label lblTenTienDoTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.TextBox txtNhanXet;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPercentTitle;
        private System.Windows.Forms.TextBox txtDiem;
        private System.Windows.Forms.TextBox txtPercent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNhanXetTienDo;
        private System.Windows.Forms.Label label4;
    }
}