using ArkDiffDisplayer.DiffCreator;
using ArkDiffDisplayer.Exceptions;
using ArkDiffDisplayer.FileManagement;
using ArkDiffDisplayer.OutputCreator;
using ArkDiffDisplayer.Parser;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System;
using ArkDiffDisplayer.Entities;
using System.Linq;

namespace ArkDiffDisplayer.Tests
{
    public class When
    {
        public DiffDisplayer diffDisplayer { get; }
        public static List<HoldingsDataItem> _holdingsDummy = new List<HoldingsDataItem>
        {
            new() {Company = "ZOOM VIDEO COMMUNICATIONS-A", Ticker = "ZM", Shares = 8524760, Weight = 10.234m},
            new() {Company = "ROKU INC", Ticker = "ROKU", Shares = 9195866, Weight = 6.123m},
            new() {Company = "COINBASE GLOBAL INC -CLASS A", Ticker = "COIN", Shares = 7703761, Weight = 4.2134m},
            new() {Company = "BLOCK INC", Ticker = "SQ", Shares = 6899533, Weight = 5.21341m},
            new() {Company = "EXACT SCIENCES CORP", Ticker = "EXAS", Shares = 7112080, Weight = 3.049530m},
            new() {Company = "UIPATH INC - CLASS A", Ticker = "PATH", Shares = 26641565, Weight = 5.635034m},
            new() {Company = "SHOPIFY INC - CLASS A", Ticker = "SHOP", Shares = 9057929, Weight = 1.2344321m},
            new() {Company = "DRAFTKINGS INC-CL A", Ticker = "DKNG UW", Shares = 15881025, Weight = 2.432104m},
        };

        private IDataManagement _dataManagement;
        private IParser _parser;
        private IDiffCreator _diffCreator;
        private IOutputCreator _outputCreator;

        private IList<string> lines;
        private HoldingsData data;
        private string _outputData = "$\"ZOOM VIDEO COMMUNICATIONS-A, ZM, 8524760 shares, weight 10.23%\"";

        public void SetUp()
        {
            _dataManagement = A.Fake<IDataManagement>();
            _parser = A.Fake<IParser>();
            _diffCreator = A.Fake<IDiffCreator>();
            _outputCreator = A.Fake<IOutputCreator>();
        }

        public When()
        {
            SetUp();
            diffDisplayer = new DiffDisplayer(_dataManagement, _parser, _diffCreator, _outputCreator);
        }


        public When CanDownloadData()
        {
            A.CallTo(() => _dataManagement.DownloadData()).Returns(true);

            return this;
        }

        public When CanNotDownloadData()
        {
            A.CallTo(() => _dataManagement.DownloadData()).Returns(false);

            return this;
        }

        public When FetchData(IList<string> lines)
        {
            this.lines = lines;
            A.CallTo(() => _dataManagement.FetchData(A<DateTime>._)).Returns(lines);

            return this;
        }

        public When CanNotFetchData(IList<string> lines)
        {
            A.CallTo(() => _dataManagement.FetchData(A<DateTime>._)).Throws<FileNotFoundException>();

            return this;
        }

        public When CanParseData(HoldingsData data)
        {
            this.data = data;
            A.CallTo(() => _parser.Parse(A<IList<string>>._)).Returns(data);

            return this;
        }

        public When ParserCanNotParse()
        {
            A.CallTo(() => _parser.Parse(lines)).Throws<ParserException>();

            return this;
        }

        public When CanCreateDataDiff()
        {
            A.CallTo(() => _diffCreator.CreateDataDiff(A<HoldingsData>._, A<HoldingsData>._))
                .Returns(new DataDiff { NewPositions = new List<DataDiffItem>(_holdingsDummy.Take(3).Select(x => new DataDiffItem(x)) )});

            return this;
        }

        public When CanOutputData()
        {
            A.CallTo(() => _outputCreator.CreateOutput(A<DataDiff>._)).Returns(_outputData);

            return this;
        }

        public void AssertTrue()
        {
            Assert.IsTrue(diffDisplayer.GetDiff(DateTime.Now).Equals(_outputData));
        }

        public void AssertThrows<T>() where T : Exception
        {
            Assert.Throws<T>(() => diffDisplayer.GetDiff(DateTime.Now));
        }
    }
}