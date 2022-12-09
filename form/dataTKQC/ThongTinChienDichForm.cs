using AdsManager.HelperLibrary;
using AdsManager.HelperLibrary.Facebook;
using AdsManager.HelperLibrary.Facebook.Info;
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using System.Xml.Linq;
using TakeCarePage;
using TakeCarePage.HelperLibrary.Facebook;
using static System.Net.Mime.MediaTypeNames;
using Excel = Microsoft.Office.Interop.Excel;

namespace AdsManager.form.dataTKQC
{
    public partial class ThongTinChienDichForm : Form
    {
        BackgroundWorker TTCD_Worker = null;

        volatile int live_thread_number = 0;
        Queue<int> qu_position_pro5 = new Queue<int>();
        public ThongTinChienDichForm()
        {
            InitializeComponent();
            ThongTinChienDichDataGridView.RowHeadersVisible = false;
            ThongTinChienDichDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 10, 147, 66); ;//10, 147, 66
            ThongTinChienDichDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            ThongTinChienDichDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            TTCD_Worker = new BackgroundWorker();
            TTCD_Worker.DoWork += new DoWorkEventHandler(TTCD_Worker_DoWork);
            TTCD_Worker.ProgressChanged += new ProgressChangedEventHandler(TTCD_Worker_ProgressChanged);
            TTCD_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(TTCD_Worker_RunWorkerCompleted);
            TTCD_Worker.WorkerReportsProgress = true;
            TTCD_Worker.WorkerSupportsCancellation = true;

            TTCDInstance.NewAccountAdded += this.Statistic_newAccountAdded;
        }
        void TTCD_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            live_thread_number = 0;
            qu_position_pro5.Clear();
            for (int i = 1; i <= 100; i++)
                qu_position_pro5.Enqueue(i);

            TTCDThreadState.allow_running = true;
            TTCDThreadState.proxy = "";
            TTCDThreadState.all_thread_together_running = true;
            try
            {
                TTCDInstance.lsTTCD.Clear();
                List<TTTKQCInfo> lsTKQCClone = new List<TTTKQCInfo>(TTTKQCInstance.lsTKQC.Count);
                TTTKQCInstance.lsTKQC.ForEach((item) =>
                {
                    lsTKQCClone.Add(new TTTKQCInfo(item));
                });

                if(TTCDInstance.businessId == "")
                {
                    TTCDInstance.getBusinessId(lsTKQCClone[0].token, lsTKQCClone[0].cookie);
                }

                for (int index = 0; index < lsTKQCClone.Count; index++) //
                {
                    var TKQC = lsTKQCClone[index];
                    while (!TTCD_Worker.CancellationPending)
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
                            while (!TTCD_Worker.CancellationPending && live_thread_number != 0 && TTCDThreadState.all_thread_together_running) // if = 0 => break
                                Thread.Sleep(TimeSpan.FromSeconds(1));
                        }

                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }


