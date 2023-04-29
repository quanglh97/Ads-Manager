namespace AdsManager.form.dataTKQC
{
    partial class ThongTinBM
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.TTBM_RefreshBtn = new AdsManager.VBButton();
            this.TTBMTotalBMBtn = new AdsManager.VBButton();
            this.ThongTinBMdataGridView = new System.Windows.Forms.DataGridView();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoaiBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Limit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Verified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Created = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLAdmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdminAn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChatLuongBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.elipseControl1 = new TakeCarePage.ElipseControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.locTimeBtn = new AdsManager.VBButton();
            this.SearchTBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.from_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SearchBtn = new AdsManager.VBButton();
            this.vbButton4 = new AdsManager.VBButton();
            this.to_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.loadingBtnTTBM = new AdsManager.VBButton();
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinBMdataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Location = new System.Drawing.Point(12, 699);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(332, 47);
            this.panel2.TabIndex = 37;
            // 
            // TTBM_RefreshBtn
            // 
            this.TTBM_RefreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TTBM_RefreshBtn.BackColor = System.Drawing.SystemColors.Window;
            this.TTBM_RefreshBtn.BackgroundColor = System.Drawing.SystemColors.Window;
            this.TTBM_RefreshBtn.BorderColor = System.Drawing.SystemColors.WindowText;
            this.TTBM_RefreshBtn.BorderRadius = 19;
            this.TTBM_RefreshBtn.BorderSize = 1;
            this.TTBM_RefreshBtn.FlatAppearance.BorderSize = 0;
            this.TTBM_RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TTBM_RefreshBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TTBM_RefreshBtn.ForeColor = System.Drawing.Color.Black;
            this.TTBM_RefreshBtn.Image = global::AdsManager.Properties.Resources.reload2;
            this.TTBM_RefreshBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TTBM_RefreshBtn.Location = new System.Drawing.Point(21, 2);
            this.TTBM_RefreshBtn.Name = "TTBM_RefreshBtn";
            this.TTBM_RefreshBtn.Size = new System.Drawing.Size(150, 33);
            this.TTBM_RefreshBtn.TabIndex = 43;
            this.TTBM_RefreshBtn.Text = "    Làm mới dữ liệu";
            this.TTBM_RefreshBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TTBM_RefreshBtn.TextColor = System.Drawing.Color.Black;
            this.TTBM_RefreshBtn.UseVisualStyleBackColor = false;
            this.TTBM_RefreshBtn.Click += new System.EventHandler(this.TTBM_RefreshBtn_Click);
            // 
            // TTBMTotalBMBtn
            // 
            this.TTBMTotalBMBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(65)))), ((int)(((byte)(24)))));
            this.TTBMTotalBMBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(65)))), ((int)(((byte)(24)))));
            this.TTBMTotalBMBtn.BorderColor = System.Drawing.Color.Purple;
            this.TTBMTotalBMBtn.BorderRadius = 20;
            this.TTBMTotalBMBtn.BorderSize = 1;
            this.TTBMTotalBMBtn.Enabled = false;
            this.TTBMTotalBMBtn.FlatAppearance.BorderSize = 0;
            this.TTBMTotalBMBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TTBMTotalBMBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TTBMTotalBMBtn.ForeColor = System.Drawing.Color.White;
            this.TTBMTotalBMBtn.Location = new System.Drawing.Point(177, 2);
            this.TTBMTotalBMBtn.Name = "TTBMTotalBMBtn";
            this.TTBMTotalBMBtn.Size = new System.Drawing.Size(173, 33);
            this.TTBMTotalBMBtn.TabIndex = 41;
            this.TTBMTotalBMBtn.Text = "TỔNG SỐ BM: 0 ";
            this.TTBMTotalBMBtn.TextColor = System.Drawing.Color.White;
            this.TTBMTotalBMBtn.UseVisualStyleBackColor = false;
            // 
            // ThongTinBMdataGridView
            // 
            this.ThongTinBMdataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ThongTinBMdataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ThongTinBMdataGridView.ColumnHeadersHeight = 41;
            this.ThongTinBMdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TrangThai,
            this.BM,
            this.LoaiBM,
            this.Role,
            this.Limit,
            this.Verified,
            this.Created,
            this.IDBM,
            this.SLAdmin,
            this.AdminAn,
            this.SLAccount,
            this.ChatLuongBM});
            this.ThongTinBMdataGridView.EnableHeadersVisualStyles = false;
            this.ThongTinBMdataGridView.Location = new System.Drawing.Point(12, 55);
            this.ThongTinBMdataGridView.Name = "ThongTinBMdataGridView";
            this.ThongTinBMdataGridView.ReadOnly = true;
            this.ThongTinBMdataGridView.Size = new System.Drawing.Size(1591, 638);
            this.ThongTinBMdataGridView.TabIndex = 38;
            this.ThongTinBMdataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.ThongTinBMdataGridView_RowPrePaint);
            this.ThongTinBMdataGridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.ThongTinBMdataGridView_SortCompare);
            // 
            // TrangThai
            // 
            this.TrangThai.HeaderText = "Trạng Thái";
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.ReadOnly = true;
            this.TrangThai.Width = 150;
            // 
            // BM
            // 
            this.BM.HeaderText = "BM";
            this.BM.Name = "BM";
            this.BM.ReadOnly = true;
            this.BM.Width = 200;
            // 
            // LoaiBM
            // 
            this.LoaiBM.HeaderText = "Loại BM";
            this.LoaiBM.Name = "LoaiBM";
            this.LoaiBM.ReadOnly = true;
            this.LoaiBM.Width = 200;
            // 
            // Role
            // 
            this.Role.HeaderText = "Quyền";
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            this.Role.Width = 150;
            // 
            // Limit
            // 
            this.Limit.HeaderText = "Limit";
            this.Limit.Name = "Limit";
            this.Limit.ReadOnly = true;
            this.Limit.Width = 150;
            // 
            // Verified
            // 
            this.Verified.HeaderText = "Verified";
            this.Verified.Name = "Verified";
            this.Verified.ReadOnly = true;
            this.Verified.Width = 150;
            // 
            // Created
            // 
            this.Created.HeaderText = "Created";
            this.Created.Name = "Created";
            this.Created.ReadOnly = true;
            this.Created.Width = 200;
            // 
            // IDBM
            // 
            this.IDBM.HeaderText = "ID BM";
            this.IDBM.Name = "IDBM";
            this.IDBM.ReadOnly = true;
            this.IDBM.Width = 200;
            // 
            // SLAdmin
            // 
            this.SLAdmin.HeaderText = "SL Admin";
            this.SLAdmin.Name = "SLAdmin";
            this.SLAdmin.ReadOnly = true;
            this.SLAdmin.Width = 150;
            // 
            // AdminAn
            // 
            this.AdminAn.HeaderText = "Admin ẩn";
            this.AdminAn.Name = "AdminAn";
            this.AdminAn.ReadOnly = true;
            this.AdminAn.Width = 150;
            // 
            // SLAccount
            // 
            this.SLAccount.HeaderText = "SL Account";
            this.SLAccount.Name = "SLAccount";
            this.SLAccount.ReadOnly = true;
            this.SLAccount.Width = 150;
            // 
            // ChatLuongBM
            // 
            this.ChatLuongBM.HeaderText = "Chất lượng BM";
            this.ChatLuongBM.Name = "ChatLuongBM";
            this.ChatLuongBM.ReadOnly = true;
            this.ChatLuongBM.Width = 150;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(1230, 719);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 34);
            this.panel1.TabIndex = 39;
            this.panel1.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 14);
            this.label1.TabIndex = 24;
            this.label1.Text = "Số lượng";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(189, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(115, 22);
            this.comboBox1.TabIndex = 23;
            this.comboBox1.Text = "Row";
            // 
            // elipseControl1
            // 
            this.elipseControl1.CornerRadius = 15;
            this.elipseControl1.TargetControl = this.comboBox1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.TTBM_RefreshBtn);
            this.panel3.Controls.Add(this.locTimeBtn);
            this.panel3.Controls.Add(this.TTBMTotalBMBtn);
            this.panel3.Controls.Add(this.SearchTBox);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.from_dateTimePicker);
            this.panel3.Controls.Add(this.SearchBtn);
            this.panel3.Controls.Add(this.vbButton4);
            this.panel3.Controls.Add(this.to_dateTimePicker);
            this.panel3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(267, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1336, 37);
            this.panel3.TabIndex = 46;
            // 
            // locTimeBtn
            // 
            this.locTimeBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.locTimeBtn.BackColor = System.Drawing.Color.Green;
            this.locTimeBtn.BackgroundColor = System.Drawing.Color.Green;
            this.locTimeBtn.BorderColor = System.Drawing.SystemColors.WindowText;
            this.locTimeBtn.BorderRadius = 20;
            this.locTimeBtn.BorderSize = 0;
            this.locTimeBtn.FlatAppearance.BorderSize = 0;
            this.locTimeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.locTimeBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locTimeBtn.ForeColor = System.Drawing.Color.Black;
            this.locTimeBtn.Location = new System.Drawing.Point(1152, 4);
            this.locTimeBtn.Name = "locTimeBtn";
            this.locTimeBtn.Size = new System.Drawing.Size(61, 31);
            this.locTimeBtn.TabIndex = 27;
            this.locTimeBtn.Text = "Lọc";
            this.locTimeBtn.TextColor = System.Drawing.Color.Black;
            this.locTimeBtn.UseVisualStyleBackColor = false;
            this.locTimeBtn.Click += new System.EventHandler(this.locTimeBtn_Click);
            // 
            // SearchTBox
            // 
            this.SearchTBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SearchTBox.BackColor = System.Drawing.Color.Gainsboro;
            this.SearchTBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTBox.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchTBox.Location = new System.Drawing.Point(535, 7);
            this.SearchTBox.MaximumSize = new System.Drawing.Size(350, 25);
            this.SearchTBox.MinimumSize = new System.Drawing.Size(316, 25);
            this.SearchTBox.Name = "SearchTBox";
            this.SearchTBox.Size = new System.Drawing.Size(316, 25);
            this.SearchTBox.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1014, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 14);
            this.label3.TabIndex = 14;
            this.label3.Text = "Đến";
            // 
            // from_dateTimePicker
            // 
            this.from_dateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.from_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.from_dateTimePicker.Location = new System.Drawing.Point(923, 9);
            this.from_dateTimePicker.Name = "from_dateTimePicker";
            this.from_dateTimePicker.Size = new System.Drawing.Size(81, 20);
            this.from_dateTimePicker.TabIndex = 13;
            // 
            // SearchBtn
            // 
            this.SearchBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SearchBtn.BackColor = System.Drawing.Color.Green;
            this.SearchBtn.BackgroundColor = System.Drawing.Color.Green;
            this.SearchBtn.BorderColor = System.Drawing.SystemColors.WindowText;
            this.SearchBtn.BorderRadius = 20;
            this.SearchBtn.BorderSize = 0;
            this.SearchBtn.FlatAppearance.BorderSize = 0;
            this.SearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchBtn.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBtn.ForeColor = System.Drawing.Color.Black;
            this.SearchBtn.Image = global::AdsManager.Properties.Resources.Screenshot_2022_10_01_024949_removebg_preview1;
            this.SearchBtn.Location = new System.Drawing.Point(857, 4);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(56, 31);
            this.SearchBtn.TabIndex = 12;
            this.SearchBtn.TextColor = System.Drawing.Color.Black;
            this.SearchBtn.UseVisualStyleBackColor = false;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // vbButton4
            // 
            this.vbButton4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.vbButton4.BackColor = System.Drawing.Color.Green;
            this.vbButton4.BackgroundColor = System.Drawing.Color.Green;
            this.vbButton4.BorderColor = System.Drawing.SystemColors.WindowText;
            this.vbButton4.BorderRadius = 20;
            this.vbButton4.BorderSize = 0;
            this.vbButton4.FlatAppearance.BorderSize = 0;
            this.vbButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton4.ForeColor = System.Drawing.Color.Black;
            this.vbButton4.Location = new System.Drawing.Point(1219, 3);
            this.vbButton4.Name = "vbButton4";
            this.vbButton4.Size = new System.Drawing.Size(108, 31);
            this.vbButton4.TabIndex = 1;
            this.vbButton4.Text = "Xuất Excel";
            this.vbButton4.TextColor = System.Drawing.Color.Black;
            this.vbButton4.UseVisualStyleBackColor = false;
            this.vbButton4.Click += new System.EventHandler(this.vbButton4_Click);
            // 
            // to_dateTimePicker
            // 
            this.to_dateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.to_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.to_dateTimePicker.Location = new System.Drawing.Point(1059, 9);
            this.to_dateTimePicker.Name = "to_dateTimePicker";
            this.to_dateTimePicker.Size = new System.Drawing.Size(87, 20);
            this.to_dateTimePicker.TabIndex = 11;
            // 
            // loadingBtnTTBM
            // 
            this.loadingBtnTTBM.BackColor = System.Drawing.SystemColors.Window;
            this.loadingBtnTTBM.BackgroundColor = System.Drawing.SystemColors.Window;
            this.loadingBtnTTBM.BorderColor = System.Drawing.Color.DarkOrange;
            this.loadingBtnTTBM.BorderRadius = 40;
            this.loadingBtnTTBM.BorderSize = 2;
            this.loadingBtnTTBM.FlatAppearance.BorderSize = 0;
            this.loadingBtnTTBM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadingBtnTTBM.ForeColor = System.Drawing.Color.White;
            this.loadingBtnTTBM.Image = global::AdsManager.Properties.Resources.loading1;
            this.loadingBtnTTBM.Location = new System.Drawing.Point(833, 227);
            this.loadingBtnTTBM.Name = "loadingBtnTTBM";
            this.loadingBtnTTBM.Size = new System.Drawing.Size(249, 244);
            this.loadingBtnTTBM.TabIndex = 45;
            this.loadingBtnTTBM.Text = "vbButton3";
            this.loadingBtnTTBM.TextColor = System.Drawing.Color.White;
            this.loadingBtnTTBM.UseVisualStyleBackColor = false;
            this.loadingBtnTTBM.Visible = false;
            // 
            // ThongTinBM
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1615, 755);
            this.ControlBox = false;
            this.Controls.Add(this.loadingBtnTTBM);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ThongTinBMdataGridView);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ThongTinBM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinBMdataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView ThongTinBMdataGridView;
        private VBButton TTBMTotalBMBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private TakeCarePage.ElipseControl elipseControl1;
        private VBButton TTBM_RefreshBtn;
        private VBButton loadingBtnTTBM;
        private System.Windows.Forms.Panel panel3;
        private VBButton locTimeBtn;
        private System.Windows.Forms.TextBox SearchTBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker from_dateTimePicker;
        private VBButton SearchBtn;
        private VBButton vbButton4;
        private System.Windows.Forms.DateTimePicker to_dateTimePicker;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn BM;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoaiBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn Limit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Verified;
#pragma warning disable CS0108 // 'ThongTinBM.Created' hides inherited member 'Control.Created'. Use the new keyword if hiding was intended.
        private System.Windows.Forms.DataGridViewTextBoxColumn Created;
#pragma warning restore CS0108 // 'ThongTinBM.Created' hides inherited member 'Control.Created'. Use the new keyword if hiding was intended.
        private System.Windows.Forms.DataGridViewTextBoxColumn IDBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLAdmin;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdminAn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChatLuongBM;
    }
}