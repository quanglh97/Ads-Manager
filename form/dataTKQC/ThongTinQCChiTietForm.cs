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
using System.Security.Policy;
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
    public partial class ThongTinQCChiTietForm : Form
    {
        BackgroundWorker dataQCChiTiet_Worker = null;

        volatile int live_thread_number = 0;
        Queue<int> qu_position_pro5 = new Queue<int>();

        public ThongTinQCChiTietForm()
        {
            InitializeComponent();
            TTQCCT_DataGridView.RowHeadersVisible = false;
            TTQCCT_DataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 10, 147, 66); ;//10, 147, 66
            TTQCCT_DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataQCChiTiet_Worker = new BackgroundWorker();
            dataQCChiTiet_Worker.DoWork += new DoWorkEventHandler(QCCC_Worker_DoWork);
            dataQCChiTiet_Worker.ProgressChanged += new ProgressChangedEventHandler(QCCC_Worker_ProgressChanged);
            dataQCChiTiet_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(QCCC_Worker_RunWorkerCompleted);
            dataQCChiTiet_Worker.WorkerReportsProgress = true;
            dataQCChiTiet_Worker.WorkerSupportsCancellation = true;

            QCCCInstance.NewAccountAdded += this.Statistic_newAccountAdded;
        }

        void QCCC_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            try
            {
                foreach (var idPost in LinkQCInstance.Links)
                {
                    var QCCC_tong = new QCCCInfo_Tong();
                    QCCC_tong.linkQC = idPost;
                    QCCC_tong.numQC = 0;
                    foreach (var TTCD in TTCDInstance.lsTTCD)
                    {
                        if (TTCD.lsLinkQC.Contains(idPost))
                        {
                            QCCC_tong.numQC++;
                            QCCC_tong.CTR_TB += double.Parse(TTCD.ctr);
                            QCCC_tong.CPM_TB = double.Parse(TTCD.cpm);
                            QCCC_tong.BinhLuan += int.Parse(TTCD.comment);
                            QCCC_tong.chiPhiQC += double.Parse(TTCD.chiPhiQC);
                            QCCC_tong.mess += int.Parse(TTCD.mess);
                            QCCC_tong.chuyenDoi += int.Parse(TTCD.chuyenDoi);
                            QCCC_tong.trangThai = TTCD.TrangThai;
                            QCCC_tong.tenTKQC = TTCD.TenTKQC;
                            QCCC_tong.idTKQC = TTCD.IdTKQC;
                            QCCC_tong.tienTe = TTCD.tienTe;

                        }
                    }
                    QCCC_tong.CTR_TB = QCCC_tong.CTR_TB / QCCC_tong.numQC;
                    QCCC_tong.CPM_TB = QCCC_tong.CPM_TB / QCCC_tong.numQC;
                    //QCCC_tong.chiPhiQC = QCCC_tong.chiPhiQC / QCCC_tong.numQC;
                    //QCCC_tong.mess = QCCC_tong.mess / QCCC_tong.numQC;
                    //QCCC_tong.BinhLuan = QCCC_tong.BinhLuan / QCCC_tong.numQC;
                    //QCCC_tong.chuyenDoi = QCCC_tong.chuyenDoi / QCCC_tong.numQC;

                    for (int i = 0; i < Common.lsMKTInfo.Count; i++)
                    {
                        if (QCCC_tong.idTKQC.Contains(Common.lsMKTInfo[i].IdTKQC))
                        {
                            QCCC_tong.MKT = Common.lsMKTInfo[i].MKT;
                            QCCC_tong.MaMKT = Common.lsMKTInfo[i].MaMKT;
                            QCCC_tong.HKD = Common.lsMKTInfo[i].HKD;
                            break;
                        }
                    }

                    QCCCInstance.OnNewAccAdded(QCCC_tong);
                    QCCCInstance.lsQCCC_tong.Add(QCCC_tong);
                    QCCCInstance.lsViewQCCC_tong.Add(QCCC_tong);
                }
            }
            catch(Exception ex) { }

        }

        void QCCC_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        void QCCC_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /////////////////////////////////////////////////
            loadingBtn_TTQCCT.Enabled = true;
            searchBtn_TTQCCT.Enabled = true;
            //LocChiPhi_TTQCCT.Enabled = true;
            //filterTimeBtn_TTQCCT.Enabled = true;
            xuatExcel_TTQCCT.Enabled = true;
            TTQCCT_RefreshBtn.BackgroundColor = Color.FromArgb(255, 255, 255, 255);
            TTQCCT_RefreshBtn.Enabled = true;
            loadingBtn_TTQCCT.Visible = false;
        }

        async Task AsyncCoreDoWork(TTTKQCInfo TKQC)
        {
            await Task.Run(() =>
            {
                // update live thread ++ & position for chrome profile    
                int pro5 = qu_position_pro5.Dequeue();
                // do task
                if (!dataQCChiTiet_Worker.CancellationPending)
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
                var businessId = QCCCInstance.businessId;
                for (int i = 0; i < BMInstance.lsBM.Count; i++)
                {

                    var BM = BMInstance.lsBM.FirstOrDefault(n => n.lsAdId.Contains(TKQC.ID));
                    if (BM != null)
                    {
                        businessId = BM.BMId;
                        break;
                    }
                }


                var accId = TKQC.ID;
                var cookie = TKQC.cookie;
                var token = TKQC.token;
                var trangThai = TKQC.trangThai;
                var tenTKQC = TKQC.tenTK;
                var tienTe = TKQC.tienTe;
                var dateFilter = Common.parseStringDateToFilter(QCCCInstance.fromDate, QCCCInstance.toDate);
                var dateSince = Common.parseStringDateToFilter2(QCCCInstance.fromDate);
                var dateUntil = Common.parseStringDateToFilter2(QCCCInstance.toDate);

                //var dataQCCCResponse = GetDetailQC(QCCCInstance.businessId, accId, cookie, token, dateSince, dateUntil);
                var dataQCCCResponse = GetDetailQC(businessId, accId, cookie, token, dateSince, dateUntil);
                JObject objectdataQCCCResponse = JObject.Parse(dataQCCCResponse);
                while (objectdataQCCCResponse != null)
                {
                    try
                    {

                        foreach (var item in objectdataQCCCResponse["data"][0]["rows"])
                        {
                            try
                            {
                                var dataQCCC = new QCCCInfo();
                                dataQCCC.idQC = item["dimensions"][1]["value"].ToString();
                                if (dataQCCC.idQC.Contains("__summary__"))
                                {
                                    continue;
                                }
                                if (QCCCInstance.lsQCCC.FirstOrDefault(n => n.idQC == dataQCCC.idQC) != null)
                                {
                                    return;
                                }
                                dataQCCC.trangThai = trangThai;
                                dataQCCC.tenTKQC = tenTKQC;
                                dataQCCC.idTKQC = accId;

                                dataQCCC.tenQC = item["dimensions"][0]["value"].ToString();


                                dataQCCC.tienTe = item["metrics"][0]["value"].ToString();
                                dataQCCC.BinhLuan = item["metrics"][2]["value"].ToString();
                                dataQCCC.chuyenDoi = item["metrics"][3]["value"].ToString();
                                dataQCCC.mess = item["metrics"][4]["value"].ToString();
                                dataQCCC.CPM = item["metrics"][8]["value"].ToString();
                                dataQCCC.CTR = item["metrics"][9]["value"].ToString();
                                dataQCCC.chiPhiQC = item["metrics"][13]["value"].ToString();


                                QCCCInstance.lsQCCC.Add(dataQCCC);




                                //QCCCInstance.OnNewAccAdded(dataQCCC);
                                //QCCCInstance.lsQCCC.Add(dataQCCC);
                                //QCCCInstance.lsViewQCCC.Add(dataQCCC);
                            }

                            catch (Exception e) { }
                        }
                        try
                        {

                            if (objectdataQCCCResponse["paging"]["next"] != null)
                            {
                                var url = objectdataQCCCResponse["paging"]["next"].ToString();
                                objectdataQCCCResponse = null;
                                dataQCCCResponse = null;
                                dataQCCCResponse = ContinueGetDetailQC(url, cookie);
                                objectdataQCCCResponse = JObject.Parse(dataQCCCResponse);
                            }
                            else
                                objectdataQCCCResponse = null;
                        }
                        catch
                        {
                            objectdataQCCCResponse = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }


        private delegate void InvokeDelegate(QCCCEventArgs e);
        private void Statistic_newAccountAdded(object sender, QCCCEventArgs e)
        {
            this.BeginInvoke(new InvokeDelegate(AddToGirdView), new object[] { e });
        }
        private void AddToGirdView(QCCCEventArgs e)
        {
            if (e == null || e.Info == null)
            {
                return;
            }
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            TTQCCT_DataGridView.Rows.Insert(TTQCCT_DataGridView.Rows.Count - 1,
                                                    e.Info.trangThai != null ? e.Info.trangThai : "",
                                                    e.Info.tenTKQC != null ? e.Info.tenTKQC : "",
                                                    e.Info.idTKQC != null ? e.Info.idTKQC : "",
                                                    e.Info.MaMKT != null ? e.Info.MaMKT : "",
                                                    e.Info.MKT != null ? e.Info.MKT : "",
                                                    e.Info.HKD != null ? e.Info.HKD : "",
                                                    e.Info.linkQC != null ? e.Info.linkQC : "",
                                                    e.Info.CPM_TB != null ? e.Info.CPM_TB.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "",
                                                    e.Info.CTR_TB != null ? e.Info.CTR_TB.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "",
                                                    e.Info.chuyenDoi != null ? e.Info.chuyenDoi.ToString() : "",
                                                    e.Info.mess != null ? e.Info.mess.ToString() : "",
                                                    e.Info.BinhLuan != null ? e.Info.BinhLuan.ToString() : "",
                                                    e.Info.chiPhiQC != null ? e.Info.chiPhiQC.ToString("N", nfi).Split(new string[] { ".00" }, StringSplitOptions.None)[0] : "",
                                                    e.Info.tienTe != null ? e.Info.tienTe : ""
                                                    );
            if (TTQCCT_DataGridView.Rows.Count - 1 % 2 == 0)
            {
                TTQCCT_DataGridView.Rows[TTQCCT_DataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237); ;//
            }
            else
            {
                TTQCCT_DataGridView.Rows[TTQCCT_DataGridView.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255); ;//
            }
        }





















        private void TTQCCT_DataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                TTQCCT_DataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 217, 235, 237);
            }
            else
            {
                TTQCCT_DataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 255);
            }
        }

        private void copyAlltoClipboard()
        {
            TTQCCT_DataGridView.SelectAll();
            DataObject dataObj = TTQCCT_DataGridView.GetClipboardContent();
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
            sfd.FileName = $"ThongQuangCaoChiTiet_{DateTime.Now.ToShortDateString().Replace('/', '_')}.xls";
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

                TTQCCT_DataGridView.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void TTQCCT_DataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 7 || e.Column.Index == 8|| e.Column.Index == 9 || e.Column.Index == 10|| e.Column.Index == 11 || e.Column.Index == 12)
            {
                e.SortResult = double.Parse(e.CellValue1.ToString()).CompareTo(double.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }

        public string GetDetailQC(string businessId, string accId, string strCookie, string accToken, string dateSince, string dateUntil)
        {
            var request = new RestRequest(Method.GET);
            var url = $"https://adsmanager-graph.facebook.com/v15.0/{businessId}/ads_reporting?access_token={accToken}&__ad_account_id={accId}&currency=VND&dimensions=%5B%22ad_name%22%2C%22ad_id%22%5D&filtering=%5B%7B%22field%22%3A%22had_delivery%22%2C%22operator%22%3A%22EQUAL%22%2C%22value%22%3A%221%22%7D%5D&limit=50&locale=vi_VN&locked_dimensions=3&method=get&metrics=%5B%22account_currency%22%2C%22account_id%22%2C%22actions%3Acomment%22%2C%22actions%3Aomni_complete_registration%22%2C%22actions%3Aonsite_conversion.messaging_conversation_started_7d%22%2C%22ad_id%22%2C%22attribution_setting%22%2C%22conversion_annotations%22%2C%22cpm%22%2C%22ctr%22%2C%22frequency%22%2C%22impressions%22%2C%22reach%22%2C%22spend%22%5D&offset=0&pretty=0&sorting=%5B%7B%22field%22%3A%22spend%22%2C%22direction%22%3A%22desc%22%7D%5D&suppress_http_code=1&time_range={{\"since\":\"{dateSince}\",\"until\":\"{dateUntil}\"}}";
            var client = new RestClient(url);
            request.AddHeader("authority", "adsmanager-graph.facebook.com");
            request.AddHeader("accept", "*/*");
            request.AddHeader("accept-language", "en-US,en;q=0.9");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var cookies = strCookie.Split(';');
            foreach (string cookie in cookies)
            {
                if (cookie == "") break;
                var datacookie = cookie.Trim().Split('=');
                if (datacookie.Length > 1)
                    request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
            }
            request.AddHeader("origin", "https://adsmanager.facebook.com");
            request.AddHeader("referer", "https://adsmanager.facebook.com/");
            request.AddHeader("sec-ch-ua", "\"Chromium\";v=\"112\", \"Google Chrome\";v=\"112\", \"Not:A-Brand\";v=\"99\"");
            request.AddHeader("sec-fetch-mode", "cors");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content;
        }

        public string ContinueGetDetailQC(string url, string strCookie)
        {
            var request = new RestRequest(Method.GET);
            var client = new RestClient(url);
            request.AddHeader("authority", "adsmanager-graph.facebook.com");
            request.AddHeader("accept", "*/*");
            request.AddHeader("accept-language", "en-US,en;q=0.9");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var cookies = strCookie.Split(';');
            foreach (string cookie in cookies)
            {
                if (cookie == "") break;
                var datacookie = cookie.Trim().Split('=');
                if (datacookie.Length > 1)
                    request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
            }
            request.AddHeader("origin", "https://adsmanager.facebook.com");
            request.AddHeader("referer", "https://adsmanager.facebook.com/");
            request.AddHeader("sec-ch-ua", "\"Chromium\";v=\"112\", \"Google Chrome\";v=\"112\", \"Not:A-Brand\";v=\"99\"");
            request.AddHeader("sec-fetch-mode", "cors");
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            return response.Content;
        }

        private void TTQCCT_RefreshBtn_Click(object sender, EventArgs e)
        {
            TTQCCT_RefreshBtn.Enabled = false;
            //LocChiPhi_TTQCCT.Enabled = false;
            //filterTimeBtn_TTQCCT.Enabled = false;
            searchBtn_TTQCCT.Enabled = false;
            xuatExcel_TTQCCT.Enabled = false;
            loadingBtn_TTQCCT.Visible = true;
            TTQCCT_DataGridView.Rows.Clear();
            TTQCCT_RefreshBtn.BackgroundColor = Color.FromArgb(255, 198, 65, 24);
            //Common.ImportMKTInfo();
            QCCCInstance.fromDate = dateTimePicker1.Value.AddDays(-3000).ToShortDateString();
            QCCCInstance.toDate = dateTimePicker1.Value.ToShortDateString();

            dataQCChiTiet_Worker.RunWorkerAsync();
        }

        private void searchBtn_TTQCCT_Click(object sender, EventArgs e)
        {
            loadingBtn_TTQCCT.Visible = true;
            TTQCCT_DataGridView.Rows.Clear();
            QCCCInstance.searchData = SearchTBox_TTQCCT.Text;
            if (QCCCInstance.searchData != "")
            {
                Thread.Sleep(1000);
                QCCCInstance.lsViewQCCC_tong.Clear();
                for (int i = 0; i < QCCCInstance.lsQCCC_tong.Count; i++)
                {

                    if (QCCCInstance.lsQCCC_tong[i].trangThai != null && QCCCInstance.lsQCCC_tong[i].trangThai.ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].tenTKQC != null && QCCCInstance.lsQCCC_tong[i].tenTKQC.ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].idTKQC != null && QCCCInstance.lsQCCC_tong[i].idTKQC.ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].MaMKT != null && QCCCInstance.lsQCCC_tong[i].MaMKT.ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].MKT != null && QCCCInstance.lsQCCC_tong[i].MKT.ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].HKD != null && QCCCInstance.lsQCCC_tong[i].HKD.ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].linkQC != null && QCCCInstance.lsQCCC_tong[i].linkQC.ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].CPM_TB != null && QCCCInstance.lsQCCC_tong[i].CPM_TB.ToString().ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].CTR_TB != null && QCCCInstance.lsQCCC_tong[i].CTR_TB.ToString().ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].chuyenDoi != null && QCCCInstance.lsQCCC_tong[i].chuyenDoi.ToString().ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].mess != null && QCCCInstance.lsQCCC_tong[i].mess.ToString().ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].BinhLuan != null && QCCCInstance.lsQCCC_tong[i].BinhLuan.ToString().ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].chiPhiQC != null && QCCCInstance.lsQCCC_tong[i].chiPhiQC.ToString().ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }
                    else if (QCCCInstance.lsQCCC_tong[i].tienTe != null && QCCCInstance.lsQCCC_tong[i].tienTe.ToLower().Contains(QCCCInstance.searchData.ToLower()))
                    {
                        QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                        QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                    }

                }
            }
            else if (QCCCInstance.searchData == "")
            {
                Thread.Sleep(1000);
                QCCCInstance.lsViewQCCC_tong.Clear();
                for (int i = 0; i < QCCCInstance.lsQCCC_tong.Count; i++)
                {
                    QCCCInstance.OnNewAccAdded(QCCCInstance.lsQCCC_tong[i]);
                    QCCCInstance.lsViewQCCC_tong.Add(QCCCInstance.lsQCCC_tong[i]);
                }
            }
            loadingBtn_TTQCCT.Visible = false;
        }
    }




    class TTQCCTThreadState
    {
        public static bool all_thread_together_running { get; set; }
        public static string proxy { get; set; }
        public static bool allow_running { get; set; }
    }
}
