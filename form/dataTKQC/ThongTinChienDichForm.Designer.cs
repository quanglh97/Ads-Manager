namespace AdsManager.form.dataTKQC
{
    partial class ThongTinChienDichForm
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
            this.ThongTinChienDichDataGridView = new System.Windows.Forms.DataGridView();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenTKQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDTKQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenChienDich = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDChienDich = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNhomQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDNhomQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XemQC = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ChiPhiQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayTaoQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TienTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.vbButton23 = new AdsManager.VBButton();
            this.vbButton1 = new AdsManager.VBButton();
            this.vbButton2 = new AdsManager.VBButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TTCDComboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vbButton3 = new AdsManager.VBButton();
            this.label4 = new System.Windows.Forms.Label();
            this.filterTimeBtn_TTCD = new AdsManager.VBButton();
            this.label3 = new System.Windows.Forms.Label();
            this.from_dateTimePicker_TTCD_search = new System.Windows.Forms.DateTimePicker();
            this.SearchTBox_TTCD = new System.Windows.Forms.TextBox();
            this.from_dateTimePicker_TTCD = new System.Windows.Forms.DateTimePicker();
            this.to_dateTimePicker_TTCD_search = new System.Windows.Forms.DateTimePicker();
            this.searchBtn_TTCD = new AdsManager.VBButton();
            this.vbButton4 = new AdsManager.VBButton();
            this.to_dateTimePicker_TTCD = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.loadingBtnTTCD = new AdsManager.VBButton();
            this.TTCD_RefreshBtn = new AdsManager.VBButton();
            this.elipseControl1 = new TakeCarePage.ElipseControl();
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinChienDichDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ThongTinChienDichDataGridView
            // 
            this.ThongTinChienDichDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ThongTinChienDichDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ThongTinChienDichDataGridView.ColumnHeadersHeight = 40;
            this.ThongTinChienDichDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TrangThai,
            this.TenTKQC,
            this.IDTKQC,
            this.TenChienDich,
            this.IDChienDich,
            this.TenNhomQC,
            this.IDNhomQC,
            this.TenQC,
            this.IDQC,
            this.XemQC,
            this.ChiPhiQC,
            this.NgayTaoQC,
            this.TienTe});
            this.ThongTinChienDichDataGridView.EnableHeadersVisualStyles = false;
            this.ThongTinChienDichDataGridView.Location = new System.Drawing.Point(12, 55);
            this.ThongTinChienDichDataGridView.Name = "ThongTinChienDichDataGridView";
            this.ThongTinChienDichDataGridView.ReadOnly = true;
            this.ThongTinChienDichDataGridView.RowHeadersWidth = 51;
            this.ThongTinChienDichDataGridView.Size = new System.Drawing.Size(1546, 622);
            this.ThongTinChienDichDataGridView.TabIndex = 0;
            this.ThongTinChienDichDataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.ThongTinChienDichDataGridView_RowPrePaint);
            this.ThongTinChienDichDataGridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.ThongTinChienDichDataGridView_SortCompare);
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
            // TenChienDich
            // 
            this.TenChienDich.HeaderText = "Tên chiến dịch";
            this.TenChienDich.MinimumWidth = 6;
            this.TenChienDich.Name = "TenChienDich";
            this.TenChienDich.ReadOnly = true;
            this.TenChienDich.Width = 200;
            // 
            // IDChienDich
            // 
            this.IDChienDich.HeaderText = "ID chiến dịch";
            this.IDChienDich.MinimumWidth = 6;
            this.IDChienDich.Name = "IDChienDich";
            this.IDChienDich.ReadOnly = true;
            this.IDChienDich.Width = 150;
            // 
            // TenNhomQC
            // 
            this.TenNhomQC.HeaderText = "Tên nhóm QC";
            this.TenNhomQC.MinimumWidth = 6;
            this.TenNhomQC.Name = "TenNhomQC";
            this.TenNhomQC.ReadOnly = true;
            this.TenNhomQC.Width = 200;
            // 
            // IDNhomQC
            // 
            this.IDNhomQC.HeaderText = "ID nhóm QC";
            this.IDNhomQC.MinimumWidth = 6;
            this.IDNhomQC.Name = "IDNhomQC";
            this.IDNhomQC.ReadOnly = true;
            this.IDNhomQC.Width = 150;
            // 
            // TenQC
            // 
            this.TenQC.HeaderText = "Tên quảng cáo";
            this.TenQC.MinimumWidth = 6;
            this.TenQC.Name = "TenQC";
            this.TenQC.ReadOnly = true;
            this.TenQC.Width = 200;
            // 
            // IDQC
            // 
            this.IDQC.HeaderText = "ID quảng cáo";
            this.IDQC.MinimumWidth = 6;
            this.IDQC.Name = "IDQC";
            this.IDQC.ReadOnly = true;
            this.IDQC.Width = 150;
            // 
            // XemQC
            // 
            this.XemQC.HeaderText = "Xem QC";
            this.XemQC.MinimumWidth = 6;
            this.XemQC.Name = "XemQC";
            this.XemQC.ReadOnly = true;
            this.XemQC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.XemQC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.XemQC.Width = 125;
            // 
            // ChiPhiQC
            // 
            this.ChiPhiQC.HeaderText = "Chi phí QC";
            this.ChiPhiQC.MinimumWidth = 6;
            this.ChiPhiQC.Name = "ChiPhiQC";
            this.ChiPhiQC.ReadOnly = true;
            this.ChiPhiQC.Width = 150;
            // 
            // NgayTaoQC
            // 
            this.NgayTaoQC.HeaderText = "Ngày tạo QC";
            this.NgayTaoQC.MinimumWidth = 6;
            this.NgayTaoQC.Name = "NgayTaoQC";
            this.NgayTaoQC.ReadOnly = true;
            this.NgayTaoQC.Width = 150;
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
            this.panel1.Controls.Add(this.vbButton23);
            this.panel1.Controls.Add(this.vbButton1);
            this.panel1.Controls.Add(this.vbButton2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TTCDComboBox);
            this.panel1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(1186, 708);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 34);
            this.panel1.TabIndex = 44;
            this.panel1.Visible = false;
            // 
            // vbButton23
            // 
            this.vbButton23.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.vbButton23.BackColor = System.Drawing.Color.White;
            this.vbButton23.BackgroundColor = System.Drawing.Color.White;
            this.vbButton23.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbButton23.BorderRadius = 10;
            this.vbButton23.BorderSize = 1;
            this.vbButton23.Enabled = false;
            this.vbButton23.FlatAppearance.BorderSize = 0;
            this.vbButton23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton23.ForeColor = System.Drawing.Color.Black;
            this.vbButton23.Location = new System.Drawing.Point(187, 6);
            this.vbButton23.Name = "vbButton23";
            this.vbButton23.Size = new System.Drawing.Size(33, 22);
            this.vbButton23.TabIndex = 33;
            this.vbButton23.Text = "10";
            this.vbButton23.TextColor = System.Drawing.Color.Black;
            this.vbButton23.UseVisualStyleBackColor = false;
            // 
            // vbButton1
            // 
            this.vbButton1.BackColor = System.Drawing.SystemColors.Window;
            this.vbButton1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.vbButton1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbButton1.BorderRadius = 12;
            this.vbButton1.BorderSize = 1;
            this.vbButton1.FlatAppearance.BorderSize = 0;
            this.vbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton1.ForeColor = System.Drawing.Color.Black;
            this.vbButton1.Location = new System.Drawing.Point(256, 6);
            this.vbButton1.Name = "vbButton1";
            this.vbButton1.Size = new System.Drawing.Size(25, 23);
            this.vbButton1.TabIndex = 31;
            this.vbButton1.Text = "<";
            this.vbButton1.TextColor = System.Drawing.Color.Black;
            this.vbButton1.UseVisualStyleBackColor = false;
            // 
            // vbButton2
            // 
            this.vbButton2.BackColor = System.Drawing.SystemColors.Window;
            this.vbButton2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.vbButton2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbButton2.BorderRadius = 12;
            this.vbButton2.BorderSize = 1;
            this.vbButton2.FlatAppearance.BorderSize = 0;
            this.vbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton2.ForeColor = System.Drawing.Color.Black;
            this.vbButton2.Location = new System.Drawing.Point(343, 5);
            this.vbButton2.Name = "vbButton2";
            this.vbButton2.Size = new System.Drawing.Size(25, 23);
            this.vbButton2.TabIndex = 32;
            this.vbButton2.Text = ">";
            this.vbButton2.TextColor = System.Drawing.Color.Black;
            this.vbButton2.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 14);
            this.label2.TabIndex = 26;
            this.label2.Text = "10/50";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 14);
            this.label1.TabIndex = 24;
            this.label1.Text = "Trang";
            // 
            // TTCDComboBox
            // 
            this.TTCDComboBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TTCDComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TTCDComboBox.FormattingEnabled = true;
            this.TTCDComboBox.Location = new System.Drawing.Point(4, 7);
            this.TTCDComboBox.Name = "TTCDComboBox";
            this.TTCDComboBox.Size = new System.Drawing.Size(115, 22);
            this.TTCDComboBox.TabIndex = 23;
            this.TTCDComboBox.Text = "Row";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.vbButton3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.filterTimeBtn_TTCD);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.from_dateTimePicker_TTCD_search);
            this.panel2.Controls.Add(this.SearchTBox_TTCD);
            this.panel2.Controls.Add(this.from_dateTimePicker_TTCD);
            this.panel2.Controls.Add(this.to_dateTimePicker_TTCD_search);
            this.panel2.Controls.Add(this.searchBtn_TTCD);
            this.panel2.Controls.Add(this.vbButton4);
            this.panel2.Controls.Add(this.to_dateTimePicker_TTCD);
            this.panel2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(216, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1343, 37);
            this.panel2.TabIndex = 46;
            // 
            // vbButton3
            // 
            this.vbButton3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.vbButton3.BackColor = System.Drawing.Color.Green;
            this.vbButton3.BackgroundColor = System.Drawing.Color.Green;
            this.vbButton3.BorderColor = System.Drawing.SystemColors.WindowText;
            this.vbButton3.BorderRadius = 20;
            this.vbButton3.BorderSize = 0;
            this.vbButton3.FlatAppearance.BorderSize = 0;
            this.vbButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton3.ForeColor = System.Drawing.Color.Black;
            this.vbButton3.Location = new System.Drawing.Point(629, 3);
            this.vbButton3.Name = "vbButton3";
            this.vbButton3.Size = new System.Drawing.Size(129, 31);
            this.vbButton3.TabIndex = 50;
            this.vbButton3.Text = "Lọc chi phí";
            this.vbButton3.TextColor = System.Drawing.Color.Black;
            this.vbButton3.UseVisualStyleBackColor = false;
            this.vbButton3.Click += new System.EventHandler(this.vbButton3_Click_1);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(923, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 14);
            this.label4.TabIndex = 49;
            this.label4.Text = "Đến";
            // 
            // filterTimeBtn_TTCD
            // 
            this.filterTimeBtn_TTCD.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.filterTimeBtn_TTCD.BackColor = System.Drawing.Color.Green;
            this.filterTimeBtn_TTCD.BackgroundColor = System.Drawing.Color.Green;
            this.filterTimeBtn_TTCD.BorderColor = System.Drawing.SystemColors.WindowText;
            this.filterTimeBtn_TTCD.BorderRadius = 20;
            this.filterTimeBtn_TTCD.BorderSize = 0;
            this.filterTimeBtn_TTCD.FlatAppearance.BorderSize = 0;
            this.filterTimeBtn_TTCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterTimeBtn_TTCD.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterTimeBtn_TTCD.ForeColor = System.Drawing.Color.Black;
            this.filterTimeBtn_TTCD.Location = new System.Drawing.Point(1061, 3);
            this.filterTimeBtn_TTCD.Name = "filterTimeBtn_TTCD";
            this.filterTimeBtn_TTCD.Size = new System.Drawing.Size(129, 31);
            this.filterTimeBtn_TTCD.TabIndex = 27;
            this.filterTimeBtn_TTCD.Text = "Lọc ngày tạo QC";
            this.filterTimeBtn_TTCD.TextColor = System.Drawing.Color.Black;
            this.filterTimeBtn_TTCD.UseVisualStyleBackColor = false;
            this.filterTimeBtn_TTCD.Click += new System.EventHandler(this.filterTimeBtn_TTCD_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(491, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 14);
            this.label3.TabIndex = 14;
            this.label3.Text = "Đến";
            // 
            // from_dateTimePicker_TTCD_search
            // 
            this.from_dateTimePicker_TTCD_search.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.from_dateTimePicker_TTCD_search.CustomFormat = "MM/dd/yyyy";
            this.from_dateTimePicker_TTCD_search.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.from_dateTimePicker_TTCD_search.Location = new System.Drawing.Point(832, 11);
            this.from_dateTimePicker_TTCD_search.Name = "from_dateTimePicker_TTCD_search";
            this.from_dateTimePicker_TTCD_search.Size = new System.Drawing.Size(81, 20);
            this.from_dateTimePicker_TTCD_search.TabIndex = 48;
            // 
            // SearchTBox_TTCD
            // 
            this.SearchTBox_TTCD.BackColor = System.Drawing.Color.Gainsboro;
            this.SearchTBox_TTCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTBox_TTCD.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchTBox_TTCD.Location = new System.Drawing.Point(6, 6);
            this.SearchTBox_TTCD.MaximumSize = new System.Drawing.Size(350, 25);
            this.SearchTBox_TTCD.MinimumSize = new System.Drawing.Size(316, 25);
            this.SearchTBox_TTCD.Name = "SearchTBox_TTCD";
            this.SearchTBox_TTCD.Size = new System.Drawing.Size(316, 25);
            this.SearchTBox_TTCD.TabIndex = 26;
            // 
            // from_dateTimePicker_TTCD
            // 
            this.from_dateTimePicker_TTCD.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.from_dateTimePicker_TTCD.CustomFormat = "MM/dd/yyyy";
            this.from_dateTimePicker_TTCD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.from_dateTimePicker_TTCD.Location = new System.Drawing.Point(400, 10);
            this.from_dateTimePicker_TTCD.Name = "from_dateTimePicker_TTCD";
            this.from_dateTimePicker_TTCD.Size = new System.Drawing.Size(81, 20);
            this.from_dateTimePicker_TTCD.TabIndex = 13;
            // 
            // to_dateTimePicker_TTCD_search
            // 
            this.to_dateTimePicker_TTCD_search.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.to_dateTimePicker_TTCD_search.CustomFormat = "MM/dd/yyyy";
            this.to_dateTimePicker_TTCD_search.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.to_dateTimePicker_TTCD_search.Location = new System.Drawing.Point(968, 11);
            this.to_dateTimePicker_TTCD_search.Name = "to_dateTimePicker_TTCD_search";
            this.to_dateTimePicker_TTCD_search.Size = new System.Drawing.Size(87, 20);
            this.to_dateTimePicker_TTCD_search.TabIndex = 47;
            // 
            // searchBtn_TTCD
            // 
            this.searchBtn_TTCD.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.searchBtn_TTCD.BackColor = System.Drawing.Color.Green;
            this.searchBtn_TTCD.BackgroundColor = System.Drawing.Color.Green;
            this.searchBtn_TTCD.BorderColor = System.Drawing.SystemColors.WindowText;
            this.searchBtn_TTCD.BorderRadius = 20;
            this.searchBtn_TTCD.BorderSize = 0;
            this.searchBtn_TTCD.FlatAppearance.BorderSize = 0;
            this.searchBtn_TTCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchBtn_TTCD.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn_TTCD.ForeColor = System.Drawing.Color.Black;
            this.searchBtn_TTCD.Image = global::AdsManager.Properties.Resources.Screenshot_2022_10_01_024949_removebg_preview1;
            this.searchBtn_TTCD.Location = new System.Drawing.Point(328, 3);
            this.searchBtn_TTCD.Name = "searchBtn_TTCD";
            this.searchBtn_TTCD.Size = new System.Drawing.Size(56, 31);
            this.searchBtn_TTCD.TabIndex = 12;
            this.searchBtn_TTCD.TextColor = System.Drawing.Color.Black;
            this.searchBtn_TTCD.UseVisualStyleBackColor = false;
            this.searchBtn_TTCD.Click += new System.EventHandler(this.searchBtn_TTCD_Click);
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
            this.vbButton4.Location = new System.Drawing.Point(1205, 3);
            this.vbButton4.Name = "vbButton4";
            this.vbButton4.Size = new System.Drawing.Size(129, 31);
            this.vbButton4.TabIndex = 1;
            this.vbButton4.Text = "Xuất Excel";
            this.vbButton4.TextColor = System.Drawing.Color.Black;
            this.vbButton4.UseVisualStyleBackColor = false;
            this.vbButton4.Click += new System.EventHandler(this.vbButton4_Click);
            // 
            // to_dateTimePicker_TTCD
            // 
            this.to_dateTimePicker_TTCD.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.to_dateTimePicker_TTCD.CustomFormat = "MM/dd/yyyy";
            this.to_dateTimePicker_TTCD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.to_dateTimePicker_TTCD.Location = new System.Drawing.Point(536, 10);
            this.to_dateTimePicker_TTCD.Name = "to_dateTimePicker_TTCD";
            this.to_dateTimePicker_TTCD.Size = new System.Drawing.Size(87, 20);
            this.to_dateTimePicker_TTCD.TabIndex = 11;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(183, 717);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(81, 20);
            this.dateTimePicker1.TabIndex = 47;
            this.dateTimePicker1.Visible = false;
            // 
            // loadingBtnTTCD
            // 
            this.loadingBtnTTCD.BackColor = System.Drawing.SystemColors.Window;
            this.loadingBtnTTCD.BackgroundColor = System.Drawing.SystemColors.Window;
            this.loadingBtnTTCD.BorderColor = System.Drawing.Color.DarkOrange;
            this.loadingBtnTTCD.BorderRadius = 40;
            this.loadingBtnTTCD.BorderSize = 2;
            this.loadingBtnTTCD.FlatAppearance.BorderSize = 0;
            this.loadingBtnTTCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadingBtnTTCD.ForeColor = System.Drawing.Color.White;
            this.loadingBtnTTCD.Image = global::AdsManager.Properties.Resources.loading1;
            this.loadingBtnTTCD.Location = new System.Drawing.Point(782, 285);
            this.loadingBtnTTCD.Name = "loadingBtnTTCD";
            this.loadingBtnTTCD.Size = new System.Drawing.Size(249, 244);
            this.loadingBtnTTCD.TabIndex = 45;
            this.loadingBtnTTCD.Text = "vbButton3";
            this.loadingBtnTTCD.TextColor = System.Drawing.Color.White;
            this.loadingBtnTTCD.UseVisualStyleBackColor = false;
            this.loadingBtnTTCD.Visible = false;
            // 
            // TTCD_RefreshBtn
            // 
            this.TTCD_RefreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TTCD_RefreshBtn.BackColor = System.Drawing.SystemColors.Window;
            this.TTCD_RefreshBtn.BackgroundColor = System.Drawing.SystemColors.Window;
            this.TTCD_RefreshBtn.BorderColor = System.Drawing.SystemColors.WindowText;
            this.TTCD_RefreshBtn.BorderRadius = 19;
            this.TTCD_RefreshBtn.BorderSize = 1;
            this.TTCD_RefreshBtn.FlatAppearance.BorderSize = 0;
            this.TTCD_RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TTCD_RefreshBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TTCD_RefreshBtn.ForeColor = System.Drawing.Color.Black;
            this.TTCD_RefreshBtn.Image = global::AdsManager.Properties.Resources.reload2;
            this.TTCD_RefreshBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TTCD_RefreshBtn.Location = new System.Drawing.Point(12, 708);
            this.TTCD_RefreshBtn.Name = "TTCD_RefreshBtn";
            this.TTCD_RefreshBtn.Size = new System.Drawing.Size(138, 33);
            this.TTCD_RefreshBtn.TabIndex = 43;
            this.TTCD_RefreshBtn.Text = "Làm mới dữ liệu";
            this.TTCD_RefreshBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TTCD_RefreshBtn.TextColor = System.Drawing.Color.Black;
            this.TTCD_RefreshBtn.UseVisualStyleBackColor = false;
            this.TTCD_RefreshBtn.Click += new System.EventHandler(this.vbButton3_Click);
            // 
            // elipseControl1
            // 
            this.elipseControl1.CornerRadius = 15;
            this.elipseControl1.TargetControl = this.TTCDComboBox;
            // 
            // ThongTinChienDichForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1571, 747);
            this.ControlBox = false;
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.loadingBtnTTCD);
            this.Controls.Add(this.TTCD_RefreshBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ThongTinChienDichDataGridView);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ThongTinChienDichForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinChienDichDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ThongTinChienDichDataGridView;
        private System.Windows.Forms.Panel panel1;
        private VBButton vbButton23;
        private VBButton vbButton1;
        private VBButton vbButton2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TTCDComboBox;
        private TakeCarePage.ElipseControl elipseControl1;
        private VBButton TTCD_RefreshBtn;
        private VBButton loadingBtnTTCD;
        private System.Windows.Forms.Panel panel2;
        private VBButton filterTimeBtn_TTCD;
        private System.Windows.Forms.TextBox SearchTBox_TTCD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker from_dateTimePicker_TTCD;
        private VBButton searchBtn_TTCD;
        private VBButton vbButton4;
        private System.Windows.Forms.DateTimePicker to_dateTimePicker_TTCD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker from_dateTimePicker_TTCD_search;
        private System.Windows.Forms.DateTimePicker to_dateTimePicker_TTCD_search;
        private VBButton vbButton3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenTKQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDTKQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenChienDich;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDChienDich;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNhomQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDNhomQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDQC;
        private System.Windows.Forms.DataGridViewLinkColumn XemQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiPhiQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayTaoQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TienTe;
    }
}