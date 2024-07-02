using Turnit.validators;

namespace Turnit.utils;

public class TimeUtils
{
    public static (int[] startTimeEvent, int[] endTimeEvent) GetTimeEvents(string time)
    {
        int[] times = GetTimes(time);
        int startTime = times[0];
        int endTime = times[1];

        int[] startTimeEvent = ConvertToTimeEvent(startTime, "start");
        int[] endTimeEvent = ConvertToTimeEvent(endTime, "end");

        return (startTimeEvent, endTimeEvent);
    }

    public static int[] GetTimes(string time)
    {
        if (!TimeValidator.IsValidTimeframeString(time))
        {
            throw new ArgumentException("Invalid time format");
        }

        int[] times = CovertTimeframeToTimesInt(time);

        if (!TimeValidator.IsValidTimeframeInt(times))
        {
            throw new ArgumentException("Invalid time format");
        }

        int startTime = times[0];
        int endTime = times[1];

        return [startTime, endTime];
    }

    public static int[] CovertTimeframeToTimesInt(string time)
    {
        time = time.Replace(":", string.Empty);

        int startTime = int.Parse(time[..^4]);
        int endTime = int.Parse(time[4..]);

        return [startTime, endTime];
    }

    public static int[] ConvertToTimeEvent(int time, string type)
    {
        return type.Equals("start") ? [time, 0] : [time, 1];
    }

    public static string TimeIntToString(int time)
    {
        // Ensures time format to be hh:mm
        string timeAsString = time.ToString().PadLeft(4, '0');
        string hours = timeAsString[..^2];
        string minutes = timeAsString[2..];
        string timeFormatted = $"{hours}:{minutes}";

        return timeFormatted;
    }
}