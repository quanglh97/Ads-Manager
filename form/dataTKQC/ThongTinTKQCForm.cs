using AdsManager.HelperLibrary;
using AdsManager.HelperLibrary.Facebook;
using AdsManager.HelperLibrary.Facebook.Instance;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TakeCarePage;
using TakeCarePage.HelperLibrary.Facebook;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

using Excel = Microsoft.Office.Interop.Excel;

namespace AdsManager
{
    public partial class ThongTinTKQCForm : Form
    {
        BackgroundWorker TTTKQC_Worker = null;
        volatile int live_thread_number = 0;
        Queue<int> qu_position_pro5 = new Queue<int>();

        public static bool statusHoatDong = true;
        public static bool statusVoHieuHoa = true;
        public static bool statusCanThanhToan = true;
        public static bool statusKhac = true;

        public ThongTinTKQCForm()
        {
            InitializeComponent();
            thongTinTKQCDataGridView.RowHeadersVisible = false;
            thongTinTKQCDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            thongTinTKQCDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 10, 147, 66); ;//10, 147, 66
            thongTinTKQCDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            thongTinTKQCToTalDataGridView.RowHeadersVisible = false;
            thongTinTKQCToTalDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //thongTinTKQCToTalDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 233, 75, 79);
            thongTinTKQCToTalDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            thongTinTKQCToTalDataGridView.Columns[0].HeaderCell.Style.BackColor = Color.FromArgb(255, 45, 127, 104);
            thongTinTKQCToTalDataGridView.Columns[1].HeaderCell.Style.BackColor = Color.FromArgb(255, 198, 65, 24);
            thongTinTKQCToTalDataGridView.Columns[2].HeaderCell.Style.BackColor = Color.FromArgb(255, 45, 127, 104);
            thongTinTKQCToTalDataGridView.Columns[3].HeaderCell.Style.BackColor = Color.FromArgb(255, 198, 65, 24);

            TTTKQC_Worker = new BackgroundWorker();
            TTTKQC_Worker.DoWork += new DoWorkEventHandler(TTTKQC_Worker_DoWork);
            TTTKQC_Worker.ProgressChanged += new ProgressChangedEventHandler(TTTKQC_Worker_ProgressChanged);
            TTTKQC_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(TTTKQC_Worker_RunWorkerCompleted);
            TTTKQC_Worker.WorkerReportsProgress = true;
            TTTKQC_Worker.WorkerSupportsCancellation = true;




            TTTKQCInstance.NewAccountAdded += this.Statistic_newAccountAdded;

            List<string> listItem = new List<string>()
            {
                 "1",
                 "5",
                 "10",
                 "50",
                 "100",
                 "500",
                 "MAX"
            };
            TTTKQCComboBox.DataSource = listItem;
        }

        void TTTKQC_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            live_thread_number = 0;
            qu_position_pro5.Clear();
            for (int i = 1; i <= 100; i++)
                qu_position_pro5.Enqueue(i);

            TKQCThreadState.allow_running = true;
            TKQCThreadState.proxy = "";
            TKQCThreadState.all_thread_together_running = true;
            try
            {
                TTTKQCInstance.lsTKQC.Clear();
                TTTKQCInstance.lsViewTKQC.Clear();
                for (int index = 0; index < ViaInstances.lsAccVia.Count; index++)
                {
                    var userId = ViaInstances.lsAccVia[index].userId;
                    var cookie = ViaInstances.lsAccVia[index].cookie;
                    var token = ViaInstances.lsAccVia[index].token;
                    var tick = ViaInstances.lsAccVia[index].tick;
                    if (tick == false) continue;
                    var dtsg = AccInfo.Get_fb_dtsg(cookie);
                    if (token == "" || dtsg == "" || tick == false)
                        continue;

                    var beginDataTKQC = RequestTTTKQCAsync(userId, token, cookie);
                    JObject jObject = JObject.Parse(beginDataTKQC);
                    for (int i = 0; i < jObject["data"].Count(); i++)
                    {
                        var item = jObject["data"][i];
                        while (!TTTKQC_Worker.CancellationPending)
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
                                while (!TTTKQC_Worker.CancellationPending && live_thread_number != 0 && TKQCThreadState.all_thread_together_running) // if = 0 => break
                                    Thread.Sleep(TimeSpan.FromSeconds(1));
                            }

                            Thread.Sleep(TimeSpan.FromSeconds(1));
                        }


                        // [1.2] do work, dam bao da reset proxy o tren theo option
                        if (!TTTKQC_Worker.CancellationPending && TKQCThreadState.allow_running)
                        {
                            // update proxy for each thread if run together
                            live_thread_number++;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                            AsyncCoreDoWork(item, cookie, token, dtsg);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                        }

                        Thread.Sleep(200);
                        //Thread.Sleep(2000);
                    }
                    while (live_thread_number > 0)
                    {
                        Thread.Sleep(5000);
                    }


