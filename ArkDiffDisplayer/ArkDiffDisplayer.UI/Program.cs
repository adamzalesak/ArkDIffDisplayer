// See https://aka.ms/new-console-template for more information

using System.Globalization;
using ArkDiffDisplayer;
using ArkDiffDisplayer.DiffCreator;
using ArkDiffDisplayer.FileManagement;
using ArkDiffDisplayer.OutputCreator;
using ArkDiffDisplayer.Parser;

IDataManagement dataManagement = new LocalFileManagement();

IParser parser = new ParserUtils();
IDiffCreator diffCreator = new DiffCreator();
IOutputCreator outputCreator = new OutputCreator();

var diffDisplayer = new DiffDisplayer(dataManagement, parser, diffCreator, outputCreator);
// The date can be fetched from commandline or such
var result = diffDisplayer.GetDiff(DateTime.Parse("03/26/2023", DateTimeFormatInfo.InvariantInfo));
Console.Write(result);

