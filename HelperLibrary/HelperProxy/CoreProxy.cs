using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TakeCarePage
{
    internal class CoreProxy
    {

        public static void Init_TMProxy(string key)
        {
            // dont use brute force so set this equal 0: wweb api, 1 is pop3/imap; 2 is webapi/pop3/imap

            if (key != null)
            {

                string new_proxy = ChangeIPHelper.GetIPTMProxy(key);
                if (new_proxy.Contains(":") && new_proxy.Split('.').Length >= 4)
                {
                    ProxyInstances.Http.Enqueue(new_proxy);
                    return;
                }
            }

        }

        public static void Init_TinsoftProxy(string key)
        {
            // dont use brute force so set this equal 0: wweb api, 1 is pop3/imap; 2 is webapi/pop3/imap

            if (key != null)
            {

                string new_proxy = ChangeIPHelper.GetProxyTinsoft(key);

                if (new_proxy.Contains(":") && new_proxy.Split('.').Length >= 4)
                {
                    ProxyInstances.Http.Enqueue(new_proxy);
                    return;
                }
            }
        }

        public static void Init_TinProxy(string key)
        {
            if (key != null)
            {
                string new_proxy = ChangeIPHelper.ChangeTinProxy(key);
                if (new_proxy.Contains(":") && new_proxy.Split('.').Length >= 4)
                {
                    ProxyInstances.Http.Enqueue(new_proxy);
                    return;
                }
            }
        }

        public static void Init_MinProxy(string key)
        {
            if (key != null)
            {
                string new_proxy = ChangeIPHelper.ChangeMinProxy(key);
                if (new_proxy.Contains(":") && new_proxy.Split('.').Length >= 4)
                {
                    ProxyInstances.Http.Enqueue(new_proxy);
                    return;
                }
            }

        }

        public static void InitProxyRotatingV4()
        {

        }

        public static void InitProxyRotatingV6()
        {

        }



    }
}
