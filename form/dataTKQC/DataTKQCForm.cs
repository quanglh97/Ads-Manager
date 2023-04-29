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
        ThongTinChienDichForm TTCDForm = new ThongTinChienDichForm();
        ThongTinBM TTBMForm = new ThongTinBM();
        ThongTinTKQCForm TTTKQCForm = new ThongTinTKQCForm();
        HoaDonTTForm hoaDonTTForm = new HoaDonTTForm();
        DuLieuPageForm duLieuPageForm = new DuLieuPageForm();
        ThongTinQCChiTietForm duLieuQCForm = new ThongTinQCChiTietForm(); 
        public DataTKQCForm()
        {
            InitializeComponent();
            //DataTKQCHeaderPanel.BringToFront();
            TTCDForm.TopLevel = false;
            TTBMForm.TopLevel = false;
            TTTKQCForm.TopLevel = false;
            hoaDonTTForm.TopLevel = false;
            duLieuPageForm.TopLevel = false;
            duLieuQCForm.TopLevel = false;
            DataTKQCContentPanel.Controls.Add(TTCDForm);
            DataTKQCContentPanel.Controls.Add(TTBMForm);
            DataTKQCContentPanel.Controls.Add(TTTKQCForm);
            DataTKQCContentPanel.Controls.Add(hoaDonTTForm);
            DataTKQCContentPanel.Controls.Add(duLieuPageForm);
            DataTKQCContentPanel.Controls.Add(duLieuQCForm);
        }

        private void TTCDBtn_Click(object sender, EventArgs e)
        {
            TTTKQCBTn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTTKQCBTn.ForeColor = Color.FromArgb(163, 163, 163);

            TTBMBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTBMBtn.ForeColor = Color.FromArgb(163, 163, 163);

            HoadonTTBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            HoadonTTBtn.ForeColor = Color.FromArgb(163, 163, 163);

            duLieuPageBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            duLieuPageBtn.ForeColor = Color.FromArgb(163, 163, 163);

            TTCDBtn.BackColor = Color.White;
            TTCDBtn.ForeColor = Color.Black;

            DuLieuQCBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            DuLieuQCBtn.ForeColor = Color.FromArgb(163, 163, 163);


            //TTCDForm.TopLevel = false;
            //DataTKQCContentPanel.Controls.Add(TTCDForm);
            TTCDForm.BringToFront();
            TTCDForm.Show();
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

            duLieuPageBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            duLieuPageBtn.ForeColor = Color.FromArgb(163, 163, 163);

            DuLieuQCBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            DuLieuQCBtn.ForeColor = Color.FromArgb(163, 163, 163);

            //TTBMForm.TopLevel = false;
            //DataTKQCContentPanel.Controls.Add(TTBMForm);
            TTBMForm.BringToFront();
            TTBMForm.Show();
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

            duLieuPageBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            duLieuPageBtn.ForeColor = Color.FromArgb(163, 163, 163);

            DuLieuQCBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            DuLieuQCBtn.ForeColor = Color.FromArgb(163, 163, 163);

            //TTTKQCForm.TopLevel = false;
            //DataTKQCContentPanel.Controls.Add(TTTKQCForm);
            TTTKQCForm.BringToFront();
            TTTKQCForm.Show();
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

            duLieuPageBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            duLieuPageBtn.ForeColor = Color.FromArgb(163, 163, 163);

            DuLieuQCBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            DuLieuQCBtn.ForeColor = Color.FromArgb(163, 163, 163);

            //hoaDonTTForm.TopLevel = false;
            //DataTKQCContentPanel.Controls.Add(hoaDonTTForm);
            hoaDonTTForm.BringToFront();
            hoaDonTTForm.Show();
        }
        private void duLieuPageBtn_Click(object sender, EventArgs e)
        {
            TTTKQCBTn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTTKQCBTn.ForeColor = Color.FromArgb(163, 163, 163);

            TTBMBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTBMBtn.ForeColor = Color.FromArgb(163, 163, 163);

            TTCDBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTCDBtn.ForeColor = Color.FromArgb(163, 163, 163);

            HoadonTTBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            HoadonTTBtn.ForeColor = Color.FromArgb(163, 163, 163);

            duLieuPageBtn.BackColor = Color.White;
            duLieuPageBtn.ForeColor = Color.Black;

            DuLieuQCBtn.BackColor =  Color.FromArgb(255, 226, 226, 226);
            DuLieuQCBtn.ForeColor =  Color.FromArgb(163, 163, 163);

            duLieuPageForm.BringToFront();
            duLieuPageForm.Show();
        }

        private void DuLieuQCBtn_Click(object sender, EventArgs e)
        {
            TTTKQCBTn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTTKQCBTn.ForeColor = Color.FromArgb(163, 163, 163);

            TTBMBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTBMBtn.ForeColor = Color.FromArgb(163, 163, 163);

            TTCDBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            TTCDBtn.ForeColor = Color.FromArgb(163, 163, 163);

            HoadonTTBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            HoadonTTBtn.ForeColor = Color.FromArgb(163, 163, 163);

            duLieuPageBtn.BackColor = Color.FromArgb(255, 226, 226, 226);
            duLieuPageBtn.ForeColor = Color.FromArgb(163, 163, 163);

            DuLieuQCBtn.BackColor = Color.White;
            DuLieuQCBtn.ForeColor = Color.Black;

            duLieuQCForm.BringToFront();
            duLieuQCForm.Show();
        }
        private void locTimeBtn_Click(object sender, EventArgs e)
        {
            TTTKQCInstance.filterTime = true;
            //
            //.Enabled = false;
            //while (TTTKQCInstance.filterTime == true)
            //{
            //    Thread.Sleep(500);
            //}
            //locTimeBtn.Enabled = true;
        }

        
    }
}
