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
    public class DataPageInstance
    {
        public static event EventHandler<DataPageEventArgs> NewAccountAdded;
        public static ConcurrentQueue<string> dataResult = new ConcurrentQueue<string>();
        public static List<DataPageInfo> lsDataPage = new List<DataPageInfo>();
        public static List<DataPageInfo> lsViewDataPage = new List<DataPageInfo>();
        public static string searchData = "";
        public static string fromDate = "";
        public static string toDate = "";

        public static void OnNewAccAdded(DataPageInfo info)
        {
            try
            {
                EventHandler<DataPageEventArgs> newAccountAdded = DataPageInstance.NewAccountAdded;
                if (newAccountAdded == null)
                    return;

                DataPageEventArgs obj = new DataPageEventArgs(info);

                newAccountAdded((object)null, new DataPageEventArgs(info));
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
