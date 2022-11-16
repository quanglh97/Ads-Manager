using AdsManager.HelperLibrary.Facebook;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeCarePage.HelperLibrary.Facebook;

namespace TakeCarePage
{
    internal class ViaInstances
    {
        public static bool CookieSP = false;
        public static bool getToken = false;

        public static int maxThread = 100;
        public static bool fakeUserAgent = false;

        public static ConcurrentQueue<string> queueFakeUserAgent = new ConcurrentQueue<string>();

        public static List<AccInfo> lsAccVia = new List<AccInfo>();

        public static MultiThreading<AccInfo> MultiThrsAccVia = new MultiThreading<AccInfo>();

        //public static string sFileExportSuccess = AppDomain.CurrentDomain.BaseDirectory + "Result\\" + "\\SAVE_OUT_PUT_SUCCESS.txt";
        //public static string sFileExportError = AppDomain.CurrentDomain.BaseDirectory + "Result\\" + "\\SAVE_OUT_PUT_ERROR.txt";
        public static FileSaver FS = new FileSaver();

        public static event EventHandler<ViaEventArgs> NewAccountAdded;

        public static int ThrsCount = 1;

        public static int delayLevel = 1;

        public static int modeRun = 0;

        public static void ImportVia()
        {
            var dataFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
            var fileVia = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "viaAcc.txt");
            if (!File.Exists(fileVia))
            {
                return;
            }
            try
            {
                string[] data = File.ReadAllText(fileVia).Trim().Split('\n');

                ViaInstances.lsAccVia.Clear();
                for (int i = 0; i < data.Length; i++)
                {
                    AccInfo acc = new AccInfo(data[i]);
                    ViaInstances.lsAccVia.Add(acc);
                    try
                    {
                        //OnNewAccAdded(acc);
                    }
                    catch
                    {

                    }
                }

                ViaInstances.MultiThrsAccVia.queueAccs = new ConcurrentQueue<AccInfo>(ViaInstances.lsAccVia);
            }
            catch(Exception ex)
            {
                ViaInstances.lsAccVia.Clear();
            }
            finally
            {

            }

        }

        public static void OnNewAccAdded(AccInfo newacc)
        {
            try
            {
                EventHandler<ViaEventArgs> newAccountAdded = ViaInstances.NewAccountAdded;
                if (newAccountAdded == null)
                    return;

                ViaEventArgs obj = new ViaEventArgs(newacc);

                newAccountAdded((object)null, new ViaEventArgs(newacc));
                // save to file
            }
            catch(Exception ex)
            {
                return;
            }
        }


    }
}
