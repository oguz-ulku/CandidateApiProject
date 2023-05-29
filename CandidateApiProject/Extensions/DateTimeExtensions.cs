namespace CandidateApiProject.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToLong16(this DateTime dateTime)
        {
            return long.Parse(dateTime.ToString("yyyyMMddHHmmssff"));
        }

        public static long ToLong14(this DateTime dateTime)
        {
            return long.Parse(dateTime.ToString("yyyyMMddHHmmss"));
        }

        public static long ToInt8(this DateTime dateTime)
        {
            return long.Parse(dateTime.ToString("yyyyMMdd"));
        }
    }
}
