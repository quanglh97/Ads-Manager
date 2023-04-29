using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsManager.HelperLibrary.Facebook.Info
{
    public class TTCDInfo
    {
        public string TrangThai { get; set; }
        public string TenTKQC { get; set; }
        public string IdTKQC { get; set; }
        public string TenChienDich { get; set; }
        public string IdChienDich { get; set; }
        public string TenNhomQC { get; set; }
        public string IdNhomQC { get; set; }
        public string TenQC { get; set; }
        public string IdQC { get; set; }
        public string xemQC { get; set; }
        public string chiPhiQC { get; set; }
        public string cpm { get; set; }
        public string ctr { get; set; }
        public string comment { get; set; }
        public string mess { get; set; }
        public string chuyenDoi { get; set; }
        public string ngayTaoQC { get; set; }
        public string tienTe { get; set; }

        public string MKT { get; set; }
        public string MaMKT { get; set; }
        public string HKD { get; set; }
        public List<string> lsLinkQC { get; set; }

        public TTCDInfo()
        {
            this.lsLinkQC = new List<string>();
            this.chuyenDoi = "0";
            this.comment = "0";
            this.mess = "0";
            this.cpm = "0";
            this.ctr = "0";
        }
    }
}
