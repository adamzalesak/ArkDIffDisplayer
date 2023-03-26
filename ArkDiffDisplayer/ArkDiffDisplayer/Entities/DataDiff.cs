namespace ArkDiffDisplayer.Entities;

public class DataDiff
{
    public IList<DataDiffItem> NewPositions { get; set; } = new List<DataDiffItem>();
    public IList<DataDiffItem> IncreasedPositions { get; set; } = new List<DataDiffItem>();
    public IList<DataDiffItem> ReducedPositions { get; set; } = new List<DataDiffItem>();
}