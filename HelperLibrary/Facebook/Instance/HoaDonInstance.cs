using AdsManager.HelperLibrary.Facebook.Info;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeCarePage;

namespace AdsManager.HelperLibrary.Facebook.Instance
{
    public class HoaDonInstance
    {
        public static event EventHandler<HoaDonEventArgs> NewAccountAdded;
        public static List<HoaDonInfo> lsHoaDon = new List<HoaDonInfo>();
        public static List<HoaDonInfo> lsViewHoaDon = new List<HoaDonInfo>();

        public static int tongHoaDon = 0;
        public static double tongTien = 0;
        public static string searchData = "";



        public static void OnNewAccAdded(HoaDonInfo info)
        {
            try
            {
                EventHandler<HoaDonEventArgs> newAccountAdded = HoaDonInstance.NewAccountAdded;
                if (newAccountAdded == null)
                    return;

                HoaDonEventArgs obj = new HoaDonEventArgs(info);

                newAccountAdded((object)null, new HoaDonEventArgs(info));
                // save to file
                //dataResult.Enqueue(info.allInfo);

            }
            catch
            {
                return;
            }
        }
    }
}
