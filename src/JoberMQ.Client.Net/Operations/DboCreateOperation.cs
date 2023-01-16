using JoberMQ.Client.Net.Helpers;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Timing;
using JoberMQ.Common.Models.Builder;

namespace JoberMQ.Client.Net.Operations
{
    internal class DboCreateOperation
    {
        internal static JobDbo JobDataDboCreate(JobBuilderModel jobBuilderData)
        {
            var jobDbo = new JobDbo();
            jobDbo.Details = new List<JobDetailDbo>();

            #region 0 - BASE
            jobDbo.Id = Guid.NewGuid();
            #endregion

            #region 1 - PRODUCER
            jobDbo.ProducerClientKey = jobBuilderData.ProducerClientKey;
            jobDbo.ProducerClientGroupKey = jobBuilderData.ProducerClientGroupKey;
            #endregion

            #region 2 - RUN
            jobDbo.PublisherType = jobBuilderData.PublisherType;
            #endregion

            #region 3 - GENERAL
            if (jobBuilderData.Option != null)
            {
                jobDbo.Name = jobBuilderData.Option.Name;
                jobDbo.Description = jobBuilderData.Option.Description;
                jobDbo.GeneralData = jobBuilderData.Option.GeneralData;
            }
            #endregion

            #region 4 - TIMING
            jobDbo.TimingType = jobBuilderData.TimingType;
            jobDbo.ScheduleType = jobBuilderData.ScheduleType;

            if (jobBuilderData.TimingType == TimingTypeEnum.Schedule && jobBuilderData.ScheduleType == ScheduleTypeEnum.Delayed)
                jobDbo.CronTime = CronHelper.DateToCron(DateTime.Now.AddSeconds(jobBuilderData.DelayedSecond.Value));
            else if (jobBuilderData.TimingType == TimingTypeEnum.Schedule && jobBuilderData.ScheduleType == ScheduleTypeEnum.Recurrent)
                jobDbo.CronTime = jobBuilderData.CronTime;
            else
                jobDbo.CronTime = null;

            jobDbo.ExecuteCountMax = jobBuilderData.ExecuteCountMax;
            jobDbo.CreatedCount = 0;
            jobDbo.IsCountMax = false;
            #endregion

            #region 5 - TRIGGER
            jobDbo.IsTrigger = false;
            jobDbo.ErrorWorkflowStop = jobBuilderData.ErrorWorkflowStop;
            jobDbo.TriggerJobId = jobBuilderData.TriggerJobId;
            jobDbo.TriggerGroupsId = jobBuilderData.TriggerGroupsId;
            #endregion

            #region 5 - CHILD
            foreach (var item in jobBuilderData.MultipleMethods)
            {
                var jobDetail = new JobDetailDbo();

                #region 0 - BASE
                jobDetail.Id = Guid.NewGuid();
                #endregion

                #region 3 - GENERAL
                if (item.Option != null)
                {
                    jobDetail.Name = item.Option.Name;
                    jobDetail.Description = item.Option.Description;
                    jobDetail.GeneralData = item.Option.GeneralData;
                }
                #endregion

                #region 6 - MESSAGE
                jobDetail.MessageType = MessageTypeEnum.Method;
                jobDetail.Message = MethodHelper.MethodPropertySerialize(item.MethodCall);
                #endregion

                #region 7 - CHILD
                jobDetail.JobDataId = jobDbo.Id;
                #endregion

                #region 8 - CONSUMER
                jobDetail.TransportType = item.JobTransportType;
                jobDetail.EventName = item.EventName;
                jobDetail.RoutingType = item.Routing.RoutingType;
                jobDetail.ConsumerClientKey = item.Routing.ClientKey;
                jobDetail.ConsumerClientGroupKey = item.Routing.ClientGroupKey;
                jobDetail.QueueName = item.Routing.QueueName;
                jobDetail.QueueKey = item.Routing.Key;
                #endregion

                jobDbo.Details.Add(jobDetail);
            }

            foreach (var item in jobBuilderData.MultipleMessages)
            {
                var jobDetail = new JobDetailDbo();

                #region 0 - BASE
                jobDetail.Id = Guid.NewGuid();
                #endregion

                #region 3 - GENERAL
                if (item.Option != null)
                {
                    jobDetail.Name = item.Option.Name;
                    jobDetail.Description = item.Option.Description;
                    jobDetail.GeneralData = item.Option.GeneralData;
                }
                #endregion

                #region 6 - MESSAGE
                jobDetail.MessageType = MessageTypeEnum.Text;
                jobDetail.Message = item.Message;
                #endregion

                #region 7 - CHILD
                jobDetail.JobDataId = jobDbo.Id;
                #endregion

                #region 8 - CONSUMER
                jobDetail.TransportType = item.JobTransportType;
                jobDetail.EventName = item.EventName;
                jobDetail.RoutingType = item.Routing.RoutingType;
                jobDetail.ConsumerClientKey = item.Routing.ClientKey;
                jobDetail.ConsumerClientGroupKey = item.Routing.ClientGroupKey;
                jobDetail.QueueName = item.Routing.QueueName;
                jobDetail.QueueKey = item.Routing.Key;
                #endregion

                jobDbo.Details.Add(jobDetail);
            }
            #endregion

            #region 9 - CONSUMER ERROR
            jobDbo.ErrorMessageRoutingType = jobBuilderData.ErrorMessageRoutingType;
            #endregion

            #region 10 - STATUS
            jobDbo.IsCompleted = false;
            jobDbo.IsError = false;
            #endregion

            return jobDbo;
        }
    }
}
