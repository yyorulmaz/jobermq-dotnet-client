using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Timing;
using JoberMQ.Common.Models.Job;
using JoberMQ.Common.Models.Timing;
using System;

public static class JobTimingExtension
{
    // Now
    public static JobBuilderTimingExtensionModel TimingNow(this JobBuilderPublisherExtensionModel jobBuilderPublisherExtension)
        => TimingNowAdd(jobBuilderPublisherExtension.Job);
    public static JobBuilderTimingExtensionModel TimingNow(this JobBuilderModel jobBuilderExtension)
        => TimingNowAdd(jobBuilderExtension.Job);
    private static JobBuilderTimingExtensionModel TimingNowAdd(JobDbo builder)
    {
        var jobBuilderTimingExtension = new JobBuilderTimingExtensionModel();
        jobBuilderTimingExtension.Job = builder;
        jobBuilderTimingExtension.Job.Timing = new TimingModel { TimingType = TimingTypeEnum.Now };
        return jobBuilderTimingExtension;
    }



    // Schedule
    public static JobBuilderTimingExtensionModel TimingScheduleDelayed(this JobBuilderPublisherExtensionModel jobBuilderPublisherExtension, int delayedSecond)
        => TimingScheduleAdd(jobBuilderPublisherExtension.Job, ScheduleTypeEnum.Delayed, delayedSecond, null, 1);
    public static JobBuilderTimingExtensionModel TimingScheduleRecurrent(this JobBuilderPublisherExtensionModel jobBuilderPublisherExtension, string cronTime, int? executeCountMax = null)
        => TimingScheduleAdd(jobBuilderPublisherExtension.Job, ScheduleTypeEnum.Recurrent, null, cronTime, executeCountMax);
    public static JobBuilderTimingExtensionModel TimingScheduleDelayed(this JobBuilderModel jobBuilderExtension, int delayedSecond)
        => TimingScheduleAdd(jobBuilderExtension.Job, ScheduleTypeEnum.Delayed, delayedSecond, null, 1);
    public static JobBuilderTimingExtensionModel TimingScheduleRecurrent(this JobBuilderModel jobBuilderExtension, string cronTime, int? executeCountMax = null)
        => TimingScheduleAdd(jobBuilderExtension.Job, ScheduleTypeEnum.Recurrent, null, cronTime, executeCountMax);
    private static JobBuilderTimingExtensionModel TimingScheduleAdd(JobDbo builder, ScheduleTypeEnum scheduleType, int? delayedSecond, string cronTime, int? executeCountMax = null)
    {
        var jobBuilderTimingExtension = new JobBuilderTimingExtensionModel();
        jobBuilderTimingExtension.Job = builder;
        jobBuilderTimingExtension.Job.Timing = new TimingModel
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
        => TimingTriggerAdd(jobBuilderPublisherExtension.Job, jobId, errorWorkflowStop);
    public static JobBuilderTimingExtensionModel TimingTriggerWhenDone(this JobBuilderModel jobBuilderExtension, Guid jobId, bool errorWorkflowStop)
        => TimingTriggerAdd(jobBuilderExtension.Job, jobId, errorWorkflowStop);
    private static JobBuilderTimingExtensionModel TimingTriggerAdd(JobDbo builder, Guid jobId, bool errorWorkflowStop)
    {
        var jobBuilderTimingExtension = new JobBuilderTimingExtensionModel();
        jobBuilderTimingExtension.Job = builder;
        jobBuilderTimingExtension.Job.Timing = new TimingModel
        {
            TimingType = TimingTypeEnum.Trigger,
            ErrorWorkflowStop = errorWorkflowStop,
            TriggerJobId = jobId
        };

        return jobBuilderTimingExtension;
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