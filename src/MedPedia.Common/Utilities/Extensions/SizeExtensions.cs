namespace MedPedia.Common.Utilities.Extensions
{
    public static class SizeExtensions
    {
        public static long ToKB(this long bytes)
        {
            return bytes / 1024;
        }

        public static long ToMB(this long bytes)
        {
            return ToKB(bytes) / 1024;
        }

        public static long ToGB(this long bytes)
        {
            return ToMB(bytes) / 1024;
        }
    }
}
