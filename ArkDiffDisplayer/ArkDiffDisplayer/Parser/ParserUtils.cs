using ArkDiffDisplayer.Entities;
using ArkDiffDisplayer.FileManagement;

namespace ArkDiffDisplayer.Parser
{
    public class ParserUtils : IParser
    {
        public HoldingsData Parse(IList<string> stringData)
        {
            var relevantLines = TrimUselessLinesFromFile(stringData);
            var holdingsData = new HoldingsData();

            foreach (var line in relevantLines)
            {
                var holdingsItem = new HoldingsDataItem();

                holdingsItem.Date = DateTime.Parse(LoadFieldFromLine(line, ParserOptions.Date));
                holdingsItem.Fund = LoadFieldFromLine(line, ParserOptions.Fund);
                holdingsItem.Company = LoadFieldFromLine(line, ParserOptions.Company);
                holdingsItem.Ticker = LoadFieldFromLine(line, ParserOptions.Ticker);
                holdingsItem.Cusip = LoadFieldFromLine(line, ParserOptions.Cusip);
                holdingsItem.Shares = (long)Convert.ToDouble(LoadFieldFromLine(line, ParserOptions.Shares));
                holdingsItem.MarketValue = Convert.ToDouble(LoadFieldFromLine(line, ParserOptions.MarketValue));
                holdingsItem.Weight = Convert.ToDouble(LoadFieldFromLine(line, ParserOptions.Weight));

                holdingsData.Data.Add(holdingsItem);
            }

            return holdingsData;
        }

        internal string LoadFieldFromLine(string line, ParserOptions option)
        {
            // All lines are of the same structure. They have 8 fields that need to be filled.
            // Sample line: 03/17/2023,ARKK,"ZOOM VIDEO COMMUNICATIONS-A",ZM,98980L101,"8,769,511","$619,039,781.49",7.95%
            var lineWithoutCommas = line.Split(',');
            var lineWithoutStringSign = line.Split('\"');

            switch (option)
            {
                case ParserOptions.Date:
                    return lineWithoutCommas[0];
                case ParserOptions.Fund:
                    return lineWithoutCommas[1];
                case ParserOptions.Company:
                    return lineWithoutCommas[2].Replace("\"","");
                case ParserOptions.Ticker:
                    return lineWithoutCommas[3];
                case ParserOptions.Cusip:
                    return lineWithoutCommas[4];
                case ParserOptions.Shares:
                    return lineWithoutStringSign[3].Replace(",", "");
                case ParserOptions.MarketValue:
                    return lineWithoutStringSign[5].TrimStart('$').Replace(",", "");
                case ParserOptions.Weight:
                    return lineWithoutCommas[lineWithoutCommas.Length - 1].TrimEnd('%');
                default:
                    throw new ArgumentException();
            }
        }

        internal IList<string> TrimUselessLinesFromFile(IList<string> lines)
        {
            lines.RemoveAt(0);
            lines.RemoveAt(lines.Count - 1);
            return lines;
        }
    }
}