namespace LTTQ_G2_2025.GUI
{
    partial class fManagerAMinGUI
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
            this.tbAdmin = new System.Windows.Forms.TabControl();
            this.tbAccount = new System.Windows.Forms.TabPage();
            this.tbTeacher = new System.Windows.Forms.TabPage();
            this.tbStudent = new System.Windows.Forms.TabPage();
            this.tbClass = new System.Windows.Forms.TabPage();
            this.tbGrid = new System.Windows.Forms.TabPage();
            this.tbCouncil = new System.Windows.Forms.TabPage();
            this.tbReport = new System.Windows.Forms.TabPage();
            this.tbAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbAdmin
            // 
            this.tbAdmin.Controls.Add(this.tbAccount);
            this.tbAdmin.Controls.Add(this.tbTeacher);
            this.tbAdmin.Controls.Add(this.tbStudent);
            this.tbAdmin.Controls.Add(this.tbClass);
            this.tbAdmin.Controls.Add(this.tbGrid);
            this.tbAdmin.Controls.Add(this.tbCouncil);
            this.tbAdmin.Controls.Add(this.tbReport);
            this.tbAdmin.Location = new System.Drawing.Point(1, 13);
            this.tbAdmin.Name = "tbAdmin";
            this.tbAdmin.SelectedIndex = 0;
            this.tbAdmin.Size = new System.Drawing.Size(1102, 588);
            this.tbAdmin.TabIndex = 0;
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(4, 25);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Padding = new System.Windows.Forms.Padding(3);
            this.tbAccount.Size = new System.Drawing.Size(1094, 559);
            this.tbAccount.TabIndex = 0;
            this.tbAccount.Text = "Tài khoản";
            this.tbAccount.UseVisualStyleBackColor = true;
            // 
            // tbTeacher
            // 
            this.tbTeacher.Location = new System.Drawing.Point(4, 25);
            this.tbTeacher.Name = "tbTeacher";
            this.tbTeacher.Padding = new System.Windows.Forms.Padding(3);
            this.tbTeacher.Size = new System.Drawing.Size(1094, 526);
            this.tbTeacher.TabIndex = 1;
            this.tbTeacher.Text = "Giáo viên";
            this.tbTeacher.UseVisualStyleBackColor = true;
            // 
            // tbStudent
            // 
            this.tbStudent.Location = new System.Drawing.Point(5, 26);
            this.tbStudent.Name = "tbStudent";
            this.tbStudent.Padding = new System.Windows.Forms.Padding(3);
            this.tbStudent.Size = new System.Drawing.Size(1094, 526);
            this.tbStudent.TabIndex = 2;
            this.tbStudent.Text = "Sinh Viên";
            this.tbStudent.UseVisualStyleBackColor = true;
            // 
            // tbClass
            // 
            this.tbClass.Location = new System.Drawing.Point(4, 25);
            this.tbClass.Name = "tbClass";
            this.tbClass.Padding = new System.Windows.Forms.Padding(3);
            this.tbClass.Size = new System.Drawing.Size(1094, 526);
            this.tbClass.TabIndex = 3;
            this.tbClass.Text = "Lớp & Khoa";
            this.tbClass.UseVisualStyleBackColor = true;
            // 
            // tbGrid
            // 
            this.tbGrid.Location = new System.Drawing.Point(4, 25);
            this.tbGrid.Name = "tbGrid";
            this.tbGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tbGrid.Size = new System.Drawing.Size(1094, 526);
            this.tbGrid.TabIndex = 4;
            this.tbGrid.Text = "Đề tài";
            this.tbGrid.UseVisualStyleBackColor = true;
            // 
            // tbCouncil
            // 
            this.tbCouncil.Location = new System.Drawing.Point(4, 25);
            this.tbCouncil.Name = "tbCouncil";
            this.tbCouncil.Padding = new System.Windows.Forms.Padding(3);
            this.tbCouncil.Size = new System.Drawing.Size(1094, 526);
            this.tbCouncil.TabIndex = 5;
            this.tbCouncil.Text = "Hội đồng";
            this.tbCouncil.UseVisualStyleBackColor = true;
            // 
            // tbReport
            // 
            this.tbReport.Location = new System.Drawing.Point(4, 25);
            this.tbReport.Name = "tbReport";
            this.tbReport.Padding = new System.Windows.Forms.Padding(3);
            this.tbReport.Size = new System.Drawing.Size(1094, 526);
            this.tbReport.TabIndex = 6;
            this.tbReport.Text = "Báo cáo";
            this.tbReport.UseVisualStyleBackColor = true;
            // 
            // fManagerAMinGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 612);
            this.Controls.Add(this.tbAdmin);
            this.Name = "fManagerAMinGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fManagerAMinGUI";
            this.tbAdmin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tbAdmin;
        private System.Windows.Forms.TabPage tbAccount;
        private System.Windows.Forms.TabPage tbTeacher;
        private System.Windows.Forms.TabPage tbStudent;
        private System.Windows.Forms.TabPage tbClass;
        private System.Windows.Forms.TabPage tbGrid;
        private System.Windows.Forms.TabPage tbCouncil;
        private System.Windows.Forms.TabPage tbReport;
    }
}