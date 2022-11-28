using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TakeCarePage
{
    public class ChangeIPHelper
    {
        public static string GetIPTMProxy(string apikey)
        {
            try
            {
                string sign = CreateMD5(apikey);

                var client = new RestClient("https://tmproxy.com/api/proxy/get-new-proxy");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                var body = @"{""api_key"":""" + apikey + @""",""sign"":""" + sign + @"""}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                var jsonResult = (JObject)JsonConvert.DeserializeObject(response.Content);
                string new_proxy = jsonResult["data"]["https"].ToString();

                if (new_proxy.Equals(""))
                    return GetCurrentIPTMProxy(apikey);
                else
                    return new_proxy;

            }
            catch { }

            return "";
        }

        public static string GetCurrentIPTMProxy(string apikey)
        {
            try
            {
                var client = new RestClient("https://tmproxy.com/api/proxy/get-current-proxy");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("Content-Type", "application/json");

                var body = @"{""api_key"":""" + apikey + @"""}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                var jsonResult = (JObject)JsonConvert.DeserializeObject(response.Content);
                string new_proxy = jsonResult["data"]["https"].ToString();

                if (new_proxy.Equals(""))
                    return jsonResult["message"].ToString();
                else
                    return new_proxy;

            }
            catch { }

            return "";
        }

        public static string GetIPMinProxy(string apikey)
        {
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            try
            {
                var client = new RestClient("https://dash.minproxy.vn/api/rotating/v1/proxy/get-new-proxy?api_key=" + apikey);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                JObject jObject = JObject.Parse(response.Content);

                if (jObject["code"] == null || !jObject["code"].ToString().Contains("2"))
                {
                    return jObject.ToString();
                }


                return jObject["data"]["http_proxy"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Get Proxy Tinsoft error: " + ex.Message, "Error");
            }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used

            return "";
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string GetProxyTinsoft(string tinsoftKey)
        {
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            try
            {
                var client = new RestClient("http://proxy.tinsoftsv.com/api/changeProxy.php?key=" + tinsoftKey); //TL32ysqjlmjBXSj9cB3DO1DF66QYQkDhY6NyLM
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                JObject jObject = JObject.Parse(response.Content);

                if (jObject["success"].ToString().Contains("alse"))
                {
                    return GetCurrentProxyTinsoft(tinsoftKey);
                }


                return jObject["proxy"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Get Proxy Tinsoft error: " + ex.Message, "Error");
            }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used

            return "";
        }


        public static string GetCurrentProxyTinsoft(string tinsoftKey)
        {
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            try
            {
                var client = new RestClient("http://proxy.tinsoftsv.com/api/getProxy.php?key=" + tinsoftKey);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                JObject jObject = JObject.Parse(response.Content);

                if (jObject["success"].ToString().Contains("alse"))
                {
                    return jObject.ToString();
                }


                return jObject["proxy"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Get Proxy Tinsoft error: " + ex.Message, "Error");
            }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used

            return "";
        }


        public static void ChangeIPHMA()
        {
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            try
            {
                // [1] Turn Off HMA
                System.Diagnostics.Process pProcess_turnOff = new System.Diagnostics.Process();
                pProcess_turnOff.StartInfo.FileName = "C:\\Windows\\System32\\netsh";
                pProcess_turnOff.StartInfo.Arguments = "interface set interface \"HMA! Pro VPN OpenVPN\" disable";
                pProcess_turnOff.StartInfo.UseShellExecute = false;
                pProcess_turnOff.StartInfo.CreateNoWindow = true;
                pProcess_turnOff.StartInfo.RedirectStandardOutput = true;
                pProcess_turnOff.Start();

                string strOutput_turn_on = pProcess_turnOff.StandardOutput.ReadToEnd();
                pProcess_turnOff.WaitForExit();
                Thread.Sleep(TimeSpan.FromSeconds(18));

                // [2] Turn On HMA
                System.Diagnostics.Process pProcess_turnOn = new System.Diagnostics.Process();
                pProcess_turnOn.StartInfo.FileName = "C:\\Windows\\System32\\netsh";
                pProcess_turnOn.StartInfo.Arguments = "interface set interface \"HMA! Pro VPN OpenVPN\" enable";
                pProcess_turnOn.StartInfo.UseShellExecute = false;
                pProcess_turnOn.StartInfo.CreateNoWindow = true;
                pProcess_turnOn.StartInfo.RedirectStandardOutput = true;
                pProcess_turnOn.Start();

                string strOutput_turnOn = pProcess_turnOn.StandardOutput.ReadToEnd();
                pProcess_turnOn.WaitForExit();
                Thread.Sleep(TimeSpan.FromSeconds(20));
            }
            catch (Exception ex) { }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used

        }

        public static void ChangeIPXProxy(string proxy)
        {
            try
            {
                string xproxy = File.ReadAllText(Directory.GetCurrentDirectory() + "\\xproxy").Trim();
                var client = new RestClient(xproxy + proxy.Trim());
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Cache-Control", "max-age=0");
                request.AddHeader("DNT", "1");
                request.AddHeader("Upgrade-Insecure-Requests", "1");
                //client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36";
                request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.AddHeader("Accept-Language", "en,en-US;q=0.9,vi;q=0.8");
                request.AddHeader("Cookie", "Admin-Token=admin-token; sidebarStatus=0");
                IRestResponse response = client.Execute(request);
            }
            catch { }

        }


        public static string getCurrentTinProxy(string key)
        {
            try
            {
                var client = new RestClient("https://api.tinproxy.com/proxy/get-current-proxy?api_key=" + key + "&authen_ips=AUTHEN_IPS");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                JObject jObject = JObject.Parse(response.Content);
                if (jObject["status"].ToString().Contains("active"))
                {
                    return jObject["data"]["http_ipv4"].ToString();
                }
            }
            catch
            {

            }
            return "";
        }

        public static string ChangeTinProxy(string key)
        {
            try
            {
                var client = new RestClient("https://api.tinproxy.com/proxy/get-new-proxy?api_key=" + key + "&location=vn_hcm");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                JObject jObject = JObject.Parse(response.Content);
                if (jObject["status"].ToString().Contains("active"))
                {
                    return jObject["data"]["http_ipv4"].ToString();
                }
                else
                {
                    return getCurrentTinProxy(key);
                }
            }
            catch
            {

            }
            return "";
        }


        public static string GetCurrentMinProxy(string key)
        {
            try
            {
                var url = "https://dash.minproxy.vn/api/rotating/v1/proxy/get-new-proxy?api_key=" + key;
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("accept", "*/*");
                request.AddHeader("X-CSRF-TOKEN", "");
                IRestResponse response = client.Execute(request);
                JObject jObject = JObject.Parse(response.Content);

                return jObject["data"]["http_proxy"].ToString();

            }
            catch
            {

            }
            return "";
        }

        public static string ChangeMinProxy(string key)
        {
            try
            {
                var url = "https://dash.minproxy.vn/api/rotating/v1/proxy/get-new-proxy?api_key=" + key;
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("accept", "*/*");
                request.AddHeader("X-CSRF-TOKEN", "");
                IRestResponse response = client.Execute(request);
                JObject jObject = JObject.Parse(response.Content);

                return jObject["data"]["http_proxy"].ToString() + ':' + jObject["data"]["username"].ToString() + ':' + jObject["data"]["password"].ToString();

            }
            catch
            {

            }
            return "";
        }
    }
}
