namespace Backend.Utils
{
    /// <summary>
    /// 日期时间工具类，用于处理时区转换
    /// </summary>
    public static class DateTimeHelper
    {
        private static readonly TimeZoneInfo BeijingTimeZone;

        static DateTimeHelper()
        {
            try
            {
                // Windows 系统使用 "China Standard Time"
                // Linux 系统使用 "Asia/Shanghai"
                BeijingTimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                try
                {
                    // 尝试 Linux 时区 ID
                    BeijingTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Shanghai");
                }
                catch (TimeZoneNotFoundException)
                {
                    // 如果都找不到，使用 UTC+8 作为备用方案
                    BeijingTimeZone = TimeZoneInfo.CreateCustomTimeZone(
                        "Beijing Standard Time",
                        new TimeSpan(8, 0, 0),
                        "Beijing Standard Time",
                        "Beijing Standard Time"
                    );
                }
            }
        }

        /// <summary>
        /// 获取当前北京时间
        /// </summary>
        /// <returns>当前北京时间</returns>
        public static DateTime GetBeijingTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, BeijingTimeZone);
        }

        /// <summary>
        /// UTC 时间转北京时间
        /// </summary>
        /// <param name="utcTime">UTC 时间</param>
        /// <returns>北京时间</returns>
        public static DateTime UtcToBeijing(DateTime utcTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, BeijingTimeZone);
        }

        /// <summary>
        /// 北京时间转 UTC 时间
        /// </summary>
        /// <param name="beijingTime">北京时间</param>
        /// <returns>UTC 时间</returns>
        public static DateTime BeijingToUtc(DateTime beijingTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(beijingTime, BeijingTimeZone);
        }

        /// <summary>
        /// 格式化为北京时间字符串
        /// </summary>
        /// <param name="dateTime">要格式化的时间</param>
        /// <param name="format">格式字符串，默认为 "yyyy-MM-dd HH:mm:ss"</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatBeijingTime(DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return dateTime.ToString(format);
        }
    }
}
