using AdsManager.form.dataTKQC;
using AdsManager.HelperLibrary;
using AdsManager.HelperLibrary.Facebook;
using AdsManager.HelperLibrary.Facebook.Instance;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TakeCarePage;

namespace AdsManager
{
    public partial class DataTKQCForm : Form
    {
        ThongTinChienDichForm TTCDFrom = new ThongTinChienDichForm();
        ThongTinBM TTBMFrom = new ThongTinBM();
        ThongTinTKQCForm TTTKQCFrom = new ThongTinTKQCForm();
        HoaDonTTForm hoaDonTTFrom = new HoaDonTTForm();
        public DataTKQCForm()
        {
            InitializeComponent();
            //DataTKQCHeaderPanel.BringToFront();
            TTCDFrom.TopLevel = false;
            TTBMFrom.TopLevel = false;
            TTTKQCFrom.TopLevel = false;
            hoaDonTTFrom.TopLevel = false;
            DataTKQCContentPanel.Controls.Add(TTCDFrom);
            DataTKQCContentPanel.Controls.Add(TTBMFrom);
            DataTKQCContentPanel.Controls.Add(TTTKQCFrom);
            DataTKQCContentPanel.Controls.Add(hoaDonTTFrom);
        }

        private void TTCDBtn_Click(object sender, EventArgs e)
        {
            TTTKQCBTn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTTKQCBTn.ForeColor = Color.FromArgb(163, 163, 163);

            TTBMBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTBMBtn.ForeColor = Color.FromArgb(163, 163, 163);

            HoadonTTBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            HoadonTTBtn.ForeColor = Color.FromArgb(163, 163, 163);

            TTCDBtn.BackColor = Color.White;
            TTCDBtn.ForeColor = Color.Black;

            
            //TTCDFrom.TopLevel = false;
            //DataTKQCContentPanel.Controls.Add(TTCDFrom);
            TTCDFrom.BringToFront();
            TTCDFrom.Show();
        }

        private void TTBMBtn_Click(object sender, EventArgs e)
        {
            TTTKQCBTn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTTKQCBTn.ForeColor = Color.FromArgb(163, 163, 163);

            TTBMBtn.BackColor = Color.White;
            TTBMBtn.ForeColor = Color.Black;

            TTCDBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTCDBtn.ForeColor = Color.FromArgb(163, 163, 163);

            HoadonTTBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            HoadonTTBtn.ForeColor = Color.FromArgb(163, 163, 163);

            
            //TTBMFrom.TopLevel = false;
            //DataTKQCContentPanel.Controls.Add(TTBMFrom);
            TTBMFrom.BringToFront();
            TTBMFrom.Show();
        }

        private void TTTKQCBTn_Click(object sender, EventArgs e)
        {
            TTTKQCBTn.BackColor = Color.White;
            TTTKQCBTn.ForeColor = Color.Black;

            TTBMBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTBMBtn.ForeColor = Color.FromArgb(163, 163, 163);

            TTCDBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTCDBtn.ForeColor = Color.FromArgb(163, 163, 163);

            HoadonTTBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            HoadonTTBtn.ForeColor = Color.FromArgb(163, 163, 163);

            //TTTKQCFrom.TopLevel = false;
            //DataTKQCContentPanel.Controls.Add(TTTKQCFrom);
            TTTKQCFrom.BringToFront();
            TTTKQCFrom.Show();
        }

        private void HoadonTTBtn_Click(object sender, EventArgs e)
        {
            TTTKQCBTn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTTKQCBTn.ForeColor = Color.FromArgb(163, 163, 163);

            TTBMBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTBMBtn.ForeColor = Color.FromArgb(163, 163, 163);

            TTCDBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTCDBtn.ForeColor = Color.FromArgb(163, 163, 163);

            HoadonTTBtn.BackColor = Color.White;
            HoadonTTBtn.ForeColor = Color.Black;

            
            //hoaDonTTFrom.TopLevel = false;
            //DataTKQCContentPanel.Controls.Add(hoaDonTTFrom);
            hoaDonTTFrom.BringToFront();
            hoaDonTTFrom.Show();
        }

        private void locTimeBtn_Click(object sender, EventArgs e)
        {
            TTTKQCInstance.filterTime = true;
            //locTimeBtn.Enabled = false;
            //while (TTTKQCInstance.filterTime == true)
            //{
            //    Thread.Sleep(500);
            //}
            //locTimeBtn.Enabled = true;
        }

       
    }
}
