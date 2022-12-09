namespace AdsManager.form.dataTKQC
{
    partial class DuLieuPageForm
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
            this.SearchTBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.from_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.to_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.DuLieuPageDataGridView = new System.Windows.Forms.DataGridView();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenTKQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDTKQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenPage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDPage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LuotNguoiTiepCan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiPhiQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TienTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkPage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.loadingBtn = new AdsManager.VBButton();
            this.DuLieuPage_RefreshBtn = new AdsManager.VBButton();
            this.locTimeBtn = new AdsManager.VBButton();
            this.searchBtn = new AdsManager.VBButton();
            this.vbButton4 = new AdsManager.VBButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DuLieuPageDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.locTimeBtn);
            this.panel2.Controls.Add(this.SearchTBox);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.from_dateTimePicker);
            this.panel2.Controls.Add(this.searchBtn);
            this.panel2.Controls.Add(this.vbButton4);
            this.panel2.Controls.Add(this.to_dateTimePicker);
            this.panel2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(560, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(905, 37);
            this.panel2.TabIndex = 51;
            // 
            // SearchTBox
            // 
            this.SearchTBox.BackColor = System.Drawing.Color.Gainsboro;
            this.SearchTBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTBox.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchTBox.Location = new System.Drawing.Point(6, 6);
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
            this.label3.Location = new System.Drawing.Point(485, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 14);
            this.label3.TabIndex = 14;
            this.label3.Text = "Đến";
            // 
            // from_dateTimePicker
            // 
            this.from_dateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.from_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.from_dateTimePicker.Location = new System.Drawing.Point(394, 8);
            this.from_dateTimePicker.Name = "from_dateTimePicker";
            this.from_dateTimePicker.Size = new System.Drawing.Size(81, 20);
            this.from_dateTimePicker.TabIndex = 13;
            // 
            // to_dateTimePicker
            // 
            this.to_dateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.to_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.to_dateTimePicker.Location = new System.Drawing.Point(530, 8);
            this.to_dateTimePicker.Name = "to_dateTimePicker";
            this.to_dateTimePicker.Size = new System.Drawing.Size(87, 20);
            this.to_dateTimePicker.TabIndex = 11;
            // 
            // DuLieuPageDataGridView
            // 
            this.DuLieuPageDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DuLieuPageDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DuLieuPageDataGridView.ColumnHeadersHeight = 40;
            this.DuLieuPageDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TrangThai,
            this.TenTKQC,
            this.IDTKQC,
            this.TenPage,
            this.IDPage,
            this.LuotNguoiTiepCan,
            this.CPM,
            this.CTR,
            this.ChiPhiQC,
            this.TienTe,
            this.LinkPage});
            this.DuLieuPageDataGridView.EnableHeadersVisualStyles = false;
            this.DuLieuPageDataGridView.Location = new System.Drawing.Point(15, 55);
            this.DuLieuPageDataGridView.Name = "DuLieuPageDataGridView";
            this.DuLieuPageDataGridView.ReadOnly = true;
            this.DuLieuPageDataGridView.RowHeadersWidth = 51;
            this.DuLieuPageDataGridView.Size = new System.Drawing.Size(1450, 704);
            this.DuLieuPageDataGridView.TabIndex = 52;
            this.DuLieuPageDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DuLieuPageDataGridView_CellFormatting);
            this.DuLieuPageDataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DuLieuPageDataGridView_RowPrePaint);
            this.DuLieuPageDataGridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.DuLieuPageDataGridView_SortCompare);
            // 
            // TrangThai
            // 
            this.TrangThai.HeaderText = "Trạng Thái";
            this.TrangThai.MinimumWidth = 6;
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.ReadOnly = true;
            this.TrangThai.Width = 125;
            // 
            // TenTKQC
            // 
            this.TenTKQC.HeaderText = "Tên TKQC";
            this.TenTKQC.MinimumWidth = 6;
            this.TenTKQC.Name = "TenTKQC";
            this.TenTKQC.ReadOnly = true;
            this.TenTKQC.Width = 200;
            // 
            // IDTKQC
            // 
            this.IDTKQC.HeaderText = "ID TKQC";
            this.IDTKQC.MinimumWidth = 6;
            this.IDTKQC.Name = "IDTKQC";
            this.IDTKQC.ReadOnly = true;
            this.IDTKQC.Width = 150;
            // 
            // TenPage
            // 
            this.TenPage.HeaderText = "Tên Page";
            this.TenPage.MinimumWidth = 6;
            this.TenPage.Name = "TenPage";
            this.TenPage.ReadOnly = true;
            this.TenPage.Width = 200;
            // 
            // IDPage
            // 
            this.IDPage.HeaderText = "ID Page";
            this.IDPage.MinimumWidth = 6;
            this.IDPage.Name = "IDPage";
            this.IDPage.ReadOnly = true;
            this.IDPage.Width = 150;
            // 
            // LuotNguoiTiepCan
            // 
            this.LuotNguoiTiepCan.HeaderText = "Lượt người tiếp cận";
            this.LuotNguoiTiepCan.MinimumWidth = 6;
            this.LuotNguoiTiepCan.Name = "LuotNguoiTiepCan";
            this.LuotNguoiTiepCan.ReadOnly = true;
            this.LuotNguoiTiepCan.Width = 200;
            // 
            // CPM
            // 
            this.CPM.HeaderText = "CPM";
            this.CPM.MinimumWidth = 6;
            this.CPM.Name = "CPM";
            this.CPM.ReadOnly = true;
            this.CPM.Width = 150;
            // 
            // CTR
            // 
            this.CTR.HeaderText = "CTR(%)";
            this.CTR.MinimumWidth = 6;
            this.CTR.Name = "CTR";
            this.CTR.ReadOnly = true;
            this.CTR.Width = 200;
            // 
            // ChiPhiQC
            // 
            this.ChiPhiQC.HeaderText = "Chi phí QC";
            this.ChiPhiQC.MinimumWidth = 6;
            this.ChiPhiQC.Name = "ChiPhiQC";
            this.ChiPhiQC.ReadOnly = true;
            this.ChiPhiQC.Width = 150;
            // 
            // TienTe
            // 
            this.TienTe.HeaderText = "Tiền tệ";
            this.TienTe.MinimumWidth = 6;
            this.TienTe.Name = "TienTe";
            this.TienTe.ReadOnly = true;
            this.TienTe.Width = 125;
            // 
            // LinkPage
            // 
            this.LinkPage.HeaderText = "Link Page";
            this.LinkPage.MinimumWidth = 6;
            this.LinkPage.Name = "LinkPage";
            this.LinkPage.ReadOnly = true;
            this.LinkPage.Width = 250;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(175, 783);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(81, 20);
            this.dateTimePicker1.TabIndex = 55;
            this.dateTimePicker1.Visible = false;
            // 
            // loadingBtn
            // 
            this.loadingBtn.BackColor = System.Drawing.SystemColors.Window;
            this.loadingBtn.BackgroundColor = System.Drawing.SystemColors.Window;
            this.loadingBtn.BorderColor = System.Drawing.Color.DarkOrange;
            this.loadingBtn.BorderRadius = 40;
            this.loadingBtn.BorderSize = 2;
            this.loadingBtn.FlatAppearance.BorderSize = 0;
            this.loadingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadingBtn.ForeColor = System.Drawing.Color.White;
            this.loadingBtn.Image = global::AdsManager.Properties.Resources.loading1;
            this.loadingBtn.Location = new System.Drawing.Point(728, 344);
            this.loadingBtn.Name = "loadingBtn";
            this.loadingBtn.Size = new System.Drawing.Size(249, 244);
            this.loadingBtn.TabIndex = 54;
            this.loadingBtn.Text = "vbButton3";
            this.loadingBtn.TextColor = System.Drawing.Color.White;
            this.loadingBtn.UseVisualStyleBackColor = false;
            this.loadingBtn.Visible = false;
            // 
            // DuLieuPage_RefreshBtn
            // 
            this.DuLieuPage_RefreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DuLieuPage_RefreshBtn.BackColor = System.Drawing.SystemColors.Window;
            this.DuLieuPage_RefreshBtn.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DuLieuPage_RefreshBtn.BorderColor = System.Drawing.SystemColors.WindowText;
            this.DuLieuPage_RefreshBtn.BorderRadius = 19;
            this.DuLieuPage_RefreshBtn.BorderSize = 1;
            this.DuLieuPage_RefreshBtn.FlatAppearance.BorderSize = 0;
            this.DuLieuPage_RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DuLieuPage_RefreshBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DuLieuPage_RefreshBtn.ForeColor = System.Drawing.Color.Black;
            this.DuLieuPage_RefreshBtn.Image = global::AdsManager.Properties.Resources.reload2;
            this.DuLieuPage_RefreshBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DuLieuPage_RefreshBtn.Location = new System.Drawing.Point(12, 770);
            this.DuLieuPage_RefreshBtn.Name = "DuLieuPage_RefreshBtn";
            this.DuLieuPage_RefreshBtn.Size = new System.Drawing.Size(138, 33);
            this.DuLieuPage_RefreshBtn.TabIndex = 53;
            this.DuLieuPage_RefreshBtn.Text = "Làm mới dữ liệu";
            this.DuLieuPage_RefreshBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DuLieuPage_RefreshBtn.TextColor = System.Drawing.Color.Black;
            this.DuLieuPage_RefreshBtn.UseVisualStyleBackColor = false;
            this.DuLieuPage_RefreshBtn.Click += new System.EventHandler(this.DuLieuPage_RefreshBtn_Click);
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
            this.locTimeBtn.Location = new System.Drawing.Point(623, 3);
            this.locTimeBtn.Name = "locTimeBtn";
            this.locTimeBtn.Size = new System.Drawing.Size(61, 31);
            this.locTimeBtn.TabIndex = 27;
            this.locTimeBtn.Text = "Lọc";
            this.locTimeBtn.TextColor = System.Drawing.Color.Black;
            this.locTimeBtn.UseVisualStyleBackColor = false;
            this.locTimeBtn.Click += new System.EventHandler(this.locTimeBtn_Click);
            // 
            // searchBtn
            // 
            this.searchBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.searchBtn.BackColor = System.Drawing.Color.Green;
            this.searchBtn.BackgroundColor = System.Drawing.Color.Green;
            this.searchBtn.BorderColor = System.Drawing.SystemColors.WindowText;
            this.searchBtn.BorderRadius = 20;
            this.searchBtn.BorderSize = 0;
            this.searchBtn.FlatAppearance.BorderSize = 0;
            this.searchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchBtn.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn.ForeColor = System.Drawing.Color.Black;
            this.searchBtn.Image = global::AdsManager.Properties.Resources.Screenshot_2022_10_01_024949_removebg_preview1;
            this.searchBtn.Location = new System.Drawing.Point(328, 3);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(56, 31);
            this.searchBtn.TabIndex = 12;
            this.searchBtn.TextColor = System.Drawing.Color.Black;
            this.searchBtn.UseVisualStyleBackColor = false;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
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
            this.vbButton4.Location = new System.Drawing.Point(767, 3);
            this.vbButton4.Name = "vbButton4";
            this.vbButton4.Size = new System.Drawing.Size(129, 31);
            this.vbButton4.TabIndex = 1;
            this.vbButton4.Text = "Xuất Excel";
            this.vbButton4.TextColor = System.Drawing.Color.Black;
            this.vbButton4.UseVisualStyleBackColor = false;
            this.vbButton4.Click += new System.EventHandler(this.vbButton4_Click);
            // 
            // DuLieuPageForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1477, 815);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.loadingBtn);
            this.Controls.Add(this.DuLieuPage_RefreshBtn);
            this.Controls.Add(this.DuLieuPageDataGridView);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DuLieuPageForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DuLieuPageDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private VBButton locTimeBtn;
        private System.Windows.Forms.TextBox SearchTBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker from_dateTimePicker;
        private VBButton searchBtn;
        private VBButton vbButton4;
        private System.Windows.Forms.DateTimePicker to_dateTimePicker;
        private System.Windows.Forms.DataGridView DuLieuPageDataGridView;
        private VBButton DuLieuPage_RefreshBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenTKQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDTKQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn LuotNguoiTiepCan;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiPhiQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TienTe;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkPage;
        private VBButton loadingBtn;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}