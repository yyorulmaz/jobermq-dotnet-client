using JoberMQ.Client.Net.Dbos;
using JoberMQ.Client.Net.Enums.Timing;
using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.Job;
using JoberMQ.Client.Net.Models.Timing;
using System;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobTimingExtension
    {
        // Now
        public static JobBuilderTimingExtensionModel TimingNow(this JobBuilderPublisherExtensionModel jobBuilderPublisherExtension)
            => TimingNowAdd(jobBuilderPublisherExtension.Builder);
        public static JobBuilderTimingExtensionModel TimingNow(this JobBuilderExtensionModel jobBuilderExtension)
            => TimingNowAdd(jobBuilderExtension.Builder);
        private static JobBuilderTimingExtensionModel TimingNowAdd(JobBuilderModel builder)
        {
            var jobBuilderTimingExtension = new JobBuilderTimingExtensionModel();
            jobBuilderTimingExtension.Builder = builder;
            jobBuilderTimingExtension.Builder.Timing = new TimingModel { TimingType = TimingTypeEnum.Now };
            return jobBuilderTimingExtension;
        }



        // Schedule
        public static JobBuilderTimingExtensionModel TimingScheduleDelayed(this JobBuilderPublisherExtensionModel jobBuilderPublisherExtension, int delayedSecond)
            => TimingScheduleAdd(jobBuilderPublisherExtension.Builder, ScheduleTypeEnum.Delayed, delayedSecond, null, 1);
        public static JobBuilderTimingExtensionModel TimingScheduleRecurrent(this JobBuilderPublisherExtensionModel jobBuilderPublisherExtension, string cronTime, int? executeCountMax = null)
            => TimingScheduleAdd(jobBuilderPublisherExtension.Builder, ScheduleTypeEnum.Recurrent, null, cronTime, executeCountMax);
        public static JobBuilderTimingExtensionModel TimingScheduleDelayed(this JobBuilderExtensionModel jobBuilderExtension, int delayedSecond)
            => TimingScheduleAdd(jobBuilderExtension.Builder, ScheduleTypeEnum.Delayed, delayedSecond, null, 1);
        public static JobBuilderTimingExtensionModel TimingScheduleRecurrent(this JobBuilderExtensionModel jobBuilderExtension, string cronTime, int? executeCountMax = null)
            => TimingScheduleAdd(jobBuilderExtension.Builder, ScheduleTypeEnum.Recurrent, null, cronTime, executeCountMax);
        private static JobBuilderTimingExtensionModel TimingScheduleAdd(JobBuilderModel builder, ScheduleTypeEnum scheduleType, int? delayedSecond, string cronTime, int? executeCountMax = null)
        {
            var jobBuilderTimingExtension = new JobBuilderTimingExtensionModel();
            jobBuilderTimingExtension.Builder = builder;
            jobBuilderTimingExtension.Builder.Timing = new TimingModel
            {
                TimingType = TimingTypeEnum.Schedule,
                ScheduleType = scheduleType,
                DelayedSecond = delayedSecond,
                CronTime = cronTime,
                ExecuteCountMax = executeCountMax
            };

            return jobBuilderTimingExtension;
        }



        // Trigger
        public static JobBuilderTimingExtensionModel TimingTriggerWhenDone(this JobBuilderPublisherExtensionModel jobBuilderPublisherExtension, Guid jobId, bool errorWorkflowStop)
            => TimingTriggerAdd(jobBuilderPublisherExtension.Builder, jobId, errorWorkflowStop);
        public static JobBuilderTimingExtensionModel TimingTriggerWhenDone(this JobBuilderExtensionModel jobBuilderExtension, Guid jobId, bool errorWorkflowStop)
            => TimingTriggerAdd(jobBuilderExtension.Builder, jobId, errorWorkflowStop);
        private static JobBuilderTimingExtensionModel TimingTriggerAdd(JobBuilderModel builder, Guid jobId, bool errorWorkflowStop)
        {
            var jobBuilderTimingExtension = new JobBuilderTimingExtensionModel();
            jobBuilderTimingExtension.Builder = builder;
            jobBuilderTimingExtension.Builder.Timing = new TimingModel
            {
                TimingType = TimingTypeEnum.Trigger,
                ErrorWorkflowStop = errorWorkflowStop,
                TriggerJobId = jobId
            };

            return jobBuilderTimingExtension;
        }
    }
}



//jobDbo.Timing.CronTime = DateToCron(DateTime.Now.AddSeconds(jobBuilderData.DelayedSecond.Value));

//public static string DateToCron(DateTime date)
//{
//    return $"{date.Second.ToString()} {date.Minute.ToString()} {date.Hour.ToString()} {date.Day.ToString()} {MonthToCronMonth(date.Month)} ? {date.Year.ToString()}";
//}
//private static string MonthToCronMonth(int month)
//{
//    string response = "";
//    switch (month)
//    {
//        case 1:
//            response = "JAN";
//            break;
//        case 2:
//            response = "FEB";
//            break;
//        case 3:
//            response = "MAR";
//            break;
//        case 4:
//            response = "APR";
//            break;
//        case 5:
//            response = "MAY";
//            break;
//        case 6:
//            response = "JUN";
//            break;
//        case 7:
//            response = "JUL";
//            break;
//        case 8:
//            response = "AUG";
//            break;
//        case 9:
//            response = "SEP";
//            break;
//        case 10:
//            response = "OCT";
//            break;
//        case 11:
//            response = "NOV";
//            break;
//        case 12:
//            response = "DEC";
//            break;
//    }

//    return response;
//}