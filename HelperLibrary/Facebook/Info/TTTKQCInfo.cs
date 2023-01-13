using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeCarePage.HelperLibrary.Facebook;
using TakeCarePage;

namespace AdsManager.HelperLibrary.Facebook
{
    public class TTTKQCInfo
    {
        public string allInfo { get; set; }
        public string trangThai { get; set; }
        public string tenTK { get; set; }
        public string soDu { get; set; }
        public string Nguong { get; set; }
        public string Limit { get; set; }
        public string tongTieu { get; set; }
        public string tienTe { get; set; }
        public string slThe { get; set; }
        public string role { get; set; }
        public string ngayLapHoaDon { get; set; }
        public string ngayTaoTK { get; set; }
        public string loaiTK { get; set; }
        public string ID { get; set; }
        public string duoiThe { get; set; }
        public string nguongConLai { get; set; }
        public string soNgayDenKyTT { get; set; }
        public string liDoDisable { get; set; }
        public string timeZone { get; set; }
        public string taoTuBM { get; set; }
        public string quocGiaTKQC { get; set; }
        public string cookie { get; set; }
        public string token { get; set; }
        public string dtsg { get; set; }
        public string MKT { get; set; }
        public string MaMKT { get; set; }
        public string HKD { get; set; }
        public int soTimeZone { get; set; }
        public int tongHoaDon = 0;
        public double tongTien = 0;
        public double tongTienGoc = 0;



        public TTTKQCInfo()
        {
        }
        public TTTKQCInfo(TTTKQCInfo item)
        {
            this.allInfo = item.allInfo;
            this.trangThai = item.trangThai;
            this.tenTK = item.tenTK;
            this.soDu = item.soDu;
            this.Nguong = item.Nguong;
            this.Limit = item.Limit;
            this.tongTieu = item.tongTieu;
            this.tienTe = item.tienTe;
            this.slThe = item.slThe;
            this.role = item.role;
            this.ngayLapHoaDon = item.ngayLapHoaDon;
            this.ngayTaoTK = item.ngayTaoTK;
            this.loaiTK = item.loaiTK;
            this.ID = item.ID;
            this.duoiThe = item.duoiThe;
            this.nguongConLai = item.nguongConLai;
            this.soNgayDenKyTT = item.soNgayDenKyTT;
            this.liDoDisable = item.liDoDisable;
            this.timeZone = item.timeZone;
            this.taoTuBM = item.taoTuBM;
            this.quocGiaTKQC = item.quocGiaTKQC;
            this.cookie = item.cookie;
            this.token = item.token;
            this.dtsg = item.dtsg;
            this.MKT = item.MKT;
            this.MaMKT = item.MaMKT;
            this.HKD = item.HKD;
            this.soTimeZone = item.soTimeZone;
        }
        //public TTTKQCInfo(string newAccount)
        //{
        //    try
        //    {
        //        this.allInfo = newAccount;

        //        var elementAccount = newAccount.Split('|');
        //        this.trangThai = elementAccount[0];
        //        this.tenTK = elementAccount[1];
        //        this.soDu = elementAccount[2];
        //        this.Nguong = elementAccount[3];
        //        this.Limit = elementAccount[4];
        //        this.tongTieu = elementAccount[5];
        //        this.tienTe = elementAccount[6];
        //        this.slThe = elementAccount[7];
        //        this.role = elementAccount[8];
        //        this.ngayLapHoaDon = elementAccount[9];
        //        this.ngayTaoTK = elementAccount[10];
        //        this.loaiTK = elementAccount[11];
        //        this.ID = elementAccount[12];
        //        this.duoiThe = elementAccount[13];
        //        this.nguongConLai = elementAccount[14];
        //        this.soNgayDenKyTT = elementAccount[15];
        //        this.liDoDisable = elementAccount[16];
        //        this.timeZone = elementAccount[17];
        //        this.taoTuBM = elementAccount[18];
        //        this.quocGiaTKQC = elementAccount[19];
        //    }
        //    catch
        //    {

        //    }
        //}

        //public override bool Equals(object obj) => obj is TTTKQCInfo account && account.tenTK == this.tenTK;
        //public override int GetHashCode() => this.tenTK.GetHashCode();
    }
}
