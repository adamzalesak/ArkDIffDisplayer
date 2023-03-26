using ArkDiffDisplayer.Entities;

namespace ArkDiffDisplayer.OutputCreator;

public interface IOutputCreator
{
    /// <summary>
    /// Creating pretty console output string from diffed data.
    /// </summary>
    /// <param name="dataDiff"></param>
    /// <returns>Console output string according to assignment</returns>
    public string CreateOutput(DataDiff dataDiff);
}