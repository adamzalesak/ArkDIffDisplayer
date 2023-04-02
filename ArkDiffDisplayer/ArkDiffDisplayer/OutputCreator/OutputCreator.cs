using System.Globalization;
using ArkDiffDisplayer.Entities;

namespace ArkDiffDisplayer.OutputCreator;

public class OutputCreator : IOutputCreator
{
    private const string NewPositionsString = "New Positions:";
    private const string IncreasedPositionsString = "Increased Positions:";
    private const string ReducedPositionsString = "Reduced Positions:";
    private const string NoneString = "None";
    private readonly NumberFormatInfo DecimalDotNumberFormat = new() { NumberDecimalSeparator = "." };

    public string CreateOutput(DataDiff dataDiff)
    {
        return string.Join(Environment.NewLine,
            NewPositionsString, CreateOutputPerDataDiffList(dataDiff.NewPositions, false),
            IncreasedPositionsString, CreateOutputPerDataDiffList(dataDiff.IncreasedPositions, true),
            ReducedPositionsString, CreateOutputPerDataDiffList(dataDiff.ReducedPositions, true));
    }

    private string CreateOutputPerDataDiffList(IList<DataDiffItem> dataDiffItems, bool includeSharesPercentageChange)
    {
        if (dataDiffItems.Count == 0)
        {
            return NoneString;
        }
        
        return string.Join(Environment.NewLine, dataDiffItems.Select(d => CreateOutputPerDataDiffItem(d, includeSharesPercentageChange)));
    }

    private string CreateOutputPerDataDiffItem(DataDiffItem dataDiffItem, bool includeSharesPercentageChange)
    {
        return $"{dataDiffItem.CompanyName}, {dataDiffItem.Ticker}, {dataDiffItem.Shares} shares" +
               $"{(includeSharesPercentageChange ? CreateSharesPercentageChangeOutput() : "")}, weight {dataDiffItem.WeightPercentage.ToString("0.##", DecimalDotNumberFormat)}%";
        
        string CreateSharesPercentageChangeOutput()
        {
            if (dataDiffItem.SharesPercentageChange > 0)
            {
                return $" (🔺{dataDiffItem.SharesPercentageChange.ToString("0.##", DecimalDotNumberFormat)}%)";
            }

            return $" (🔻{(-1 * dataDiffItem.SharesPercentageChange).ToString("0.##", DecimalDotNumberFormat)}%)";
        }
    }
}