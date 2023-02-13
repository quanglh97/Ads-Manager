using AdsManager.HelperLibrary.Facebook.Info;
using AdsManager.HelperLibrary.Facebook;
using AdsManager.HelperLibrary.Facebook.Instance;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TakeCarePage.HelperLibrary.Facebook;
using TakeCarePage;
using RestSharp;
using System.Diagnostics;
using AdsManager.HelperLibrary;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;

namespace AdsManager.form.dataTKQC
{
    public partial class HoaDonTTForm : Form
    {
        BackgroundWorker Hoadon_Worker = null;
        volatile int live_thread_number = 0;
        Queue<int> qu_position_pro5 = new Queue<int>();
        public HoaDonTTForm()
        {
            InitializeComponent();

            HoaDonThanhToanDataGridView.RowHeadersVisible = false;
            HoaDonThanhToanDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            HoaDonThanhToanDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 10, 147, 66); ;//10, 147, 66
            HoaDonThanhToanDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            HoaDonTKQCdataGridView.RowHeadersVisible = false;
            HoaDonTKQCdataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            HoaDonTKQCdataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 10, 147, 66); ;//10, 147, 66
            HoaDonTKQCdataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            HoaDonThanhToanToTalDataGridView.RowHeadersVisible = false;
            HoaDonThanhToanToTalDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            HoaDonThanhToanToTalDataGridView.Columns[0].HeaderCell.Style.BackColor = Color.FromArgb(255, 45, 127, 104);
            HoaDonThanhToanToTalDataGridView.Columns[1].HeaderCell.Style.BackColor = Color.FromArgb(255, 198, 65, 24);
            HoaDonThanhToanToTalDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            Hoadon_Worker = new BackgroundWorker();
            Hoadon_Worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            Hoadon_Worker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            Hoadon_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
            Hoadon_Worker.WorkerReportsProgress = true;
            Hoadon_Worker.WorkerSupportsCancellation = true;

