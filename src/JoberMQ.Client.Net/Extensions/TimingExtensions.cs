using JoberMQ.Client.Common.Enums.Timing;
using JoberMQ.Client.Common.Models.Builder;

namespace JoberMQ.Client.Net.Extensions
{
    public static class TimingExtensions
    {
        // Now
        public static JobBuilderTimingModel Now(this JobBuilderMessageDataModel builderData) 
            => NowAdd(builderData.JobBuilder);
        private static JobBuilderTimingModel NowAdd(JobBuilderModel builderData)
        {
            var builder = new JobBuilderTimingModel();
            builder.JobBuilder = builderData;
            builder.JobBuilder.TimingType = TimingTypeEnum.Now;
            return builder;
        }


        // Schedule
        public static JobBuilderTimingModel ScheduleDelayed(this JobBuilderMessageDataModel builderData, int delayedSecond) 
            => ScheduleAdd(builderData.JobBuilder, ScheduleTypeEnum.Delayed, delayedSecond, null, 1);
        public static JobBuilderTimingModel ScheduleRecurrent(this JobBuilderMessageDataModel builderData, string cronTime, int? executeCountMax = null) 
            => ScheduleAdd(builderData.JobBuilder, ScheduleTypeEnum.Recurrent, null, cronTime, executeCountMax);
        private static JobBuilderTimingModel ScheduleAdd(JobBuilderModel builderData, ScheduleTypeEnum jobScheduleType, int? delayedSecond, string cronTime, int? executeCountMax = null)
        {
            var builder = new JobBuilderTimingModel();
            builder.JobBuilder = builderData;
            builder.JobBuilder.TimingType = TimingTypeEnum.Schedule;
            builder.JobBuilder.ScheduleType = jobScheduleType;
            builder.JobBuilder.DelayedSecond = delayedSecond;
            builder.JobBuilder.CronTime = cronTime;
            builder.JobBuilder.ExecuteCountMax = executeCountMax;
            return builder;
        }


        // Trigger
        public static JobBuilderTimingModel TriggerWhenDone(this JobBuilderMessageDataModel builderData, Guid jobId, bool errorWorkflowStop) 
            => TriggerAdd(builderData.JobBuilder, jobId, errorWorkflowStop);
        private static JobBuilderTimingModel TriggerAdd(JobBuilderModel builderData, Guid jobId, bool errorWorkflowStop)
        {
            var builder = new JobBuilderTimingModel();
            builder.JobBuilder = builderData;
            builder.JobBuilder.TimingType = TimingTypeEnum.Trigger;
            builder.JobBuilder.ErrorWorkflowStop = errorWorkflowStop;
            builder.JobBuilder.TriggerJobId = jobId;
            return builder;
        }
    }
}
