using NUnit.Framework;
using ArkDiffDisplayer.Parser;
using ArkDiffDisplayer.FileManagement;
using System;

namespace ArkDiffDisplayer.Tests
{
    public class ParserTests
    {
        private string[]? _sampleLines;

        [SetUp]
        public void Setup()
        {
            _sampleLines = new[] { "03/17/2023,ARKK,\"BLOCK INC\",SQ,852234103,\"6,445,308\",\"$483,978,177.72\",6.22%",
                                   "03/18/2023,ARKK,\"UIPATH INC - CLASS A\",PATH,90364P105,\"27,406,387\",\"$471,937,984.14\",6.06%",
                                   "03/19/2023,ARKK,\"SHOPIFY INC - CLASS A\",SHOP,82509L107,\"9,317,996\",\"$416,234,881.32\",5.35%",
                                   "03/20/2023,ARKK,\"TELADOC HEALTH INC\",TDOC,87918A105,\"12,014,131\",\"$302,996,383.82\",3.89%"};
        }

        [TestCase(new object[] { "BLOCK INC", "UIPATH INC - CLASS A", "SHOPIFY INC - CLASS A", "TELADOC HEALTH INC" }, ParserOptions.Name)]
        [TestCase(new object[] { "03/17/2023", "03/18/2023", "03/19/2023", "03/20/2023" }, ParserOptions.Date)]
        [TestCase(new object[] { "6.22%", "6.06%", "5.35%", "3.89%" }, ParserOptions.Weight)]
        [TestCase(new object[] { "6,445,308", "27,406,387", "9,317,996", "12,014,131" }, ParserOptions.Shares)]
        public void AllFieldsOfSampleDatasetAreParsed(object[] correctValues, ParserOptions option)
        {   
            for (int i = 0; i < _sampleLines.Length; i++)
            {
                var field = ParserUtils.LoadFieldFromLine(_sampleLines[i], option);
                Assert.AreEqual(field, correctValues[i].ToString());
            }
        }

        [Test]
        public void TrimsUselessLinesFromFile()
        {
            var trimmedLines = ParserUtils.TrimUselessLinesFromFile(DateTime.Today);
            Assert.AreEqual(trimmedLines[0].StartsWith("date"), false);
            Assert.AreEqual(trimmedLines[trimmedLines.Count - 1].StartsWith("\"The principal risks"), false);
        }
    }
}