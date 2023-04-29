namespace AdsManager.form.dataTKQC
{
    partial class ThongTinQCChiTietForm
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
            this.TTQCCT_DataGridView = new System.Windows.Forms.DataGridView();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenTKQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDTKQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaMKT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MKT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HKD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkPostQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPMTrungBinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTRTrungBinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chuyendoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiPhiQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TienTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TTQCCTComboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SearchTBox_TTQCCT = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.loadingBtn_TTQCCT = new AdsManager.VBButton();
            this.TTQCCT_RefreshBtn = new AdsManager.VBButton();
            this.searchBtn_TTQCCT = new AdsManager.VBButton();
            this.xuatExcel_TTQCCT = new AdsManager.VBButton();
            this.elipseControl1 = new TakeCarePage.ElipseControl();
            ((System.ComponentModel.ISupportInitialize)(this.TTQCCT_DataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TTQCCT_DataGridView
            // 
            this.TTQCCT_DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TTQCCT_DataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.TTQCCT_DataGridView.ColumnHeadersHeight = 40;
            this.TTQCCT_DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TrangThai,
            this.TenTKQC,
            this.IDTKQC,
            this.MaMKT,
            this.MKT,
            this.HKD,
            this.LinkPostQC,
            this.CPMTrungBinh,
            this.CTRTrungBinh,
            this.Chuyendoi,
            this.Mess,
            this.Comments,
            this.ChiPhiQC,
            this.TienTe});
            this.TTQCCT_DataGridView.EnableHeadersVisualStyles = false;
            this.TTQCCT_DataGridView.Location = new System.Drawing.Point(12, 55);
            this.TTQCCT_DataGridView.Name = "TTQCCT_DataGridView";
            this.TTQCCT_DataGridView.ReadOnly = true;
            this.TTQCCT_DataGridView.RowHeadersWidth = 51;
            this.TTQCCT_DataGridView.Size = new System.Drawing.Size(1546, 622);
            this.TTQCCT_DataGridView.TabIndex = 0;
            this.TTQCCT_DataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.TTQCCT_DataGridView_RowPrePaint);
            this.TTQCCT_DataGridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.TTQCCT_DataGridView_SortCompare);
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
            // MaMKT
            // 
            this.MaMKT.HeaderText = "Mã MKT";
            this.MaMKT.Name = "MaMKT";
            this.MaMKT.ReadOnly = true;
            // 
            // MKT
            // 
            this.MKT.HeaderText = "MKT";
            this.MKT.Name = "MKT";
            this.MKT.ReadOnly = true;
            // 
            // HKD
            // 
            this.HKD.HeaderText = "HKD";
            this.HKD.Name = "HKD";
            this.HKD.ReadOnly = true;
            // 
            // LinkPostQC
            // 
            this.LinkPostQC.HeaderText = "Link bài QC";
            this.LinkPostQC.MinimumWidth = 6;
            this.LinkPostQC.Name = "LinkPostQC";
            this.LinkPostQC.ReadOnly = true;
            this.LinkPostQC.Width = 200;
            // 
            // CPMTrungBinh
            // 
            this.CPMTrungBinh.HeaderText = "CPM trung bình";
            this.CPMTrungBinh.MinimumWidth = 6;
            this.CPMTrungBinh.Name = "CPMTrungBinh";
            this.CPMTrungBinh.ReadOnly = true;
            this.CPMTrungBinh.Width = 150;
            // 
            // CTRTrungBinh
            // 
            this.CTRTrungBinh.HeaderText = "CTR trung bình";
            this.CTRTrungBinh.Name = "CTRTrungBinh";
            this.CTRTrungBinh.ReadOnly = true;
            // 
            // Chuyendoi
            // 
            this.Chuyendoi.HeaderText = "Chuyển đổi";
            this.Chuyendoi.MinimumWidth = 6;
            this.Chuyendoi.Name = "Chuyendoi";
            this.Chuyendoi.ReadOnly = true;
            this.Chuyendoi.Width = 200;
            // 
            // Mess
            // 
            this.Mess.HeaderText = "Mess";
            this.Mess.MinimumWidth = 6;
            this.Mess.Name = "Mess";
            this.Mess.ReadOnly = true;
            this.Mess.Width = 150;
            // 
            // Comments
            // 
            this.Comments.HeaderText = "Bình luận";
            this.Comments.MinimumWidth = 6;
            this.Comments.Name = "Comments";
            this.Comments.ReadOnly = true;
            this.Comments.Width = 200;
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TTQCCTComboBox);
            this.panel1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(1186, 708);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 34);
            this.panel1.TabIndex = 44;
            this.panel1.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(309, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 14);
            this.label1.TabIndex = 24;
            this.label1.Text = "Số lượng";
            // 
            // TTQCCTComboBox
            // 
            this.TTQCCTComboBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TTQCCTComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TTQCCTComboBox.FormattingEnabled = true;
            this.TTQCCTComboBox.Location = new System.Drawing.Point(188, 8);
            this.TTQCCTComboBox.Name = "TTQCCTComboBox";
            this.TTQCCTComboBox.Size = new System.Drawing.Size(115, 22);
            this.TTQCCTComboBox.TabIndex = 23;
            this.TTQCCTComboBox.Text = "Row";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.TTQCCT_RefreshBtn);
            this.panel2.Controls.Add(this.SearchTBox_TTQCCT);
            this.panel2.Controls.Add(this.searchBtn_TTQCCT);
            this.panel2.Controls.Add(this.xuatExcel_TTQCCT);
            this.panel2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(896, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(663, 37);
            this.panel2.TabIndex = 46;
            // 
            // SearchTBox_TTQCCT
            // 
            this.SearchTBox_TTQCCT.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SearchTBox_TTQCCT.BackColor = System.Drawing.Color.Gainsboro;
            this.SearchTBox_TTQCCT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTBox_TTQCCT.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchTBox_TTQCCT.Location = new System.Drawing.Point(182, 7);
            this.SearchTBox_TTQCCT.MaximumSize = new System.Drawing.Size(350, 25);
            this.SearchTBox_TTQCCT.MinimumSize = new System.Drawing.Size(316, 25);
            this.SearchTBox_TTQCCT.Name = "SearchTBox_TTQCCT";
            this.SearchTBox_TTQCCT.Size = new System.Drawing.Size(316, 25);
            this.SearchTBox_TTQCCT.TabIndex = 26;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(57, 716);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(81, 20);
            this.dateTimePicker1.TabIndex = 56;
            this.dateTimePicker1.Visible = false;
            // 
            // loadingBtn_TTQCCT
            // 
            this.loadingBtn_TTQCCT.BackColor = System.Drawing.SystemColors.Window;
            this.loadingBtn_TTQCCT.BackgroundColor = System.Drawing.SystemColors.Window;
            this.loadingBtn_TTQCCT.BorderColor = System.Drawing.Color.DarkOrange;
            this.loadingBtn_TTQCCT.BorderRadius = 40;
            this.loadingBtn_TTQCCT.BorderSize = 2;
            this.loadingBtn_TTQCCT.FlatAppearance.BorderSize = 0;
            this.loadingBtn_TTQCCT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadingBtn_TTQCCT.ForeColor = System.Drawing.Color.White;
            this.loadingBtn_TTQCCT.Image = global::AdsManager.Properties.Resources.loading1;
            this.loadingBtn_TTQCCT.Location = new System.Drawing.Point(896, 306);
            this.loadingBtn_TTQCCT.Name = "loadingBtn_TTQCCT";
            this.loadingBtn_TTQCCT.Size = new System.Drawing.Size(249, 244);
            this.loadingBtn_TTQCCT.TabIndex = 45;
            this.loadingBtn_TTQCCT.Text = "vbButton3";
            this.loadingBtn_TTQCCT.TextColor = System.Drawing.Color.White;
            this.loadingBtn_TTQCCT.UseVisualStyleBackColor = false;
            this.loadingBtn_TTQCCT.Visible = false;
            // 
            // TTQCCT_RefreshBtn
            // 
            this.TTQCCT_RefreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TTQCCT_RefreshBtn.BackColor = System.Drawing.SystemColors.Window;
            this.TTQCCT_RefreshBtn.BackgroundColor = System.Drawing.SystemColors.Window;
            this.TTQCCT_RefreshBtn.BorderColor = System.Drawing.SystemColors.WindowText;
            this.TTQCCT_RefreshBtn.BorderRadius = 19;
            this.TTQCCT_RefreshBtn.BorderSize = 1;
            this.TTQCCT_RefreshBtn.FlatAppearance.BorderSize = 0;
            this.TTQCCT_RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TTQCCT_RefreshBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TTQCCT_RefreshBtn.ForeColor = System.Drawing.Color.Black;
            this.TTQCCT_RefreshBtn.Image = global::AdsManager.Properties.Resources.reload2;
            this.TTQCCT_RefreshBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TTQCCT_RefreshBtn.Location = new System.Drawing.Point(3, 2);
            this.TTQCCT_RefreshBtn.Name = "TTQCCT_RefreshBtn";
            this.TTQCCT_RefreshBtn.Size = new System.Drawing.Size(138, 33);
            this.TTQCCT_RefreshBtn.TabIndex = 43;
            this.TTQCCT_RefreshBtn.Text = "Làm mới dữ liệu";
            this.TTQCCT_RefreshBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TTQCCT_RefreshBtn.TextColor = System.Drawing.Color.Black;
            this.TTQCCT_RefreshBtn.UseVisualStyleBackColor = false;
            this.TTQCCT_RefreshBtn.Click += new System.EventHandler(this.TTQCCT_RefreshBtn_Click);
            // 
            // searchBtn_TTQCCT
            // 
            this.searchBtn_TTQCCT.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.searchBtn_TTQCCT.BackColor = System.Drawing.Color.Green;
            this.searchBtn_TTQCCT.BackgroundColor = System.Drawing.Color.Green;
            this.searchBtn_TTQCCT.BorderColor = System.Drawing.SystemColors.WindowText;
            this.searchBtn_TTQCCT.BorderRadius = 20;
            this.searchBtn_TTQCCT.BorderSize = 0;
            this.searchBtn_TTQCCT.FlatAppearance.BorderSize = 0;
            this.searchBtn_TTQCCT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchBtn_TTQCCT.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn_TTQCCT.ForeColor = System.Drawing.Color.Black;
            this.searchBtn_TTQCCT.Image = global::AdsManager.Properties.Resources.Screenshot_2022_10_01_024949_removebg_preview1;
            this.searchBtn_TTQCCT.Location = new System.Drawing.Point(504, 4);
            this.searchBtn_TTQCCT.Name = "searchBtn_TTQCCT";
            this.searchBtn_TTQCCT.Size = new System.Drawing.Size(56, 31);
            this.searchBtn_TTQCCT.TabIndex = 12;
            this.searchBtn_TTQCCT.TextColor = System.Drawing.Color.Black;
            this.searchBtn_TTQCCT.UseVisualStyleBackColor = false;
            this.searchBtn_TTQCCT.Click += new System.EventHandler(this.searchBtn_TTQCCT_Click);
            // 
            // xuatExcel_TTQCCT
            // 
            this.xuatExcel_TTQCCT.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.xuatExcel_TTQCCT.BackColor = System.Drawing.Color.Green;
            this.xuatExcel_TTQCCT.BackgroundColor = System.Drawing.Color.Green;
            this.xuatExcel_TTQCCT.BorderColor = System.Drawing.SystemColors.WindowText;
            this.xuatExcel_TTQCCT.BorderRadius = 20;
            this.xuatExcel_TTQCCT.BorderSize = 0;
            this.xuatExcel_TTQCCT.FlatAppearance.BorderSize = 0;
            this.xuatExcel_TTQCCT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xuatExcel_TTQCCT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xuatExcel_TTQCCT.ForeColor = System.Drawing.Color.Black;
            this.xuatExcel_TTQCCT.Location = new System.Drawing.Point(568, 3);
            this.xuatExcel_TTQCCT.Name = "xuatExcel_TTQCCT";
            this.xuatExcel_TTQCCT.Size = new System.Drawing.Size(86, 31);
            this.xuatExcel_TTQCCT.TabIndex = 1;
            this.xuatExcel_TTQCCT.Text = "Xuất Excel";
            this.xuatExcel_TTQCCT.TextColor = System.Drawing.Color.Black;
            this.xuatExcel_TTQCCT.UseVisualStyleBackColor = false;
            this.xuatExcel_TTQCCT.Click += new System.EventHandler(this.vbButton4_Click);
            // 
            // elipseControl1
            // 
            this.elipseControl1.CornerRadius = 15;
            this.elipseControl1.TargetControl = this.TTQCCTComboBox;
            // 
            // ThongTinQCChiTietForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1571, 747);
            this.ControlBox = false;
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.loadingBtn_TTQCCT);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TTQCCT_DataGridView);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ThongTinQCChiTietForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.TTQCCT_DataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TTQCCT_DataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TTQCCTComboBox;
        private TakeCarePage.ElipseControl elipseControl1;
        private VBButton TTQCCT_RefreshBtn;
        private VBButton loadingBtn_TTQCCT;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox SearchTBox_TTQCCT;
        private VBButton searchBtn_TTQCCT;
        private VBButton xuatExcel_TTQCCT;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenTKQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDTKQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaMKT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MKT;
        private System.Windows.Forms.DataGridViewTextBoxColumn HKD;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkPostQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPMTrungBinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTRTrungBinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn Chuyendoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mess;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comments;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiPhiQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TienTe;
    }
}