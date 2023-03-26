namespace ArkDiffDisplayer.Entities;

public class DataDiffItem
{
    public string CompanyName { get; set; }
    public string Ticker { get; set; }
    public long Shares { get; set; }
    public decimal? SharesPercentageChange { get; set; }
    public decimal WeightPercentage { get; set; }
}