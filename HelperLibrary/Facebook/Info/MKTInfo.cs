using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsManager.HelperLibrary.Facebook.Info
{
    public class MKTInfo
    {
        public string MaMKT { get; set; }
        public string MKT { get; set; }
        public string HKD { get; set; }

        public MKTInfo()
        { }
        public MKTInfo(string newInfo) //test commit
        {
            try
            {
                var elementInfo = newInfo.Split('|');
                this.MaMKT = elementInfo[0].Trim();
                this.MKT = elementInfo[1].Trim();
                if (elementInfo.Length > 2)
                    this.HKD = elementInfo[2].Trim();
            }
            catch
            {

            }
        }
    }
}
