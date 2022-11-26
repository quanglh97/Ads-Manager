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
    public class BMInstance
    {
        public static event EventHandler<BMEventArgs> NewAccountAdded;
        public static ConcurrentQueue<string> dataResult = new ConcurrentQueue<string>();
        public static List<BMInfo> lsBM = new List<BMInfo>();
        public static List<BMInfo> lsViewBM = new List<BMInfo>();
        public static string searchData = "";

        public static void OnNewAccAdded(BMInfo info)
        {
            try
            {
                EventHandler<BMEventArgs> newAccountAdded = BMInstance.NewAccountAdded;
                if (newAccountAdded == null)
                    return;

                BMEventArgs obj = new BMEventArgs(info);

                newAccountAdded((object)null, new BMEventArgs(info));
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
