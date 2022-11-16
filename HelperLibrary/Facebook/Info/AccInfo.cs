using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TakeCarePage.HelperLibrary.Facebook
{
    public class AccInfo
    {
        public int index { get; set; }
        public bool tick { get; set; }
        public string Account { get; set; }
        public string status { get; set; }
        public string cookie { get; set; }
        public string token { get; set; }

        public string userId;
        public string pass;
        public string key2fa;

        public string TenHT;
        public string HKD;

        

        public AccInfo()
        {
        }
        public AccInfo(string newAccount)
        {
            try
            {
                this.Account = newAccount;
                var elementAccount = newAccount.Split('|');
                this.userId = elementAccount[0].Trim();
                this.pass = elementAccount[1].Trim();
                if (elementAccount.Length >= 2)
                    this.key2fa = elementAccount[2].Trim();
                if (elementAccount.Length >= 3)
                {
                    this.cookie = elementAccount[3].Trim();
                    try
                    {
                        this.TenHT = elementAccount[4].Trim();
                    }
                    catch { }
                    try
                    {
                        this.HKD = elementAccount[5].Trim();
                    }
                    catch { }
                    try
                    {
                        var tick = elementAccount[6].Trim();
                        if(tick == "1")
                        {
                            this.tick = true;
                        }
                    }
                    catch { }
                    this.token = getToken(this.cookie);
                    if(this.token != "")
                    {
                        this.status = "Hoạt động";
                    }
                    else
                    {
                        this.status = "Checkpoint";
                    }
                }
                    
            }
            catch
            {

            }
        }

        public override bool Equals(object obj) => obj is AccInfo account && account.userId == this.userId;
        public override int GetHashCode() => this.userId.GetHashCode();

        public static string getToken(string cookie)
        {
            try
            {
                var client = new RestClient("https://ffb.vn/api/tool/cookie2token");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded; charset=UTF-8");
                request.AddHeader("cookie", "_ga=GA1.1.2088243002.1661171484; PHPSESSID=0dbf177456a0012aea0dbca8e63e3f0d; _ga_SV8NY0NNJT=GS1.1.1665237893.5.0.1665237893.0.0.0; __cf_bm=7PEzgO3ECVQGULbe1B97.0aWA4qJEMNt4TZFTmQEmZk-1665237898-0-AabxOYLqNgQ/uDL6ee3selE+2bzSRH02LZlr7Sr9DgO5H8qRT+mVxMSRpt8pQNe8nR/MGdKQImCxxFVdHGacaBRIbtaAyv0TgxlKtss7l8mrtFabJ9nUryhMD8+lEqQOOg==");
                var body = $"cookie={cookie}&type=eaab";
                request.AddParameter("application/x-www-form-urlencoded; charset=UTF-8", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                JObject jObject = JObject.Parse(response.Content);
                var token = jObject["token"].ToString();
                return token;
            }
            catch { return ""; }

        }

        public static string Get_fb_dtsg(string cookie)
        {
            try
            {
                var url = "https://business.facebook.com/";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Headers["cookie"] = cookie;
                httpRequest.Headers["sec-fetch-mode"] = "navigate";
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var temp = result.Split(new string[] { ",{\"token\":\"" }, StringSplitOptions.None);
                        var arDtsg = temp[1].Split(new string[] { "\"}," }, StringSplitOptions.None);

                        return arDtsg[0];
                    }
                }
                return "";
            }
            catch { return ""; }
        }



    }
}
