// See https://aka.ms/new-console-template for more information
using ArkDiffDisplayer.FileManagement;

Console.WriteLine("Hello, World!");

FileManagementUtils.DownloadHoldingsCsv();
Console.WriteLine(FileManagementUtils.ReadHoldingsCsvFile(DateTime.Today));
