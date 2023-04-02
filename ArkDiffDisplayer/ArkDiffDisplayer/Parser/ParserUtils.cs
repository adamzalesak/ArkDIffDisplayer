using System.Globalization;
using System.Text.RegularExpressions;
using ArkDiffDisplayer.Entities;

namespace ArkDiffDisplayer.Parser
{
    public class ParserUtils : IParser
    {
        private const string DatePattern =  @"""?(?<date>[0-1][0-9]/[0-3][0-9]/20[0-9][0-9])""?";
        private const string FundPattern = @"""?(?<fund>[A-Z \s]*)""?";
        private const string CompanyPattern = @"""?(?<company>[\w \s -]*)""?";
        private const string TickerPattern = @"""?(?<ticker>[A-Z \s]*)""?";
        private const string CussipPattern = @"""?(?<cussip>[\w]*)""?";
        private const string SharesPattern = @"""?(?<shares>[\d ,]*)""?";
        private const string MarketValuePattern = @"""?\$(?<marketValue>[\d , \.]*)""?";
        private const string WeightPattern = @"""?(?<weight>[\d .]*)%""?";
        private readonly Regex _lineRegex = new(string.Join(",", DatePattern, FundPattern, CompanyPattern, TickerPattern,
            CussipPattern, SharesPattern, MarketValuePattern, WeightPattern)); 
        
        public HoldingsData Parse(IList<string> stringData)
        {
            var relevantLines = TrimUselessLinesFromFile(stringData);
            var holdingsData = new HoldingsData();

            foreach (var line in relevantLines)
            {
                var holdingsItem = new HoldingsDataItem
                {
                    Date = DateTime.Parse(LoadFieldFromLine(line, ParserOptions.Date), DateTimeFormatInfo.InvariantInfo),
                    Fund = LoadFieldFromLine(line, ParserOptions.Fund),
                    Company = LoadFieldFromLine(line, ParserOptions.Company),
                    Ticker = LoadFieldFromLine(line, ParserOptions.Ticker),
                    Cusip = LoadFieldFromLine(line, ParserOptions.Cusip),
                    Shares = long.Parse(LoadFieldFromLine(line, ParserOptions.Shares), NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo),
                    MarketValue = decimal.Parse(LoadFieldFromLine(line, ParserOptions.MarketValue), NumberFormatInfo.InvariantInfo),
                    Weight = decimal.Parse(LoadFieldFromLine(line, ParserOptions.Weight), NumberFormatInfo.InvariantInfo)
                };

                holdingsData.Data.Add(holdingsItem);
            }

            return holdingsData;
        }

        internal string LoadFieldFromLine(string line, ParserOptions option)
        {
            // All lines are of the same structure. They have 8 fields that need to be filled.
            // Sample line: 03/17/2023,ARKK,"ZOOM VIDEO COMMUNICATIONS-A",ZM,98980L101,"8,769,511","$619,039,781.49",7.95%
            if (!_lineRegex.IsMatch(line))
            {
                return "";
            }

            var match = _lineRegex.Match(line);

            return option switch
            {
                ParserOptions.Date => match.Groups["date"].Value,
                ParserOptions.Fund => match.Groups["fund"].Value,
                ParserOptions.Company => match.Groups["company"].Value,
                ParserOptions.Ticker => match.Groups["ticker"].Value,
                ParserOptions.Cusip => match.Groups["cussip"].Value,
                ParserOptions.Shares => match.Groups["shares"].Value,
                ParserOptions.MarketValue => match.Groups["marketValue"].Value,
                ParserOptions.Weight => match.Groups["weight"].Value,
                _ => throw new ArgumentException()
            };
        }

        internal IList<string> TrimUselessLinesFromFile(IList<string> lines)
        {
            lines.RemoveAt(0);
            lines.RemoveAt(lines.Count - 1);
            return lines;
        }
    }
}