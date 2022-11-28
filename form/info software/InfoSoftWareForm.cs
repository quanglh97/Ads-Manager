using AdsManager.HelperLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdsManager
{
    public partial class InfoSoftWareForm : Form
    {
        public InfoSoftWareForm()
        {
            InitializeComponent();
            Common.key = Common.GetKey();
            keyBtn.Text = Common.key;
            timeBtn.Text = DateTime.Now.AddYears(1).ToString();
        }

        private void copyKeyBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, Common.key);

        }  
    }
}
