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
                        AsyncCoreDoWork(TKQC);
                    }
                    Thread.Sleep(200);
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


                var detailHoaDon = GetDataHoaDon(cookie, dtsg, accId);
                JObject jObject = JObject.Parse(detailHoaDon);
                for (int i = 0; i < jObject["data"]["node"]["billing_txns"]["edges"].Count(); i++)
                {
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
                        HoaDon.HanhDong = "";
                        HoaDon.Ngay = Common.UnixTimeStampToDateTime(double.Parse(HoaDon.Ngay)).ToString();
                        HoaDonInstance.OnNewAccAdded(HoaDon);
                        HoaDonInstance.lsHoaDon.Add(HoaDon);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDon);
                    }
                    catch (Exception ex) { }
                }
            }
            catch (Exception ex)
            {
                //test commit nữa
            }
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
                                                    e.Info.Ngay != null ? e.Info.Ngay : "",
                                                    e.Info.SoTien != null ? e.Info.SoTien : "",
                                                    e.Info.PTThanhToan != null ? e.Info.PTThanhToan : "",
                                                    e.Info.TrangThaiThanhToan != null ? e.Info.TrangThaiThanhToan : "",
                                                    e.Info.IDHoaDonVAT != null ? e.Info.IDHoaDonVAT : "",
                                                    e.Info.HanhDong != null ? e.Info.HanhDong : ""
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
                if (HoaDonInstance.lsHoaDon[i].SoTien.Contains("$"))
                {
                    HoaDonInstance.tongTien += double.Parse(HoaDonInstance.lsHoaDon[i].SoTien.Replace('$', ' ').Trim())*TTTKQCInstance.tiGia["USD"];
                }
                else
                {
                    HoaDonInstance.tongTien += double.Parse(HoaDonInstance.lsHoaDon[i].SoTien.Replace('.', ' ').Trim().Split(' ')[0].Replace('₫', ' '));
                }   
            }
            HoaDonThanhToanToTalDataGridView.Rows.Clear();
            HoaDonThanhToanToTalDataGridView.Rows.Insert(HoaDonThanhToanToTalDataGridView.Rows.Count - 1,
                                                    HoaDonInstance.tongHoaDon.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                                                     HoaDonInstance.tongTien.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0]);

            //TTTKQCInstance.tongTK.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
            loadingBtnHoadon.Visible = false;
        }

        private void HDTT_RefeshBtn_Click(object sender, EventArgs e)
        {
            loadingBtnHoadon.Visible = true;
            HoaDonThanhToanDataGridView.Rows.Clear();
            HDTT_RefeshBtn.BackgroundColor = Color.FromArgb(255, 198, 65, 24);
            Hoadon_Worker.RunWorkerAsync();
        }

        public static string GetDataHoaDon(string cookie, string dtsg, string accId)
        {
            try
            {
                var url = "https://business.facebook.com/api/graphql/";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                var variables = "{\"count\":10000,\"cursor\":null,\"end_time\":1665939600,\"filters\":[],\"start_time\":1634403600,\"id\":\"" + accId + "\"}";
                var postData = "variables=" + variables;
                postData += "&doc_id=" + "5088279497901543";
                postData += "&fb_dtsg=" + dtsg;

                var data = Encoding.UTF8.GetBytes(postData);

                httpRequest.Method = "POST";

                httpRequest.Headers["cookie"] = cookie;
                httpRequest.Headers["sec-fetch-site"] = "same-origin";
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36";
                httpRequest.ContentType = "application/x-www-form-urlencoded";

                using (var stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return result;
                    }
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

                    if (HoaDonInstance.lsHoaDon[i].TrangThaiTKQC != null && HoaDonInstance.lsHoaDon[i].TrangThaiTKQC.Contains(HoaDonInstance.searchData))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }
                    else if (HoaDonInstance.lsHoaDon[i].TenTaiKhoan != null && HoaDonInstance.lsHoaDon[i].TenTaiKhoan.Contains(HoaDonInstance.searchData))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }else if (HoaDonInstance.lsHoaDon[i].IDTKQC != null && HoaDonInstance.lsHoaDon[i].IDTKQC.Contains(HoaDonInstance.searchData))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }else if (HoaDonInstance.lsHoaDon[i].IDGiaoDich != null && HoaDonInstance.lsHoaDon[i].IDGiaoDich.Contains(HoaDonInstance.searchData))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }else if (HoaDonInstance.lsHoaDon[i].Ngay != null && HoaDonInstance.lsHoaDon[i].Ngay.Contains(HoaDonInstance.searchData))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }else if (HoaDonInstance.lsHoaDon[i].SoTien != null && HoaDonInstance.lsHoaDon[i].SoTien.Contains(HoaDonInstance.searchData))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }else if (HoaDonInstance.lsHoaDon[i].PTThanhToan != null && HoaDonInstance.lsHoaDon[i].PTThanhToan.Contains(HoaDonInstance.searchData))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }else if (HoaDonInstance.lsHoaDon[i].TrangThaiThanhToan != null && HoaDonInstance.lsHoaDon[i].TrangThaiThanhToan.Contains(HoaDonInstance.searchData))
                    {
                        HoaDonInstance.OnNewAccAdded(HoaDonInstance.lsHoaDon[i]);
                        HoaDonInstance.lsViewHoaDon.Add(HoaDonInstance.lsHoaDon[i]);
                    }else if (HoaDonInstance.lsHoaDon[i].IDHoaDonVAT != null && HoaDonInstance.lsHoaDon[i].IDHoaDonVAT.Contains(HoaDonInstance.searchData))
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
    }

    class HoaDonThreadState
    {
        public static bool all_thread_together_running { get; set; }
        public static string proxy { get; set; }
        public static bool allow_running { get; set; }
    }
}