                    TTTKQC_Worker.ReportProgress(1);
                    TTTKQC_Worker.ReportProgress(0, ProxyInstances.Http);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        async Task AsyncCoreDoWork(JToken item, string cookie, string token, string dtsg)
        {
            await Task.Run(() =>
            {
                // update live thread ++ & position for chrome profile    
                int pro5 = qu_position_pro5.Dequeue();
                // do task
                if (!TTTKQC_Worker.CancellationPending)
                {
                    DoTaskOne(item, cookie, token, dtsg);
                }
                // update number of thread when finished
                live_thread_number--;
                // update pro5 of position
                qu_position_pro5.Enqueue(pro5);
            });
        }

        void DoTaskOne(JToken item, string cookie, string token, string dtsg)
        {
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            try
            {
                var TKQC = new TTTKQCInfo();
                TKQC.cookie = cookie;
                TKQC.token = token;
                TKQC.dtsg = dtsg;
                TKQC.tenTK = item["name"].ToString();
                TKQC.ID = item["account_id"].ToString();
                var status = item["account_status"];
                if ((int)status == 2)
                    TKQC.trangThai = "Vô hiệu hóa";
                else if ((int)status == 3)
                    TKQC.trangThai = "Cần thanh toán";
                else
                    TKQC.trangThai = "Hoạt động";

                var moreInfo = RequestFullInfoTTTKQCAsync(TKQC.ID, token, cookie);

                JObject jObjectMoreInfo = JObject.Parse(moreInfo);

                TKQC.tienTe = jObjectMoreInfo["currency"].ToString();

                try
                {
                    if (!TKQC.tienTe.Contains("VND"))
                    {
                        TKQC.Nguong = (double.Parse(jObjectMoreInfo["adspaymentcycle"]["data"][0]["threshold_amount"].ToString()) / 100).ToString();
                        try { TKQC.soDu = (double.Parse(jObjectMoreInfo["balance"].ToString()) / 100).ToString(); } catch (Exception) { TKQC.soDu = "0"; }
                    }
                    else
                    {
                        TKQC.Nguong = jObjectMoreInfo["adspaymentcycle"]["data"][0]["threshold_amount"].ToString();
                        try
                        {
                            TKQC.soDu = jObjectMoreInfo["balance"].ToString();
                        }
                        catch (Exception) { TKQC.soDu = "0"; }
                    }

                }
                catch { TKQC.Nguong = "0"; }

                if (TKQC.soDu == null) TKQC.soDu = "0";

                try { TKQC.Limit = jObjectMoreInfo["adtrust_dsl"].ToString(); }
                catch (Exception) { TKQC.Limit = "0"; }
                //if(TKQC.Limit == "-1") TKQC.Limit = "NoLimit";
                try { TKQC.tongTieu = jObjectMoreInfo["insights"]["data"][0]["spend"].ToString(); }
                catch (Exception) { TKQC.tongTieu = "0"; }


                //TKQC.slThe = jObjectMoreInfo[""];//
                try { TKQC.role = item["userpermissions"]["data"][0]["role"].ToString(); }
                catch (Exception) { TKQC.role = "UNKNOWN"; }
                TKQC.ngayLapHoaDon = jObjectMoreInfo["next_bill_date"].ToString();
                TKQC.ngayTaoTK = jObjectMoreInfo["created_time"].ToString();
                if (jObjectMoreInfo.ContainsKey("owner_business"))
                {
                    TKQC.loaiTK = "Business";
                }
                else
                {
                    TKQC.loaiTK = "Cá nhân thường";
                }
                ;
                try
                {
                    TKQC.duoiThe = jObjectMoreInfo["all_payment_methods"]["pm_credit_card"]["data"][0]["display_string"].ToString();
                    TKQC.slThe = jObjectMoreInfo["all_payment_methods"]["pm_credit_card"]["data"].Count().ToString();
                }
                catch { TKQC.duoiThe = "-"; }
                try
                {
                    TKQC.nguongConLai = (double.Parse(TKQC.Nguong) - double.Parse(TKQC.soDu)).ToString();
                }
                catch { TKQC.nguongConLai = "0"; }
                TKQC.soNgayDenKyTT = jObjectMoreInfo["next_bill_date"].ToString(); //today - nextbill
                TKQC.liDoDisable = jObjectMoreInfo["disable_reason"].ToString() == "1" ? "ADS_INTEGRITY_POLICY" : jObjectMoreInfo["disable_reason"].ToString() == "3" ? "RISK_PAYMENT" : jObjectMoreInfo["disable_reason"].ToString() == "6" ? "BUSINESS_INTEGRITY_RAR" : "";
                var utc = jObjectMoreInfo["timezone_offset_hours_utc"].ToString().Contains("-") ? jObjectMoreInfo["timezone_offset_hours_utc"].ToString() : "+" + jObjectMoreInfo["timezone_offset_hours_utc"].ToString();
                TKQC.timeZone = jObjectMoreInfo["timezone_name"].ToString() + "/ " + utc;
                try { TKQC.taoTuBM = jObjectMoreInfo["owner_business"]["id"].ToString(); }
                catch { TKQC.taoTuBM = ""; }
                try { TKQC.quocGiaTKQC = jObjectMoreInfo["business_country_code"].ToString(); }
                catch { TKQC.quocGiaTKQC = "-"; }
                if (TTTKQCInstance.lsTKQC.Find(x => x.ID == TKQC.ID) == null)
                {


                    TTTKQCInstance.OnNewAccAdded(TKQC);
                    TTTKQCInstance.lsTKQC.Add(TKQC);
                    TTTKQCInstance.lsViewTKQC.Add(TKQC);
                }
            }
            catch (Exception ex)
            {

            }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        }


