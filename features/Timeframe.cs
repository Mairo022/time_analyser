using Turnit.models;
using Turnit.utils;

namespace Turnit.features;

public class Timeframe
{
    List<int[]> timeEvents = [];
    readonly PopularTimes popularTimes = new();

    // Implements line sweep algorithm to find the most popular timeframes
    (List<PopularTime> popularStarts, List<PopularTime> popularEnds, int maxCount) GetPopularTimes(List<int[]> timeEvents)
    {
        List<PopularTime> popularStarts = [];
        List<PopularTime> popularEnds = [];
        int maxCount = 0;
        int count = 0;

        if (timeEvents.Count == 0) return default;

        foreach (int[] timeEvent in timeEvents)
        {
            int time = timeEvent[0];
            bool isStartTime = timeEvent[1] == 0;

            if (isStartTime)
            {
                count++;

                if (count >= maxCount)
                {
                    // Remove times with smaller count from popularStarts
                    // Add current time
                    popularStarts = popularStarts.FindAll(time => time.count == count); 
                    popularStarts.Add(new PopularTime(time, count));

                    maxCount = count;
                    continue;
                }

                // Handles case where last break's end time and current break's start time are equal
                // via incrementing start and end time count
                PopularTime lastPopularEnd = popularEnds.LastOrDefault();

                if (lastPopularEnd.time == time)
                {
                    PopularTime lastPopularStart = popularStarts.LastOrDefault();

                    maxCount = lastPopularEnd.count + 1;
                    lastPopularEnd.count = maxCount;
                    lastPopularStart.count = maxCount;

                    popularStarts[^1] = lastPopularStart;
                    popularEnds[^1] = lastPopularEnd;
                }
            }

            if (!isStartTime)
            {
                if (count == maxCount)
                {
                    popularEnds = popularEnds.FindAll(time => time.count == count);
                    popularEnds.Add(new PopularTime(time, count));
                }
                count--;
            }
        }

        return (popularStarts, popularEnds, maxCount);
    }

    // Sorts time events by time in ascending order
    // If start and end times are equal, puts end time before
    List<int[]> getSortedTimeEvents(List<int[]> timeEvents)
    {
        timeEvents.Sort((a, b) =>
        {
            if (a[0] == b[0]) return a[1] == 1 ? -1 : 1;

            return a[0].CompareTo(b[0]);
        });

        return timeEvents;
    }


    public void AddTimeframes(List<int[]> timeEvents)
    {
        this.timeEvents = getSortedTimeEvents(timeEvents);
        UpdatePopularTimes();
    }

    // Searches for appropriate insertion point for time events and inserts them
    public void AddTimeframe(int[] startEvent, int[] endEvent)
    {
        int starTime = startEvent[0];
        int endTime = endEvent[0];

        int startEventInsertIndex = -1;
        int endEventInsertIndex = -1;

        for (int i = 0; i < timeEvents.Count; i++)
        {
            int time = timeEvents[i][0];

            if (startEventInsertIndex == -1 && starTime < time) startEventInsertIndex = i;
            if (endEventInsertIndex == -1 && endTime <= time) endEventInsertIndex = i + 1;

            if (startEventInsertIndex != -1 && endEventInsertIndex != -1) break;
        }

        // If index is not set, point to end of the list
        if (startEventInsertIndex == -1) startEventInsertIndex = timeEvents.Count;
        if (endEventInsertIndex == -1) endEventInsertIndex = timeEvents.Count + 1;

        timeEvents.Insert(startEventInsertIndex, startEvent);
        timeEvents.Insert(endEventInsertIndex, endEvent);

        UpdatePopularTimes();
    }

    void UpdatePopularTimes()
    {
        popularTimes.SetTimes(GetPopularTimes(timeEvents));
    }

    public void PrintPopularTimes()
    {
        popularTimes.Print();
    }

    public void CLIAddTimeframe()
    {
        while (true)
        {
            try
            {
                Console.Write("Enter timeframe (example format 13:1514:00) or type 'q' to exit the program: ");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) continue;
                if (input == "q") break;

                var (startTimeEvent, endTimeEvent) = TimeUtils.GetTimeEvents(input);

                AddTimeframe(startTimeEvent, endTimeEvent);
                PrintPopularTimes();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
