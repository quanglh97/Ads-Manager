using AdsManager.HelperLibrary.Facebook;
using AdsManager.HelperLibrary.Facebook.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeCarePage.HelperLibrary.Facebook;

namespace TakeCarePage
{
    public class dgvOutput : EventArgs
    {
        public int nID { get; set; }
        public string sOutput { get; set; }

        public dgvOutput(int nID, string sOutput)
        {
            this.nID = nID;
            this.sOutput = sOutput;
        }
    }

    public class dgvEventArgs : EventArgs
    {
        public AccInfo InfoAccount { get; set; }
        public dgvEventArgs(AccInfo newacc)
        {
            this.InfoAccount = newacc;
        }
    }

    public class TTTKQCEventArgs : EventArgs
    {
        public TTTKQCInfo InfoAccount { get; set; }
        public TTTKQCEventArgs(TTTKQCInfo newacc)
        {
            this.InfoAccount = newacc;
        }
    }

    public class BMEventArgs : EventArgs
    {
        public BMInfo Info { get; set; }
        public BMEventArgs(BMInfo info)
        {
            this.Info = info;
        }
    }

    public class TTCDEventArgs : EventArgs
    {
        public TTCDInfo Info { get; set; }
        public TTCDEventArgs(TTCDInfo info)
        {
            this.Info = info;
        }
    }

    public class HoaDonEventArgs : EventArgs
    {
        public HoaDonInfo Info { get; set; }
        public HoaDonEventArgs(HoaDonInfo info)
        {
            this.Info = info;
        }
    }

    public class ViaEventArgs : EventArgs
    {
        public AccInfo Info { get; set; }
        public ViaEventArgs(AccInfo info)
        {
            this.Info = info;
        }
    }


}
