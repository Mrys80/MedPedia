namespace MedPedia.Common.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToUtcString(this DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return dateTime.ToUniversalTime().ToString(format);
        }
    }
}