namespace MedPedia.Common.Utilities.Assistant;

public class RandomHelper
{
    public static string GetRandomNumber(int digit)
    {
        var max = 10;

        for (int i = 1; i < digit; i++)
            max *= 10;

        Random random = new();
        return random.Next(0, max).ToString($"D{digit}");
    }

    public static int GetRandomIntNumber(int minValue = 10, int maxValue = 20)
    {
        return new Random().Next(minValue, maxValue);
    }

    public static string GetGuid()
    {
        return Guid.NewGuid().ToString().ToLower().Replace("-", "");
    }

    public static string GetGuid(string str, int length = 32)
    {
        return new Guid(str).ToString().ToLower().Replace("-", "").Substring(0, length);
    }

    public static string GetGuid(int length = 32)
    {
        return Guid.NewGuid().ToString().ToLower().Replace("-", "").Substring(0, length);
    }

}