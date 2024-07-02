using Turnit.utils;

namespace Turnit.io;

public class TimeFileReader()
{
    public static List<int[]> Load(string filename)
    {
        List<int[]> timeEvents = [];

        try
        {
            using StreamReader read = new(filename);

            string? line;
            while ((line = read.ReadLine()) != null)
            {
                try
                {
                    var (startTimeEvent, endTimeEvent) = TimeUtils.GetTimeEvents(line);

                    timeEvents.Add(startTimeEvent);
                    timeEvents.Add(endTimeEvent);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}. Could not process: {line}");
                }
            }
        }
        catch (Exception e) 
        {
            Console.WriteLine("Error reading file. " + e.Message);
        }

        return timeEvents;
    }
}