            HoaDonInstance.NewAccountAdded += this.Statistic_newAccountAdded;
            HoaDonTKQCdataGridView.Visible = false;
        }

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            live_thread_number = 0;
            qu_position_pro5.Clear();
            for (int i = 1; i <= 100; i++)
                qu_position_pro5.Enqueue(i);

            HoaDonThreadState.allow_running = true;
            HoaDonThreadState.proxy = "";
            HoaDonThreadState.all_thread_together_running = true;
            try
            {
                HoaDonInstance.lsHoaDon.Clear();
                List<TTTKQCInfo> lsTKQCClone = new List<TTTKQCInfo>(TTTKQCInstance.lsTKQC.Count);
                TTTKQCInstance.lsTKQC.ForEach((item) =>
                {
                    lsTKQCClone.Add(new TTTKQCInfo(item));
                });


                for (int index = 0; index < lsTKQCClone.Count; index++)
                {
                    var TKQC = lsTKQCClone[index];
                    while (!Hoadon_Worker.CancellationPending)
                    {
                        // [1.1.1] live thread = 0 => reset ip
                        if (live_thread_number == 0)
                        {
                            // [option] Change ip HMA
                            break;
                        }

                        // [1.1.2] if not reach max thread -> break to open new thread
                        if (live_thread_number < 100 && live_thread_number != 0)
                            break;

                        // [1.1.3] reach max thread + all thread run together -> wait for end all -> break
                        if (live_thread_number == 100)
                        {
                            // wait for all thread finished -> open new thread with number of thread max
                            while (!Hoadon_Worker.CancellationPending && live_thread_number != 0 && HoaDonThreadState.all_thread_together_running) // if = 0 => break
                                Thread.Sleep(TimeSpan.FromSeconds(1));
                        }

                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }


                    // [1.2] do work, dam bao da reset proxy o tren theo option
                    if (!Hoadon_Worker.CancellationPending && HoaDonThreadState.allow_running)
                    {
                        // update proxy for each thread if run together
                        live_thread_number++;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                        AsyncCoreDoWork(TKQC);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                    }
                    Thread.Sleep(5000);
                }

                while (live_thread_number > 0)
                {
                    Thread.Sleep(5000);
                }


                Hoadon_Worker.ReportProgress(1);
                Hoadon_Worker.ReportProgress(0, ProxyInstances.Http);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        async Task AsyncCoreDoWork(TTTKQCInfo TKQC)
        {
            await Task.Run(() =>
            {
                // update live thread ++ & position for chrome profile    
                int pro5 = qu_position_pro5.Dequeue();
                // do task
                if (!Hoadon_Worker.CancellationPending)
                {
                    DoTaskOne(TKQC);
                }
                // update number of thread when finished
                live_thread_number--;
                // update pro5 of position
                qu_position_pro5.Enqueue(pro5);
            });
        }

        void DoTaskOne(TTTKQCInfo TKQC)
        {
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            try
            {
                var businessId = TKQC.taoTuBM;
                var accId = TKQC.ID;
                var cookie = TKQC.cookie;
                var token = TKQC.token;
                var dtsg = TKQC.dtsg;
                if (dtsg == "")
                {
                    return;
                }
                var startTime = HoaDonInstance.startTime;
                var endTime = HoaDonInstance.endTime;

                var detailHoaDon = GetDataHoaDon(cookie, dtsg, accId, startTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString().Split('.')[0],
                    endTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString().Split('.')[0]);
                JObject jObject = JObject.Parse(detailHoaDon);
                for (int i = 0; i < jObject["data"]["node"]["billing_txns"]["edges"].Count(); i++)
                {
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                    try
                    {
                        if (jObject["data"]["node"]["billing_txns"]["edges"][i]["node"]["formatted_payment_status"].ToString().Contains("Không"))
                        {
                            continue;
                        }
                        var HoaDon = new HoaDonInfo();
                        HoaDon.TrangThaiTKQC = TKQC.trangThai;
                        HoaDon.TenTaiKhoan = TKQC.tenTK;
                        HoaDon.IDTKQC = accId;
                        HoaDon.IDGiaoDich = jObject["data"]["node"]["billing_txns"]["edges"][i]["node"]["id"].ToString();
                        HoaDon.Ngay = jObject["data"]["node"]["billing_txns"]["edges"][i]["node"]["transaction_time"].ToString(); ;//transaction_time
                        HoaDon.SoTien = jObject["data"]["node"]["billing_txns"]["edges"][i]["node"]["total_amount"]["formatted_amount"].ToString();
                        HoaDon.PTThanhToan = jObject["data"]["node"]["billing_txns"]["edges"][i]["node"]["payment_method_label"].ToString();
                        HoaDon.TrangThaiThanhToan = jObject["data"]["node"]["billing_txns"]["edges"][i]["node"]["formatted_payment_status"].ToString();
                        HoaDon.IDHoaDonVAT = jObject["data"]["node"]["billing_txns"]["edges"][i]["node"]["vat_invoice_id"].ToString();
                        HoaDon.MaThamChieu = jObject["data"]["node"]["billing_txns"]["edges"][i]["node"]["tracking_id"].ToString();
                        HoaDon.Ngay = Common.UnixTimeStampToDateTime(double.Parse(HoaDon.Ngay), TKQC.soTimeZone).ToString();

                        HoaDon.MKT = TKQC.MKT;
                        HoaDon.MaMKT = TKQC.MaMKT;
                        HoaDon.HKD = TKQC.HKD;

                        HoaDonInstance.OnNewAccAdded(HoaDon);
                        HoaDonInstance.lsHoaDon.Add(HoaDon);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDon);
                    }
                    catch (Exception ex) { }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                }
            }
            catch (Exception ex)
            {
                //test commit nữa
            }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        }



        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Here you play with the main UI thread
            //string runningStatus = e.UserState.ToString();
            if (e.ProgressPercentage == 0)
            {
                ConcurrentQueue<string> queueHttp = (ConcurrentQueue<string>)e.UserState;

            }
            else if (e.ProgressPercentage == 1)
            {
            }
        }

        private delegate void InvokeDelegate(HoaDonEventArgs e);
        private void Statistic_newAccountAdded(object sender, HoaDonEventArgs e)
        {
            this.BeginInvoke(new InvokeDelegate(AddToGirdView), new object[] { e });
        }
        private void AddToGirdView(HoaDonEventArgs e)
        {
            if (e == null || e.Info == null)
            {
                return;
            }

            HoaDonThanhToanDataGridView.Rows.Insert(HoaDonThanhToanDataGridView.Rows.Count - 1,
                                                    e.Info.TrangThaiTKQC != null ? e.Info.TrangThaiTKQC : "",
                                                    e.Info.TenTaiKhoan != null ? e.Info.TenTaiKhoan : "",
                                                    e.Info.IDTKQC != null ? e.Info.IDTKQC : "",
                                                    e.Info.IDGiaoDich != null ? e.Info.IDGiaoDich : "",
                                                    e.Info.MaMKT != null ? e.Info.MaMKT : "",
                                                    e.Info.MKT != null ? e.Info.MKT : "",
                                                    e.Info.HKD != null ? e.Info.HKD : "",
                                                    e.Info.Ngay != null ? e.Info.Ngay : "",
                                                    e.Info.SoTien != null ? e.Info.SoTien : "",
                                                    e.Info.PTThanhToan != null ? e.Info.PTThanhToan : "",
                                                    e.Info.TrangThaiThanhToan != null ? e.Info.TrangThaiThanhToan : "",
                                                    e.Info.IDHoaDonVAT != null ? e.Info.IDHoaDonVAT : "",
                                                    e.Info.MaThamChieu != null ? e.Info.MaThamChieu : ""
                                                    );

        }

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            while (live_thread_number > 0)
            {
                Thread.Sleep(5000);
            }
            HDTT_RefeshBtn.BackgroundColor = Color.FromArgb(255, 255, 255, 255);
            HDTT_RefeshBtn.Enabled = true;
            HoaDonInstance.tongHoaDon = HoaDonInstance.lsHoaDon.Count;
            HoaDonInstance.tongTien = 0;
            for (int i = 0; i < HoaDonInstance.lsHoaDon.Count; i++)
            {
                var tien = HoaDonInstance.lsHoaDon[i].SoTien;
                if (HoaDonInstance.lsHoaDon[i].SoTien.Contains("$"))
                {
                    tien = tien.Replace("$", "").Trim();
                    if (tien.Contains('.'))
                    {
                        tien = tien.Replace(".", "");
                    }
                    if (tien.Contains(','))
                    {
                        tien = tien.Replace(",", ".");
                    }
                    HoaDonInstance.tongTien += double.Parse(tien) * TTTKQCInstance.tiGia["USD"];
                }
                else
                {

                    if (tien.Contains('.'))
                    {
                        tien = tien.Replace(".", "");
                    }
                    if (tien.Contains(','))
                    {
                        tien = tien.Replace(",", ".");
                    }
                    
                    if (tien.Contains("₫"))
                    {
                        tien = tien.Replace("₫", "").Trim();
                    }
                    if (tien != "")
                    {
                        HoaDonInstance.tongTien += double.Parse(tien.Replace(" ", ""));
                    }
                }
            }
            HoaDonThanhToanToTalDataGridView.Rows.Clear();
            HoaDonThanhToanToTalDataGridView.Rows.Insert(HoaDonThanhToanToTalDataGridView.Rows.Count - 1,
                                                    HoaDonInstance.tongHoaDon.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                                                     HoaDonInstance.tongTien.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0]);

            HoaDonTKQCdataGridView.Rows.Clear();


            for (int j = 0; j < TTTKQCInstance.lsTKQC.Count; j++)
            {
                TTTKQCInstance.lsTKQC[j].tongTien = 0;
                TTTKQCInstance.lsTKQC[j].tongTienGoc = 0;
                TTTKQCInstance.lsTKQC[j].tongHoaDon = 0;
                for (int i = 0; i < HoaDonInstance.lsHoaDon.Count; i++)
                {
                    if (TTTKQCInstance.lsTKQC[j].ID == HoaDonInstance.lsHoaDon[i].IDTKQC)
                    {
                        var tien = HoaDonInstance.lsHoaDon[i].SoTien;
                        if (tien.Contains("$"))
                        {
                            tien = tien.Replace("$", "").Trim();
                            if (tien.Contains('.'))
                            {
                                tien = tien.Replace(".", "");
                            }
                            if (tien.Contains(','))
                            {
                                tien = tien.Replace(",", ".");
                            }
                            TTTKQCInstance.lsTKQC[j].tongTien += double.Parse(tien) * TTTKQCInstance.tiGia["USD"];
                            TTTKQCInstance.lsTKQC[j].tongTienGoc += double.Parse(tien);
                        }
                        else
                        {

                            if (tien.Contains('.'))
                            {
                                tien = tien.Replace(".", "");
                            }
                            if (tien.Contains(','))
                            {
                                tien = tien.Replace(",", ".");
                            }
                            if (tien.Contains("₫"))
                            {
                                tien = tien.Replace("₫", "").Trim();
                            }
                            if (tien != "")
                            {
                                TTTKQCInstance.lsTKQC[j].tongTien += double.Parse(tien.Replace(" ", ""));
                                TTTKQCInstance.lsTKQC[j].tongTienGoc += double.Parse(tien.Replace(" ", ""));
                            }
                        }
                        TTTKQCInstance.lsTKQC[j].tongHoaDon++;
                    }
                }
                HoaDonTKQCdataGridView.Rows.Insert(HoaDonTKQCdataGridView.Rows.Count - 1,
                    TTTKQCInstance.lsTKQC[j].trangThai,
                    TTTKQCInstance.lsTKQC[j].tenTK,
                    TTTKQCInstance.lsTKQC[j].ID,
                    TTTKQCInstance.lsTKQC[j].MaMKT,
                    TTTKQCInstance.lsTKQC[j].MKT,
                    TTTKQCInstance.lsTKQC[j].HKD,
                    TTTKQCInstance.lsTKQC[j].tongHoaDon.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                    TTTKQCInstance.lsTKQC[j].tongTien.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                    TTTKQCInstance.lsTKQC[j].tongTienGoc.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                    TTTKQCInstance.lsTKQC[j].tienTe);
                modeShowBtn.Enabled = true;
            }



            //TTTKQCInstance.tongTK.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
            loadingBtnHoadon.Visible = false;
        }

        private void HDTT_RefeshBtn_Click(object sender, EventArgs e)
        {
            loadingBtnHoadon.Visible = true;
            HoaDonTKQCdataGridView.Visible = false;
            modeShowBtn.Text = "Hiện theo TKQC";
            modeShowBtn.Enabled = false;
            HoaDonThanhToanDataGridView.Visible = true;
            HoaDonThanhToanDataGridView.Rows.Clear();
            HoaDonTKQCdataGridView.Rows.Clear();
            HDTT_RefeshBtn.BackgroundColor = Color.FromArgb(255, 198, 65, 24);

            HoaDonInstance.startTime = dateTimePickerHD.Value.AddYears(-15).ToUniversalTime();
            HoaDonInstance.endTime = dateTimePickerHD.Value.ToUniversalTime();

            Hoadon_Worker.RunWorkerAsync();
        }

        public static string GetDataHoaDon(string stringCookie, string dtsg, string accId, string startTime, string endTime)
        {
            try
            {
                var id = "";
                var client = new RestClient("https://business.facebook.com/api/graphql/");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                var cookies = stringCookie.Split(';');
                foreach (string cookie in cookies)
                {
                    if (cookie == "") break;
                    var datacookie = cookie.Trim().Split('=');
                    request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
                }
                request.AddHeader("sec-fetch-site", "same-origin");
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("__ccg", "EXCELLENT");
                request.AddParameter("fb_dtsg", dtsg);
                request.AddParameter("fb_api_caller_class", "RelayModern");
                request.AddParameter("fb_api_req_friendly_name", "BillingTransactionTableQuery");
                var variables = "{\"paymentAccountID\":\"" + accId +
                    "\",\"count\":10,\"cursor\":null,\"filters\":[],\"start_time\":" + startTime +
                    ",\"end_time\":" + endTime + "}";
                request.AddParameter("variables", variables);
                request.AddParameter("server_timestamps", "true");
                request.AddParameter("doc_id", "5041460922628774");
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    JObject jObject = JObject.Parse(response.Content);
                    id = jObject["data"]["billable_account_by_payment_account"]["billing_payment_account"]["id"].ToString();
                }

                client = new RestClient("https://business.facebook.com/api/graphql/");
                client.Timeout = -1;
                request = new RestRequest(Method.POST);
                foreach (string cookie in cookies)
                {
                    if (cookie == "") break;
                    var datacookie = cookie.Trim().Split('=');
                    request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
                }
                request.AddHeader("sec-fetch-site", "same-origin");
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("fb_dtsg", dtsg);
                request.AddParameter("fb_api_caller_class", "RelayModern");
                request.AddParameter("fb_api_req_friendly_name", "BillingTransactionTableConnection");
                variables = "{\"count\":1000,\"cursor\":null,\"end_time\":" + endTime + ",\"filters\":[],\"start_time\":" + startTime + ",\"id\":\"" + id + "\"}";
                request.AddParameter("variables", variables);
                request.AddParameter("server_timestamps", "true");
                request.AddParameter("doc_id", "5088279497901543");
                response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response.Content;
                }
                return "";

            }
            catch
            {
                return "";
            }
        }

        private void HoaDonThanhToanDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                HoaDonThanhToanDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237);
            }
            else
            {
                HoaDonThanhToanDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255);
            }
        }

        private void HoaDonThanhToanDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (HoaDonThanhToanDataGridView.Rows[e.RowIndex].Cells[0].Value != null && HoaDonThanhToanDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Vô"))
            {
                HoaDonThanhToanDataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
            }
            else if (HoaDonThanhToanDataGridView.Rows[e.RowIndex].Cells[0].Value != null && HoaDonThanhToanDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Hoạt"))
            {
                HoaDonThanhToanDataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
            }
            else
            {
                HoaDonThanhToanDataGridView.Rows[e.RowIndex].Cells[0].Style = HoaDonThanhToanDataGridView.DefaultCellStyle;
            }
        }

        private void searchHD_Click(object sender, EventArgs e)
        {
            loadingBtnHoadon.Visible = true;
            HoaDonThanhToanDataGridView.Rows.Clear();
            HoaDonInstance.searchData = SearchTBox_HD.Text;
            if (HoaDonInstance.searchData != "")
            {
                Thread.Sleep(1000);
                HoaDonInstance.lsViewHoaDon.Clear();
                for (int i = 0; i < HoaDonInstance.lsHoaDon.Count; i++)
                {

                    if (HoaDonInstance.lsHoaDon[i].TrangThaiTKQC != null && HoaDonInstance.lsHoaDon[i].TrangThaiTKQC.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].TenTaiKhoan != null && HoaDonInstance.lsHoaDon[i].TenTaiKhoan.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].IDTKQC != null && HoaDonInstance.lsHoaDon[i].IDTKQC.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].IDGiaoDich != null && HoaDonInstance.lsHoaDon[i].IDGiaoDich.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].MKT != null && HoaDonInstance.lsHoaDon[i].MKT.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].MaMKT != null && HoaDonInstance.lsHoaDon[i].MaMKT.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].HKD != null && HoaDonInstance.lsHoaDon[i].HKD.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].Ngay != null && HoaDonInstance.lsHoaDon[i].Ngay.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].SoTien != null && HoaDonInstance.lsHoaDon[i].SoTien.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].PTThanhToan != null && HoaDonInstance.lsHoaDon[i].PTThanhToan.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].TrangThaiThanhToan != null && HoaDonInstance.lsHoaDon[i].TrangThaiThanhToan.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].IDHoaDonVAT != null && HoaDonInstance.lsHoaDon[i].IDHoaDonVAT.ToLower().Contains(HoaDonInstance.searchData.ToLower()))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                }
            }
            else if (HoaDonInstance.searchData == "")
            {
                Thread.Sleep(1000);
                HoaDonInstance.lsViewHoaDon.Clear();
                for (int i = 0; i < HoaDonInstance.lsHoaDon.Count; i++)
                {
                    HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                    HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                }
            }
            loadingBtnHoadon.Visible = false;
        }

        private void copyAlltoClipboard()
        {
            HoaDonThanhToanDataGridView.SelectAll();
            DataObject dataObj = HoaDonThanhToanDataGridView.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void copyAlltoClipboardforTKQC()
        {
            HoaDonTKQCdataGridView.SelectAll();
            DataObject dataObj = HoaDonTKQCdataGridView.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void vbButton4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = $"ThongTinHoaDon_{DateTime.Now.ToShortDateString().Replace('/', '_')}.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Copy DataGridView results to clipboard
                copyAlltoClipboard();

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Format column D as text before pasting results, this was required for my data
                Excel.Range rng = xlWorkSheet.get_Range("D:D").Cells;
                rng.NumberFormat = "@";

                // Paste clipboard results to worksheet range
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
                // Delete blank column A and select cell A1
                Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
                delRng.Delete(Type.Missing);
                xlWorkSheet.get_Range("A1").Select();

                // Save the excel file under the captured location from the SaveFileDialog
                xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlexcel);

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();
                HoaDonThanhToanDataGridView.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void locTimeBtn_Click(object sender, EventArgs e)
        {
            loadingBtnHoadon.Visible = true;
            HoaDonTKQCdataGridView.Visible = false;
            modeShowBtn.Text = "Hiện theo TKQC";
            modeShowBtn.Enabled = false;
            HoaDonThanhToanDataGridView.Visible = true;
            HoaDonThanhToanDataGridView.Rows.Clear();
            HoaDonTKQCdataGridView.Rows.Clear();
            HDTT_RefeshBtn.BackgroundColor = Color.FromArgb(255, 198, 65, 24);

            HoaDonInstance.startTime = from_dateTimePicker_HD.Value.ToUniversalTime();
            HoaDonInstance.endTime = to_dateTimePicker_HD.Value.AddDays(1).ToUniversalTime();
            if(!Hoadon_Worker.IsBusy)
                Hoadon_Worker.RunWorkerAsync();
        }

        private void vbButton1_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = $"ThongTinHoaDonTheoTKQC_{DateTime.Now.ToShortDateString().Replace('/', '_')}.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Copy DataGridView results to clipboard
                copyAlltoClipboardforTKQC();

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Format column D as text before pasting results, this was required for my data
                Excel.Range rng = xlWorkSheet.get_Range("D:D").Cells;
                rng.NumberFormat = "@";

                // Paste clipboard results to worksheet range
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
                // Delete blank column A and select cell A1
                Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
                delRng.Delete(Type.Missing);
                xlWorkSheet.get_Range("A1").Select();

                // Save the excel file under the captured location from the SaveFileDialog
                xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlexcel);

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();
                HoaDonThanhToanDataGridView.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void modeShowBtn_Click(object sender, EventArgs e)
        {
            if (modeShowBtn.Text.Contains("TKQC"))
            {
                HoaDonTKQCdataGridView.Visible = true;
                HoaDonThanhToanDataGridView.Visible = false;
                modeShowBtn.Text = "Hiện chi tiết";
            }
            else
            {
                HoaDonTKQCdataGridView.Visible = false;
                HoaDonThanhToanDataGridView.Visible = true;
                modeShowBtn.Text = "Hiện theo TKQC";
            }
        }

        private void HoaDonTKQCdataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                HoaDonTKQCdataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237);
            }
            else
            {
                HoaDonTKQCdataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255);
            }
        }
    }

    class HoaDonThreadState
    {
        public static bool all_thread_together_running { get; set; }
        public static string proxy { get; set; }
        public static bool allow_running { get; set; }
    }
}
