using Google.Apis.Drive.v3.Data;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OtpNet;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
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
                    this.token = elementAccount[4].Trim();
                    try
                    {
                        this.TenHT = elementAccount[5].Trim();
                    }
                    catch { }
                    try
                    {
                        this.HKD = elementAccount[6].Trim();
                    }
                    catch { }
                    try
                    {
                        var tick = elementAccount[7].Trim();
                        if(tick == "1")
                        {
                            this.tick = true;
                        }
                    }
                    catch { }

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
        public static ChromeDriver buildDriver(string userName, string pass, string key2fa)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            //options.AddArgument("--window-position=-32000,-32000");
            options.AddArgument("ignore-certificate-errors");
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--disable-web-security");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--start-maximized");
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            ChromeDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("https://mbasic.facebook.com/");
            Thread.Sleep(2000);
            var userInput = driver.FindElement(By.Id("m_login_email"));
            var passInput = driver.FindElement(By.Name("pass"));
            var loginBtn = driver.FindElement(By.Name("login"));

            userInput.SendKeys(userName);
            passInput.SendKeys(pass);

            Thread.Sleep(1 * 1000);
            loginBtn.Click();
            Thread.Sleep(2 * 1000);

            if (driver.Url.Contains("/login/?") || !driver.Url.Contains(".com/"))
            {
                driver.Close();
                driver.Quit();
                return null;
            }
            int checkpointCount = 0;
            if (driver.Url.Contains("mbasic.facebook.com/checkpoint/?_rdr"))
            {
                if (key2fa != null)
                {
                    var ele2faInput = driver.FindElement(By.Id("approvals_code"));//approvals_code
                    var secretKey = Base32Encoding.ToBytes(key2fa);
                    var totp = new Totp(secretKey);
                    var otp = totp.ComputeTotp();
                    ele2faInput.SendKeys(otp);
                    Thread.Sleep(1 * 1000);
                    var continueBtn = driver.FindElement(By.Id("checkpointSubmitButton-actual-button"));//checkpointSubmitButton
                    continueBtn.Submit();
                    Thread.Sleep(1 * 1000);

                    while (driver.Url.Contains("mbasic.facebook.com/login/checkpoint/"))
                    {
                        continueBtn = driver.FindElement(By.Id("checkpointSubmitButton-actual-button"));
                        continueBtn.Submit();
                        Thread.Sleep(1 * 1000);
                        if (checkpointCount > 2)
                        {
                            break;
                        }
                    }
                }
            }
            return driver;
        }

        public static string getCookie(IWebDriver driver)
        {
            try
            {
                var cookies = "";
                // Get All available cookies
                var lsCookies = driver.Manage().Cookies.AllCookies;
                foreach (var cookie in lsCookies)
                {
                    cookies += cookie.ToString().Split(';')[0] + ";";
                }
                return cookies;
            }
            catch
            {
                return null;
            }
        }
        public static string getToken(IWebDriver driver, string key2fa)
        {
            try
            {
                //token EAAB
                var accessToken = "";
                driver.Navigate().GoToUrl(@"https://business.facebook.com/content_management");
                Thread.Sleep(3 * 1000);
                if (driver.Url.Contains("security/twofactor"))
                {
                    var ele2faInput = driver.FindElement(By.XPath("//input[@placeholder='Enter code']"));//placeholder="Enter code"
                    var secretKey = Base32Encoding.ToBytes(key2fa);
                    var totp = new Totp(secretKey);
                    var otp = totp.ComputeTotp();
                    ele2faInput.SendKeys(otp);
                    Thread.Sleep(1 * 1000);
                    driver.FindElements(By.XPath("//div[@class='x3nfvp2 x193iq5w xxymvpz']"))[1].Click();
                    Thread.Sleep(2 * 1000);
                }
               
                Thread.Sleep(3 * 1000);
                var pageSource = driver.PageSource;
                if (pageSource.Contains("[{\"accessToken\":\"EAAG"))
                {
                    var beginToken = pageSource.IndexOf("[{\"accessToken\":\"EAAG");
                    pageSource = pageSource.Substring(beginToken);
                    beginToken = pageSource.IndexOf(":\"");
                    pageSource = pageSource.Substring(beginToken + 2);
                    var endToken = pageSource.IndexOf('"');

                    accessToken += pageSource.Substring(0, endToken);
                }
                return accessToken;
            }
            catch
            {
                return null;
            }
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
