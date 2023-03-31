using NUnit.Framework;
using ArkDiffDisplayer.FileManagement;
using System;
using System.IO;

namespace ArkDiffDisplayer.Tests
{
    public class FileManagementTests
    {
        [Test]
        public void TestCsvFileDownloaded()
        {
            FileManagementUtils.DownloadHoldingsCsv();
            FileAssert.Exists(FileManagementUtils.GetFileLocationAndName(DateTime.Today));
        }

        [Test]
        public void TestCsvReadFile()
        {
            FileManagementUtils.DownloadHoldingsCsv();
            Assert.DoesNotThrow(() => FileManagementUtils.ReadLinesHoldingsCsvFile(DateTime.Today));
            var LinesList = FileManagementUtils.ReadLinesHoldingsCsvFile(DateTime.Today);
            Assert.AreEqual(30, LinesList.Count);
            Assert.AreEqual("date,fund,company,ticker,cusip,shares,\"market value ($)\",\"weight (%)\"", LinesList[0]);
        }


        [TestCase("holdings.csv", 10, 2, 2023, "holdings_10.2.2023.csv")]
        [TestCase("holdings", 5, 8, 1998, "holdings_5.8.1998")]
        [TestCase(".csv", 7, 6, 2020, "_7.6.2020.csv")]
        [TestCase("somefile", 2, 2, 2222, "somefile_2.2.2222")]
        public void TestAppendDateToFileName(string fileName, int day, int month, int year, string correct)
        {
            string appended = FileManagementUtils.AppendDateToFileName(fileName, new DateTime(year, month, day));
            
            Assert.AreEqual(appended, correct);
        }

        [TestCase("ARK_INNOVATION_ETF_ARKK_HOLDINGS_10.2.2023.csv", 10, 2, 2023, true)]
        [TestCase("ARK_INNOVATION_ETF_ARKK_HOLDINGS_5.8.1998", 5, 8, 1998, true)]
        [TestCase("ARK_INNOVATION_ETF_ARKK_HOLDINGS_2.2.2222.csv", 2, 2, 2222, true)]
        [TestCase("7.6.2020.csv", 7, 6, 2020, false)]
        [TestCase("ARK_INNOVATION_ETF_ARKK_HOLDINGS_2.2222.csv", 2, 2, 2222, false)]
        [TestCase("ARK_INNOVATION_ETF_ARKK_HOLDINGS_2.2.csv", 2, 2, 2222, false)]
        public void TestExtractDateFromFileName(string fileName, int day, int month, int year, bool isCorrect)
        {
            bool success = FileManagementUtils.ExtractDateFromHoldingsCsvFileName(fileName, out DateTime date);

            Assert.AreEqual(success, isCorrect);

            if (success)
            {
                Assert.AreEqual(date.Day, day);
                Assert.AreEqual(date.Month, month);
                Assert.AreEqual(date.Year, year);
            }
        }
    }
}
