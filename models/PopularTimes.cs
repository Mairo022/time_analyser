using Turnit.utils;

namespace Turnit.models;

public class PopularTimes
{
    readonly List<int> startTimes = [];
    readonly List<int> endTimes = [];
    int count;
     
    public void SetTimes((List<PopularTime> popularStarts, List<PopularTime> popularEnds, int maxCount) popularTimes)
    {
        startTimes.Clear();
        endTimes.Clear();

        var (popularStarts, popularEnds, maxCount) = popularTimes;

        foreach (PopularTime popularStart in popularStarts)
            startTimes.Add(popularStart.time);

        foreach (PopularTime popularEnd in popularEnds)
            endTimes.Add(popularEnd.time);

        count = maxCount;
    }

    public override string ToString()
    {
        if (count == 0) return "No timeframes found";

        if (startTimes.Count == 1)
        {
            string startTime = TimeUtils.TimeIntToString(startTimes[0]);
            string endTime = TimeUtils.TimeIntToString(endTimes[0]);

            return $"The busiest time is {startTime}-{endTime} with total of {count} drivers taking a break.";
        }

        if (startTimes.Count > 1)
        {
            string timeframes = "";

            for (int i = 0; i < startTimes.Count; i++)
            {
                string startTime = TimeUtils.TimeIntToString(startTimes[i]);
                string endTime = TimeUtils.TimeIntToString(endTimes[i]);

                if (i == 0) timeframes += $"{startTime}-{endTime}";
                if (i != 0) timeframes += $", {startTime}-{endTime}";
            }

            return $"The busiest times are {timeframes} with {count} drivers taking a break at both times.";
        }

        return "Something went wrong during printing the most popular timeframe(s).";
    }

    public void Print()
    {
        Console.WriteLine(ToString() + "\n");
    }
}