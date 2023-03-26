using ArkDiffDisplayer.Entities;
using ArkDiffDisplayer.Exceptions;

namespace ArkDiffDisplayer.Parser;

public interface IParser
{
    /// <summary>
    /// Parsing today's and older holdings.csv according to HoldingsData structure.
    /// </summary>
    /// <param name="stringData"></param>
    /// <returns>Parsed HoldingsData for every file</returns>
    /// <exception cref="ParserException"></exception>
    public HoldingsData Parse(IList<string> stringData);
}