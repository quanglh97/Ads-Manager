using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeCarePage
{
    public static class ProxyInstances
    {
        public static ConcurrentQueue<string> Http = new ConcurrentQueue<string>();
        public static MultiThreading<string> MultiThrs = new MultiThreading<string>();
        public static int type = 1;
        public static long beginTime = 0;
        public static int timeUpdate = -1;
        public static bool userProxy = true;

        public static void Clear<T>(this ConcurrentQueue<T> queue)
        {
            T item;
            while (queue.TryDequeue(out item))
            {
                // do nothing
                //Console.WriteLine("");
            }
        }
    }
}
