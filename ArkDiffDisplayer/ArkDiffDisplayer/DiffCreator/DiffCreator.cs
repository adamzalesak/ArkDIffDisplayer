using ArkDiffDisplayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkDiffDisplayer.DiffCreator
{
    public class DiffCreator : IDiffCreator
    {
        public DiffCreator() { }

        public DataDiff CreateDataDiff(HoldingsData olderHoldingsData, HoldingsData newerHoldingsData)
        {
            var oldData = ExtractData(olderHoldingsData);
            var newData = ExtractData(newerHoldingsData);
            var diffDataList = DifferentiateAllData(newData, oldData);

            return new DataDiff
            {
                IncreasedPositions = diffDataList.Where(item => item.SharesPercentageChange >= 0).ToList(),
                ReducedPositions = diffDataList.Where(item => item.SharesPercentageChange < 0).ToList(),
                NewPositions = GetNewData(newData, oldData).ToList()
            };
        }

        private Dictionary<string, HoldingsDataItem> ExtractData(HoldingsData holdingsData) =>
            holdingsData.Data.ToDictionary(dataItem => dataItem.Company);

        private IEnumerable<DataDiffItem> DifferentiateAllData(Dictionary<string, HoldingsDataItem> newData, Dictionary<string, HoldingsDataItem> oldData) =>
            newData
                 .Where(pair => oldData.ContainsKey(pair.Key))
                 .Select(pair => new DataDiffItem(pair.Value, 
                     (decimal)(pair.Value.Shares - oldData[pair.Key].Shares) * 100 / oldData[pair.Key].Shares));

        private IEnumerable<DataDiffItem> GetNewData(Dictionary<string, HoldingsDataItem> newData, Dictionary<string, HoldingsDataItem> oldData) =>
            newData
                .Where(pair => !oldData.ContainsKey(pair.Key))
                .Select(pair => new DataDiffItem(pair.Value));
    }
}
