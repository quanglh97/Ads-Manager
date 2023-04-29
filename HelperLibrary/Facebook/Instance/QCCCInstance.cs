using AdsManager.HelperLibrary.Facebook.Info;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeCarePage;

namespace AdsManager.HelperLibrary.Facebook.Instance
{
    public class QCCCInstance
    {
        public static event EventHandler<QCCCEventArgs> NewAccountAdded;
        public static ConcurrentQueue<string> dataResult = new ConcurrentQueue<string>();
        public static List<QCCCInfo> lsQCCC = new List<QCCCInfo>();
        public static List<QCCCInfo_Tong> lsQCCC_tong = new List<QCCCInfo_Tong>();
        public static List<QCCCInfo_Tong> lsViewQCCC_tong = new List<QCCCInfo_Tong>();
        public static string businessId = "";

        public static string searchData = "";
        public static string fromDate = "";
        public static string toDate = "";

        public static void OnNewAccAdded(QCCCInfo_Tong info)
        {
            try
            {
                EventHandler<QCCCEventArgs> newAccountAdded = QCCCInstance.NewAccountAdded;
                if (newAccountAdded == null)
                    return;

                QCCCEventArgs obj = new QCCCEventArgs(info);

                newAccountAdded((object)null, new QCCCEventArgs(info));
                // save to file
                //dataResult.Enqueue(info.allInfo);

            }
            catch
            {
                return;
            }
        }

        public static void getBusinessId(string token, string stringCookie)
        {
            var url = $"https://graph.facebook.com/v14.0/me/businesses?fields=business_users{{id,name,role}}&access_token={token}";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var cookies = stringCookie.Split(';');
            foreach (string cookie in cookies)
            {
                if (cookie == "") break;
                var datacookie = cookie.Trim().Split('=');
                request.AddCookie(datacookie[0].Trim(), datacookie[1].Trim());
            }
            request.AddHeader("sec-fetch-mode", "navigate");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36";
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject objectBusiness = JObject.Parse(response.Content);
                businessId = objectBusiness["data"][0]["id"].ToString();
            }

        }
    }
}