        void TTTKQC_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
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

        void TTTKQC_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TTTKQC_RefreshBtn.BackgroundColor = Color.FromArgb(255, 255, 255, 255);
            TTTKQC_RefreshBtn.Enabled = true;
            loadingBtnTKQC.Visible = false;

            TTTKQCInstance.tongTK = TTTKQCInstance.lsViewTKQC.Count;
            TTTKQCInstance.tongNguong = 0;
            TTTKQCInstance.tongTieu = 0;
            TTTKQCInstance.tongSoDu = 0;
            for (int i = 0; i < TTTKQCInstance.lsViewTKQC.Count; i++)
            {
                if (TTTKQCInstance.lsTKQC[i].tienTe.Contains("USD"))
                {
                    TTTKQCInstance.tongTieu += double.Parse(TTTKQCInstance.lsViewTKQC[i].tongTieu) * TTTKQCInstance.tiGia["USD"];
                    TTTKQCInstance.tongSoDu += double.Parse(TTTKQCInstance.lsViewTKQC[i].soDu) * TTTKQCInstance.tiGia["USD"];
                    TTTKQCInstance.tongNguong += double.Parse(TTTKQCInstance.lsViewTKQC[i].Nguong) * TTTKQCInstance.tiGia["USD"];
                }
                else if (TTTKQCInstance.lsTKQC[i].tienTe.Contains("SGD"))
                {
                    TTTKQCInstance.tongTieu += double.Parse(TTTKQCInstance.lsViewTKQC[i].tongTieu) * TTTKQCInstance.tiGia["SGD"];
                    TTTKQCInstance.tongSoDu += double.Parse(TTTKQCInstance.lsViewTKQC[i].soDu) * TTTKQCInstance.tiGia["SGD"];
                    TTTKQCInstance.tongNguong += double.Parse(TTTKQCInstance.lsViewTKQC[i].Nguong) * TTTKQCInstance.tiGia["SGD"];
                }
                else if (TTTKQCInstance.lsTKQC[i].tienTe.Contains("HKD"))
                {
                    TTTKQCInstance.tongTieu += double.Parse(TTTKQCInstance.lsViewTKQC[i].tongTieu) * TTTKQCInstance.tiGia["HKD"];
                    TTTKQCInstance.tongSoDu += double.Parse(TTTKQCInstance.lsViewTKQC[i].soDu) * TTTKQCInstance.tiGia["HKD"];
                    TTTKQCInstance.tongNguong += double.Parse(TTTKQCInstance.lsViewTKQC[i].Nguong) * TTTKQCInstance.tiGia["HKD"];
                }
                else
                {
                    TTTKQCInstance.tongTieu += double.Parse(TTTKQCInstance.lsViewTKQC[i].tongTieu);
                    TTTKQCInstance.tongSoDu += double.Parse(TTTKQCInstance.lsViewTKQC[i].soDu);
                    TTTKQCInstance.tongNguong += double.Parse(TTTKQCInstance.lsViewTKQC[i].Nguong);
                }


            }
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            thongTinTKQCToTalDataGridView.Rows.Clear();
            thongTinTKQCToTalDataGridView.Rows.Insert(thongTinTKQCToTalDataGridView.Rows.Count - 1,
                                                    TTTKQCInstance.tongTK.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                                                    TTTKQCInstance.tongNguong.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                                                    TTTKQCInstance.tongTieu.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                                                    TTTKQCInstance.tongSoDu.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0]);
        }

        private delegate void InvokeDelegate(TTTKQCEventArgs e);
        private void Statistic_newAccountAdded(object sender, TTTKQCEventArgs e)
        {
            this.BeginInvoke(new InvokeDelegate(AddToGirdView), new object[] { e });
        }
        private void AddToGirdView(TTTKQCEventArgs e)
        {
            if (e == null || e.InfoAccount == null)
            {
                return;
            }
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            var tongTieu = "0";
            try
            {
                tongTieu = e.InfoAccount.tongTieu != null ? double.Parse(e.InfoAccount.tongTieu).ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "0";
            }
            catch
            {
                tongTieu = "0";
            }
            thongTinTKQCDataGridView.Rows.Insert(thongTinTKQCDataGridView.Rows.Count - 1,
                                                    e.InfoAccount.trangThai != null ? e.InfoAccount.trangThai : "",
                                                    e.InfoAccount.ID != null ? e.InfoAccount.ID : "",
                                                    e.InfoAccount.tenTK != null ? e.InfoAccount.tenTK : "",
                                                    e.InfoAccount.soDu != null ? double.Parse(e.InfoAccount.soDu).ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "",
                                                    e.InfoAccount.Nguong != null ? double.Parse(e.InfoAccount.Nguong).ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "",
                                                    e.InfoAccount.Limit != null && e.InfoAccount.Limit != "-1" ? double.Parse(e.InfoAccount.Limit).ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "No Limit",
                                                    e.InfoAccount.nguongConLai != null ? double.Parse(e.InfoAccount.nguongConLai).ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "",
                                                    tongTieu,
                                                    e.InfoAccount.tienTe != null ? e.InfoAccount.tienTe : "",
                                                    e.InfoAccount.role != null ? e.InfoAccount.role : "",
                                                    e.InfoAccount.timeZone != null ? e.InfoAccount.timeZone : "",
                                                    e.InfoAccount.quocGiaTKQC != null ? e.InfoAccount.quocGiaTKQC : "",
                                                    e.InfoAccount.loaiTK != null ? e.InfoAccount.loaiTK : "",
                                                    e.InfoAccount.taoTuBM != null ? e.InfoAccount.taoTuBM : "",
                                                    e.InfoAccount.ngayTaoTK != null ? e.InfoAccount.ngayTaoTK.Split(' ')[0] : "",
                                                    e.InfoAccount.slThe != null ? e.InfoAccount.slThe : "",
                                                    e.InfoAccount.duoiThe != null ? e.InfoAccount.duoiThe : "",
                                                    e.InfoAccount.soNgayDenKyTT != null ? e.InfoAccount.soNgayDenKyTT.Split(' ')[0] : "",
                                                    e.InfoAccount.liDoDisable != null ? e.InfoAccount.liDoDisable : "");
        }

        private void TTTKQC_RefreshBtn_Click(object sender, EventArgs e)
        {
            loadingBtnTKQC.Visible = true;
            TTTKQC_RefreshBtn.Enabled = false;
            TTTKQC_RefreshBtn.BackgroundColor = Color.FromArgb(255, 198, 65, 24);
            thongTinTKQCDataGridView.Rows.Clear();

            TTTKQCInstance.tiGia = RequestTiGiaNgoaiTe();
            ViaInstances.ImportVia();
            
            if (!TTTKQCComboBox.SelectedValue.ToString().Contains("MAX"))
                TTTKQCInstance.limit = Int32.Parse(TTTKQCComboBox.SelectedValue.ToString());
            else
                TTTKQCInstance.limit = 100000;
            
            if (!TTTKQC_Worker.IsBusy)
            {
                TTTKQC_Worker.RunWorkerAsync();
            }
        }


        public static string RequestTTTKQCAsync(string userId, string token, string cookie)
        {
            var url = $"https://graph.facebook.com/v13.0/me/adaccounts?limit={TTTKQCInstance.limit.ToString()}&fields=name,account_id,account_status,userpermissions.user({userId})%7Brole%7D&access_token={token}&summary=1&locale=en_US";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Headers["authority"] = "business.facebook.com";
            httpRequest.Headers["cookie"] = cookie;


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.Default, true))
            {
                var result = streamReader.ReadToEnd();
                return result.ToString();
            }
        }

        public static string RequestFullInfoTTTKQCAsync(string accId, string token, string coookie)
        {
            var url = $"https://graph.facebook.com/v13.0/act_{accId}?fields=account_id,owner_business,created_time,next_bill_date,currency,adtrust_dsl,timezone_name,timezone_offset_hours_utc,business_country_code,disable_reason,adspaymentcycle{{threshold_amount}},balance,is_prepay_account,owner,all_payment_methods{{pm_credit_card{{display_string}}}},insights.date_preset(maximum){{spend}}&access_token={token}&locale=en_US";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Headers["authority"] = "business.facebook.com";
            httpRequest.Headers["cookie"] = coookie;


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result.ToString();
            }
        }

        public static IDictionary<string, double> RequestTiGiaNgoaiTe()
        {
            var client = new RestClient("https://portal.vietcombank.com.vn/Usercontrols/TVPortal.TyGia/pXML.aspx?b=100");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            //request.AddHeader("Cookie", "__cf_bm=YUjKo74e0jKa_RTGAHgm5VtXUnj0KQqd4eU_W8Q4SGI-1666605990-0-AfQgJX0Fx8fLj7c2COIDA/4UaGtFW515FqN1NwI6GLOMX617gHIEU3ueRrv+k2iDj/US6v0ScatcrYHPCDm8zu+EhGYZ1MHD6+r0IpFOo0iS");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            var pattern = @"CurrencyCode=""(\w+)"".*Transfer=""(.*)"" Sell";
            var matches = Regex.Matches(response.Content, pattern);
            IDictionary<string, double> tiGia = new Dictionary<string, double>();
            foreach (Match match in matches)
            {
                var name = match.Groups[1].Value;
                var value = match.Groups[2].Value;

                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";
                double doubleVal = Convert.ToDouble(value, provider);

                tiGia.Add(name, doubleVal);
            }
            return tiGia;
        }



        private void TTTKQCComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox cb = sender as ComboBox;
            //if (cb.SelectedIndex != null)
            //{
            //    int cl = Int32.Parse(cb.SelectedValue.ToString());
            //    TTTKQCInstance.limit = cl;
            //}
        }





        private void thongTinTKQCDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                thongTinTKQCDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237);
            }
            else
            {
                thongTinTKQCDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255);
            }
        }

        private void thongTinTKQCDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (thongTinTKQCDataGridView.Rows[e.RowIndex].Cells[0].Value != null && thongTinTKQCDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Vô"))
            {
                thongTinTKQCDataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
            }
            else if (thongTinTKQCDataGridView.Rows[e.RowIndex].Cells[0].Value != null && thongTinTKQCDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Hoạt"))
            {
                thongTinTKQCDataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
            }
            else if (thongTinTKQCDataGridView.Rows[e.RowIndex].Cells[0].Value != null && thongTinTKQCDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Cần"))
            {
                thongTinTKQCDataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Orange };
            }
            else
            {
                thongTinTKQCDataGridView.Rows[e.RowIndex].Cells[0].Style = thongTinTKQCDataGridView.DefaultCellStyle;
            }
        }

        private void hoạtĐộngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusHoatDong == false)
            {
                statusHoatDong = true;
                hoạtĐộngToolStripMenuItem.BackColor = Color.FromArgb(255, 192, 255, 192);

                thongTinTKQCDataGridView.Rows.Clear();
                for (int i = 0; i < TTTKQCInstance.lsViewTKQC.Count; i++)
                {
                    var TKQC = TTTKQCInstance.lsViewTKQC[i];
                    if ((TKQC.trangThai == "Hoạt động" && statusHoatDong) ||
                       (TKQC.trangThai == "Vô hiệu hóa" && statusVoHieuHoa) ||
                       (TKQC.trangThai == "Cần thanh toán" && statusCanThanhToan))
                    {
                        TTTKQCInstance.OnNewAccAdded(TKQC);
                    }
                }
            }
            else
            {
                statusHoatDong = false;
                hoạtĐộngToolStripMenuItem.BackColor = Color.White;

                thongTinTKQCDataGridView.Rows.Clear();
                for (int i = 0; i < TTTKQCInstance.lsViewTKQC.Count; i++)
                {
                    var TKQC = TTTKQCInstance.lsViewTKQC[i];
                    if ((TKQC.trangThai == "Hoạt động" && statusHoatDong) ||
                       (TKQC.trangThai == "Vô hiệu hóa" && statusVoHieuHoa) ||
                       (TKQC.trangThai == "Cần thanh toán" && statusCanThanhToan))
                    {
                        TTTKQCInstance.OnNewAccAdded(TKQC);
                    }
                }
            }

        }
        private void vôHiệuHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusVoHieuHoa == false)
            {
                statusVoHieuHoa = true;

                vôHiệuHóaToolStripMenuItem.BackColor = Color.FromArgb(255, 192, 255, 192);
                thongTinTKQCDataGridView.Rows.Clear();
                for (int i = 0; i < TTTKQCInstance.lsViewTKQC.Count; i++)
                {
                    var TKQC = TTTKQCInstance.lsViewTKQC[i];
                    if ((TKQC.trangThai == "Hoạt động" && statusHoatDong) ||
                       (TKQC.trangThai == "Vô hiệu hóa" && statusVoHieuHoa) ||
                       (TKQC.trangThai == "Cần thanh toán" && statusCanThanhToan))
                    {
                        TTTKQCInstance.OnNewAccAdded(TKQC);
                    }
                }
            }
            else
            {
                statusVoHieuHoa = false;
                vôHiệuHóaToolStripMenuItem.BackColor = Color.White;
                thongTinTKQCDataGridView.Rows.Clear();
                for (int i = 0; i < TTTKQCInstance.lsViewTKQC.Count; i++)
                {
                    var TKQC = TTTKQCInstance.lsViewTKQC[i];
                    if ((TKQC.trangThai == "Hoạt động" && statusHoatDong) ||
                       (TKQC.trangThai == "Vô hiệu hóa" && statusVoHieuHoa) ||
                       (TKQC.trangThai == "Cần thanh toán" && statusCanThanhToan))
                    {
                        TTTKQCInstance.OnNewAccAdded(TKQC);
                    }
                }
            }
        }

        private void cầnThanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusCanThanhToan == false)
            {
                statusCanThanhToan = true;

                cầnThanhToánToolStripMenuItem.BackColor = Color.FromArgb(255, 192, 255, 192);
                thongTinTKQCDataGridView.Rows.Clear();
                for (int i = 0; i < TTTKQCInstance.lsViewTKQC.Count; i++)
                {
                    var TKQC = TTTKQCInstance.lsViewTKQC[i];
                    if ((TKQC.trangThai == "Hoạt động" && statusHoatDong) ||
                       (TKQC.trangThai == "Vô hiệu hóa" && statusVoHieuHoa) ||
                       (TKQC.trangThai == "Cần thanh toán" && statusCanThanhToan))
                    {
                        TTTKQCInstance.OnNewAccAdded(TKQC);
                    }
                }
            }
            else
            {
                statusCanThanhToan = false;
                cầnThanhToánToolStripMenuItem.BackColor = Color.White;
                thongTinTKQCDataGridView.Rows.Clear();
                for (int i = 0; i < TTTKQCInstance.lsViewTKQC.Count; i++)
                {
                    var TKQC = TTTKQCInstance.lsViewTKQC[i];
                    if ((TKQC.trangThai == "Hoạt động" && statusHoatDong) ||
                       (TKQC.trangThai == "Vô hiệu hóa" && statusVoHieuHoa) ||
                       (TKQC.trangThai == "Cần thanh toán" && statusCanThanhToan))
                    {
                        TTTKQCInstance.OnNewAccAdded(TKQC);
                    }
                }
            }
        }

        private void khácToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusKhac == false)
            {
                statusKhac = true;

                loạiKhácToolStripMenuItem.BackColor = Color.FromArgb(255, 192, 255, 192);
                thongTinTKQCDataGridView.Rows.Clear();
                for (int i = 0; i < TTTKQCInstance.lsViewTKQC.Count; i++)
                {
                    var TKQC = TTTKQCInstance.lsViewTKQC[i];
                    if ((TKQC.trangThai == "Hoạt động" && statusHoatDong) ||
                       (TKQC.trangThai == "Vô hiệu hóa" && statusVoHieuHoa) ||
                       (TKQC.trangThai == "Cần thanh toán" && statusCanThanhToan))
                    {
                        TTTKQCInstance.OnNewAccAdded(TKQC);
                    }
                }
            }
            else
            {
                statusKhac = false;
                loạiKhácToolStripMenuItem.BackColor = Color.White;
                thongTinTKQCDataGridView.Rows.Clear();
                for (int i = 0; i < TTTKQCInstance.lsViewTKQC.Count; i++)
                {
                    var TKQC = TTTKQCInstance.lsViewTKQC[i];
                    if ((TKQC.trangThai == "Hoạt động" && statusHoatDong) ||
                       (TKQC.trangThai == "Vô hiệu hóa" && statusVoHieuHoa) ||
                       (TKQC.trangThai == "Cần thanh toán" && statusCanThanhToan))
                    {
                        TTTKQCInstance.OnNewAccAdded(TKQC);
                    }
                }
            }
        }


        private void thongTinTKQCDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripTTTKQC.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        public static string GetDataFilterTime(string token, string stringCookie, string timeSince, string timeUntil, List<TTTKQCInfo> lsTKQC)
        {
            var url = $"https://graph.facebook.com/v14.0/?access_token={token}&locale=en_US";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            var cookies = stringCookie.Split(';');
            foreach (string cookie in cookies)
            {
                if (cookie == "") break;
                var datacookie = cookie.Trim().Split('=');
                request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
            }
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36";
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            var bodyBatch = "[";
            for (int i = 0; i < lsTKQC.Count - 1; i++)
            {
                var data = $"{{\"relative_url\":\"/act_{lsTKQC[i].ID}?fields=account_id,insights.time_range({{since: '{timeSince}',until:'{timeUntil}'}}){{spend}}\",\"method\":\"GET\"}},";
                bodyBatch += data;
            }
            bodyBatch += $"{{\"relative_url\":\"/act_{lsTKQC.Last().ID}?fields=account_id,insights.time_range({{since: '{timeSince}',until:'{timeUntil}'}}){{spend}}\",\"method\":\"GET\"}}]";

            request.AddParameter("batch", bodyBatch);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            return "";
        }

        private void locTimeTKQCBtn_Click(object sender, EventArgs e)
        {

            Thread.Sleep(1000);
            //searchTTTKQC_Worker.ReportProgress(0);
            thongTinTKQCDataGridView.Rows.Clear();
            loadingBtnTKQC.Visible = true;
            //TTTKQCInstance.lsViewTKQC.Clear();
            foreach (var item in TTTKQCInstance.lsViewTKQC)
            {
                item.tongTieu = "0";
            }
            var timeSince = Common.parseStringDateToFilter2(from_dateTimePicker_TKQC.Value.ToShortDateString());
            var timeUntil = Common.parseStringDateToFilter2(to_dateTimePicker_TKQC.Value.ToShortDateString());

            for (int index = 0; index < ViaInstances.lsAccVia.Count; index++)
            {
                var cookie = ViaInstances.lsAccVia[index].cookie;
                List<TTTKQCInfo> lsTKQC = new List<TTTKQCInfo>();
                ConcurrentQueue<List<TTTKQCInfo>> queue = new ConcurrentQueue<List<TTTKQCInfo>>();
                foreach (var tkqc in TTTKQCInstance.lsTKQC)
                {

                    if (tkqc.cookie == cookie) { lsTKQC.Add(tkqc); }
                    if (lsTKQC.Count == 50)
                    {
                        List<TTTKQCInfo> lsTKQCClone = new List<TTTKQCInfo>(lsTKQC.Count);
                        lsTKQC.ForEach((item) =>
                        {
                            lsTKQCClone.Add(new TTTKQCInfo(item));
                        });
                        queue.Enqueue(lsTKQCClone);
                        lsTKQC.Clear();
                    }
                }
                if (lsTKQC.Count > 0)
                    queue.Enqueue(lsTKQC);

                while (queue.Count > 0)
                {
                    List<TTTKQCInfo> tempLsTKQC = new List<TTTKQCInfo>();
                    queue.TryDequeue(out tempLsTKQC);
                    var dataFilterTime = GetDataFilterTime(ViaInstances.lsAccVia[index].token, cookie, timeSince, timeUntil, tempLsTKQC);
                    dataFilterTime = "{\"data\":" + dataFilterTime + "}";
                    JObject dataFilter = JObject.Parse(dataFilterTime);
                    if (dataFilter != null)
                    {
                        //var lsBody = dataFilterTime.Split(new string[] { " \"body\": ", "},\r\n   {" }, StringSplitOptions.None);
                        //var pattern = @"account_id\\"": \\""(\d+).*spend\\"": \\""(\d+?.\d+)\\"",";
                        for(int i = 0; i< dataFilter["data"].Count(); i++)
                        {
                            var itemBody = dataFilter["data"][i]["body"].ToString();
                            if (itemBody.Contains("spend"))
                            {
                                JObject dataFilterItem = JObject.Parse(itemBody);
                                //var match = Regex.Match(item, pattern);
                                var tempId = dataFilterItem["account_id"].ToString();
                                var tempTongTieu = dataFilterItem["insights"]["data"][0]["spend"].ToString();
                                TTTKQCInstance.lsViewTKQC.Find(x => x.ID == tempId).tongTieu = tempTongTieu;
                            }
                        }
                    }
                }

            }
            foreach (var item in TTTKQCInstance.lsViewTKQC)
            {
                TTTKQCInstance.OnNewAccAdded(item);
            }

            thongTinTKQCToTalDataGridView.Rows.Clear();
            loadingBtnTKQC.Visible = false;

            TTTKQCInstance.tongTK = TTTKQCInstance.lsViewTKQC.Count;
            TTTKQCInstance.tongNguong = 0;
            TTTKQCInstance.tongTieu = 0;
            TTTKQCInstance.tongSoDu = 0;
            for (int i = 0; i < TTTKQCInstance.lsViewTKQC.Count; i++)
            {
                if (TTTKQCInstance.lsTKQC[i].tienTe.Contains("USD"))
                {
                    TTTKQCInstance.tongTieu += double.Parse(TTTKQCInstance.lsViewTKQC[i].tongTieu) * TTTKQCInstance.tiGia["USD"];
                    TTTKQCInstance.tongSoDu += double.Parse(TTTKQCInstance.lsViewTKQC[i].soDu) * TTTKQCInstance.tiGia["USD"];
                    TTTKQCInstance.tongNguong += double.Parse(TTTKQCInstance.lsViewTKQC[i].Nguong) * TTTKQCInstance.tiGia["USD"];
                }
                else if (TTTKQCInstance.lsTKQC[i].tienTe.Contains("SGD"))
                {
                    TTTKQCInstance.tongTieu += double.Parse(TTTKQCInstance.lsViewTKQC[i].tongTieu) * TTTKQCInstance.tiGia["SGD"];
                    TTTKQCInstance.tongSoDu += double.Parse(TTTKQCInstance.lsViewTKQC[i].soDu) * TTTKQCInstance.tiGia["SGD"];
                    TTTKQCInstance.tongNguong += double.Parse(TTTKQCInstance.lsViewTKQC[i].Nguong) * TTTKQCInstance.tiGia["SGD"];
                }
                else if (TTTKQCInstance.lsTKQC[i].tienTe.Contains("HKD"))
                {
                    TTTKQCInstance.tongTieu += double.Parse(TTTKQCInstance.lsViewTKQC[i].tongTieu) * TTTKQCInstance.tiGia["HKD"];
                    TTTKQCInstance.tongSoDu += double.Parse(TTTKQCInstance.lsViewTKQC[i].soDu) * TTTKQCInstance.tiGia["HKD"];
                    TTTKQCInstance.tongNguong += double.Parse(TTTKQCInstance.lsViewTKQC[i].Nguong) * TTTKQCInstance.tiGia["HKD"];
                }
                else
                {
                    TTTKQCInstance.tongTieu += double.Parse(TTTKQCInstance.lsViewTKQC[i].tongTieu);
                    TTTKQCInstance.tongSoDu += double.Parse(TTTKQCInstance.lsViewTKQC[i].soDu);
                    TTTKQCInstance.tongNguong += double.Parse(TTTKQCInstance.lsViewTKQC[i].Nguong);
                }


            }
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            thongTinTKQCToTalDataGridView.Rows.Clear();
            thongTinTKQCToTalDataGridView.Rows.Insert(thongTinTKQCToTalDataGridView.Rows.Count - 1,
                                                    TTTKQCInstance.tongTK.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                                                    TTTKQCInstance.tongNguong.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                                                    TTTKQCInstance.tongTieu.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0],
                                                    TTTKQCInstance.tongSoDu.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0]);
            loadingBtnTKQC.Visible = false;
        }

        private void searchKTQCBtn_Click(object sender, EventArgs e)
        {
            loadingBtnTKQC.Visible = true;
            thongTinTKQCDataGridView.Rows.Clear();
            TTTKQCInstance.searchData = Search_TKQC_TBox.Text;
            if (TTTKQCInstance.searchData != "")
            {
                Thread.Sleep(1000);
                TTTKQCInstance.lsViewTKQC.Clear();
                for (int i = 0; i < TTTKQCInstance.lsTKQC.Count; i++)
                {

                    if (TTTKQCInstance.lsTKQC[i].ID != null && TTTKQCInstance.lsTKQC[i].ID.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].tenTK != null && TTTKQCInstance.lsTKQC[i].tenTK.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].soDu != null && TTTKQCInstance.lsTKQC[i].soDu.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].Nguong != null && TTTKQCInstance.lsTKQC[i].Nguong.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].Limit != null && TTTKQCInstance.lsTKQC[i].Limit.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].nguongConLai != null && TTTKQCInstance.lsTKQC[i].nguongConLai.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].tongTieu != null && TTTKQCInstance.lsTKQC[i].tongTieu.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].tienTe != null && TTTKQCInstance.lsTKQC[i].tienTe.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].role != null && TTTKQCInstance.lsTKQC[i].role.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].timeZone != null && TTTKQCInstance.lsTKQC[i].timeZone.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].quocGiaTKQC != null && TTTKQCInstance.lsTKQC[i].quocGiaTKQC.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].loaiTK != null && TTTKQCInstance.lsTKQC[i].loaiTK.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].taoTuBM != null && TTTKQCInstance.lsTKQC[i].taoTuBM.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].ngayTaoTK != null && TTTKQCInstance.lsTKQC[i].ngayTaoTK.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].slThe != null && TTTKQCInstance.lsTKQC[i].slThe.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].duoiThe != null && TTTKQCInstance.lsTKQC[i].duoiThe.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].soNgayDenKyTT != null && TTTKQCInstance.lsTKQC[i].soNgayDenKyTT.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                    else if (TTTKQCInstance.lsTKQC[i].liDoDisable != null && TTTKQCInstance.lsTKQC[i].liDoDisable.Contains(TTTKQCInstance.searchData))
                    {
                        TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                        TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                    }
                }
            }
            else if (TTTKQCInstance.searchData == "")
            {
                Thread.Sleep(1000);
                TTTKQCInstance.lsViewTKQC.Clear();
                for (int i = 0; i < TTTKQCInstance.lsTKQC.Count; i++)
                {
                    TTTKQCInstance.OnNewAccAdded(TTTKQCInstance.lsTKQC[i]);
                    TTTKQCInstance.lsViewTKQC.Add(TTTKQCInstance.lsTKQC[i]);
                }
            }
            loadingBtnTKQC.Visible = false;
        }
        private void copyAlltoClipboard()
        {
            thongTinTKQCDataGridView.SelectAll();
            DataObject dataObj = thongTinTKQCDataGridView.GetClipboardContent();
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
            sfd.FileName = "ThongTinTKQC.xls";
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
                thongTinTKQCDataGridView.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void thongTinTKQCDataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 3|| e.Column.Index == 4 || e.Column.Index == 5 || e.Column.Index == 6 ||
                e.Column.Index == 7)
            {
                try
                {
                    string a = "0";
                    string b = "0";
                    if(e.CellValue1.ToString() == "No Limit")
                    {
                        a = "999999999";
                    }
                    else
                    {
                        a = e.CellValue1.ToString();
                    }
                    if (e.CellValue2.ToString() == "No Limit")
                    {
                        b = "999999999";
                    }
                    else
                    {
                        b = e.CellValue2.ToString();
                    }
                    e.SortResult = double.Parse(a).CompareTo(double.Parse(b));
                    e.Handled = true;//pass by the default sorting
                }
                catch
                {
                    e.SortResult = 1;
                    e.Handled = true;
                }
            }
        }
    }

    class TKQCThreadState
    {
        public static bool all_thread_together_running { get; set; }
        public static string proxy { get; set; }
        public static bool allow_running { get; set; }
    }
}
