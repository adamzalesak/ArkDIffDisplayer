using NUnit.Framework;
using ArkDiffDisplayer.Entities;
using ArkDiffDisplayer.FileManagement;
using ArkDiffDisplayer.Parser;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ArkDiffDisplayer.Tests
{
    public class ParserTests
    {
        private string[]? _sampleLines;
        private IList<string>? _sampleFile;
        private HoldingsData? _holdingsData;
        private ParserUtils _parserUtils;

        [SetUp]
        public void Setup()
        {
            _parserUtils = new ParserUtils();
            _sampleLines = new[] { "03/17/2023,ARKK,\"BLOCK INC\",SQ,852234103,\"6,445,308\",\"$483,978,177.72\",6.22%",
                                   "03/18/2023,ARKK,\"UIPATH INC - CLASS A\",PATH,90364P105,\"27,406,387\",\"$471,937,984.14\",6.06%",
                                   "03/19/2023,ARKK,\"SHOPIFY INC - CLASS A\",SHOP,82509L107,\"9,317,996\",\"$416,234,881.32\",5.35%",
                                   "03/20/2023,ARKK,\"TELADOC HEALTH INC\",TDOC,87918A105,\"12,014,131\",\"$302,996,383.82\",3.89%"};
            _sampleFile = new List<string>() {  "date,fund,company,ticker,cusip,shares,\"market value ($)\",\"weight (%)\"",
                                                "03/17/2023,ARKK,\"BLOCK INC\",SQ,852234103,\"6,445,308\",\"$483,978,177.72\",6.22%",
                                                "03/18/2023,ARKK,\"UIPATH INC - CLASS A\",PATH,90364P105,\"27,406,387\",\"$471,937,984.14\",6.06%",
                                                "The principal risks of investing in ARK ETFs include equity, market, management and non-div..."};

            _holdingsData = new HoldingsData();

            var holdingsDataItem1 = new HoldingsDataItem();
            holdingsDataItem1.Date = DateTime.Parse("03/17/2023", DateTimeFormatInfo.InvariantInfo);
            holdingsDataItem1.Fund = "ARKK";
            holdingsDataItem1.Company = "BLOCK INC";
            holdingsDataItem1.Ticker = "SQ";
            holdingsDataItem1.Cusip = "852234103";
            holdingsDataItem1.Shares = 6445308;
            holdingsDataItem1.MarketValue = 483978177.72m;
            holdingsDataItem1.Weight = 6.22m;

            var holdingsDataItem2 = new HoldingsDataItem();
            holdingsDataItem2.Date = DateTime.Parse("03/18/2023", DateTimeFormatInfo.InvariantInfo);
            holdingsDataItem2.Fund = "ARKK";
            holdingsDataItem2.Company = "UIPATH INC - CLASS A";
            holdingsDataItem2.Ticker = "PATH";
            holdingsDataItem2.Cusip = "90364P105";
            holdingsDataItem2.Shares = 27406387;
            holdingsDataItem2.MarketValue = 471937984.14m;
            holdingsDataItem2.Weight = 6.06m;

            _holdingsData.Data.Add(holdingsDataItem1);
            _holdingsData.Data.Add(holdingsDataItem2);
        }

        [TestCase(new object[] { "BLOCK INC", "UIPATH INC - CLASS A", "SHOPIFY INC - CLASS A", "TELADOC HEALTH INC" }, ParserOptions.Company)]
        [TestCase(new object[] { "ARKK", "ARKK", "ARKK", "ARKK" }, ParserOptions.Fund)]
        [TestCase(new object[] { "SQ", "PATH", "SHOP", "TDOC" }, ParserOptions.Ticker)]
        [TestCase(new object[] { "852234103", "90364P105", "82509L107", "87918A105" }, ParserOptions.Cusip)]
        [TestCase(new object[] { "483,978,177.72", "471,937,984.14", "416,234,881.32", "302,996,383.82" }, ParserOptions.MarketValue)]
        [TestCase(new object[] { "03/17/2023", "03/18/2023", "03/19/2023", "03/20/2023" }, ParserOptions.Date)]
        [TestCase(new object[] { "6.22", "6.06", "5.35", "3.89" }, ParserOptions.Weight)]
        [TestCase(new object[] { "6,445,308", "27,406,387", "9,317,996", "12,014,131" }, ParserOptions.Shares)]
        public void AllFieldsOfSampleDatasetAreParsed(object[] correctValues, ParserOptions option)
        {   
            for (int i = 0; i < _sampleLines.Length; i++)
            {
                var field = _parserUtils.LoadFieldFromLine(_sampleLines[i], option);
                Assert.AreEqual(correctValues[i].ToString(), field);
            }
        }

        [Test]
        public void TrimsUselessLinesFromFile()
        {
            FileManagementUtils.DownloadHoldingsCsv();
            var trimmedLines = _parserUtils.TrimUselessLinesFromFile(FileManagementUtils.ReadLinesHoldingsCsvFile(DateTime.Today));
            Assert.AreEqual(trimmedLines[0].StartsWith("date"), false);
            Assert.AreEqual(trimmedLines[trimmedLines.Count - 1].StartsWith("\"The principal risks"), false);
        }

        [Test]
        public void LinesWereCorrectlyParsed()
        {
            var parsedHoldings = _parserUtils.Parse(_sampleFile);

            for(int i = 0; i < parsedHoldings.Data.Count; i++)
            {
                Assert.AreEqual(_holdingsData.Data[i].Date, parsedHoldings.Data[i].Date);
                Assert.AreEqual(_holdingsData.Data[i].Fund, parsedHoldings.Data[i].Fund);
                Assert.AreEqual(_holdingsData.Data[i].Ticker, parsedHoldings.Data[i].Ticker);
                Assert.AreEqual(_holdingsData.Data[i].Company, parsedHoldings.Data[i].Company);
                Assert.AreEqual(_holdingsData.Data[i].Cusip, parsedHoldings.Data[i].Cusip);
                Assert.AreEqual(_holdingsData.Data[i].Shares, parsedHoldings.Data[i].Shares);
                Assert.AreEqual(_holdingsData.Data[i].MarketValue, parsedHoldings.Data[i].MarketValue);
                Assert.AreEqual(_holdingsData.Data[i].Weight, parsedHoldings.Data[i].Weight);
            }
        }
    }
}