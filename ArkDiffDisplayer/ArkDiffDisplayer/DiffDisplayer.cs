using ArkDiffDisplayer.DiffCreator;
using ArkDiffDisplayer.Entities;
using ArkDiffDisplayer.Exceptions;
using ArkDiffDisplayer.FileManagement;
using ArkDiffDisplayer.OutputCreator;
using ArkDiffDisplayer.Parser;

namespace ArkDiffDisplayer;

public class DiffDisplayer
{
    private readonly IDataManagement _dataManagement;
    private readonly IParser _parser;
    private readonly IDiffCreator _diffCreator;
    private readonly IOutputCreator _outputCreator;
    
    public DiffDisplayer(IDataManagement dataManagement, IParser parser, IDiffCreator diffCreator, IOutputCreator outputCreator)
    {
        _dataManagement = dataManagement;
        _parser = parser;
        _diffCreator = diffCreator;
        _outputCreator = outputCreator;
    }

    public string GetDiff(DateTime dayToCompareTo)
    {
        // Downloads today's data
        var downloadStatus = _dataManagement.DownloadData();
        if (!downloadStatus)
        {
            throw new DiffDisplayerException("Unsuccessful data download.");
        }
        
        // Fetch today's data
        IList<string> todaysData;
        IList<string> olderData;
        try
        {
            todaysData = _dataManagement.FetchData(DateTime.Today);
            olderData = _dataManagement.FetchData(dayToCompareTo);
        }
        catch (FileNotFoundException ex)
        {
            throw new DiffDisplayerException("File fetch failed: " + ex.Message);
        }

        // parse data
        HoldingsData parsedOlderData;
        HoldingsData parsedTodaysData;
        try
        {
            parsedTodaysData = _parser.Parse(todaysData);
            parsedOlderData = _parser.Parse(olderData);
        }
        catch (ParserException ex)
        {
            throw new DiffDisplayerException(ex.Message);
        }

        // create diff
        var diff = _diffCreator.CreateDataDiff(parsedOlderData, parsedTodaysData);
        
        // create string from diff
        var result = _outputCreator.CreateOutput(diff);

        return result;
    }
}