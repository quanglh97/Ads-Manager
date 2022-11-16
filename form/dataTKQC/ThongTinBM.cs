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
using System.Threading.Tasks;
using System.Windows.Forms;
using TakeCarePage;
using TakeCarePage.HelperLibrary.Facebook;

namespace AdsManager.form.dataTKQC
{
    public partial class ThongTinBM : Form
    {
        BackgroundWorker TTBM_Worker = null;
        public ThongTinBM()
        {
            InitializeComponent();
            //ThongTinBMdataGridView
            ThongTinBMdataGridView.RowHeadersVisible = false;
            ThongTinBMdataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 10, 147, 66); ;//10, 147, 66
            ThongTinBMdataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            ThongTinBMdataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            TTBM_Worker = new BackgroundWorker();
            TTBM_Worker.DoWork += new DoWorkEventHandler(TTBM_Worker_DoWork);
            TTBM_Worker.ProgressChanged += new ProgressChangedEventHandler(TTBM_Worker_ProgressChanged);
            TTBM_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(TTBM_Worker_RunWorkerCompleted);
            TTBM_Worker.WorkerReportsProgress = true;
            TTBM_Worker.WorkerSupportsCancellation = true;

            BMInstance.NewAccountAdded += this.Statistic_newAccountAdded;

        }

        void TTBM_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (int index = 0; index < ViaInstances.lsAccVia.Count; index++)
                {
                    var userId = ViaInstances.lsAccVia[index].userId;
                    var cookie = ViaInstances.lsAccVia[index].cookie;
                    var token = ViaInstances.lsAccVia[index].token;
                    if (ViaInstances.lsAccVia[index].tick == false) continue;
                    if (token == "")
                        continue;
                    var InfoBM = RequestTTBM(userId, token, cookie);
                    JObject jObject = JObject.Parse(InfoBM);
                    for (int i = 0; i < jObject["data"].Count(); i++)
                    {
                        var item = jObject["data"][i];
                        try
                        {
                            var TTBM = new BMInfo();
                            TTBM.trangThai = item["sharing_eligibility_status"].ToString();
                            TTBM.BM = item["name"].ToString();
                            TTBM.loaiBM = "BM1";
                            TTBM.role = item["permitted_roles"][0].ToString();
                            TTBM.limit = "BM350";
                            TTBM.verified = item["verification_status"].ToString();
                            TTBM.created = item["created_time"].ToString();
                            TTBM.IDBM = item["id"].ToString();
                            TTBM.slAdmin = item["business_users"]["data"].Count().ToString();
                            TTBM.adminAn = "";
                            TTBM.slAccount = item["client_ad_accounts"]["data"].Count().ToString();
                            TTBM.chatLuongBM = "";

                            BMInstance.OnNewAccAdded(TTBM);
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    TTBM_Worker.ReportProgress(1);
                    TTBM_Worker.ReportProgress(0, ProxyInstances.Http);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void TTBM_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
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

        void TTBM_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TTBM_RefreshBtn.Enabled = true;
            TTBM_RefreshBtn.BackgroundColor = Color.FromArgb(255, 255, 255, 255);

            loadingBtnTTBM.Visible = false;
        }

        private delegate void InvokeDelegate(BMEventArgs e);
        private void Statistic_newAccountAdded(object sender, BMEventArgs e)
        {
            this.BeginInvoke(new InvokeDelegate(AddToGirdView), new object[] { e });
        }
        private void AddToGirdView(BMEventArgs e)
        {
            if (e == null || e.Info == null)
            {
                return;
            }

            ThongTinBMdataGridView.Rows.Insert(ThongTinBMdataGridView.Rows.Count - 1,
                                                    e.Info.trangThai != null ? e.Info.trangThai : "",
                                                    e.Info.BM != null ? e.Info.BM : "",
                                                    e.Info.loaiBM != null ? e.Info.loaiBM : "",
                                                    e.Info.role != null ? e.Info.role : "",
                                                    e.Info.limit != null ? e.Info.limit : "",
                                                    e.Info.verified != null ? e.Info.verified : "",
                                                    e.Info.created != null ? e.Info.created : "",
                                                    e.Info.IDBM != null ? e.Info.IDBM : "",
                                                    e.Info.slAdmin != null ? e.Info.slAdmin : "",
                                                    e.Info.adminAn != null ? e.Info.adminAn : "",
                                                    e.Info.slAccount != null ? e.Info.slAccount : "",
                                                    e.Info.chatLuongBM != null ? e.Info.chatLuongBM : "");
            if (ThongTinBMdataGridView.Rows.Count - 1 % 2 == 0)
            {
                ThongTinBMdataGridView.Rows[ThongTinBMdataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237); ;//
            }
            else
            {
                ThongTinBMdataGridView.Rows[ThongTinBMdataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255); ;//
            }
        }


        private void TTBM_RefreshBtn_Click(object sender, EventArgs e)
        {
            ViaInstances.ImportVia();
            ThongTinBMdataGridView.Rows.Clear();
            TTBM_RefreshBtn.BackgroundColor = Color.FromArgb(255, 198, 65, 24);
            TTBM_RefreshBtn.Enabled = false;
            TTBM_Worker.RunWorkerAsync();

            loadingBtnTTBM.Visible = true;
        }


        public static string RequestTTBM(string userId, string token, string cookie)
        {
            var url = $"https://graph.facebook.com/v13.0/me/businesses?fields=name,id,verification_status,business_users,allow_page_management_in_www,sharing_eligibility_status,created_time,permitted_roles,client_ad_accounts.summary(1),owned_ad_accounts.summary(1)&limit=10&access_token={token}&locale=en_US";

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

        public static string RequestFullInfoTTBM(string accId, string token, string coookie)
        {
            var url = $"https://graph.facebook.com/v13.0/act_{accId}?fields=account_id,owner_business,created_time,next_bill_date,currency,adtrust_dsl,timezone_name,timezone_offset_hours_utc,business_country_code,disable_reason,adspaymentcycle{{threshold_amount}},balance,is_prepay_account,owner,insights.date_preset(maximum){{spend}}&access_token={token}&locale=en_US";

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

        private void ThongTinBMdataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (ThongTinBMdataGridView.Rows.Count - 1 % 2 == 0)
            {
                ThongTinBMdataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237); ;//
            }
            else
            {
                ThongTinBMdataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255); ;//
            }
        }

        private void ThongTinBMdataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (ThongTinBMdataGridView.Rows[e.RowIndex].Cells[0].Value != null && !string.IsNullOrWhiteSpace(ThongTinBMdataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()))
            {
                ThongTinBMdataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Red};
            }
            else
            {
                ThongTinBMdataGridView.Rows[e.RowIndex].Cells[0].Style = ThongTinBMdataGridView.DefaultCellStyle;
            }
        }
    }
}
