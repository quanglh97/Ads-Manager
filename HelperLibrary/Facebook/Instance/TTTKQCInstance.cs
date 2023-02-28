using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeCarePage;

namespace AdsManager.HelperLibrary.Facebook
{
    public class TTTKQCInstance
    {
        public static event EventHandler<TTTKQCEventArgs> NewAccountAdded;
        public static ConcurrentQueue<string> dataResult = new ConcurrentQueue<string>();
        public static List<TTTKQCInfo> lsTKQC = new List<TTTKQCInfo>();
        public static List<TTTKQCInfo> lsViewTKQC = new List<TTTKQCInfo>();
        public static int limit = 0;
        public static bool serviceBG = false;
        public static int tongTK = 0;
        public static double tongNguong = 0;
        public static double tongTieu = 0;
        public static double tongSoDu = 0;
        public static IDictionary<string, double> tiGia = new Dictionary<string, double>();

        public static string fromDate = "";
        public static string toDate = "";
        public static bool filterTime = false;
        public static string searchData = "";

        public static void OnNewAccAdded(TTTKQCInfo newacc)
        {
            try
            {
                EventHandler<TTTKQCEventArgs> newAccountAdded = TTTKQCInstance.NewAccountAdded;
                if (newAccountAdded == null)
                    return;

                TTTKQCEventArgs obj = new TTTKQCEventArgs(newacc);

                newAccountAdded((object)null, new TTTKQCEventArgs(newacc));
                // save to file
                dataResult.Enqueue(newacc.allInfo);

            }
            catch
            {
                return;
            }
        }
    }
}
