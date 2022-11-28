using AdsManager;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TakeCarePage.HelperLibrary.Facebook;
using DeviceId;
using AdsManager.HelperLibrary;

namespace TakeCarePage
{
    public partial class FormMain : Form
    {
        //cac loại form 
        AccountForm accFrom = new AccountForm();
        DataTKQCForm dataTKQCFrom = new DataTKQCForm();
        InfoSoftWareForm infoSWFrom = new InfoSoftWareForm();





#pragma warning disable CS0414 // The field 'FormMain.workerVia' is assigned but its value is never used
        BackgroundWorker workerVia = null;
#pragma warning restore CS0414 // The field 'FormMain.workerVia' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'FormMain.workerImportProxy' is assigned but its value is never used
        BackgroundWorker workerImportProxy = null;
#pragma warning restore CS0414 // The field 'FormMain.workerImportProxy' is assigned but its value is never used

        List<dgvEventArgs> lsSeedingDataResult = new List<dgvEventArgs>();
        public static int nIndex = 0;

        public static List<object> argumentsTMProxy = new List<object>();
        public static List<object> argumentsTinsoftProxy = new List<object>();
        public static List<object> argumentsTinProxy = new List<object>();
        public static List<object> argumentsMinProxy = new List<object>();

        public static bool stopCmd = false;

        public static List<object> argumentsProxyRotatingV4 = new List<object>();
        public static List<object> argumentsProxyRotatingV6 = new List<object>();

        volatile List<AccInfo> gAccVia = new List<AccInfo>();

        public static FileSaver FS = new FileSaver();
        Random rnd = new Random();
        ConcurrentQueue<string> gQueueNhanXetPage = new ConcurrentQueue<string>();

        bool haveLicence = false;
        BackgroundWorker checkLicense_Worker = null;
        public FormMain()
        {
            InitializeComponent();
            this.FormClosing += FormMain_FormClosing;
            headerPanel.BringToFront();
            //contentPanel.BringToFront();
            accFrom.TopLevel = false;
            dataTKQCFrom.TopLevel = false;
            infoSWFrom.TopLevel = false;
            contentPanel.Controls.Add(accFrom);
            contentPanel.Controls.Add(dataTKQCFrom);
            contentPanel.Controls.Add(infoSWFrom);
            

            accBtn.Enabled = false;
            dataTKQCBtn.Enabled = false;

            checkLicense_Worker = new BackgroundWorker();
            checkLicense_Worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            checkLicense_Worker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            checkLicense_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
            checkLicense_Worker.WorkerReportsProgress = true;
            checkLicense_Worker.WorkerSupportsCancellation = true;

            checkLicense_Worker.RunWorkerAsync();

        }

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            while (true)
            {
                if(Common.key == null || Common.key == "")
                {
                    Thread.Sleep(3000);
                    continue;
                }
                Dictionary<string, string> infoKey = Logger.ReadEntries();
                var existKey = false;
                foreach(var key in infoKey.Keys)
                {

                    if (key == Common.key)
                    {
                        existKey = true;
                        if (infoKey[key] == "ok")
                        {
                            haveLicence = true;
                            checkLicense_Worker.ReportProgress(0);
                            break;
                        }
                        else
                        {
                            haveLicence = false;
                            checkLicense_Worker.ReportProgress(0);
                        }
                    }
                }
                if (!existKey)
                {
                    Logger.CreateEntry(Common.key);
                }
                Thread.Sleep(10000);
            }
        }

        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Here you play with the main UI thread
            //string runningStatus = e.UserState.ToString();
            if (e.ProgressPercentage == 0)
            {
                if (haveLicence)
                {
                    accBtn.Enabled = true;
                    dataTKQCBtn.Enabled = true;
                }
                else
                {
                    accBtn.Enabled = false;
                    dataTKQCBtn.Enabled = false;
                }

            }
            
        }

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { } 

            private void FormMain_FormClosing(Object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            else if (e.CloseReason == CloseReason.UserClosing)
            {
                switch (MessageBox.Show(this, "BẠN CÓ CHẮC MUỐN TẮT KHÔNG?", "BẠN VẪN MUỐN TĂT À?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        if (!ViaInstances.MultiThrsAccVia.IsStopping)
                        {
                            ViaInstances.MultiThrsAccVia.Cancel();
                        }
                        break;
                }
            }
        }

        private void accBtn_Click(object sender, EventArgs e)
        {
            accBtn.BackColor = Color.FromArgb(255, 233, 75, 79);
            accBtn.ForeColor = Color.White;

            dataTKQCBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            dataTKQCBtn.ForeColor = Color.Black;

            infoSoftwareBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            infoSoftwareBtn.ForeColor = Color.Black;

            accFrom.TopLevel = false;
            accFrom.Dock = DockStyle.Fill;
            accFrom.MaximumSize = new System.Drawing.Size(1024, 547);

            //contentPanel.Controls.Add(accFrom);
            accFrom.BringToFront();
            accFrom.Show();
        }

        private void dataTKQCBtn_Click(object sender, EventArgs e)
        {
            dataTKQCBtn.BackColor = Color.FromArgb(255, 233, 75, 79);
            dataTKQCBtn.ForeColor = Color.White;

            accBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            accBtn.ForeColor = Color.Black;

            infoSoftwareBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            infoSoftwareBtn.ForeColor = Color.Black;

            dataTKQCFrom.TopLevel = false;
            dataTKQCFrom.Dock = DockStyle.Fill;
            dataTKQCFrom.MaximumSize = new System.Drawing.Size(1024, 547);

            //contentPanel.Controls.Add(dataTKQCFrom);
            dataTKQCFrom.BringToFront();
            dataTKQCFrom.Show();
        }

        private void infoSoftwareBtn_Click(object sender, EventArgs e)
        {
            infoSoftwareBtn.BackColor = Color.FromArgb(255, 233, 75, 79);
            infoSoftwareBtn.ForeColor = Color.White;

            accBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            accBtn.ForeColor = Color.Black;

            dataTKQCBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            dataTKQCBtn.ForeColor = Color.Black;

            infoSWFrom.TopLevel = false;
            infoSWFrom.Dock = DockStyle.Fill;
            infoSWFrom.MaximumSize = new System.Drawing.Size(1024, 547);


            //contentPanel.Controls.Add(infoSWFrom);
            infoSWFrom.BringToFront();
            infoSWFrom.Show();


            //infoSWFrom.Controls.Add(InfoSoftWarePanel);
            //InfoSoftWarePanel.BringToFront();
            //InfoSoftWarePanel.Show();
        }

        public void BeginFrom()
        {
            accBtn.BackColor = Color.FromArgb(255, 233, 75, 79);
            accBtn.ForeColor = Color.White;

            dataTKQCBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            dataTKQCBtn.ForeColor = Color.Black;

            infoSoftwareBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            infoSoftwareBtn.ForeColor = Color.Black;

            accFrom.TopLevel = false;
            accFrom.Dock = DockStyle.Fill;
            accFrom.MaximumSize = new System.Drawing.Size(1024, 547);

            contentPanel.Controls.Add(accFrom);
            accFrom.BringToFront();
            accFrom.Show();
        }
    }
}
