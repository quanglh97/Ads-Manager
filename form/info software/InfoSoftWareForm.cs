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
            keyBtn.Text = "Quangdeptrai";
            Common.key = keyBtn.Text;
            
        }

        private void copyKeyBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, Common.key);

        }  
    }
}
