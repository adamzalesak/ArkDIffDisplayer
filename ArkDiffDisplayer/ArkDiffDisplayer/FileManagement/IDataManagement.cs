namespace ArkDiffDisplayer.FileManagement;

public interface IDataManagement
{
    /// <summary>
    /// Downloading today's holdings.csv file.
    /// </summary>
    /// <returns>True or False according to downloading success</returns>
    public bool DownloadData();

    /// <summary>
    /// Fetching today's holdings.csv and some other date-picked file.
    /// </summary>
    /// <param name="dateToFetch"></param>
    /// <returns>IList of string lines for every fetched file</returns>
    /// <exception cref="FileNotFoundException"></exception>
    public IList<string> FetchData(DateTime dateToFetch);
}