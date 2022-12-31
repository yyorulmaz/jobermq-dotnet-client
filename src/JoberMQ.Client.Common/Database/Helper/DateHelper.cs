using System;

namespace JoberMQ.Client.Common.Database.Helper
{
    public class DateHelper
    {
        public static DateTime GetUniversalNow() => DateTime.Now.ToUniversalTime();
    }
}
