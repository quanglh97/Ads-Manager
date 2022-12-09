using AdsManager.HelperLibrary;
using AdsManager.HelperLibrary.Facebook;
using AdsManager.HelperLibrary.Facebook.Info;
using AdsManager.HelperLibrary.Facebook.Instance;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TakeCarePage;
using Excel = Microsoft.Office.Interop.Excel;

namespace AdsManager.form.dataTKQC
{
    public partial class DuLieuPageForm : Form
    {
        BackgroundWorker dataPage_Worker = null;

        volatile int live_thread_number = 0;
        Queue<int> qu_position_pro5 = new Queue<int>();

        public DuLieuPageForm()
        {
            InitializeComponent();
            DuLieuPageDataGridView.RowHeadersVisible = false;
            DuLieuPageDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 10, 147, 66); ;//10, 147, 66
            DuLieuPageDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DuLieuPageDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataPage_Worker = new BackgroundWorker();
            dataPage_Worker.DoWork += new DoWorkEventHandler(TTCD_Worker_DoWork);
            dataPage_Worker.ProgressChanged += new ProgressChangedEventHandler(TTCD_Worker_ProgressChanged);
            dataPage_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(TTCD_Worker_RunWorkerCompleted);
            dataPage_Worker.WorkerReportsProgress = true;
            dataPage_Worker.WorkerSupportsCancellation = true;

            DataPageInstance.NewAccountAdded += this.Statistic_newAccountAdded;
        }

        void TTCD_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            live_thread_number = 0;
            qu_position_pro5.Clear();
            for (int i = 1; i <= 100; i++)
                qu_position_pro5.Enqueue(i);

            DataPageThreadState.allow_running = true;
            DataPageThreadState.proxy = "";
            DataPageThreadState.all_thread_together_running = true;
            try
            {
                DataPageInstance.lsDataPage.Clear();
                List<TTTKQCInfo> lsTKQCClone = new List<TTTKQCInfo>(TTTKQCInstance.lsTKQC.Count);
                TTTKQCInstance.lsTKQC.ForEach((item) =>
                {
                    lsTKQCClone.Add(new TTTKQCInfo(item));
                });

                //if (TTCDInstance.businessId == "")
                //{
                //    TTCDInstance.getBusinessId(lsTKQCClone[0].token, lsTKQCClone[0].cookie);
                //}

                for (int index = 0; index < lsTKQCClone.Count; index++) //
                {
                    var TKQC = lsTKQCClone[index];
                    while (!dataPage_Worker.CancellationPending)
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
                            while (!dataPage_Worker.CancellationPending && live_thread_number != 0 && DataPageThreadState.all_thread_together_running) // if = 0 => break
                                Thread.Sleep(TimeSpan.FromSeconds(1));
                        }

                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }


                    // [1.2] do work, dam bao da reset proxy o tren theo option
                    if (!dataPage_Worker.CancellationPending && DataPageThreadState.allow_running)
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


                dataPage_Worker.ReportProgress(1);
                dataPage_Worker.ReportProgress(0, ProxyInstances.Http);
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
                if (!dataPage_Worker.CancellationPending)
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
                var trangThai = TKQC.trangThai;
                var tenTKQC = TKQC.tenTK;
                var tienTe = TKQC.tienTe;
                var dateFilter = Common.parseStringDateToFilter(DataPageInstance.fromDate, DataPageInstance.toDate);

                var dateSince = Common.parseStringDateToFilter2(DataPageInstance.fromDate);
                var dateUntil = Common.parseStringDateToFilter2(DataPageInstance.toDate);

                var dataPageResponse = RequestDataReportPage(accId, token, cookie, dateSince, dateUntil);
                JObject objectdataPageResponse = JObject.Parse(dataPageResponse);

