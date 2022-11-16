using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeCarePage
{
    internal class FileLoader
    {
        public static HashSet<string> LoadStringsFromFile(string filePath)
        {
            HashSet<string> stringSet = new HashSet<string>();
            if (!File.Exists(filePath))
                return stringSet;
            foreach (string readLine in File.ReadLines(filePath))
            {
                try
                {
                    if (readLine != null)
                        stringSet.Add(readLine);
                }
                catch
                {
                }
            }
            return stringSet;
        }
    }
}
