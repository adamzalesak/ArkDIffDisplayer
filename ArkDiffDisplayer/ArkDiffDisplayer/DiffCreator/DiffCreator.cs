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
            var oldData = extractData(olderHoldingsData);
            var newData = extractData(newerHoldingsData);
            var diffDataList = differentiateAllData(newData, oldData);

            return new DataDiff
            {
                IncreasedPositions = diffDataList.Where(item => item.SharesPercentageChange >= 0).ToList(),
                ReducedPositions = diffDataList.Where(item => item.SharesPercentageChange < 0).ToList(),
                NewPositions = getNewData(newData, oldData).ToList()
            };
        }

        private Dictionary<string, HoldingsDataItem> extractData(HoldingsData holdingsData) =>
            holdingsData.Data.ToDictionary(dataItem => dataItem.Company);

        private IEnumerable<DataDiffItem> differentiateAllData(Dictionary<string, HoldingsDataItem> newData, Dictionary<string, HoldingsDataItem> oldData) =>
            newData
                 .Where(pair => oldData.ContainsKey(pair.Key))
                 .Select(pair => new DataDiffItem(pair.Value, pair.Value.Shares - oldData[pair.Key].Shares, pair.Value.MarketValue - oldData[pair.Key].MarketValue));

        private IEnumerable<DataDiffItem> getNewData(Dictionary<string, HoldingsDataItem> newData, Dictionary<string, HoldingsDataItem> oldData) =>
            newData
                .Where(pair => !oldData.ContainsKey(pair.Key))
                .Select(pair => new DataDiffItem(pair.Value));
    }
}
