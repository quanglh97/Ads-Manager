using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdsManager.HelperLibrary
{
    class Logger
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets, DriveService.Scope.Drive };
        private const string ApplicationName = "Current Legislators";
        private const string SpreadsheetId = "1olxRxdU3asjcrEM7uRa6hhiMDlGk6Vl3RBUNA4NC_dc";
        private const string DriverId = "18aNxkjQVwRfFwuUepA3kTt2xyMc2RgyE";
        private const string Sheet = "keyADS";
        private static SheetsService sheet_service = null;
        private static DriveService drive_service = null;

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public static async void Init()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var assembly = Assembly.GetExecutingAssembly();
            GoogleCredential credential;

            var ressourceList = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (var stream = assembly.GetManifestResourceStream("AdsManager.resources.black-display-204513-1c02239a9b12.json"))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }

            // Create Google Sheets API service.
            sheet_service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            drive_service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        // return file ID
        public static async Task<List<string>> SearchFile(string sKeyWord = "")
        {
            if (drive_service == null)
            {
                Init();
            }

            List<string> lsFile = new List<string>();
            var request = drive_service.Files.List();
            request.PageSize = 1000;
            request.Q = "parents in '18aNxkjQVwRfFwuUepA3kTt2xyMc2RgyE'";
            var resultFile = await request.ExecuteAsync();

            if (sKeyWord != null && sKeyWord != "")
            {
                foreach (var files in resultFile.Files)
                {
                    if (files.Name.ToLower().Contains(sKeyWord.ToLower()))
                    {
                        lsFile.Add(files.Id);
                    }
                }
            }
            else
            {
                foreach (var files in resultFile.Files)
                {
                    lsFile.Add(files.Id);
                }
            }

            return lsFile;
        }

        public static MemoryStream DriveDownloadFile(string fileId)
        {
            try
            {
                var request = drive_service.Files.Get(fileId);
                var stream = new MemoryStream();

                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Console.WriteLine(progress.BytesDownloaded);
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Console.WriteLine("Download complete.");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Console.WriteLine("Download failed.");
                                    break;
                                }
                        }
                    };
                request.Download(stream);

                return stream;
            }
            catch (Exception e)
            {
                // TODO(developer) - handle error appropriately
                if (e is AggregateException)
                {
                    Console.WriteLine("Credential Not found");
                }
                else
                {
                    throw;
                }
            }
            return null;
        }

        public static Dictionary<string, string> ReadEntries()
        {
            try
            {
                Dictionary<string, string> dicKey = null;

                if (sheet_service == null)
                {
                    Init();
                }

                var range = $"{Sheet}!A:F";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        sheet_service.Spreadsheets.Values.Get(SpreadsheetId, range);

                var response = request.Execute();
                IList<IList<object>> values = response.Values;
                if (values != null && values.Count > 0)
                {
                    foreach (var row in values)
                    {

                        if (row.Count >= 2)
                        {
                            if (dicKey == null)
                            {
                                dicKey = new Dictionary<string, string>();
                            }

                            dicKey[row[0].ToString()] = row[1].ToString();

                        }
                        else if (row.Count == 1) // have only key
                        {
                            if (dicKey == null)
                            {
                                dicKey = new Dictionary<string, string>();
                            }

                            dicKey[row[0].ToString()] = "";
                        }
                    }
                }
                else
                {
                    //Console.WriteLine("No data found.");
                }

                return dicKey;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("string " + ex.ToString());
            }

            return null;
        }

        // ghi sheet
        public static void CreateEntry(string sValue)
        {
#pragma warning disable CS0168 // The variable 'ex' is declared but never used

            if (sheet_service == null)
            {
                Init();
            }

            try
            {
                var range = $"{Sheet}!A:F";
                var valueRange = new ValueRange();
                var oblist = new List<object>() { sValue };
                valueRange.Values = new List<IList<object>> { oblist };

                var appendRequest = sheet_service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
                appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                appendRequest.Execute();
            }
            catch (Exception ex)
            {

            }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used

        }
    }
}
