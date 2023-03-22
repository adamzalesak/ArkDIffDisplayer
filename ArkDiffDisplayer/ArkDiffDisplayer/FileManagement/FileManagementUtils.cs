using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArkDiffDisplayer.FileManagement
{
    public static class FileManagementUtils
    {
        public static readonly string HoldingsCsvUrl = "https://ark-funds.com/wp-content/uploads/funds-etf-csv/ARK_INNOVATION_ETF_ARKK_HOLDINGS.csv";
        public static readonly string HoldingsCsvFileName = "ARK_INNOVATION_ETF_ARKK_HOLDINGS.csv";
        public static readonly string HoldingsCsvFileLocation = AppDomain.CurrentDomain.BaseDirectory;

        public static bool DownloadHoldingsCsv(bool verbose = false)
        {
            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Other");

                try
                {
                    string fileLocationAndName = GetFileLocationAndName();
                    string content = client.GetStringAsync(HoldingsCsvUrl).Result;

                    File.WriteAllText(fileLocationAndName, content);
                    return true;
                }
                catch(Exception ecx)
                {
                    if (verbose)
                        Console.WriteLine(ecx.Message);

                    return false;
                }
            }
        }

        public static string ReadHoldingsCsvFile()
        {
            return File.ReadAllText(GetFileLocationAndName());
        }

        public static List<string> ReadLinesHoldingsCsvFile()
        {
            return File.ReadAllLines(GetFileLocationAndName()).ToList();
        }

        private static string GetFileLocationAndName()
        {
            return Path.Combine(HoldingsCsvFileLocation, HoldingsCsvFileName);
        }
    }
}
