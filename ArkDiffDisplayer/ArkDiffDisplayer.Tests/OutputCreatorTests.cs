using System;
using System.Collections;
using System.Collections.Generic;
using ArkDiffDisplayer.Entities;
using NUnit.Framework;

namespace ArkDiffDisplayer.Tests;

public class OutputCreatorTests
{
    private IList<DataDiffItem> _newPositions;
    private IList<DataDiffItem> _increasedPositions;
    private IList<DataDiffItem> _reducedPositions;
    private OutputCreator.OutputCreator _outputCreator;
    private IList<string> _outputs;
    private const string NewPositionsString = "New Positions:";
    private const string IncreasedPositionsString = "Increased Positions:";
    private const string ReducedPositionsString = "Reduced Positions:";
    private const string NoneString = "None";

    [SetUp]
    public void Setup()
    {
        _outputCreator = new OutputCreator.OutputCreator();
        
        IList<HoldingsDataItem> holdings = new List<HoldingsDataItem>
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

        _newPositions = new List<DataDiffItem>
        {
            new (holdings[0]),
            new (holdings[1]),
        };

        _increasedPositions = new List<DataDiffItem>
        {
            new (holdings[2], 1.12341341243m),
            new (holdings[3], 0.22512313413m),
            new (holdings[4], 2.12313241234m),
        };

        _reducedPositions = new List<DataDiffItem>
        {
            new (holdings[5], -0.9348569238m),
            new (holdings[6], -1.0504390232m),
            new (holdings[7], -0.0242241341m),
        };

        _outputs = new List<string>
        {
            $"ZOOM VIDEO COMMUNICATIONS-A, ZM, 8524760 shares, weight 10.23%",
            $"ROKU INC, ROKU, 9195866 shares, weight 6.12%",
            $"COINBASE GLOBAL INC -CLASS A, COIN, 7703761 shares (🔺1.12%), weight 4.21%",
            $"BLOCK INC, SQ, 6899533 shares (🔺0.23%), weight 5.21%",
            $"EXACT SCIENCES CORP, EXAS, 7112080 shares (🔺2.12%), weight 3.05%",
            $"UIPATH INC - CLASS A, PATH, 26641565 shares (🔻0.93%), weight 5.64%",
            $"SHOPIFY INC - CLASS A, SHOP, 9057929 shares (🔻1.05%), weight 1.23%",
            $"DRAFTKINGS INC-CL A, DKNG UW, 15881025 shares (🔻0.02%), weight 2.43%"
        };
    }
    
    [Test]
    public void CreateOutput_OnGeneralInput_ReturnsCorrectString()
    {
        var dataDiff = new DataDiff
        {
            NewPositions = _newPositions,
            IncreasedPositions = _increasedPositions,
            ReducedPositions = _reducedPositions
        };

        var expected = string.Join(Environment.NewLine,
            NewPositionsString, _outputs[0], _outputs[1],
            IncreasedPositionsString, _outputs[2], _outputs[3], _outputs[4],
            ReducedPositionsString, _outputs[5], _outputs[6], _outputs[7]);

        var actual = _outputCreator.CreateOutput(dataDiff);
        
        Assert.AreEqual(expected, actual);
    } 
    
    [Test]
    public void CreateOutput_NoNewPositions_ReturnsCorrectString()
    {
        var dataDiff = new DataDiff
        {
            NewPositions = new List<DataDiffItem>(),
            IncreasedPositions = _increasedPositions,
            ReducedPositions = _reducedPositions
        };

        var expected = string.Join(Environment.NewLine,
            NewPositionsString, NoneString,
            IncreasedPositionsString, _outputs[2], _outputs[3], _outputs[4],
            ReducedPositionsString, _outputs[5], _outputs[6], _outputs[7]);

        var actual = _outputCreator.CreateOutput(dataDiff);
        
        Assert.AreEqual(expected, actual);
    } 
    
    [Test]
    public void CreateOutput_NoIncreasedPositions_ReturnsCorrectString()
    {
        var dataDiff = new DataDiff
        {
            NewPositions = _newPositions,
            IncreasedPositions = new List<DataDiffItem>(),
            ReducedPositions = _reducedPositions
        };

        var expected = string.Join(Environment.NewLine,
            NewPositionsString, _outputs[0], _outputs[1],
            IncreasedPositionsString, NoneString,
            ReducedPositionsString, _outputs[5], _outputs[6], _outputs[7]);

        var actual = _outputCreator.CreateOutput(dataDiff);
        
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void CreateOutput_NoReducedPositions_ReturnsCorrectString()
    {
        var dataDiff = new DataDiff
        {
            NewPositions = _newPositions,
            IncreasedPositions = _increasedPositions,
            ReducedPositions = new List<DataDiffItem>()
        };

        var expected = string.Join(Environment.NewLine,
            NewPositionsString, _outputs[0], _outputs[1],
            IncreasedPositionsString, _outputs[2], _outputs[3], _outputs[4],
            ReducedPositionsString, NoneString);

        var actual = _outputCreator.CreateOutput(dataDiff);
        
        Assert.AreEqual(expected, actual);
    } 
}