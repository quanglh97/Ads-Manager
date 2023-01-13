using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsManager.HelperLibrary.Facebook.Info
{
    public class MKTInfo
    {
        public string IdTKQC { get; set; }
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
                if (elementInfo.Length == 4)
                {
                    this.IdTKQC = elementInfo[0].Trim();
                    this.MaMKT = elementInfo[1].Trim();
                    this.MKT = elementInfo[2].Trim();
                    this.HKD = elementInfo[3].Trim();
                }
                else if (elementInfo.Length == 3)
                {
                    this.IdTKQC = elementInfo[0].Trim();
                    this.MaMKT = elementInfo[1].Trim();
                    this.MKT = elementInfo[2].Trim();
                    this.HKD = "";
                }
                else if (elementInfo.Length == 2)
                {
                    this.IdTKQC = elementInfo[0].Trim();
                    this.MaMKT = elementInfo[1].Trim();
                    this.MKT = "";

                }
            }
            catch
            {

            }
        }
    }
}
