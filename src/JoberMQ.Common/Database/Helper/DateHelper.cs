using System;

namespace JoberMQ.Common.Database.Helper
{
    public class DateHelper
    {
        public static DateTime GetUniversalNow() => DateTime.Now.ToUniversalTime();
    }
}
