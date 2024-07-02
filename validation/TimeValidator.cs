using System.Text.RegularExpressions;

namespace Turnit.validators;

public class TimeValidator
{
    public static bool IsValidTimeframeString(string time)
    {
        string timeframeFormat = @"^(?:[01]\d|2[0-3]):[0-5]\d(?:[01]\d|2[0-3]):[0-5]\d$";
        Regex timeframeRegex = new(timeframeFormat);

        bool isCorrectFormat = timeframeRegex.IsMatch(time);

        if (!isCorrectFormat) return false;

        return true;
    }

    // End times extending to next day are considered invalid
    public static bool IsValidTimeframeInt(int[] times)
    {
        int startTime = times[0];
        int endTime = times[1];

        if (startTime >= endTime) return false;

        return true;
    }
}

