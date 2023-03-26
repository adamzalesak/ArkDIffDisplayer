using ArkDiffDisplayer.Entities;

namespace ArkDiffDisplayer.OutputCreator;

public interface IOutputCreator
{
    public string CreateOutput(DataDiff dataDiff);
}