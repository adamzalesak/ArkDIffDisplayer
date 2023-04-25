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

    public DataDiff GetDataDiff(DateTime dayToCompareTo)
    {
        // Downloads today's data
        var isSuccessfulStatus = _dataManagement.DownloadData();
        if (!isSuccessfulStatus)
        {
            throw new DiffDisplayerException("File download failed.");
        }

        // Fetch today's data and some older
        IList<string> todayData;
        IList<string> olderData;
        try
        {
            todayData = _dataManagement.FetchData(DateTime.Today);
            olderData = _dataManagement.FetchData(dayToCompareTo);
        }
        catch (FileNotFoundException ex)
        {
            throw new DiffDisplayerException("File fetch failed: " + ex.Message);
        }

        // parse data
        HoldingsData parsedOlderData;
        HoldingsData parsedTodayData;
        try
        {
            parsedTodayData = _parser.Parse(todayData);
            parsedOlderData = _parser.Parse(olderData);
        }
        catch (ParserException ex)
        {
            throw new DiffDisplayerException("File parse failed:" + ex.Message);
        }

        // create diff
        var diff = _diffCreator.CreateDataDiff(parsedOlderData, parsedTodayData);

        return diff;
    }

    public string GetDiff(DateTime dayToCompareTo)
    {   
        // create pretty console output string from diff
        var diff = GetDataDiff(dayToCompareTo);
        var result = _outputCreator.CreateOutput(diff);

        return result;
    }
}