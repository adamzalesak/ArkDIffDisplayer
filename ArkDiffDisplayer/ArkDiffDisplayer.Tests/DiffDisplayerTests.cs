using System.Collections.Generic;
using System.Linq;
using ArkDiffDisplayer.Entities;
using ArkDiffDisplayer.Exceptions;
using NUnit.Framework;

namespace ArkDiffDisplayer.Tests;

public class DiffDisplayerTests
{

    [Test]
    public void UnableToDownloadDataTest()
    {
        new When()
            .CanNotDownloadData()
            .AssertThrows<DiffDisplayerException>();
    }

    [Test]
    public void UnableToFetchDataTest()
    {
        new When()
            .CanDownloadData()
            .CanNotFetchData(new List<string> { "a", "b" })
            .AssertThrows<DiffDisplayerException>();
    }

    [Test]
    public void UnableToParseDataTest()
    {
        new When()
            .CanDownloadData()
            .FetchData(new List<string> { "a", "b" })
            .ParserCanNotParse()
            .AssertThrows<DiffDisplayerException>();
    }

    [Test]
    public void CreateDataDiffOutputTest()
    {
        new When()
            .CanDownloadData()
            .FetchData(new List<string> { "a", "b" })
            .CanParseData(new HoldingsData())
            .CanCreateDataDiff()
            .CanOutputData()
            .AssertTrue();
    }
}