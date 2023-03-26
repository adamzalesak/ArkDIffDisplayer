namespace ArkDiffDisplayer.FileManagement;

public class LocalFileManagement : IDataManagement
{
    public bool DownloadData()
    {
        return FileManagementUtils.DownloadHoldingsCsv();
    }

    public IList<string> FetchData(DateTime dateToFetch)
    {
        return FileManagementUtils.ReadLinesHoldingsCsvFile(dateToFetch);
    }
}