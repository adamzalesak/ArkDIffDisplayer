using NUnit.Framework;
using ArkDiffDisplayer.Parser;
using ArkDiffDisplayer.FileManagement;
using System;

namespace ArkDiffDisplayer.Tests
{
    public class FileManagementTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        
        public void TestCsvFileDownloaded()
        {
            FileManagementUtils.DownloadHoldingsCsv();
            Assert.DoesNotThrow(() => FileManagementUtils.ReadHoldingsCsvFile(DateTime.Today));
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
