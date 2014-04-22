using System;

namespace LootTrack.Web.Foundation.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this long timestamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(timestamp).ToLocalTime();
            return dtDateTime;
        }
    }
}