namespace AdsManager.form.dataTKQC
{
    partial class HoaDonTTForm
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
            this.HoaDonThanhToanDataGridView = new System.Windows.Forms.DataGridView();
            this.HoaDonThanhToanToTalDataGridView = new System.Windows.Forms.DataGridView();
            this.TongHoaDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongTienToTal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.locTimeBtn = new AdsManager.VBButton();
            this.vbButton1 = new AdsManager.VBButton();
            this.SearchTBox_HD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.from_dateTimePicker_HD = new System.Windows.Forms.DateTimePicker();
            this.searchBtn_HD = new AdsManager.VBButton();
            this.vbButton4 = new AdsManager.VBButton();
            this.to_dateTimePicker_HD = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerHD = new System.Windows.Forms.DateTimePicker();
            this.HoaDonTKQCdataGridView = new System.Windows.Forms.DataGridView();
            this.modeShowBtn = new AdsManager.VBButton();
            this.loadingBtnHoadon = new AdsManager.VBButton();
            this.HDTT_RefeshBtn = new AdsManager.VBButton();
            this.TrangThaiTKQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenTK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDTKQC2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaMKT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MKT_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HKD_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TONGTIENGOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TienTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDTKQC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDGiaoDich = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaMKT_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MKT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HKD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ngay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhuongThucThanhToan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrangThaiThanhToan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDHoaDonVAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaThamChieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.HoaDonThanhToanDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HoaDonThanhToanToTalDataGridView)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HoaDonTKQCdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // HoaDonThanhToanDataGridView
            // 
            this.HoaDonThanhToanDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HoaDonThanhToanDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.HoaDonThanhToanDataGridView.ColumnHeadersHeight = 40;
            this.HoaDonThanhToanDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.AccName,
            this.IDTKQC,
            this.IDGiaoDich,
            this.MaMKT_,
            this.MKT,
            this.HKD,
            this.Ngay,
            this.SoTien,
            this.PhuongThucThanhToan,
            this.TrangThaiThanhToan,
            this.IDHoaDonVAT,
            this.MaThamChieu});
            this.HoaDonThanhToanDataGridView.EnableHeadersVisualStyles = false;
            this.HoaDonThanhToanDataGridView.Location = new System.Drawing.Point(14, 55);
            this.HoaDonThanhToanDataGridView.Name = "HoaDonThanhToanDataGridView";
            this.HoaDonThanhToanDataGridView.ReadOnly = true;
            this.HoaDonThanhToanDataGridView.RowHeadersWidth = 60;
            this.HoaDonThanhToanDataGridView.Size = new System.Drawing.Size(1495, 573);
            this.HoaDonThanhToanDataGridView.TabIndex = 44;
            this.HoaDonThanhToanDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.HoaDonThanhToanDataGridView_CellFormatting);
            this.HoaDonThanhToanDataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.HoaDonThanhToanDataGridView_RowPrePaint);
            // 
            // HoaDonThanhToanToTalDataGridView
            // 
            this.HoaDonThanhToanToTalDataGridView.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.HoaDonThanhToanToTalDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.HoaDonThanhToanToTalDataGridView.ColumnHeadersHeight = 40;
            this.HoaDonThanhToanToTalDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TongHoaDon,
            this.TongTienToTal});
            this.HoaDonThanhToanToTalDataGridView.EnableHeadersVisualStyles = false;
            this.HoaDonThanhToanToTalDataGridView.Location = new System.Drawing.Point(1109, 634);
            this.HoaDonThanhToanToTalDataGridView.Name = "HoaDonThanhToanToTalDataGridView";
            this.HoaDonThanhToanToTalDataGridView.RowHeadersWidth = 51;
            this.HoaDonThanhToanToTalDataGridView.RowTemplate.Height = 40;
            this.HoaDonThanhToanToTalDataGridView.Size = new System.Drawing.Size(400, 92);
            this.HoaDonThanhToanToTalDataGridView.TabIndex = 47;
            // 
            // TongHoaDon
            // 
            this.TongHoaDon.HeaderText = "TỔNG HÓA ĐƠN";
            this.TongHoaDon.MinimumWidth = 6;
            this.TongHoaDon.Name = "TongHoaDon";
            this.TongHoaDon.Width = 200;
            // 
            // TongTienToTal
            // 
            this.TongTienToTal.HeaderText = "TỔNG TIỀN";
            this.TongTienToTal.MinimumWidth = 6;
            this.TongTienToTal.Name = "TongTienToTal";
            this.TongTienToTal.Width = 200;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.locTimeBtn);
            this.panel2.Controls.Add(this.vbButton1);
            this.panel2.Controls.Add(this.SearchTBox_HD);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.from_dateTimePicker_HD);
            this.panel2.Controls.Add(this.searchBtn_HD);
            this.panel2.Controls.Add(this.vbButton4);
            this.panel2.Controls.Add(this.to_dateTimePicker_HD);
            this.panel2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(406, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1103, 37);
            this.panel2.TabIndex = 50;
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
            this.locTimeBtn.Location = new System.Drawing.Point(638, 2);
            this.locTimeBtn.Name = "locTimeBtn";
            this.locTimeBtn.Size = new System.Drawing.Size(61, 31);
            this.locTimeBtn.TabIndex = 27;
            this.locTimeBtn.Text = "Lọc";
            this.locTimeBtn.TextColor = System.Drawing.Color.Black;
            this.locTimeBtn.UseVisualStyleBackColor = false;
            this.locTimeBtn.Click += new System.EventHandler(this.locTimeBtn_Click);
            // 
            // vbButton1
            // 
            this.vbButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.vbButton1.BackColor = System.Drawing.Color.Green;
            this.vbButton1.BackgroundColor = System.Drawing.Color.Green;
            this.vbButton1.BorderColor = System.Drawing.SystemColors.WindowText;
            this.vbButton1.BorderRadius = 20;
            this.vbButton1.BorderSize = 0;
            this.vbButton1.FlatAppearance.BorderSize = 0;
            this.vbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton1.ForeColor = System.Drawing.Color.Black;
            this.vbButton1.Location = new System.Drawing.Point(721, 2);
            this.vbButton1.Name = "vbButton1";
            this.vbButton1.Size = new System.Drawing.Size(179, 31);
            this.vbButton1.TabIndex = 28;
            this.vbButton1.Text = "Xuất Excel bảng tổng";
            this.vbButton1.TextColor = System.Drawing.Color.Black;
            this.vbButton1.UseVisualStyleBackColor = false;
            this.vbButton1.Click += new System.EventHandler(this.vbButton1_Click_1);
            // 
            // SearchTBox_HD
            // 
            this.SearchTBox_HD.BackColor = System.Drawing.Color.Gainsboro;
            this.SearchTBox_HD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTBox_HD.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchTBox_HD.Location = new System.Drawing.Point(6, 6);
            this.SearchTBox_HD.MaximumSize = new System.Drawing.Size(350, 25);
            this.SearchTBox_HD.MinimumSize = new System.Drawing.Size(316, 25);
            this.SearchTBox_HD.Name = "SearchTBox_HD";
            this.SearchTBox_HD.Size = new System.Drawing.Size(316, 25);
            this.SearchTBox_HD.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(500, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 14);
            this.label3.TabIndex = 14;
            this.label3.Text = "Đến";
            // 
            // from_dateTimePicker_HD
            // 
            this.from_dateTimePicker_HD.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.from_dateTimePicker_HD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.from_dateTimePicker_HD.Location = new System.Drawing.Point(409, 7);
            this.from_dateTimePicker_HD.Name = "from_dateTimePicker_HD";
            this.from_dateTimePicker_HD.Size = new System.Drawing.Size(81, 20);
            this.from_dateTimePicker_HD.TabIndex = 13;
            // 
            // searchBtn_HD
            // 
            this.searchBtn_HD.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.searchBtn_HD.BackColor = System.Drawing.Color.Green;
            this.searchBtn_HD.BackgroundColor = System.Drawing.Color.Green;
            this.searchBtn_HD.BorderColor = System.Drawing.SystemColors.WindowText;
            this.searchBtn_HD.BorderRadius = 20;
            this.searchBtn_HD.BorderSize = 0;
            this.searchBtn_HD.FlatAppearance.BorderSize = 0;
            this.searchBtn_HD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchBtn_HD.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn_HD.ForeColor = System.Drawing.Color.Black;
            this.searchBtn_HD.Image = global::AdsManager.Properties.Resources.Screenshot_2022_10_01_024949_removebg_preview1;
            this.searchBtn_HD.Location = new System.Drawing.Point(343, 2);
            this.searchBtn_HD.Name = "searchBtn_HD";
            this.searchBtn_HD.Size = new System.Drawing.Size(56, 31);
            this.searchBtn_HD.TabIndex = 12;
            this.searchBtn_HD.TextColor = System.Drawing.Color.Black;
            this.searchBtn_HD.UseVisualStyleBackColor = false;
            this.searchBtn_HD.Click += new System.EventHandler(this.searchHD_Click);
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
            this.vbButton4.Location = new System.Drawing.Point(917, 3);
            this.vbButton4.Name = "vbButton4";
            this.vbButton4.Size = new System.Drawing.Size(177, 31);
            this.vbButton4.TabIndex = 1;
            this.vbButton4.Text = "Xuất Excel bảng chi tiết";
            this.vbButton4.TextColor = System.Drawing.Color.Black;
            this.vbButton4.UseVisualStyleBackColor = false;
            this.vbButton4.Click += new System.EventHandler(this.vbButton4_Click);
            // 
            // to_dateTimePicker_HD
            // 
            this.to_dateTimePicker_HD.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.to_dateTimePicker_HD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.to_dateTimePicker_HD.Location = new System.Drawing.Point(545, 7);
            this.to_dateTimePicker_HD.Name = "to_dateTimePicker_HD";
            this.to_dateTimePicker_HD.Size = new System.Drawing.Size(87, 20);
            this.to_dateTimePicker_HD.TabIndex = 11;
            // 
            // dateTimePickerHD
            // 
            this.dateTimePickerHD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerHD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerHD.Location = new System.Drawing.Point(12, 667);
            this.dateTimePickerHD.Name = "dateTimePickerHD";
            this.dateTimePickerHD.Size = new System.Drawing.Size(87, 20);
            this.dateTimePickerHD.TabIndex = 51;
            this.dateTimePickerHD.Visible = false;
            // 
            // HoaDonTKQCdataGridView
            // 
            this.HoaDonTKQCdataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HoaDonTKQCdataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.HoaDonTKQCdataGridView.ColumnHeadersHeight = 40;
            this.HoaDonTKQCdataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TrangThaiTKQC,
            this.TenTK,
            this.IDTKQC2,
            this.MaMKT,
            this.MKT_,
            this.HKD_,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.TONGTIENGOC,
            this.TienTe});
            this.HoaDonTKQCdataGridView.EnableHeadersVisualStyles = false;
            this.HoaDonTKQCdataGridView.Location = new System.Drawing.Point(24, 72);
            this.HoaDonTKQCdataGridView.Name = "HoaDonTKQCdataGridView";
            this.HoaDonTKQCdataGridView.ReadOnly = true;
            this.HoaDonTKQCdataGridView.RowHeadersWidth = 51;
            this.HoaDonTKQCdataGridView.RowTemplate.Height = 40;
            this.HoaDonTKQCdataGridView.Size = new System.Drawing.Size(1476, 543);
            this.HoaDonTKQCdataGridView.TabIndex = 52;
            this.HoaDonTKQCdataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.HoaDonTKQCdataGridView_RowPrePaint);
            // 
            // modeShowBtn
            // 
            this.modeShowBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.modeShowBtn.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.modeShowBtn.BackgroundColor = System.Drawing.SystemColors.MenuHighlight;
            this.modeShowBtn.BorderColor = System.Drawing.SystemColors.WindowText;
            this.modeShowBtn.BorderRadius = 19;
            this.modeShowBtn.BorderSize = 1;
            this.modeShowBtn.FlatAppearance.BorderSize = 0;
            this.modeShowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modeShowBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modeShowBtn.ForeColor = System.Drawing.Color.Black;
            this.modeShowBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.modeShowBtn.Location = new System.Drawing.Point(170, 693);
            this.modeShowBtn.Name = "modeShowBtn";
            this.modeShowBtn.Size = new System.Drawing.Size(137, 33);
            this.modeShowBtn.TabIndex = 53;
            this.modeShowBtn.Text = "Hiện Theo TKQC";
            this.modeShowBtn.TextColor = System.Drawing.Color.Black;
            this.modeShowBtn.UseVisualStyleBackColor = false;
            this.modeShowBtn.Click += new System.EventHandler(this.modeShowBtn_Click);
            // 
            // loadingBtnHoadon
            // 
            this.loadingBtnHoadon.BackColor = System.Drawing.SystemColors.Window;
            this.loadingBtnHoadon.BackgroundColor = System.Drawing.SystemColors.Window;
            this.loadingBtnHoadon.BorderColor = System.Drawing.Color.DarkOrange;
            this.loadingBtnHoadon.BorderRadius = 40;
            this.loadingBtnHoadon.BorderSize = 2;
            this.loadingBtnHoadon.FlatAppearance.BorderSize = 0;
            this.loadingBtnHoadon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadingBtnHoadon.ForeColor = System.Drawing.Color.White;
            this.loadingBtnHoadon.Image = global::AdsManager.Properties.Resources.loading1;
            this.loadingBtnHoadon.Location = new System.Drawing.Point(787, 211);
            this.loadingBtnHoadon.Name = "loadingBtnHoadon";
            this.loadingBtnHoadon.Size = new System.Drawing.Size(249, 244);
            this.loadingBtnHoadon.TabIndex = 49;
            this.loadingBtnHoadon.Text = "vbButton3";
            this.loadingBtnHoadon.TextColor = System.Drawing.Color.White;
            this.loadingBtnHoadon.UseVisualStyleBackColor = false;
            this.loadingBtnHoadon.Visible = false;
            // 
            // HDTT_RefeshBtn
            // 
            this.HDTT_RefeshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HDTT_RefeshBtn.BackColor = System.Drawing.SystemColors.Window;
            this.HDTT_RefeshBtn.BackgroundColor = System.Drawing.SystemColors.Window;
            this.HDTT_RefeshBtn.BorderColor = System.Drawing.SystemColors.WindowText;
            this.HDTT_RefeshBtn.BorderRadius = 19;
            this.HDTT_RefeshBtn.BorderSize = 1;
            this.HDTT_RefeshBtn.FlatAppearance.BorderSize = 0;
            this.HDTT_RefeshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HDTT_RefeshBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HDTT_RefeshBtn.ForeColor = System.Drawing.Color.Black;
            this.HDTT_RefeshBtn.Image = global::AdsManager.Properties.Resources.reload2;
            this.HDTT_RefeshBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HDTT_RefeshBtn.Location = new System.Drawing.Point(14, 693);
            this.HDTT_RefeshBtn.Name = "HDTT_RefeshBtn";
            this.HDTT_RefeshBtn.Size = new System.Drawing.Size(137, 33);
            this.HDTT_RefeshBtn.TabIndex = 48;
            this.HDTT_RefeshBtn.Text = "Làm mới dữ liệu";
            this.HDTT_RefeshBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.HDTT_RefeshBtn.TextColor = System.Drawing.Color.Black;
            this.HDTT_RefeshBtn.UseVisualStyleBackColor = false;
            this.HDTT_RefeshBtn.Click += new System.EventHandler(this.HDTT_RefeshBtn_Click);
            // 
            // TrangThaiTKQC
            // 
            this.TrangThaiTKQC.HeaderText = "Trạng Thái TKQC";
            this.TrangThaiTKQC.Name = "TrangThaiTKQC";
            this.TrangThaiTKQC.ReadOnly = true;
            this.TrangThaiTKQC.Width = 200;
            // 
            // TenTK
            // 
            this.TenTK.HeaderText = "Tên tài khoản";
            this.TenTK.Name = "TenTK";
            this.TenTK.ReadOnly = true;
            this.TenTK.Width = 600;
            // 
            // IDTKQC2
            // 
            this.IDTKQC2.HeaderText = "ID TKQC";
            this.IDTKQC2.Name = "IDTKQC2";
            this.IDTKQC2.ReadOnly = true;
            this.IDTKQC2.Width = 200;
            // 
            // MaMKT
            // 
            this.MaMKT.HeaderText = "Mã MKT";
            this.MaMKT.Name = "MaMKT";
            this.MaMKT.ReadOnly = true;
            // 
            // MKT_
            // 
            this.MKT_.HeaderText = "MKT";
            this.MKT_.Name = "MKT_";
            this.MKT_.ReadOnly = true;
            // 
            // HKD_
            // 
            this.HKD_.HeaderText = "HKD";
            this.HKD_.Name = "HKD_";
            this.HKD_.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "TỔNG HÓA ĐƠN";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "TỔNG TIỀN QUY DỔI";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // TONGTIENGOC
            // 
            this.TONGTIENGOC.HeaderText = "TỔNG TIỀN GỐC";
            this.TONGTIENGOC.Name = "TONGTIENGOC";
            this.TONGTIENGOC.ReadOnly = true;
            this.TONGTIENGOC.Width = 300;
            // 
            // TienTe
            // 
            this.TienTe.HeaderText = "TIỀN TỆ";
            this.TienTe.Name = "TienTe";
            this.TienTe.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Trạng thái TKQC";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 120;
            // 
            // AccName
            // 
            this.AccName.HeaderText = "Tên tài khoản";
            this.AccName.MinimumWidth = 6;
            this.AccName.Name = "AccName";
            this.AccName.ReadOnly = true;
            this.AccName.Width = 300;
            // 
            // IDTKQC
            // 
            this.IDTKQC.HeaderText = "ID TKQC";
            this.IDTKQC.MinimumWidth = 6;
            this.IDTKQC.Name = "IDTKQC";
            this.IDTKQC.ReadOnly = true;
            this.IDTKQC.Width = 200;
            // 
            // IDGiaoDich
            // 
            this.IDGiaoDich.HeaderText = "ID giao dịch";
            this.IDGiaoDich.MinimumWidth = 6;
            this.IDGiaoDich.Name = "IDGiaoDich";
            this.IDGiaoDich.ReadOnly = true;
            this.IDGiaoDich.Width = 200;
            // 
            // MaMKT_
            // 
            this.MaMKT_.HeaderText = "Mã MKT";
            this.MaMKT_.Name = "MaMKT_";
            this.MaMKT_.ReadOnly = true;
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
            // Ngay
            // 
            this.Ngay.HeaderText = "Ngày";
            this.Ngay.MinimumWidth = 6;
            this.Ngay.Name = "Ngay";
            this.Ngay.ReadOnly = true;
            this.Ngay.Width = 150;
            // 
            // SoTien
            // 
            this.SoTien.HeaderText = "Số tiền";
            this.SoTien.MinimumWidth = 6;
            this.SoTien.Name = "SoTien";
            this.SoTien.ReadOnly = true;
            this.SoTien.Width = 150;
            // 
            // PhuongThucThanhToan
            // 
            this.PhuongThucThanhToan.HeaderText = "Phương thức thanh toán";
            this.PhuongThucThanhToan.MinimumWidth = 6;
            this.PhuongThucThanhToan.Name = "PhuongThucThanhToan";
            this.PhuongThucThanhToan.ReadOnly = true;
            this.PhuongThucThanhToan.Width = 200;
            // 
            // TrangThaiThanhToan
            // 
            this.TrangThaiThanhToan.HeaderText = "Trạng thái thanh toán";
            this.TrangThaiThanhToan.MinimumWidth = 6;
            this.TrangThaiThanhToan.Name = "TrangThaiThanhToan";
            this.TrangThaiThanhToan.ReadOnly = true;
            this.TrangThaiThanhToan.Width = 150;
            // 
            // IDHoaDonVAT
            // 
            this.IDHoaDonVAT.HeaderText = "ID Hóa đơn VAT";
            this.IDHoaDonVAT.MinimumWidth = 6;
            this.IDHoaDonVAT.Name = "IDHoaDonVAT";
            this.IDHoaDonVAT.ReadOnly = true;
            this.IDHoaDonVAT.Width = 200;
            // 
            // MaThamChieu
            // 
            this.MaThamChieu.HeaderText = "Mã tham chiếu";
            this.MaThamChieu.MinimumWidth = 6;
            this.MaThamChieu.Name = "MaThamChieu";
            this.MaThamChieu.ReadOnly = true;
            this.MaThamChieu.Width = 200;
            // 
            // HoaDonTTForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1528, 735);
            this.ControlBox = false;
            this.Controls.Add(this.modeShowBtn);
            this.Controls.Add(this.loadingBtnHoadon);
            this.Controls.Add(this.HoaDonTKQCdataGridView);
            this.Controls.Add(this.dateTimePickerHD);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.HDTT_RefeshBtn);
            this.Controls.Add(this.HoaDonThanhToanToTalDataGridView);
            this.Controls.Add(this.HoaDonThanhToanDataGridView);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HoaDonTTForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.HoaDonThanhToanDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HoaDonThanhToanToTalDataGridView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HoaDonTKQCdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView HoaDonThanhToanDataGridView;
        private System.Windows.Forms.DataGridView HoaDonThanhToanToTalDataGridView;
        private VBButton HDTT_RefeshBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongHoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongTienToTal;
        private System.Windows.Forms.DataGridViewTextBoxColumn HanhDong;
        private VBButton loadingBtnHoadon;
        private System.Windows.Forms.Panel panel2;
        private VBButton locTimeBtn;
        private System.Windows.Forms.TextBox SearchTBox_HD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker from_dateTimePicker_HD;
        private VBButton searchBtn_HD;
        private VBButton vbButton4;
        private System.Windows.Forms.DateTimePicker to_dateTimePicker_HD;
        private System.Windows.Forms.DateTimePicker dateTimePickerHD;
        private System.Windows.Forms.DataGridView HoaDonTKQCdataGridView;
        private VBButton vbButton1;
        private VBButton modeShowBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDTKQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDGiaoDich;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaMKT_;
        private System.Windows.Forms.DataGridViewTextBoxColumn MKT;
        private System.Windows.Forms.DataGridViewTextBoxColumn HKD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ngay;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhuongThucThanhToan;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThaiThanhToan;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDHoaDonVAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaThamChieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThaiTKQC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenTK;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDTKQC2;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaMKT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MKT_;
        private System.Windows.Forms.DataGridViewTextBoxColumn HKD_;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn TONGTIENGOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TienTe;
    }
}