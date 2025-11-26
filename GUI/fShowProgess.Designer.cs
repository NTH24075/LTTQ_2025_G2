namespace LTTQ_G2_2025.GUI
{
    partial class fShowProgess
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
            this.lblTenDoAn = new System.Windows.Forms.Label();
            this.lblStage1 = new System.Windows.Forms.Label();
            this.lblStage2 = new System.Windows.Forms.Label();
            this.lblStage3 = new System.Windows.Forms.Label();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.lblStatus3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTenDoAn
            // 
            this.lblTenDoAn.AutoSize = true;
            this.lblTenDoAn.Location = new System.Drawing.Point(112, 34);
            this.lblTenDoAn.Name = "lblTenDoAn";
            this.lblTenDoAn.Size = new System.Drawing.Size(51, 20);
            this.lblTenDoAn.TabIndex = 1;
            this.lblTenDoAn.Text = "label2";
            // 
            // lblStage1
            // 
            this.lblStage1.AutoSize = true;
            this.lblStage1.Location = new System.Drawing.Point(43, 129);
            this.lblStage1.Name = "lblStage1";
            this.lblStage1.Size = new System.Drawing.Size(90, 20);
            this.lblStage1.TabIndex = 2;
            this.lblStage1.Text = "Giai đoạn 1";
            // 
            // lblStage2
            // 
            this.lblStage2.AutoSize = true;
            this.lblStage2.Location = new System.Drawing.Point(43, 195);
            this.lblStage2.Name = "lblStage2";
            this.lblStage2.Size = new System.Drawing.Size(90, 20);
            this.lblStage2.TabIndex = 3;
            this.lblStage2.Text = "Giai đoạn 2";
            // 
            // lblStage3
            // 
            this.lblStage3.AutoSize = true;
            this.lblStage3.Location = new System.Drawing.Point(43, 262);
            this.lblStage3.Name = "lblStage3";
            this.lblStage3.Size = new System.Drawing.Size(90, 20);
            this.lblStage3.TabIndex = 4;
            this.lblStage3.Text = "Giai đoạn 3";
            // 
            // lblStatus1
            // 
            this.lblStatus1.AutoSize = true;
            this.lblStatus1.Location = new System.Drawing.Point(166, 129);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(51, 20);
            this.lblStatus1.TabIndex = 5;
            this.lblStatus1.Text = "label3";
            // 
            // lblStatus2
            // 
            this.lblStatus2.AutoSize = true;
            this.lblStatus2.Location = new System.Drawing.Point(166, 195);
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(51, 20);
            this.lblStatus2.TabIndex = 6;
            this.lblStatus2.Text = "label4";
            // 
            // lblStatus3
            // 
            this.lblStatus3.AutoSize = true;
            this.lblStatus3.Location = new System.Drawing.Point(166, 262);
            this.lblStatus3.Name = "lblStatus3";
            this.lblStatus3.Size = new System.Drawing.Size(51, 20);
            this.lblStatus3.TabIndex = 7;
            this.lblStatus3.Text = "label5";
            // 
            // fShowProgess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 446);
            this.Controls.Add(this.lblStatus3);
            this.Controls.Add(this.lblStatus2);
            this.Controls.Add(this.lblStatus1);
            this.Controls.Add(this.lblStage3);
            this.Controls.Add(this.lblStage2);
            this.Controls.Add(this.lblStage1);
            this.Controls.Add(this.lblTenDoAn);
            this.Name = "fShowProgess";
            this.Text = "fShowProgess";
            this.Load += new System.EventHandler(this.fShowProgess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTenDoAn;
        private System.Windows.Forms.Label lblStage1;
        private System.Windows.Forms.Label lblStage2;
        private System.Windows.Forms.Label lblStage3;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.Label lblStatus2;
        private System.Windows.Forms.Label lblStatus3;
    }
}