                    // [1.2] do work, dam bao da reset proxy o tren theo option
                    if (!TTCD_Worker.CancellationPending && TTCDThreadState.allow_running)
                    {
                        // update proxy for each thread if run together
                        live_thread_number++;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                        AsyncCoreDoWork(TKQC);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                    }
                    Thread.Sleep(200);
                }

                while (live_thread_number > 0)
                {
                    Thread.Sleep(5000);
                }


                TTCD_Worker.ReportProgress(1);
                TTCD_Worker.ReportProgress(0, ProxyInstances.Http);
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
                if (!TTCD_Worker.CancellationPending)
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
                //var arIds = RequestGetListCampainsIds(businessId, accId, token, cookie);
                //var arIds = RequestGetListIds(businessId, accId, token, cookie);
                //if (arIds == "" || arIds == null)
                //{
                //    return;
                //}
                //Thread.Sleep(3000);
                //var detailChienDich = RequestGetListDetailChiendich(businessId, token, cookie, arIds);
                //var detailChienDich = RequestGetListDetailAds(businessId, token, cookie, arIds);

                var dateFilter = Common.parseStringDateToFilter(TTCDInstance.fromDate, TTCDInstance.toDate);

                var dateSince = Common.parseStringDateToFilter2(TTCDInstance.fromDate);
                var dateUntil = Common.parseStringDateToFilter2(TTCDInstance.toDate);

                //var detailChienDich = TTCDInstance.GetDataChienDich(accId, cookie, dateFilter, businessId);
                if(accId == "14984105")
                {
#pragma warning disable CS0219 // The variable 'a' is assigned but its value is never used
                    var a = 0;
#pragma warning restore CS0219 // The variable 'a' is assigned but its value is never used
                }
                List<string> lsAds = new List<string>();
                JObject objectLsAds = JObject.Parse( RequestGetListAds(accId, token, cookie));
                for (int i = 0; i < objectLsAds["data"].Count(); i++)
                {
                    try
                    {
                        var ads = objectLsAds["data"][i]["id"].ToString().Trim();
                        lsAds.Add(ads);
                    }
                    catch { return; }
                }
                //return;
                //var arIds = detailChienDich.Split(new string[] { "window.__preloaderData[\"AdsPEAdObjectTableDataManagerPreloader\"]=[{\"get\\/v14.0\\/{\\\"ids\\\":\\\"" }, StringSplitOptions.None)[1];
                //arIds = arIds.Split('\\')[0];   //

                //var arTongTieu = detailChienDich.Split(new string[] { ",\"atomic_values\":[\"" }, StringSplitOptions.None);
                //Dictionary<string, string> tongTieuAds = new Dictionary<string, string>();
                //for (int i = 0; i < arTongTieu.Length; i++)
                //{
                //    if (arTongTieu[i].Contains("dimension_values"))
                //    {
                //        var adsTongTieu = arTongTieu[i];
                //        var tongTieu = adsTongTieu.Split('"')[0];
                //        adsTongTieu = adsTongTieu.Split(new string[] { "dimension_values" }, StringSplitOptions.None)[1];
                //        var checkAdsId = adsTongTieu.Split(',')[2].Replace('"', ' ').Trim();
                //        tongTieuAds.Add(checkAdsId, tongTieu);
                //    }

                //}

                var resultIdPostAds = GetIdPostAds(accId, cookie, token);
                JObject objectResultIdPostAds = JObject.Parse(resultIdPostAds);
                Dictionary<string, string> postAds = new Dictionary<string, string>();
                
                for (int i = 0; i < objectResultIdPostAds["ads"]["data"].Count(); i++)
                {
                    try
                    {
                        var id = objectResultIdPostAds["ads"]["data"][i]["id"].ToString();
                        var postId = objectResultIdPostAds["ads"]["data"][i]["creative"]["object_story_id"].ToString();

                        postAds.Add(id, postId);
                    }
                    catch { }
                }

                  var resultCostAds = getCostAds(accId, cookie, token, dateSince, dateUntil);
                JObject objectresultCostAds = JObject.Parse(resultCostAds);
                Dictionary<string, string> costAds = new Dictionary<string, string>();
               
                for (int i = 0; i < objectresultCostAds["data"].Count(); i++)
                {
                    try
                    {
                        var id = objectresultCostAds["data"][i]["ad_id"].ToString();
                        var impression = objectresultCostAds["data"][i]["impressions"].ToString();
                        var cpm = objectresultCostAds["data"][i]["cpm"].ToString();
                        var cost = double.Parse(impression) * double.Parse(cpm) / 1000;
                        costAds.Add(id, cost.ToString());
                    }
                    catch
                    {
                        continue;
                    }
                }


                //detailChienDich.Split(new string[] { "window.__preloaderData[\"AdsPEAdObjectTableDataManagerPreloader\"]=[" }, StringSplitOptions.None);
                //string regexString = @"window\.__preloaderData\[""AdsPEAdObjectTableDataManagerPreloader""\]=\[(.*),new Date\(\)\.getTime\(\)\];window";
                //Regex r = new Regex(regexString, RegexOptions.IgnoreCase);
                //Match m = r.Match(detailChienDich);
                //var detailData = "";

                //if (m.Success)
                //{
                //    detailData = m.Groups[1].Value;
                //}
                //JObject jObject = null;
                //JObject objectAds = null;
                //JObject objectCampaign = null;
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                try
                {
                    //try
                    //{
                    //    jObject = JObject.Parse(detailData);

                    //    List<string> listDetail = new List<string>();
                    //    foreach (var a in jObject)
                    //    {
                    //        var b = a.Value.ToString();
                    //        listDetail.Add(b);
                    //    }
                    //    //ads

                    //    objectAds = JObject.Parse(listDetail[0]);
                    //    objectCampaign = JObject.Parse(listDetail[2]);
                    //}
                    //catch
                    //{

                    //}
                    foreach (var AdsId in lsAds)
                    {
#pragma warning disable CS0168 // The variable 'e' is declared but never used
                        try
                        {
                            var TTCD = new TTCDInfo();
                            //if (jObject != null)
                            //{
                            //    TTCD.IdQC = AdsId;
                            //    TTCD.TenQC = objectAds[AdsId]["name"].ToString();
                            //    TTCD.IdNhomQC = objectAds[AdsId]["adset"]["id"].ToString();
                            //    TTCD.TenNhomQC = objectAds[AdsId]["adset"]["name"].ToString();
                            //    TTCD.IdChienDich = objectAds[AdsId]["campaign_id"].ToString();
                            //    TTCD.TrangThai = objectAds[AdsId]["status"].ToString();
                            //    //TTCD.ngayTaoQC = objectAds[AdsId]["created_time"].ToString().Split(' ')[0];
                            //    TTCD.TenChienDich = objectCampaign[TTCD.IdChienDich]["name"].ToString();
                            //    TTCD.IdTKQC = TKQC.ID;
                            //    TTCD.TenTKQC = TKQC.tenTK;
                            //    TTCD.tienTe = TKQC.tienTe;

                            //    if (postAds.ContainsKey(AdsId))
                            //    {
                            //        TTCD.xemQC = "https://fb.com/" + postAds[AdsId];
                            //    }

                            //    if (costAds.ContainsKey(TTCD.IdNhomQC))
                            //    {
                            //        TTCD.chiPhiQC = costAds[TTCD.IdNhomQC];
                            //    }
                            //    else
                            //    {
                            //        TTCD.chiPhiQC = "0";
                            //    }
                            //}
                            //if (TTCD.xemQC == null || TTCD.xemQC == "")
                            //{
                            var tempObject = JObject.Parse(RequestGetDetailAds(token, cookie, AdsId));
                            TTCD.IdQC = AdsId;
                            TTCD.TenQC = tempObject[AdsId]["name"].ToString();
                            TTCD.IdNhomQC = tempObject[AdsId]["adset_id"].ToString();
                            TTCD.TenNhomQC = tempObject[AdsId]["adset"]["name"].ToString();
                            TTCD.IdChienDich = tempObject[AdsId]["campaign_id"].ToString();
                            TTCD.TrangThai = tempObject[AdsId]["status"].ToString();
                            TTCD.ngayTaoQC = tempObject[AdsId]["created_time"].ToString().Split(' ')[0];
                            TTCD.TenChienDich = tempObject[AdsId]["campaign"]["name"].ToString();
                            TTCD.IdTKQC = TKQC.ID;
                            TTCD.TenTKQC = TKQC.tenTK;
                            TTCD.tienTe = TKQC.tienTe;
                            //if (postAds.ContainsKey(AdsId))
                            //{
                            //    TTCD.xemQC = "https://fb.com/" + postAds[AdsId];
                            //}
                            try
                            {
                                TTCD.xemQC = "https://fb.com/" + tempObject[AdsId]["creative"]["effective_object_story_id"].ToString();
                            }
                            catch { TTCD.xemQC = ""; }
                            if (costAds.ContainsKey(TTCD.IdQC))
                            {
                                TTCD.chiPhiQC = costAds[TTCD.IdQC];
                            }
                            else
                            {
                                TTCD.chiPhiQC = "0";
                            }
                            // }

                            for (int i = 0; i < Common.lsMKTInfo.Count; i++)
                            {
                                if (TTCD.TenChienDich.Contains(Common.lsMKTInfo[i].MaMKT))
                                {
                                    TTCD.MKT = Common.lsMKTInfo[i].MKT;
                                    TTCD.HKD = Common.lsMKTInfo[i].HKD;
                                    break;
                                }
                                else if(TTCD.TenNhomQC.Contains(Common.lsMKTInfo[i].MaMKT))
                                {
                                    TTCD.MKT = Common.lsMKTInfo[i].MKT;
                                    TTCD.HKD = Common.lsMKTInfo[i].HKD;
                                    break;
                                }
                                else if (TTCD.TenQC.Contains(Common.lsMKTInfo[i].MaMKT))
                                {
                                    TTCD.MKT = Common.lsMKTInfo[i].MKT;
                                    TTCD.HKD = Common.lsMKTInfo[i].HKD;
                                    break;
                                }
                            }
                            if(TTCD.MKT == ""|| TTCD.MKT == null)
                            {
                                TTCD.MKT = TKQC.MKT;
                            }
                            if (TTCD.HKD == "" || TTCD.HKD == null)
                            {
                                TTCD.HKD = TKQC.HKD;
                            }

                            if (TTCDInstance.lsTTCD.Find(x => x.IdQC == TTCD.IdQC) == null)
                            {
                                TTCDInstance.OnNewAccAdded(TTCD);
                                TTCDInstance.lsTTCD.Add(TTCD);
                                TTCDInstance.lsViewTTCD.Add(TTCD);
                            }
                        }
                        catch (Exception e){ }
#pragma warning restore CS0168 // The variable 'e' is declared but never used
                    }
                }
                catch (Exception ex)
                {
                    return;
                }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used

            }
            catch (Exception ex)
            {

            }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        }

        void TTCD_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
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

        private delegate void InvokeDelegate(TTCDEventArgs e);
        private void Statistic_newAccountAdded(object sender, TTCDEventArgs e)
        {
            this.BeginInvoke(new InvokeDelegate(AddToGirdView), new object[] { e });
        }
        private void AddToGirdView(TTCDEventArgs e)
        {
            if (e == null || e.Info == null)
            {
                return;
            }
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            if (e.Info.TrangThai.Contains("ACTIVE"))
            {
                e.Info.TrangThai = "Hoạt động";
            }
            else if (e.Info.TrangThai.Contains("PAUSED"))
            {
                e.Info.TrangThai = "Tạm dừng";
            }
            else if (e.Info.TrangThai.Contains("ARCHIVED"))
            {
                e.Info.TrangThai = "Đã xóa";
            }

            ThongTinChienDichDataGridView.Rows.Insert(ThongTinChienDichDataGridView.Rows.Count - 1,
                                                    e.Info.TrangThai != null ? e.Info.TrangThai : "",
                                                    e.Info.TenTKQC != null ? e.Info.TenTKQC : "",
                                                    e.Info.IdTKQC != null ? e.Info.IdTKQC : "",
                                                    e.Info.MKT != null ? e.Info.MKT : "",
                                                    e.Info.HKD != null ? e.Info.HKD : "",
                                                    e.Info.TenChienDich != null ? e.Info.TenChienDich : "",
                                                    e.Info.IdChienDich != null ? e.Info.IdChienDich : "",
                                                    e.Info.TenNhomQC != null ? e.Info.TenNhomQC : "",
                                                    e.Info.IdNhomQC != null ? e.Info.IdNhomQC : "",
                                                    e.Info.TenQC != null ? e.Info.TenQC : "",
                                                    e.Info.IdQC != null ? e.Info.IdQC : "",
                                                    e.Info.xemQC != null ? e.Info.xemQC : "",
                                                    e.Info.chiPhiQC != null ? string.Format("{0:F2}", double.Parse(e.Info.chiPhiQC).ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0]) : "",
                                                    e.Info.ngayTaoQC != null ? e.Info.ngayTaoQC : "",
                                                    e.Info.tienTe != null ? e.Info.tienTe : ""
                                                    );
            if (ThongTinChienDichDataGridView.Rows.Count - 1 % 2 == 0)
            {
                ThongTinChienDichDataGridView.Rows[ThongTinChienDichDataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237); ;//
            }
            else
            {
                ThongTinChienDichDataGridView.Rows[ThongTinChienDichDataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255); ;//
            }
        }

        void TTCD_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TTCD_RefreshBtn.Enabled = true;
            vbButton3.Enabled = true;
            filterTimeBtn_TTCD.Enabled = true;
            searchBtn_TTCD.Enabled = true;
            TTCD_RefreshBtn.BackgroundColor = Color.FromArgb(255, 255, 255, 255);

            loadingBtnTTCD.Visible = false;
        }
        private void vbButton3_Click(object sender, EventArgs e)
        {
            
            TTCD_RefreshBtn.Enabled = false;
            vbButton3.Enabled = false;
            filterTimeBtn_TTCD.Enabled = false;
            searchBtn_TTCD.Enabled=false;
            loadingBtnTTCD.Visible = true;
            ThongTinChienDichDataGridView.Rows.Clear();
            TTCD_RefreshBtn.Enabled = false;
            TTCD_RefreshBtn.BackgroundColor = Color.FromArgb(255, 198, 65, 24);

            TTCDInstance.fromDate = dateTimePicker1.Value.AddDays(-3000).ToShortDateString();
            TTCDInstance.toDate = dateTimePicker1.Value.ToShortDateString();
            
            TTCD_Worker.RunWorkerAsync();
        }

        private void vbButton3_Click_1(object sender, EventArgs e)
        {
            TTCD_RefreshBtn.Enabled = false;
            vbButton3.Enabled = false;
            filterTimeBtn_TTCD.Enabled = false;
            searchBtn_TTCD.Enabled = false;
            TTCDInstance.fromDate = from_dateTimePicker_TTCD.Value.ToShortDateString();
            TTCDInstance.toDate = to_dateTimePicker_TTCD.Value.ToShortDateString();
            loadingBtnTTCD.Visible = true;
            ThongTinChienDichDataGridView.Rows.Clear();
            TTCD_RefreshBtn.Enabled = false;
            TTCD_RefreshBtn.BackgroundColor = Color.FromArgb(255, 198, 65, 24);
            TTCD_Worker.RunWorkerAsync();
        }


        public static string RequestGetListCampainsIds(string businessId, string accId, string token, string cookie)
        {
            try
            {
                var url = "";
                if (businessId != "")
                    url = $"https://business.facebook.com/adsmanager/manage/campaigns?act={accId}&business_id={businessId}&nav_source=no_referrer";
                else
                {
                    return "";
#pragma warning disable CS0162 // Unreachable code detected
                    url = $"https://business.facebook.com/adsmanager/manage/campaigns?act={accId}";
#pragma warning restore CS0162 // Unreachable code detected
                }
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Headers["cookie"] = cookie;
                httpRequest.Headers["sec-fetch-mode"] = "navigate";
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var ArrResult = result.Split(new string[] { "preloaderData[\"AdsDynamicCampaignGroupDataManagerPreloader\"]=[{\"get\\/v13.0\\/{\\\"ids\\\":\\\"" }, StringSplitOptions.None);
                        var ArrIds = ArrResult[1].Split(new string[] { "\\\",\\\"fields" }, StringSplitOptions.None);

                        return ArrIds[0];
                    }
                }
                return null;
            }
            catch { return null; }
        }

        public static string RequestGetListIds(string businessId, string accId, string token, string cookie)
        {
            try
            {
                var url = "";
                if (businessId != "")
                    url = $"https://business.facebook.com/adsmanager/manage/ads?act={accId}&business_id={businessId}";
                else
                {
                    return "";
#pragma warning disable CS0162 // Unreachable code detected
                    url = $"https://business.facebook.com/adsmanager/manage/campaigns?act={accId}";
#pragma warning restore CS0162 // Unreachable code detected
                }
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Headers["cookie"] = cookie;
                httpRequest.Headers["sec-fetch-mode"] = "navigate";
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var ArrResult = result.Split(new string[] { "preloaderData[\"AdsDynamicAdgroupDataManagerPreloader\"]=[{\"get\\/v14.0\\/{\\\"ids\\\":\\\"" }, StringSplitOptions.None);
                        var ArrIds = ArrResult[1].Split(new string[] { "\\\",\\\"fields" }, StringSplitOptions.None);

                        return ArrIds[0];
                    }
                }
                return null;
            }
            catch { return null; }
        }

        public static string RequestGetListAds(string accId, string token, string stringCookie)
        {
            try
            {
                var url = $"https://graph.facebook.com/v14.0/act_{accId}/lightads";
                url += "?filtering=[{\"field\":\"ad.delivery_status\",\"operator\":\"IN\",\"value\":[\"active\",\"deleted\",\"error\",\"inactive\",\"off\",\"pending\"]},{\"field\":\"draft_action\",\"operator\":\"NOT_IN\",\"value\":[\"add\"]}]";
                url += "&fields=id,name,adset_id";
                url += "&limit=1000";
                url += $"&access_token={token}";
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                var cookies = stringCookie.Split(';');
                foreach (string cookie in cookies)
                {
                    if (cookie == "") break;
                    var datacookie = cookie.Trim().Split('=');
                    request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
                }
                request.AddHeader("cookie", "sb=bPF1Y770-1k09Fbl1C0EjVn9; datr=bPF1Y3-ETOpD04TVTgnkTKZZ; locale=en_GB; dpr=1; c_user=100031578372045; xs=7%3AC9_UFkRGxICrnA%3A2%3A1668673921%3A-1%3A12231%3A%3AAcUdAwOQ3QdIucvfkNlBsqVaAFYfTN0-wIxtRPWAiQ; fr=0EUu0SwqAf3KkxuPd.AWWKLrNL-nQuyQh9sLpYiGWAp1k.Bjdfjx.Mi.AAA.0.0.Bjdfjx.AWX5_ghgYV0; cppo=1; usida=eyJ2ZXIiOjEsImlkIjoiQXJsaGx0a2ZpcG8yaSIsInRpbWUiOjE2Njg2ODAxMjB9; presence=EDvF3EtimeF1668680131EuserFA21B31578372045A2EstateFDutF0CEchF_7bCC; wd=600x979; c_user=100001549118292; fr=0uIyGrJrwuGgKCrcc.AWXevadV3I4YUK1wH5ZWouZl85k.Bjdgba.7M.AAA.0.0.Bjdgba.AWWr03iPXEI; xs=39%3AS0CggPRp5izfZg%3A2%3A1668673800%3A-1%3A8077%3A%3AAcWTnG8rirytOVQcZfw0JQEUfjF7953bpLQSOSLxNg");
                request.AddHeader("sec-fetch-mode", "navigate");
                request.AddHeader("sec-fetch-site", "same-origin");
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36";
                IRestResponse response = client.Execute(request);
                if(response.StatusCode== HttpStatusCode.OK)
                {
                    return response.Content;
                }
                return "";
            }
            catch { return "" ; }
        }



        public static string RequestGetListDetailChiendich(string businessId, string token, string cookie, string lsIds)
        {
            try
            {
                var url = $"https://graph.facebook.com/v13.0/?access_token={token}&__business_id={businessId}&fields=%5B%22account_id%22%2C%22ad_creation_package_config%22%2C%22ad_strategy_group_id%22%2C%22ad_strategy_id%22%2C%22adlabels%22%2C%22benchmark_report_count%22%2C%22bid_strategy%22%2C%22boosted_component_client_app_id%22%2C%22boosted_component_product%22%2C%22boosted_page_id%22%2C%22brand_lift_studies%22%2C%22budget_remaining%22%2C%22buying_type%22%2C%22can_use_spend_cap%22%2C%22collaborative_ads_partner_info%22%2C%22created_time%22%2C%22daily_budget%22%2C%22effective_status%22%2C%22has_secondary_skadnetwork_reporting%22%2C%22id%22%2C%22is_autobid%22%2C%22is_average_price_pacing%22%2C%22is_job_openning_post%22%2C%22is_odax_campaign_group%22%2C%22is_skadnetwork_attribution%22%2C%22is_using_l3_schedule%22%2C%22last_budget_toggling_time%22%2C%22lifetime_budget%22%2C%22lifetime_spent%22%2C%22name%22%2C%22naming_template_custom_fields%22%2C%22objective%22%2C%22pacing_type%22%2C%22primary_attribution%22%2C%22promoted_object%22%2C%22smart_promotion_type%22%2C%22special_ad_categories%22%2C%22special_ad_category%22%2C%22special_ad_category_country%22%2C%22spend_cap%22%2C%22split_test_config%22%2C%22split_test_status%22%2C%22start_time%22%2C%22status%22%2C%22stop_time%22%2C%22study_ids%22%2C%22today_spent%22%2C%22topline_id%22%2C%22updated_time%22%2C%22yesterday_spent%22%5D&ids={lsIds}";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Headers["cookie"] = cookie;
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36";


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
            catch { return ""; }
        }

        public static string RequestGetListDetailAds(string businessId, string token, string stringCookie, string lsIds)
        {
            try
            {
                var client = new RestClient($"https://graph.facebook.com/v14.0/?access_token={token}&method=get");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept-language", "en-US,en;q=0.9");
                var cookies = stringCookie.Split(';');
                foreach (string cookie in cookies)
                {
                    if (cookie == "") break;
                    var datacookie = cookie.Trim().Split('=');
                    request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
                }
                //   request.AddHeader("cookie", );
                client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36";
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("__business_id", businessId);
                request.AddParameter("fields", "[\"account_id\",\"adset_id\",\"campaign_id\",\"creative.thumbnail_height(46).thumbnail_width(46){actor_type,ad_disclaimer_spec,applink_treatment,asset_feed_spec,authorization_category,body,branded_content_boosting_type,branded_content_sponsor_page_id,call_to_action,call_to_action_type,categorization_criteria,category_media_source,contextual_multi_ads{eligibility},degrees_of_freedom_spec,dynamic_ad_voice,effective_authorization_category,effective_instagram_media_id,effective_instagram_story_id,effective_object_story_id,enable_direct_install,facebook_branded_content{sponsor_page_id},id,image_crops,image_hash,image_url,instagram_actor_id,instagram_branded_content{sponsor_id},instagram_permalink_url,instagram_story_id,instant_checkout_setting,interactive_components_spec,link_url,messenger_sponsored_message,mockup_id,object_id,object_story_id,object_story_spec{instagram_actor_id,instant_checkout_ad_type,link_data{additional_image_index,attachment_style,branded_content_sponsor_page_id,call_to_action,caption,child_attachments,collection_thumbnails,custom_overlay_spec,customization_rules_spec,description,event_id,image_crops,image_hash,image_overlay_spec,link,message,moment,multi_share_end_card,multi_share_optimized,name,offer_id,page_welcome_message,phone_data_id,picture,post_click_configuration,product_discount_id,retailer_item_ids},page_id,photo_data,template_data,text_data,video_data},object_type,object_url,omnichannel_link_spec{app{application_id,platform_specs{android{app_name,package_name,url},ios{app_name,app_store_id,url},ipad{app_name,app_store_id,url},iphone{app_name,app_store_id,url}}},web{url}},page_welcome_message,place_page_set_id,platform_customizations,playable_asset_id,portrait_customizations,product_set_id,recommender_settings{preferred_events,product_sales_channel},shop_spec{collection_id,landing_view,shop_id},source_instagram_media_id,template_url,template_url_spec{android{app_name,package,url},config{app_id},ios{app_name,app_store_id,url},ipad{app_name,app_store_id,url},iphone{app_name,app_store_id,url},web{should_fallback,url},windows_phone{app_id,app_name,url}},thumbnail_url,title,uca_draft_version,url_tags,use_page_actor_override,video_id,whatsapp_business_profile_id}\",\"effective_status\",\"id\",\"name\",\"status\"]");
                request.AddParameter("ids", lsIds);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                if (response.StatusCode == HttpStatusCode.OK)
                    return response.Content;
                else
                    return "";
            }
            catch { return ""; }
        }
        public static string RequestGetDetailAds( string token, string stringCookie, string Ids)
        {
            var url = $"https://graph.facebook.com/v14.0/?access_token={token}&method=get";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("authority", "graph.facebook.com");
            request.AddHeader("accept", "*/*");
            request.AddHeader("accept-language", "en-US,en;q=0.9");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var cookies = stringCookie.Split(';');
            foreach (string cookie in cookies)
            {
                if (cookie == "") break;
                var datacookie = cookie.Trim().Split('=');
                request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
            }
            request.AddHeader("origin", "https://business.facebook.com");
            request.AddHeader("referer", "https://business.facebook.com/");
            request.AddHeader("sec-ch-ua", "\"Google Chrome\";v=\"107\", \"Chromium\";v=\"107\", \"Not=A?Brand\";v=\"24\"");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("sec-ch-ua-platform", "\"Windows\"");
            request.AddHeader("sec-fetch-dest", "empty");
            request.AddHeader("sec-fetch-mode", "cors");
            request.AddHeader("sec-fetch-site", "same-site");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36";
            request.AddParameter("__business_id", TTCDInstance.businessId);
            request.AddParameter("am_call_tags", "{\"data_manager\":\"AdsPEAdObjectTableDataManager\"}");
            request.AddParameter("fields", "[\"account_id\",\"adset_id\",\"campaign_id\",\"creative.thumbnail_height(46).thumbnail_width(46){actor_type,ad_disclaimer_spec,applink_treatment,asset_feed_spec,authorization_category,body,branded_content_boosting_type,branded_content_sponsor_page_id,branded_content{instagram_boost_post_access_token,partners,ui_version},call_to_action,call_to_action_type,categorization_criteria,category_media_source,contextual_multi_ads{eligibility},degrees_of_freedom_spec,dynamic_ad_voice,effective_authorization_category,effective_instagram_media_id,effective_instagram_story_id,effective_object_story_id,enable_direct_install,facebook_branded_content{sponsor_page_id},id,image_crops,image_hash,image_url,instagram_actor_id,instagram_branded_content{sponsor_id},instagram_permalink_url,instagram_story_id,instant_checkout_setting,interactive_components_spec,link_url,messenger_sponsored_message,mockup_id,object_id,object_story_id,object_story_spec{instagram_actor_id,instant_checkout_ad_type,link_data{additional_image_index,attachment_style,branded_content_sponsor_page_id,call_to_action,caption,child_attachments,collection_thumbnails,custom_overlay_spec,customization_rules_spec,description,event_id,image_crops,image_hash,image_overlay_spec,link,message,moment,multi_share_end_card,multi_share_optimized,name,offer_id,page_welcome_message,phone_data_id,picture,post_click_configuration,product_discount_id,retailer_item_ids},page_id,photo_data,template_data,text_data,video_data},object_type,object_url,omnichannel_link_spec{app{application_id,platform_specs{android{app_name,package_name,url},ios{app_name,app_store_id,url},ipad{app_name,app_store_id,url},iphone{app_name,app_store_id,url}}},web{url}},page_welcome_message,place_page_set_id,platform_customizations,playable_asset_id,portrait_customizations,product_set_id,recommender_settings{preferred_events,product_sales_channel},shop_spec{collection_id,landing_view,shop_id},source_instagram_media_id,template_url,template_url_spec{android{app_name,package,url},config{app_id},ios{app_name,app_store_id,url},ipad{app_name,app_store_id,url},iphone{app_name,app_store_id,url},web{should_fallback,url},windows_phone{app_id,app_name,url}},thumbnail_url,title,uca_draft_version,url_tags,use_page_actor_override,video_id,whatsapp_business_profile_id}\",\"effective_status\",\"id\",\"name\",\"status\",\"ad_review_feedback\",\"bid_amount\",\"bid_info\",\"bid_type\",\"adset{name}\",\"campaign{name}\",\"conversion_domain\",\"conversion_specs\",\"created_time\",\"creative_history\",\"creative_link_url\",\"demolink_hash\",\"display_sequence\",\"engagement_audience\",\"naming_template_custom_fields\",\"metadata\",\"priority\",\"source_app_id\",\"tracking_and_conversion_with_defaults\",\"tracking_specs\",\"updated_time\",\"view_tags\",\"can_aggregate_social_feedback_on_edit\"]");
            request.AddParameter("ids", Ids);
            IRestResponse response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            return "";
        }


        public static string GetIdPostAds(string IdTKQC, string stringCookie, string token)
        {

            var url = $"https://graph.facebook.com/v14.0/act_{IdTKQC}/?fields=id,ads{{id,name,adset_id,adset{{id,name,end_time,start_time,campaign{{id,name}},promoted_object}},campaign_id,creative{{id,name,object_story_id}},created_time,status,spent,cost_per}}&access_token={token}";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var cookies = stringCookie.Split(';');
            foreach (string cookie in cookies)
            {
                if (cookie == "") break;
                var datacookie = cookie.Trim().Split('=');
                request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
            }
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36";
            request.AddHeader("sec-fetch-mode", "navigate");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            return "";
        }

        public static string getCostAds(string IdTKQC, string stringCookie, string token, string dateSince, string dateUntil)
        {
            var url = $"https://graph.facebook.com/v11.0/act_{IdTKQC}/insights?fields=ad_id,clicks,cost_per_outbound_click,cpc,cpm,cpp,ctr,frequency,impressions,reach,spend,results,actions,cost_per_action_type,mobile_app_purchase_roas,outbound_clicks,outbound_clicks_ctr,purchase_roas,website_purchase_roas&level=ad&time_range[since]={dateSince}&time_range[until]={dateUntil}&access_token={token}&limit=1000";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var cookies = stringCookie.Split(';');
            foreach (string cookie in cookies)
            {
                if (cookie == "") break;
                var datacookie = cookie.Trim().Split('=');
                request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
            }
            request.AddHeader("sec-fetch-mode", "navigate");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36";
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }

            return "";
        }



        private void ThongTinChienDichDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                ThongTinChienDichDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237);
            }
            else
            {
                ThongTinChienDichDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255);
            }
        }

        private void searchBtn_TTCD_Click(object sender, EventArgs e)
        {
            loadingBtnTTCD.Visible = true;
            ThongTinChienDichDataGridView.Rows.Clear();
            TTCDInstance.searchData = SearchTBox_TTCD.Text;
            if (TTCDInstance.searchData != "")
            {
                Thread.Sleep(1000);
                TTCDInstance.lsViewTTCD.Clear();
                for (int i = 0; i < TTCDInstance.lsTTCD.Count; i++)
                {

                    if (TTCDInstance.lsTTCD[i].TrangThai != null && TTCDInstance.lsTTCD[i].TrangThai.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].TenTKQC != null && TTCDInstance.lsTTCD[i].TenTKQC.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].IdTKQC != null && TTCDInstance.lsTTCD[i].IdTKQC.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].MKT != null && TTCDInstance.lsTTCD[i].MKT.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].HKD != null && TTCDInstance.lsTTCD[i].HKD.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].TenChienDich != null && TTCDInstance.lsTTCD[i].TenChienDich.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].IdChienDich != null && TTCDInstance.lsTTCD[i].IdChienDich.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].TenNhomQC != null && TTCDInstance.lsTTCD[i].TenNhomQC.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].IdNhomQC != null && TTCDInstance.lsTTCD[i].IdNhomQC.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].TenQC != null && TTCDInstance.lsTTCD[i].TenQC.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].chiPhiQC != null && TTCDInstance.lsTTCD[i].chiPhiQC.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].ngayTaoQC != null && TTCDInstance.lsTTCD[i].ngayTaoQC.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                    else if (TTCDInstance.lsTTCD[i].tienTe != null && TTCDInstance.lsTTCD[i].tienTe.ToLower().Contains(TTCDInstance.searchData.ToLower()))
                    {
                        TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                        TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                    }
                }
            }
            else if (TTCDInstance.searchData == "")
            {
                Thread.Sleep(1000);
                TTCDInstance.lsViewTTCD.Clear();
                for (int i = 0; i < TTCDInstance.lsTTCD.Count; i++)
                {
                    TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                    TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                }
            }
            loadingBtnTTCD.Visible = false;
        }

        private void filterTimeBtn_TTCD_Click(object sender, EventArgs e)
        {
            loadingBtnTTCD.Visible = true;
            var timeFromSearch = from_dateTimePicker_TTCD_search.Value.ToShortDateString();
            var timeToSearch = to_dateTimePicker_TTCD_search.Value.ToShortDateString();

            ThongTinChienDichDataGridView.Rows.Clear();

            Thread.Sleep(1000);
            TTCDInstance.lsViewTTCD.Clear();
            for (int i = 0; i < TTCDInstance.lsTTCD.Count; i++)
            {
                var date = TTCDInstance.lsTTCD[i].ngayTaoQC;
                if (Common.compareDate(timeFromSearch, date) && Common.compareDate(date, timeToSearch))
                {
                    TTCDInstance.OnNewAccAdded(TTCDInstance.lsTTCD[i]);
                    TTCDInstance.lsViewTTCD.Add(TTCDInstance.lsTTCD[i]);
                }
            }
            loadingBtnTTCD.Visible = false;
        }

        private void copyAlltoClipboard()
        {
            ThongTinChienDichDataGridView.SelectAll();
            DataObject dataObj = ThongTinChienDichDataGridView.GetClipboardContent();
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
            sfd.FileName = $"ThongTinChienDich_{DateTime.Now.ToShortDateString().Replace('/', '_')}.xls";
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
                ThongTinChienDichDataGridView.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void ThongTinChienDichDataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 12)
            {
                e.SortResult = double.Parse(e.CellValue1.ToString()).CompareTo(double.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }

        private void ThongTinChienDichDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    class TTCDThreadState
    {
        public static bool all_thread_together_running { get; set; }
        public static string proxy { get; set; }
        public static bool allow_running { get; set; }
    }
}
