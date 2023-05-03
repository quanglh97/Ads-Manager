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
    public class TTCDInstance
    {
        public static event EventHandler<TTCDEventArgs> NewAccountAdded;
        public static ConcurrentQueue<string> dataResult = new ConcurrentQueue<string>();
        public static List<TTCDInfo> lsTTCD = new List<TTCDInfo>();
        public static List<TTCDInfo> lsViewTTCD = new List<TTCDInfo>();
        public static List<string> lsError = new List<string>();
        public static string fromDate = "";
        public static string toDate = "";
        public static string searchData = "";

        public static string businessId = "";
        public static string fileErrorLogTTCD = AppDomain.CurrentDomain.BaseDirectory + "\\data" + "\\fileErrorLogTTCD.txt";

        public static void OnNewAccAdded(TTCDInfo info)
        {
            try
            {
                EventHandler<TTCDEventArgs> newAccountAdded = TTCDInstance.NewAccountAdded;
                if (newAccountAdded == null)
                    return;

                TTCDEventArgs obj = new TTCDEventArgs(info);

                newAccountAdded((object)null, new TTCDEventArgs(info));
                // save to file
                //dataResult.Enqueue(info.allInfo);

            }
            catch
            {
                return; 
            }
        }


        public static string GetDataChienDich(string accId, string stringCookie, string date, string bussinessId)
        {
            var url = "";
            if(bussinessId == "")
            {
                url = $"https://business.facebook.com/adsmanager/manage/ads?act={accId}&&business_id={TTCDInstance.businessId}&columns=name%2Cdelivery%2Ccampaign_name%2Cbid%2Cbudget%2Clast_significant_edit%2Cattribution_setting%2Cresults%2Creach%2Cimpressions%2Ccost_per_result%2Cquality_score_organic%2Cquality_score_ectr%2Cquality_score_ecvr%2Cspend%2Cend_time%2Cschedule%2Cadgroup_id%2Ccampaign_group_name%2Ccampaign_group_id%2Ccampaign_id&attribution_windows=default&date={date}&insights_date={date}&nav_source=no_referrer&filter_set=ADGROUP_DELIVERY_STATUS-STRING_SET%1EIN%1E[%22active%22%2C%22deleted%22%2C%22error%22%2C%22inactive%22%2C%22off%22%2C%22pending%22]";
            }
            else
            {
                url = $"https://business.facebook.com/adsmanager/manage/ads?act={accId}&business_id={bussinessId}&columns=name%2Cdelivery%2Ccampaign_name%2Cbid%2Cbudget%2Clast_significant_edit%2Cattribution_setting%2Cresults%2Creach%2Cimpressions%2Ccost_per_result%2Cquality_score_organic%2Cquality_score_ectr%2Cquality_score_ecvr%2Cspend%2Cend_time%2Cschedule%2Cadgroup_id%2Ccampaign_group_name%2Ccampaign_group_id%2Ccampaign_id&attribution_windows=default&date={date}&insights_date={date}&nav_source=no_referrer&filter_set=ADGROUP_DELIVERY_STATUS-STRING_SET%1EIN%1E[%22active%22%2C%22deleted%22%2C%22error%22%2C%22inactive%22%2C%22off%22%2C%22pending%22]";
            }
            //url = "https://business.facebook.com/adsmanager/manage/ads?act=884788985417782&business_id=542557230234808&columns=name%2Cdelivery%2Ccampaign_name%2Cbid%2Cbudget%2Clast_significant_edit%2Cattribution_setting%2Cresults%2Creach%2Cimpressions%2Ccost_per_result%2Cquality_score_organic%2Cquality_score_ectr%2Cquality_score_ecvr%2Cspend%2Cend_time%2Cschedule%2Cadgroup_id%2Ccampaign_group_name%2Ccampaign_group_id%2Ccampaign_id&attribution_windows=default&date=2022-11-03_2022-11-04&insights_date=2022-11-03_2022-11-04";
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
            request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.AddHeader("sec-fetch-mode", "navigate");

            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36";
            IRestResponse response = client.Execute(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content;
            }
            return "";
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
            if( response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject objectBusiness = JObject.Parse(response.Content);
                businessId = objectBusiness["data"][0]["id"].ToString();
            }
            
        }
    }
}
