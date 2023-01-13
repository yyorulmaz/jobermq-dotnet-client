using JoberMQ.Client.Net.Helpers;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Timing;
using JoberMQ.Common.Models.Builder;

namespace JoberMQ.Client.Net.Operations
{
    internal class DboCreateOperation
    {
        internal static JobDataDbo JobDataDboCreate(JobBuilderModel jobBuilderData)
        {
            var jobDataDbo = new JobDataDbo();
            jobDataDbo.Details = new List<JobDataDetailDbo>();

            #region 0 - BASE
            jobDataDbo.Id = Guid.NewGuid();
            #endregion

            #region 1 - PRODUCER
            jobDataDbo.ProducerClientKey = jobBuilderData.ProducerClientKey;
            jobDataDbo.ProducerClientGroupKey = jobBuilderData.ProducerClientGroupKey;
            #endregion

            #region 2 - RUN
            jobDataDbo.RunType = jobBuilderData.JobRunType;
            #endregion

            #region 3 - GENERAL
            if (jobBuilderData.Option != null)
            {
                jobDataDbo.Name = jobBuilderData.Option.Name;
                jobDataDbo.Description = jobBuilderData.Option.Description;
                jobDataDbo.GeneralData = jobBuilderData.Option.GeneralData;
            }
            #endregion

            #region 4 - TIMING
            jobDataDbo.TimingType = jobBuilderData.TimingType;
            jobDataDbo.ScheduleType = jobBuilderData.ScheduleType;

            if (jobBuilderData.TimingType == TimingTypeEnum.Schedule && jobBuilderData.ScheduleType == ScheduleTypeEnum.Delayed)
                jobDataDbo.CronTime = CronHelper.DateToCron(DateTime.Now.AddSeconds(jobBuilderData.DelayedSecond.Value));
            else if (jobBuilderData.TimingType == TimingTypeEnum.Schedule && jobBuilderData.ScheduleType == ScheduleTypeEnum.Recurrent)
                jobDataDbo.CronTime = jobBuilderData.CronTime;
            else
                jobDataDbo.CronTime = null;

            jobDataDbo.ExecuteCountMax = jobBuilderData.ExecuteCountMax;
            jobDataDbo.CreatedCount = 0;
            jobDataDbo.IsCountMax = false;
            #endregion

            #region 5 - TRIGGER
            jobDataDbo.IsTrigger = false;
            jobDataDbo.ErrorWorkflowStop = jobBuilderData.ErrorWorkflowStop;
            jobDataDbo.TriggerJobId = jobBuilderData.TriggerJobId;
            jobDataDbo.TriggerGroupsId = jobBuilderData.TriggerGroupsId;
            #endregion

            #region 5 - CHILD
            foreach (var item in jobBuilderData.MultipleMethods)
            {
                var jobDataDetail = new JobDataDetailDbo();

                #region 0 - BASE
                jobDataDetail.Id = Guid.NewGuid();
                #endregion

                #region 3 - GENERAL
                if (item.Option != null)
                {
                    jobDataDetail.Name = item.Option.Name;
                    jobDataDetail.Description = item.Option.Description;
                    jobDataDetail.GeneralData = item.Option.GeneralData;
                }
                #endregion

                #region 6 - MESSAGE
                jobDataDetail.MessageType = MessageTypeEnum.Method;
                jobDataDetail.Message = MethodHelper.MethodPropertySerialize(item.MethodCall);
                #endregion

                #region 7 - CHILD
                jobDataDetail.JobDataId = jobDataDbo.Id;
                #endregion

                #region 8 - CONSUMER
                jobDataDetail.TransportType = item.JobTransportType;
                jobDataDetail.EventName = item.EventName;
                jobDataDetail.RoutingType = item.Routing.RoutingType;
                jobDataDetail.ConsumerClientKey = item.Routing.ClientKey;
                jobDataDetail.ConsumerClientGroupKey = item.Routing.ClientGroupKey;
                jobDataDetail.QueueName = item.Routing.QueueName;
                jobDataDetail.QueueKey = item.Routing.Key;
                #endregion

                jobDataDbo.Details.Add(jobDataDetail);
            }

            foreach (var item in jobBuilderData.MultipleMessages)
            {
                var jobDataDetail = new JobDataDetailDbo();

                #region 0 - BASE
                jobDataDetail.Id = Guid.NewGuid();
                #endregion

                #region 3 - GENERAL
                if (item.Option != null)
                {
                    jobDataDetail.Name = item.Option.Name;
                    jobDataDetail.Description = item.Option.Description;
                    jobDataDetail.GeneralData = item.Option.GeneralData;
                }
                #endregion

                #region 6 - MESSAGE
                jobDataDetail.MessageType = MessageTypeEnum.Text;
                jobDataDetail.Message = item.Message;
                #endregion

                #region 7 - CHILD
                jobDataDetail.JobDataId = jobDataDbo.Id;
                #endregion

                #region 8 - CONSUMER
                jobDataDetail.TransportType = item.JobTransportType;
                jobDataDetail.EventName = item.EventName;
                jobDataDetail.RoutingType = item.Routing.RoutingType;
                jobDataDetail.ConsumerClientKey = item.Routing.ClientKey;
                jobDataDetail.ConsumerClientGroupKey = item.Routing.ClientGroupKey;
                jobDataDetail.QueueName = item.Routing.QueueName;
                jobDataDetail.QueueKey = item.Routing.Key;
                #endregion

                jobDataDbo.Details.Add(jobDataDetail);
            }
            #endregion

            #region 9 - CONSUMER ERROR
            jobDataDbo.ErrorMessageRoutingType = jobBuilderData.ErrorMessageRoutingType;
            #endregion

            #region 10 - STATUS
            jobDataDbo.IsCompleted = false;
            jobDataDbo.IsError = false;
            #endregion

            return jobDataDbo;
        }
    }
}
