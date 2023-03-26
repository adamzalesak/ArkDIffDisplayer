using ArkDiffDisplayer.Entities;
using ArkDiffDisplayer.Exceptions;

namespace ArkDiffDisplayer.Parser;

public interface IParser
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stringData"></param>
    /// <returns></returns>
    /// <exception cref="ParserException"></exception>
    public HoldingsData Parse(IList<string> stringData);
}