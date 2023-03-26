namespace ArkDiffDisplayer.FileManagement;

public interface IDataManagement
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool DownloadData();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateToFetch"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public IList<string> FetchData(DateTime dateToFetch);
}