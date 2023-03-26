// See https://aka.ms/new-console-template for more information
using ArkDiffDisplayer.FileManagement;

Console.WriteLine("Hello, World!");

FileManagementUtils.DownloadHoldingsCsv();

foreach (var line in FileManagementUtils.ReadLinesHoldingsCsvFile(DateTime.Today))
{
    Console.WriteLine(line);
}
