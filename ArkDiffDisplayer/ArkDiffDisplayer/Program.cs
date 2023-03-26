// See https://aka.ms/new-console-template for more information
using ArkDiffDisplayer.FileManagement;

Console.WriteLine("Hello, World!");

FileManagementUtils.DownloadHoldingsCsv();

foreach (var line in FileManagementUtils.ReadLinesHoldingsCsvFile(DateTime.Today))
{
    Console.WriteLine(line);
}

// TODO instead of nulls, insert instances of classes that implement given interface, then uncomment


IDataManagement dataManagement = new LocalFileManagement();
/*
IParser parser = null;
IDiffCreator diffCreator = null;
IOutputCreator outputCreator = null;

var diffDisplayer = new DiffDisplayer(dataManagement, parser, diffCreator, outputCreator);
// The date can be fetched from commandline or such
var result = diffDisplayer.GetDiff(DateTime.Today - TimeSpan.FromDays(1));
Console.Write(result);
*/
