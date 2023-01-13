using System;

namespace JoberMQ.Client.Net.Helpers
{
    internal class CronHelper
    {
        //bu kütüphane büyük ihtimalle cron ifadesi doğrumu diye kontrol ediyor
        //Quartz.CronExpression.IsValidExpression("* * * * * ");





        //public static string DateToCron(int cronField, DateTime date)
        //{
        //    //var saniye = date.Second;
        //    //var dakika = date.Minute;
        //    //var saat = date.Hour;
        //    //var gun = date.Day;
        //    //var ay = date.Month;
        //    //var yil = date.Year;

        //    string response = "";
        //    if (cronField == 5)
        //    {
        //        response = $"{date.Minute.ToString()} {date.Hour.ToString()} {date.Day.ToString()} {MonthToCronMonth(date.Month)} {date.Year.ToString()}";
        //    }
        //    else if (cronField == 6)
        //    {
        //        response = $"{date.Second.ToString()} {date.Minute.ToString()} {date.Hour.ToString()} {date.Day.ToString()} {MonthToCronMonth(date.Month)} {date.Year.ToString()}";
        //    }
        //    else if (cronField == 7)
        //    {
        //        response = $"{date.Second.ToString()} {date.Minute.ToString()} {date.Hour.ToString()} {date.Day.ToString()} {MonthToCronMonth(date.Month)} ? {date.Year.ToString()}";
        //    }

        //    return response;
        //}
        public static string DateToCron(DateTime date)
        {
            return $"{date.Second.ToString()} {date.Minute.ToString()} {date.Hour.ToString()} {date.Day.ToString()} {MonthToCronMonth(date.Month)} ? {date.Year.ToString()}";
        }

        private static string MonthToCronMonth(int month)
        {
            string response = "";
            switch (month)
            {
                case 1:
                    response = "JAN";
                    break;
                case 2:
                    response = "FEB";
                    break;
                case 3:
                    response = "MAR";
                    break;
                case 4:
                    response = "APR";
                    break;
                case 5:
                    response = "MAY";
                    break;
                case 6:
                    response = "JUN";
                    break;
                case 7:
                    response = "JUL";
                    break;
                case 8:
                    response = "AUG";
                    break;
                case 9:
                    response = "SEP";
                    break;
                case 10:
                    response = "OCT";
                    break;
                case 11:
                    response = "NOV";
                    break;
                case 12:
                    response = "DEC";
                    break;
            }

            return response;
        }

        public static string CronDescription(string cron)
        {
            return CronExpressionDescriptor.ExpressionDescriptor.GetDescription(cron);

            //SIMPLE
            //var data = CronExpressionDescriptor.ExpressionDescriptor.GetDescription("0,4,36 * 0/7 3,16,19 MAR,JUL ? 2021");
        }
        public static string CronDescription(string cron, CronExpressionDescriptor.Options options)
        {
            return CronExpressionDescriptor.ExpressionDescriptor.GetDescription(cron, options);

            //SIMPLE
            //var data2 = CronExpressionDescriptor.ExpressionDescriptor.GetDescription("0,4,36 * 0/7 3,16,19 MAR,JUL ? 2021", new CronExpressionDescriptor.Options()
            //{
            //    DayOfWeekStartIndexZero = false,
            //    Use24HourTimeFormat = true,
            //    Locale = "tr"
            //});
        }





    }
}
