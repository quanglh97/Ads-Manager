using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsManager.HelperLibrary.Facebook.Info
{
    public class QCCCInfo
    {
        public string trangThai { get; set; }
        public string idTKQC { get; set; }
        public string tenTKQC { get; set; }
        public string idQC { get; set; }
        public string tenQC { get; set; }
        public string luotNguoiTiepCan { get; set; }
        public string CPM { get; set; }
        public string CTR { get; set; }
        public string chuyenDoi { get; set; }
        public string mess { get; set; }
        public string BinhLuan { get; set; }
        public string chiPhiQC { get; set; }
        public string tienTe { get; set; }
        public string MKT { get; set; }
        public string MaMKT { get; set; }
        public string HKD { get; set; }

        public QCCCInfo() { }
    }

    public class QCCCInfo_Tong
    {
        public string trangThai { get; set; }
        public string idTKQC { get; set; }
        public string tenTKQC { get; set; }
        public double CPM_TB { get; set; }
        public double CTR_TB { get; set; }
        public int chuyenDoi { get; set; }
        public int mess { get; set; }
        public int BinhLuan { get; set; }
        public double chiPhiQC { get; set; }
        public string tienTe { get; set; }
        public string linkQC { get; set; }
        public int numQC { get; set; }

        public string MKT { get; set; }
        public string MaMKT { get; set; }
        public string HKD { get; set; }

        public QCCCInfo_Tong() { }
    }


}
