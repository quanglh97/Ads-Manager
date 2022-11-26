using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsManager.HelperLibrary
{
    public class Common
    {
        public static string key { get; set; }


        public static string parseStringDate(string stringDate)
        {
            var newStringDate = "";
            var month = stringDate.Split('/')[0].Length < 2 ? "0" + stringDate.Split('/')[0] : stringDate.Split('/')[0];
            var day = stringDate.Split('/')[1].Length < 2 ? "0" + stringDate.Split('/')[1] : stringDate.Split('/')[1];
            var year = stringDate.Split('/')[2];
            newStringDate = month + "/" + day + "/" + year;
            return newStringDate;
        }

        public static string parseStringDateToFilter(string fromDate, string toDate)
        {

            //2022-10-01_2022-11-03
            var newStringDate = "";
            var month = fromDate.Split('/')[0].Length < 2 ? "0" + fromDate.Split('/')[0] : fromDate.Split('/')[0];
            var day = fromDate.Split('/')[1].Length < 2 ? "0" + fromDate.Split('/')[1] : fromDate.Split('/')[1];
            var year = fromDate.Split('/')[2];
            newStringDate = year + "-" + month + "-" + day + "_";
            month = toDate.Split('/')[0].Length < 2 ? "0" + toDate.Split('/')[0] : toDate.Split('/')[0];
            day = toDate.Split('/')[1].Length < 2 ? "0" + toDate.Split('/')[1] : toDate.Split('/')[1];
            year = toDate.Split('/')[2];
            newStringDate = newStringDate + year + "-" + month + "-" + day; //
            return newStringDate;
        }

        public static string parseStringDateToFilter2(string date)
        {
            var newStringDate = "";
            var month = date.Split('/')[0].Length < 2 ? "0" + date.Split('/')[0] : date.Split('/')[0];
            var day = date.Split('/')[1].Length < 2 ? "0" + date.Split('/')[1] : date.Split('/')[1];
            var year = date.Split('/')[2];
            newStringDate = newStringDate + year + "-" + month + "-" + day; //
            return newStringDate;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static bool compareDate(string date1, string date2)
        {
            
            var month1 = date1.Split('/')[0].Length < 2 ? "0" + date1.Split('/')[0] : date1.Split('/')[0];
            var day1 = date1.Split('/')[1].Length < 2 ? "0" + date1.Split('/')[1] : date1.Split('/')[1];
            var year1 = date1.Split('/')[2];

            var month2 = date2.Split('/')[0].Length < 2 ? "0" + date2.Split('/')[0] : date2.Split('/')[0];
            var day2 = date2.Split('/')[1].Length < 2 ? "0" + date2.Split('/')[1] : date2.Split('/')[1];
            var year2 = date2.Split('/')[2];

            if(int.Parse(year1) < int.Parse(year2) ||
                int.Parse(year1) == int.Parse(year2) && int.Parse(month1) < int.Parse(month2)||
                int.Parse(year1) == int.Parse(year2) && int.Parse(month1) == int.Parse(month2) && int.Parse(day1) <= int.Parse(day2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
