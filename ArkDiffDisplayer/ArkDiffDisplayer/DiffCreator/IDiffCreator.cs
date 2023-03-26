using ArkDiffDisplayer.Entities;

namespace ArkDiffDisplayer.DiffCreator;

public interface IDiffCreator
{
    /// <summary>
    /// Creating diff from today's fetched holdings.csv and other older fetched csv.
    /// </summary>
    /// <param name="olderHoldingsData"></param>
    /// <param name="newerHoldingsData"></param>
    /// <returns>List of data and/or changes according to DataDiff structure</returns>
    public DataDiff CreateDataDiff(HoldingsData olderHoldingsData, HoldingsData newerHoldingsData);
}