
namespace TakeCarePage
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.headerPanel = new System.Windows.Forms.Panel();
            this.infoSoftwareBtn = new AdsManager.VBButton();
            this.dataTKQCBtn = new AdsManager.VBButton();
            this.accBtn = new AdsManager.VBButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerPanel.AutoSize = true;
            this.headerPanel.Controls.Add(this.infoSoftwareBtn);
            this.headerPanel.Controls.Add(this.dataTKQCBtn);
            this.headerPanel.Controls.Add(this.accBtn);
            this.headerPanel.Controls.Add(this.pictureBox1);
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(4);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(884, 85);
            this.headerPanel.TabIndex = 3;
            // 
            // infoSoftwareBtn
            // 
            this.infoSoftwareBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.infoSoftwareBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.infoSoftwareBtn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.infoSoftwareBtn.BorderRadius = 20;
            this.infoSoftwareBtn.BorderSize = 0;
            this.infoSoftwareBtn.FlatAppearance.BorderSize = 0;
            this.infoSoftwareBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.infoSoftwareBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoSoftwareBtn.ForeColor = System.Drawing.Color.Black;
            this.infoSoftwareBtn.Location = new System.Drawing.Point(417, 15);
            this.infoSoftwareBtn.Name = "infoSoftwareBtn";
            this.infoSoftwareBtn.Size = new System.Drawing.Size(218, 40);
            this.infoSoftwareBtn.TabIndex = 6;
            this.infoSoftwareBtn.Text = "THÔNG TIN PHẦN MỀM";
            this.infoSoftwareBtn.TextColor = System.Drawing.Color.Black;
            this.infoSoftwareBtn.UseVisualStyleBackColor = false;
            this.infoSoftwareBtn.Click += new System.EventHandler(this.infoSoftwareBtn_Click);
            // 
            // dataTKQCBtn
            // 
            this.dataTKQCBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dataTKQCBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dataTKQCBtn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.dataTKQCBtn.BorderRadius = 20;
            this.dataTKQCBtn.BorderSize = 0;
            this.dataTKQCBtn.FlatAppearance.BorderSize = 0;
            this.dataTKQCBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataTKQCBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataTKQCBtn.ForeColor = System.Drawing.Color.Black;
            this.dataTKQCBtn.Location = new System.Drawing.Point(261, 15);
            this.dataTKQCBtn.Name = "dataTKQCBtn";
            this.dataTKQCBtn.Size = new System.Drawing.Size(142, 40);
            this.dataTKQCBtn.TabIndex = 5;
            this.dataTKQCBtn.Text = "DỮ LIỆU TKQC";
            this.dataTKQCBtn.TextColor = System.Drawing.Color.Black;
            this.dataTKQCBtn.UseVisualStyleBackColor = false;
            this.dataTKQCBtn.Click += new System.EventHandler(this.dataTKQCBtn_Click);
            // 
            // accBtn
            // 
            this.accBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.accBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.accBtn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.accBtn.BorderRadius = 20;
            this.accBtn.BorderSize = 0;
            this.accBtn.FlatAppearance.BorderSize = 0;
            this.accBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.accBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accBtn.ForeColor = System.Drawing.Color.White;
            this.accBtn.Location = new System.Drawing.Point(105, 15);
            this.accBtn.Name = "accBtn";
            this.accBtn.Size = new System.Drawing.Size(142, 40);
            this.accBtn.TabIndex = 4;
            this.accBtn.Text = "ACCOUNT";
            this.accBtn.TextColor = System.Drawing.Color.White;
            this.accBtn.UseVisualStyleBackColor = false;
            this.accBtn.Click += new System.EventHandler(this.accBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AdsManager.Properties.Resources.log_exis_co_do_bong1;
            this.pictureBox1.Location = new System.Drawing.Point(3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(83, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // contentPanel
            // 
            this.contentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentPanel.AutoSize = true;
            this.contentPanel.Location = new System.Drawing.Point(0, 80);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(884, 480);
            this.contentPanel.TabIndex = 4;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.headerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AdsManager.VBButton infoSoftwareBtn;
        private AdsManager.VBButton dataTKQCBtn;
        private AdsManager.VBButton accBtn;
        private System.Windows.Forms.Panel contentPanel;
    }
}

