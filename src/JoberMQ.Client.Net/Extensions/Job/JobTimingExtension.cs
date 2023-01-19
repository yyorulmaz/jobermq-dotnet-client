using JoberMQ.Client.Net.Dbos;
using JoberMQ.Client.Net.Enums.Timing;
using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.Timing;
using System;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobTimingExtension
    {
        // Now
        public static JobBuilderTimingModel TimingNow(this JobBuilderPublisherModel jobBuilderPublisher)
            => TimingNowAdd(jobBuilderPublisher.Builder);
        public static JobBuilderTimingModel TimingNow(this JobBuilderModel jobBuilderModel)
            => TimingNowAdd(jobBuilderModel.Builder);
        private static JobBuilderTimingModel TimingNowAdd(BuilderModel builder)
        {
            var jobBuilderTiming = new JobBuilderTimingModel();
            jobBuilderTiming.Builder = builder;
            jobBuilderTiming.Builder.Timing = new TimingModel { TimingType = TimingTypeEnum.Now };
            return jobBuilderTiming;
        }



        // Schedule
        public static JobBuilderTimingModel TimingScheduleDelayed(this JobBuilderPublisherModel jobBuilderPublisher, int delayedSecond)
            => TimingScheduleAdd(jobBuilderPublisher.Builder, ScheduleTypeEnum.Delayed, delayedSecond, null, 1);
        public static JobBuilderTimingModel TimingScheduleRecurrent(this JobBuilderPublisherModel jobBuilderPublisher, string cronTime, int? executeCountMax = null)
            => TimingScheduleAdd(jobBuilderPublisher.Builder, ScheduleTypeEnum.Recurrent, null, cronTime, executeCountMax);
        public static JobBuilderTimingModel TimingScheduleDelayed(this JobBuilderModel jobBuilderModel, int delayedSecond)
            => TimingScheduleAdd(jobBuilderModel.Builder, ScheduleTypeEnum.Delayed, delayedSecond, null, 1);
        public static JobBuilderTimingModel TimingScheduleRecurrent(this JobBuilderModel jobBuilderModel, string cronTime, int? executeCountMax = null)
            => TimingScheduleAdd(jobBuilderModel.Builder, ScheduleTypeEnum.Recurrent, null, cronTime, executeCountMax);
        private static JobBuilderTimingModel TimingScheduleAdd(BuilderModel builder, ScheduleTypeEnum scheduleType, int? delayedSecond, string cronTime, int? executeCountMax = null)
        {
            var jobBuilderTiming = new JobBuilderTimingModel();
            jobBuilderTiming.Builder = builder;
            jobBuilderTiming.Builder.Timing = new TimingModel
            {
                TimingType = TimingTypeEnum.Schedule,
                ScheduleType = scheduleType,
                DelayedSecond = delayedSecond,
                CronTime = cronTime,
                ExecuteCountMax = executeCountMax
            };

            return jobBuilderTiming;
        }



        // Trigger
        public static JobBuilderTimingModel TimingTriggerWhenDone(this JobBuilderPublisherModel jobBuilderPublisher, Guid jobId, bool errorWorkflowStop)
            => TimingTriggerAdd(jobBuilderPublisher.Builder, jobId, errorWorkflowStop);
        public static JobBuilderTimingModel TimingTriggerWhenDone(this JobBuilderModel jobBuilderModel, Guid jobId, bool errorWorkflowStop)
            => TimingTriggerAdd(jobBuilderModel.Builder, jobId, errorWorkflowStop);
        private static JobBuilderTimingModel TimingTriggerAdd(BuilderModel builder, Guid jobId, bool errorWorkflowStop)
        {
            var jobBuilderTiming = new JobBuilderTimingModel();
            jobBuilderTiming.Builder = builder;
            jobBuilderTiming.Builder.Timing = new TimingModel
            {
                TimingType = TimingTypeEnum.Trigger,
                ErrorWorkflowStop = errorWorkflowStop,
                TriggerJobId = jobId
            };

            return jobBuilderTiming;
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