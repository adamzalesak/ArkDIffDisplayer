using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ArkDiffDisplayer.Tests")]
namespace ArkDiffDisplayer.FileManagement
{
    public static class FileManagementUtils
    {
        public static readonly string HoldingsCsvUrl = "https://ark-funds.com/wp-content/uploads/funds-etf-csv/ARK_INNOVATION_ETF_ARKK_HOLDINGS.csv";
        public static readonly string HoldingsCsvFileName = "ARK_INNOVATION_ETF_ARKK_HOLDINGS.csv";
        public static readonly string HoldingsCsvFileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "holdings_files");

        public static bool DownloadHoldingsCsv(bool verbose = false)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("User-Agent", "Other");

            try
            {
                CreateCsvHoldingsFilesFolderIfNotExists();

                string fileLocationAndName = GetFileLocationAndName(DateTime.Today);
                string content = client.GetStringAsync(HoldingsCsvUrl).Result;

                File.WriteAllText(fileLocationAndName, content);
                return true;
            }
            catch (Exception ecx)
            {
                if (verbose)
                    Console.WriteLine(ecx.Message);

                return false;
            }
        }

        public static string ReadHoldingsCsvFile(DateTime date)
        {
            string fileLocationAndName = GetFileLocationAndName(date);

            if (!File.Exists(fileLocationAndName))
                throw new FileNotFoundException();
            
            return File.ReadAllText(fileLocationAndName);
        }

        public static List<string> ReadLinesHoldingsCsvFile(DateTime date)
        {
            string fileLocationAndName = GetFileLocationAndName(date);

            if (!File.Exists(fileLocationAndName))
                throw new FileNotFoundException();

            return File.ReadAllLines(fileLocationAndName).ToList();
        }

        internal static string GetFileLocationAndName(DateTime date)
        {
            return Path.Combine(HoldingsCsvFileLocation,
                                AppendDateToFileName(HoldingsCsvFileName, date));
        }


        /// <param name="fileName">May contain the .csv suffix</param>
        internal static string AppendDateToFileName(string fileName, DateTime date)
        {
            int index = fileName.LastIndexOf(".csv");
            string dateAsString = DateToString(date);

            if (index == -1 || index != fileName.Length - 4)
                return fileName + "_" + dateAsString;

            return fileName.Remove(index, 4) + "_" + dateAsString + ".csv";
        }

        /// <param name="holdingsFileName">A valid file name for a csv holdings file.
        /// May contain the .csv suffix</param>
        /// <param name="date">Datetime representing dateString parameter</param>
        /// <returns>Whether it was successful</returns>
        internal static bool ExtractDateFromHoldingsCsvFileName(string holdingsFileName, out DateTime date)
        {
            date = new DateTime();

            if (!holdingsFileName.Contains(Path.GetFileNameWithoutExtension(HoldingsCsvFileName)))
                return false;

            holdingsFileName = holdingsFileName.Remove(0, HoldingsCsvFileName.Length -4 + 1);
            int csvIndex = holdingsFileName.LastIndexOf(".csv");

            if (csvIndex != -1)
                holdingsFileName= holdingsFileName.Remove(csvIndex);

            return DateStringToDateTime(holdingsFileName, out date);
        }

        /// <returns>String representing date portion of date parameter</returns>
        internal static string DateToString(DateTime date)
        {
            return date.Day.ToString() + "." + date.Month.ToString() + "." + date.Year.ToString();
        }

        /// <param name="date">Datetime representing dateString parameter</param>
        /// <returns>Whether it was successful</returns>
        internal static bool DateStringToDateTime(string dateString, out DateTime date)
        {
            date = new DateTime();
            string[] partsString = dateString.Split('.');

            if (partsString.Length != 3)
                return false;

            List<int> partsInt = new();

            foreach (string part in partsString)
            {
                if (!int.TryParse(part, out int partInt))
                    return false;

                partsInt.Add(partInt);
            }

            date = new DateTime(partsInt[2], partsInt[1], partsInt[0]);
            return true;
        }

        private static void CreateCsvHoldingsFilesFolderIfNotExists()
        {
            if (!Directory.Exists(HoldingsCsvFileLocation))
                Directory.CreateDirectory(HoldingsCsvFileLocation);
        }
    }
}
