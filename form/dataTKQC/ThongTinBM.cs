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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TakeCarePage;
using TakeCarePage.HelperLibrary.Facebook;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using Excel = Microsoft.Office.Interop.Excel;

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
            //loadingBtnTTBM.Visible = false;
        }

        void TTBM_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                TTBM_Worker.ReportProgress(0);
                for (int index = 0; index < ViaInstances.lsAccVia.Count; index++)
                {
                    var userId = ViaInstances.lsAccVia[index].userId;
                    var cookie = ViaInstances.lsAccVia[index].cookie;
                    var token = ViaInstances.lsAccVia[index].token;
                    var dtsg = AccInfo.Get_fb_dtsg(cookie);
                    if (token == "" || dtsg == "" || ViaInstances.lsAccVia[index].tick == false)
                        continue;
                    if (ViaInstances.lsAccVia[index].tick == false) continue;
                    if (token == "")
                        continue;
                    var InfoBM = RequestTTBM(userId, token, cookie);
                    JObject jObject = JObject.Parse(InfoBM);
                    for (int i = 0; i < jObject["data"].Count(); i++)
                    {
                        var item = jObject["data"][i];
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                        try
                        {
                            var TTBM = new BMInfo();
                            
                            TTBM.BM = item["name"].ToString();
                            TTBM.loaiBM = "BM1";
                            TTBM.role = item["permitted_roles"][0].ToString();
                            if (TTBM.role == "EMPLOYEE")
                            {
                                TTBM.role = "NHÂN VIÊN";
                            }
                            else
                            {
                                TTBM.role = "ADMIN";
                            }
                            if (item["sharing_eligibility_status"].ToString() == "enabled")
                            {
                                TTBM.limit = "BM350";
                            }
                            else
                            {
                                TTBM.limit = "BM50";
                                TTBM.loaiBM = "BM1";
                            }
                            
                            TTBM.verified = item["verification_status"].ToString();
                            TTBM.created = item["created_time"].ToString().Split(' ')[0];
                            TTBM.IDBM = item["id"].ToString();
                            TTBM.slAdmin = item["business_users"]["data"].Count().ToString();
                            TTBM.adminAn = "";
                            TTBM.slAccount = item["client_ad_accounts"]["data"].Count().ToString();
                            TTBM.chatLuongBM = "";
                            try
                            {
                                var infoLiveDie = JObject.Parse(CheckLiveDieBM(TTBM.IDBM, dtsg, cookie));
                                if (infoLiveDie["data"]["data"]["isRestricted"].ToString() == "False")
                                {
                                    TTBM.trangThai = "live";
                                }
                                else
                                {
                                    TTBM.trangThai = "die";
                                }

                                if (infoLiveDie["data"]["data"]["restrictionType"].ToString() != "")
                                {
                                    TTBM.chatLuongBM = "Hạn chế";
                                }
                                else
                                {
                                    TTBM.chatLuongBM = "Tốt";
                                }
                            }
                            catch
                            {

                            }
                            BMInstance.OnNewAccAdded(TTBM);
                            BMInstance.lsBM.Add(TTBM);
                            BMInstance.lsViewBM.Add(TTBM);
                        }
                        catch (Exception ex)
                        {
                        }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
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
                loadingBtnTTBM.Visible = true;
                //ConcurrentQueue<string> queueHttp = (ConcurrentQueue<string>)e.UserState;

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

            var tongBM = ThongTinBMdataGridView.Rows.Count - 1;
            TTBMTotalBMBtn.Text = "TỔNG SỐ BM: " + tongBM.ToString();
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
            TTBM_RefreshBtn.BackgroundColor = Color.FromArgb(255, 198, 65, 24);
            loadingBtnTTBM.Visible = true;
            TTBM_RefreshBtn.Enabled = false;
            ViaInstances.ImportVia();
            ThongTinBMdataGridView.Rows.Clear();
            TTBM_Worker.RunWorkerAsync();

        }


        public static string RequestTTBM(string userId, string token, string cookie)
        {
            var url = $"https://graph.facebook.com/v13.0/me/businesses?fields=name,id,verification_status,business_users,allow_page_management_in_www,sharing_eligibility_status,created_time,permitted_roles,client_ad_accounts.summary(1),owned_ad_accounts.summary(1)&limit=500&access_token={token}&locale=en_US";

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
        private void copyAlltoClipboard()
        {
            ThongTinBMdataGridView.SelectAll();
            DataObject dataObj = ThongTinBMdataGridView.GetClipboardContent();
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
            sfd.FileName = $"ThongTinBM_{DateTime.Now.ToShortDateString().Replace('/', '_')}.xls";
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
                ThongTinBMdataGridView.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        public static string CheckLiveDieBM(string idBM, string dtsg, string stringCookie)
        {
            var client = new RestClient("https://www.facebook.com/api/graphql/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            var cookies = stringCookie.Split(';');
            foreach (string cookie in cookies)
            {
                if (cookie == "") break;
                var datacookie = cookie.Trim().Split('=');
                request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
            }
            request.AddHeader("sec-fetch-site", "none");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36";
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("fb_dtsg", dtsg);
            var varible = $"{{\"entity_id\":\"{idBM}\",\"action\":null}}";
            request.AddParameter("variables", varible);
            request.AddParameter("doc_id", "3920367411328805");
            IRestResponse response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            return "";
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            loadingBtnTTBM.Visible = true;
            ThongTinBMdataGridView.Rows.Clear();
            BMInstance.searchData = SearchTBox.Text;
            if (BMInstance.searchData != "")
            {
                Thread.Sleep(1000);
                BMInstance.lsViewBM.Clear();
                for (int i = 0; i < BMInstance.lsBM.Count; i++)
                {

                    if (BMInstance.lsBM[i].IDBM != null && BMInstance.lsBM[i].IDBM.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }
                    else if (BMInstance.lsBM[i].created != null && BMInstance.lsBM[i].created.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }
                    else if (BMInstance.lsBM[i].loaiBM != null && BMInstance.lsBM[i].loaiBM.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }
                    else if (BMInstance.lsBM[i].limit != null && BMInstance.lsBM[i].limit.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }
                    else if (BMInstance.lsBM[i].chatLuongBM != null && BMInstance.lsBM[i].chatLuongBM.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }
                    else if (BMInstance.lsBM[i].trangThai != null && BMInstance.lsBM[i].trangThai.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }
                    else if (BMInstance.lsBM[i].BM != null && BMInstance.lsBM[i].BM.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }
                    else if (BMInstance.lsBM[i].verified != null && BMInstance.lsBM[i].verified.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }
                    else if (BMInstance.lsBM[i].role != null && BMInstance.lsBM[i].role.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }
                    else if (BMInstance.lsBM[i].slAdmin != null && BMInstance.lsBM[i].slAdmin.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }
                    else if (BMInstance.lsBM[i].slAccount != null && BMInstance.lsBM[i].slAccount.Contains(BMInstance.searchData))
                    {
                        BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                        BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                    }

                }
            }
            else if (BMInstance.searchData == "")
            {
                Thread.Sleep(1000);
                BMInstance.lsViewBM.Clear();
                for (int i = 0; i < BMInstance.lsBM.Count; i++)
                {
                    BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                    BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                }
            }
            loadingBtnTTBM.Visible = false;
        }

        private void locTimeBtn_Click(object sender, EventArgs e)
        {
            loadingBtnTTBM.Visible = true;
            var timeFromSearch = from_dateTimePicker.Value.ToShortDateString();
            var timeToSearch = to_dateTimePicker.Value.ToShortDateString();

            ThongTinBMdataGridView.Rows.Clear();

            Thread.Sleep(1000);
            BMInstance.lsViewBM.Clear();
            for (int i = 0; i < BMInstance.lsBM.Count; i++)
            {
                var date = BMInstance.lsBM[i].created;
                if (Common.compareDate(timeFromSearch, date) && Common.compareDate(date, timeToSearch))
                {
                    BMInstance.OnNewAccAdded(BMInstance.lsBM[i]);
                    BMInstance.lsViewBM.Add(BMInstance.lsBM[i]);
                }
            }
            loadingBtnTTBM.Visible = false;
        }

        private void ThongTinBMdataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 8)
            {
                e.SortResult = double.Parse(e.CellValue1.ToString()).CompareTo(double.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }
    }
}
