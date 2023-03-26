using ArkDiffDisplayer.Entities;

namespace ArkDiffDisplayer.DiffCreator;

public interface IDiffCreator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="olderHoldingsData"></param>
    /// <param name="newerHoldingsData"></param>
    /// <returns></returns>
    public DataDiff CreateDataDiff(HoldingsData olderHoldingsData, HoldingsData newerHoldingsData);
}