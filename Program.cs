using Turnit.arguments;
using Turnit.features;
using Turnit.io;

Arguments arguments = new(args);
string filename = arguments.GetFilename();

Timeframe timeframe = new();

if (!String.IsNullOrEmpty(filename))
{
    List<int[]> lines = TimeFileReader.Load(filename);
    timeframe.AddTimeframes(lines);
    timeframe.PrintPopularTimes();
}

timeframe.CLIAddTimeframe();
Environment.Exit(0);
