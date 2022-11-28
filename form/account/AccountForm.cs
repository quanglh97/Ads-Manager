using AdsManager.HelperLibrary.Facebook;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TakeCarePage;
using TakeCarePage.HelperLibrary.Facebook;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AdsManager 
{
    public partial class AccountForm : Form
    {
        public AccountForm()
        {
            InitializeComponent();
            ViaInstances.NewAccountAdded += this.Statistic_newAccountAdded;

            AccountDataGridView.RowHeadersVisible = false;
            AccountDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AccountDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 10, 147, 66); ;//10, 147, 66
            AccountDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            //UidTBox.TextAlign = HorizontalAlignment.Center;
            //PassTBox.TextAlign = HorizontalAlignment.Center;
            //CodeTBox.TextAlign = HorizontalAlignment.Center;
            //TokenTBox.TextAlign = HorizontalAlignment.Center;
            //StatusTBox.TextAlign = HorizontalAlignment.Center;

            //test tí thôi í mà
        }

        private delegate void InvokeDelegate(ViaEventArgs e);
        private void Statistic_newAccountAdded(object sender, ViaEventArgs e)
        {
            this.BeginInvoke(new InvokeDelegate(AddToGirdView), new object[] { e });
        }
        private void AddToGirdView(ViaEventArgs e)
        {
            if (e == null || e.Info == null)
            {
                return;
            }

            AccountDataGridView.Rows.Insert(AccountDataGridView.Rows.Count - 1,
                                                    e.Info.status != null ? e.Info.status : "",
                                                    e.Info.tick,
                                                    e.Info.TenHT != null ? e.Info.TenHT : "",
                                                    e.Info.HKD != null ? e.Info.HKD : "",
                                                    e.Info.userId != null ? e.Info.userId : "",
                                                    e.Info.pass != null ? e.Info.pass : "",
                                                    e.Info.key2fa != null ? e.Info.key2fa : "",
                                                    e.Info.cookie != null ? e.Info.cookie : "",
                                                    e.Info.token != null ? e.Info.token : "");

            var status = AccountDataGridView.Rows[0].Cells["Status"].Value.ToString();
            var uid = AccountDataGridView.Rows[0].Cells["Uid"].Value.ToString();
            var pass = AccountDataGridView.Rows[0].Cells["Pass"].Value.ToString();
            var code = AccountDataGridView.Rows[0].Cells["Code"].Value.ToString();
            var cookie = AccountDataGridView.Rows[0].Cells["Cookie"].Value.ToString();
            var token = AccountDataGridView.Rows[0].Cells["token"].Value.ToString();
            var tenHT = AccountDataGridView.Rows[0].Cells["TenHT"].Value.ToString();
            var HKD = AccountDataGridView.Rows[0].Cells["HKD"].Value.ToString();

            UidTBox.Text = uid;
            PassTBox.Text = pass;
            CodeTBox.Text = code;
            CookieTBox.Text = cookie;
            StatusTBox.Text = status;
            TokenTBox.Text = token;
            TenHTTBox.Text = tenHT;
            HKDTBox.Text = HKD;
        }

        private void AccountDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var status = AccountDataGridView.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            var uid = AccountDataGridView.Rows[e.RowIndex].Cells["Uid"].Value.ToString();
            var pass = AccountDataGridView.Rows[e.RowIndex].Cells["Pass"].Value.ToString();
            var code = AccountDataGridView.Rows[e.RowIndex].Cells["Code"].Value.ToString();
            var cookie = AccountDataGridView.Rows[e.RowIndex].Cells["Cookie"].Value.ToString();
            var token = AccountDataGridView.Rows[e.RowIndex].Cells["token"].Value.ToString();
            var tenHT = AccountDataGridView.Rows[e.RowIndex].Cells["TenHT"].Value.ToString();
            var HKD = AccountDataGridView.Rows[e.RowIndex].Cells["HKD"].Value.ToString();

            UidTBox.Text = uid;
            PassTBox.Text = pass;
            CodeTBox.Text = code;
            CookieTBox.Text = cookie;
            StatusTBox.Text = status;
            TokenTBox.Text = token;
            TenHTTBox.Text = tenHT;
            HKDTBox.Text = HKD;
        }

        private void Account_RefreshBtn_Click(object sender, EventArgs e)
        {
            //Account_RefreshBtn.BackgroundColor = Color.Red;
            AccountDataGridView.Rows.Clear();
            ViaInstances.ImportVia();

            for (int i = 0; i < ViaInstances.lsAccVia.Count; i++)
            {
                try
                {
                    ViaInstances.OnNewAccAdded(ViaInstances.lsAccVia[i]);
                }
                catch
                {

                }
            }


        }

        private void AddAccBtn_Click(object sender, EventArgs e)
        {
            if (AccAddBtn.Text == "Clear")
            {
                AccAddBtn.Text = "Add";

                UidTBox.Text = "";
                PassTBox.Text = "";
                CodeTBox.Text = "";
                CookieTBox.Text = "";
                StatusTBox.Text = "";
                TokenTBox.Text = "";
                TenHTTBox.Text = "";
                HKDTBox.Text = "";
            }
            else
            {
                var status = StatusTBox.Text;
                var uid = UidTBox.Text;
                var pass = PassTBox.Text;
                var code = CodeTBox.Text;
                var cookie = CookieTBox.Text;
                var token = TokenTBox.Text;
                var tenHT = TenHTTBox.Text;
                var HKD = HKDTBox.Text;

                if (uid == "" || pass == "" || cookie == "")
                {
                    AccAddBtn.Text = "lost Info";
                    return;
                }
                var acc = new AccInfo();
                acc.status = status;
                acc.userId = uid;
                acc.pass = pass;
                acc.key2fa = code;
                acc.cookie = cookie;
                acc.token = token;
                acc.TenHT = tenHT;
                acc.HKD = HKD;
                acc.Account = acc.userId + "|" + acc.pass + "|" + acc.key2fa + "|" + acc.cookie + "|" + acc.TenHT + "|" + acc.HKD;
                if (ViaInstances.lsAccVia.Find(x => x.userId == acc.userId) == null)
                {
                    ViaInstances.lsAccVia.Add(acc);
                    var dataFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
                    if (!Directory.Exists(dataFolder))
                    {
                        Directory.CreateDirectory(dataFolder);
                    }
                    var fileVia = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "viaAcc.txt");
                    if (!File.Exists(fileVia))
                    {
                        return;
                    }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                    try
                    {
                        File.Delete(fileVia);
                        AccountDataGridView.Rows.Clear();
                        for (int i = 0; i < ViaInstances.lsAccVia.Count; i++)
                        {
                            File.AppendAllText(fileVia, ViaInstances.lsAccVia[i].Account + Environment.NewLine);

                        }

                    }
                    catch (Exception ex)
                    {
                        ViaInstances.lsAccVia.Clear();
                    }
                    finally
                    {

                    }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used

                    ViaInstances.ImportVia();

                    for (int i = 0; i < ViaInstances.lsAccVia.Count; i++)
                    {
                        try
                        {
                            ViaInstances.OnNewAccAdded(ViaInstances.lsAccVia[i]);
                        }
                        catch
                        {

                        }
                    }
                }

                AccAddBtn.Text = "Clear";

                UidTBox.Text = "";
                PassTBox.Text = "";
                CodeTBox.Text = "";
                CookieTBox.Text = "";
                StatusTBox.Text = "";
                TokenTBox.Text = "";
                TenHTTBox.Text = "";
                HKDTBox.Text = "";
            }
        }

        private void AccUpdateBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= AccountDataGridView.Rows.Count - 1; i++)
            {
                try
                {
                    bool checkedCell = (bool)AccountDataGridView.Rows[i].Cells[1].Value;
                    string Uid = (string)AccountDataGridView.Rows[i].Cells[4].Value;
                    var TempVia = ViaInstances.lsAccVia.Find(x => x.userId == Uid);
                    TempVia.tick = checkedCell;
                }
                catch { }
            }

            var userId = UidTBox.Text;
            var Via = ViaInstances.lsAccVia.Find(x => x.userId == userId);
            if (Via != null)
            {
                Via.cookie = CookieTBox.Text.Trim();
                Via.pass = PassTBox.Text.Trim();
                Via.key2fa = CodeTBox.Text.Trim();
                var tenHT = TenHTTBox.Text;
                var HKD = HKDTBox.Text;
                Via.TenHT = tenHT;
                Via.HKD = HKD;
                var tick = Via.tick ? "1" : "0";
                Via.Account = Via.userId + "|" + Via.pass + "|" + Via.key2fa + "|" + Via.cookie + "|" + Via.TenHT + "|" + Via.HKD + "|" + tick;

                var dataFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
                if (!Directory.Exists(dataFolder))
                {
                    Directory.CreateDirectory(dataFolder);
                }
                var fileVia = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "viaAcc.txt");
                if (!File.Exists(fileVia))
                {
                    return;
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                try
                {
                    File.Delete(fileVia);
                    AccountDataGridView.Rows.Clear();
                    for (int i = 0; i < ViaInstances.lsAccVia.Count; i++)
                    {
                        File.AppendAllText(fileVia, ViaInstances.lsAccVia[i].Account + Environment.NewLine);

                    }

                }
                catch (Exception ex)
                {
                    ViaInstances.lsAccVia.Clear();
                }
                finally
                {

                }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used

                ViaInstances.ImportVia();

                for (int i = 0; i < ViaInstances.lsAccVia.Count; i++)
                {
                    try
                    {
                        ViaInstances.OnNewAccAdded(ViaInstances.lsAccVia[i]);
                    }
                    catch
                    {

                    }
                }

            }
        }

        private void AccountDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (AccountDataGridView.Rows[e.RowIndex].Cells[0].Value != null && AccountDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Vô"))
            {
                AccountDataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
            }
            else if (AccountDataGridView.Rows[e.RowIndex].Cells[0].Value != null && AccountDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Contains("Hoạt"))
            {
                AccountDataGridView.Rows[e.RowIndex].Cells[0].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
            }
            else
            {
                AccountDataGridView.Rows[e.RowIndex].Cells[0].Style = AccountDataGridView.DefaultCellStyle;
            }
        }

        private void vbButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "data//");
        }
    }
}