                try
                {

                    foreach (var item in objectdataPageResponse["data"][0]["rows"])
                    {
                        try
                        {
                            var dataPage = new DataPageInfo();

                            dataPage.trangThai = trangThai;
                            dataPage.tenTKQC = tenTKQC;
                            dataPage.idTKQC = accId;
                            dataPage.tenPage = item["dimensions"][0]["value"].ToString();
                            dataPage.idPage = item["metrics"][7]["value"].ToString();
                            dataPage.luotNguoiTiepCan = item["metrics"][6]["value"].ToString();
                            dataPage.CPM = item["metrics"][2]["value"].ToString();
                            dataPage.CTR = item["metrics"][3]["value"].ToString();
                            dataPage.chiPhiQC = item["metrics"][9]["value"].ToString();
                            dataPage.tienTe = tienTe;
                            dataPage.linkPage = "https://www.facebook.com/" + dataPage.idPage;

                            DataPageInstance.OnNewAccAdded(dataPage);
                            DataPageInstance.lsDataPage.Add(dataPage);
                            DataPageInstance.lsViewDataPage.Add(dataPage);
                        }
                        catch (Exception e) { }
                    }
                }
                catch (Exception ex)
                {
                    return;
                }

            }
            catch (Exception ex)
            {

            }
        }


        void TTCD_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        void TTCD_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DuLieuPage_RefreshBtn.Enabled = true;
            vbButton4.Enabled = true;
            locTimeBtn.Enabled = true;
            searchBtn.Enabled = true;
            DuLieuPage_RefreshBtn.BackgroundColor = Color.FromArgb(255, 255, 255, 255);

            loadingBtn.Visible = false;
        }


        private delegate void InvokeDelegate(DataPageEventArgs e);
        private void Statistic_newAccountAdded(object sender, DataPageEventArgs e)
        {
            this.BeginInvoke(new InvokeDelegate(AddToGirdView), new object[] { e });
        }

        private void AddToGirdView(DataPageEventArgs e)
        {
            if (e == null || e.Info == null)
            {
                return;
            }
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            DuLieuPageDataGridView.Rows.Insert(DuLieuPageDataGridView.Rows.Count - 1,
                                                    e.Info.trangThai != null ? e.Info.trangThai : "",
                                                    e.Info.tenTKQC != null ? e.Info.tenTKQC : "",
                                                    e.Info.idTKQC != null ? e.Info.idTKQC : "",
                                                    e.Info.tenPage != null ? e.Info.tenPage : "",
                                                    e.Info.idPage != null ? e.Info.idPage : "",
                                                    e.Info.luotNguoiTiepCan != null ? double.Parse(e.Info.luotNguoiTiepCan).ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "",
                                                    e.Info.CPM != null ? double.Parse(e.Info.CPM).ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "",
                                                    e.Info.CTR != null ? double.Parse(e.Info.CTR).ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "",
                                                    e.Info.chiPhiQC != null ? double.Parse(e.Info.chiPhiQC).ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "",
                                                    e.Info.tienTe != null ? e.Info.tienTe : "",
                                                    e.Info.linkPage != null ? e.Info.linkPage : ""
                                                    );
            if (DuLieuPageDataGridView.Rows.Count - 1 % 2 == 0)
            {
                DuLieuPageDataGridView.Rows[DuLieuPageDataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237);
            }
            else
            {
                DuLieuPageDataGridView.Rows[DuLieuPageDataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255);
            }
        }






        public static string RequestDataReportPage(string accId, string accessToken, string stringCookie, string sinceTime, string untilTime)
        {
            var url = $"https://graph.facebook.com/v14.0/act_{accId}/reporting?access_token={accessToken}&dimensions=%5B%22page_name%22%5D&filtering=%5B%7B%22field%22%3A%22had_delivery%22%2C%22operator%22%3A%22EQUAL%22%2C%22value%22%3A%221%22%7D%5D&limit=500&method=get&metrics=%5B%22attribution_setting%22%2C%22conversion_annotations%22%2C%22cpm%22%2C%22ctr%22%2C%22delivery_end%22%2C%22delivery_start%22%2C%22impressions%22%2C%22page_id%22%2C%22reach%22%2C%22spend%22%5D&time_range={{\"since\":\"{sinceTime}\",\"until\":\"{untilTime}\"}}";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
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
            request.AddHeader("sec-fetch-dest", "empty");
            request.AddHeader("sec-fetch-mode", "cors");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content;
            }
            return "";
        }

        private void DuLieuPage_RefreshBtn_Click(object sender, EventArgs e)
        {
            DuLieuPage_RefreshBtn.Enabled = false;
            vbButton4.Enabled = false;
            locTimeBtn.Enabled = false;
            searchBtn.Enabled = false;
            loadingBtn.Visible = true;
            DuLieuPageDataGridView.Rows.Clear();
            DuLieuPage_RefreshBtn.BackgroundColor = Color.FromArgb(255, 198, 65, 24);

            DataPageInstance.fromDate = dateTimePicker1.Value.AddDays(-3000).ToShortDateString();
            DataPageInstance.toDate = dateTimePicker1.Value.ToShortDateString();

            dataPage_Worker.RunWorkerAsync();
        }

        private void DuLieuPageDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                DuLieuPageDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237);
            }
            else
            {
                DuLieuPageDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255);
            }
        }

        private void DuLieuPageDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DuLieuPageDataGridView.Rows[e.RowIndex].Cells[0].Value != null && DuLieuPageDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Vô"))
            {
                DuLieuPageDataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
            }
            else if (DuLieuPageDataGridView.Rows[e.RowIndex].Cells[0].Value != null && DuLieuPageDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Hoạt"))
            {
                DuLieuPageDataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
            }
            else if (DuLieuPageDataGridView.Rows[e.RowIndex].Cells[0].Value != null && DuLieuPageDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Cần"))
            {
                DuLieuPageDataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Orange };
            }
            else
            {
                DuLieuPageDataGridView.Rows[e.RowIndex].Cells[0].Style = DuLieuPageDataGridView.DefaultCellStyle;
            }
        }

        private void locTimeBtn_Click(object sender, EventArgs e)
        {
            DuLieuPage_RefreshBtn.Enabled = false;
            vbButton4.Enabled = false;
            locTimeBtn.Enabled = false;
            searchBtn.Enabled = false;
            loadingBtn.Visible = true;
            DuLieuPageDataGridView.Rows.Clear();
            DataPageInstance.fromDate = from_dateTimePicker.Value.ToShortDateString();
            DataPageInstance.toDate = to_dateTimePicker.Value.ToShortDateString();
            dataPage_Worker.RunWorkerAsync();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            loadingBtn.Visible = true;
            DuLieuPageDataGridView.Rows.Clear();
            DataPageInstance.searchData = SearchTBox.Text;
            if (DataPageInstance.searchData != "")
            {
                Thread.Sleep(1000);
                DataPageInstance.lsViewDataPage.Clear();
                for (int i = 0; i < DataPageInstance.lsDataPage.Count; i++)
                {

                    if (DataPageInstance.lsDataPage[i].trangThai != null && DataPageInstance.lsDataPage[i].trangThai.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                    else if (DataPageInstance.lsDataPage[i].tenPage != null && DataPageInstance.lsDataPage[i].tenPage.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                    else if (DataPageInstance.lsDataPage[i].idPage != null && DataPageInstance.lsDataPage[i].idPage.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                    else if (DataPageInstance.lsDataPage[i].tenTKQC != null && DataPageInstance.lsDataPage[i].tenTKQC.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                    else if (DataPageInstance.lsDataPage[i].idTKQC != null && DataPageInstance.lsDataPage[i].idTKQC.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                    else if (DataPageInstance.lsDataPage[i].luotNguoiTiepCan != null && DataPageInstance.lsDataPage[i].luotNguoiTiepCan.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                    else if (DataPageInstance.lsDataPage[i].CPM != null && DataPageInstance.lsDataPage[i].CPM.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                    else if (DataPageInstance.lsDataPage[i].CTR != null && DataPageInstance.lsDataPage[i].CTR.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                    else if (DataPageInstance.lsDataPage[i].chiPhiQC != null && DataPageInstance.lsDataPage[i].chiPhiQC.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                    else if (DataPageInstance.lsDataPage[i].linkPage != null && DataPageInstance.lsDataPage[i].linkPage.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                    else if (DataPageInstance.lsDataPage[i].tienTe != null && DataPageInstance.lsDataPage[i].tienTe.Contains(DataPageInstance.searchData))
                    {
                        DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                        DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                    }
                }
            }
            else if (DataPageInstance.searchData == "")
            {
                Thread.Sleep(1000);
                DataPageInstance.lsViewDataPage.Clear();
                for (int i = 0; i < DataPageInstance.lsDataPage.Count; i++)
                {
                    DataPageInstance.OnNewAccAdded(DataPageInstance.lsDataPage[i]);
                    DataPageInstance.lsViewDataPage.Add(DataPageInstance.lsDataPage[i]);
                }
            }
            loadingBtn.Visible = false;
        }
        private void copyAlltoClipboard()
        {
            DuLieuPageDataGridView.SelectAll();
            DataObject dataObj = DuLieuPageDataGridView.GetClipboardContent();
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
            sfd.FileName = $"DuLieuPage_{DateTime.Now.ToShortDateString().Replace('/', '_')}.xls";
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
                DuLieuPageDataGridView.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void DuLieuPageDataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 5 || e.Column.Index == 6 || e.Column.Index == 7 || e.Column.Index == 8)
            {
                e.SortResult = double.Parse(e.CellValue1.ToString()).CompareTo(double.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }
    }

    class DataPageThreadState
    {
        public static bool all_thread_together_running { get; set; }
        public static string proxy { get; set; }
        public static bool allow_running { get; set; }
    }

}
