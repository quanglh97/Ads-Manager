using AdsManager;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TakeCarePage.HelperLibrary.Facebook;

namespace TakeCarePage
{
    public partial class FormMain : Form
    {
        //cac loại form 
        AccountForm accFrom = new AccountForm();
        DataTKQCForm dataTKQCFrom = new DataTKQCForm();
        InfoSoftWareForm infoSWFrom = new InfoSoftWareForm();
        




        BackgroundWorker workerVia = null;
        BackgroundWorker workerImportProxy = null;

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

        }

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
