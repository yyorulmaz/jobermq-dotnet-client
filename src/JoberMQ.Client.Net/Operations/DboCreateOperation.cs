using JoberMQ.Client.Common.Database.Helper;
using JoberMQ.Client.Common.Dbos;
using JoberMQ.Client.Common.Enums.Message;
using JoberMQ.Client.Common.Enums.Routing;
using JoberMQ.Client.Common.Enums.Status;
using JoberMQ.Client.Common.Enums.Timing;
using JoberMQ.Client.Common.Models.Builder;
using JoberMQ.Client.Common.Models.Routing;
using JoberMQ.Client.Common.Helpers;

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
            jobDataDbo.ProducerClientId = jobBuilderData.ProducerClientId;
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
                jobDataDetail.ConsumerClientId = item.Routing.ClientId;
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
                jobDataDetail.ConsumerClientId = item.Routing.ClientId;
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
        internal static JobDbo JobDboCreate(JobDataDbo jobDataDbo)
        {
            var jobDbo = new JobDbo();
            jobDbo.Details = new List<JobDetailDbo>();

            #region 0 - BASE
            jobDbo.Id = Guid.NewGuid();
            #endregion

            #region 5 - TRIGGER
            jobDbo.IsTrigger = jobDataDbo.IsTrigger;
            jobDbo.ErrorWorkflowStop = jobDataDbo.ErrorWorkflowStop;
            jobDbo.TriggerJobId = jobDataDbo.TriggerJobId;
            jobDbo.IsTriggerMain = jobDataDbo.IsTriggerMain;
            #endregion

            #region 7 - CHILD
            foreach (var item in jobDataDbo.Details)
            {
                var jobDetailDbo = new JobDetailDbo();

                #region 0 - BASE
                jobDetailDbo.Id = Guid.NewGuid();
                #endregion

                #region 7 - CHILD
                jobDetailDbo.JobId = jobDbo.Id;
                #endregion

                #region 11 - CLONE CREATED
                jobDetailDbo.CreatedJobDataDetailId = item.Id;
                #endregion

                jobDbo.Details.Add(jobDetailDbo);
            }
            #endregion

            #region 10 - STATUS
            jobDbo.IsCompleted = false;
            jobDbo.IsError = false;
            #endregion

            #region 11 - CLONE CREATED 
            jobDbo.Version = jobDataDbo.Version;
            jobDbo.CreatedJobDataId = jobDataDbo.Id;
            #endregion

            #region 13 - GROUP
            jobDbo.TriggerGroupsId = jobDataDbo.TriggerGroupsId;
            #endregion

            return jobDbo;
        }
        internal static JobMessageDbo JobMessageDboCreate(JobDataDbo jobDataDbo, JobDataDetailDbo jobDataDetailDbo, JobDbo jobDbo, JobDetailDbo jobDetailDbo, Guid? eventGroupId)
        {
            var jobMessageDbo = new JobMessageDbo();

            #region 0 - BASE
            jobMessageDbo.Id = Guid.NewGuid();
            #endregion

            #region 1 - PRODUCER
            jobMessageDbo.ProducerClientId = jobDataDbo.ProducerClientId;
            jobMessageDbo.ProducerClientGroupKey = jobDataDbo.ProducerClientGroupKey;
            #endregion

            #region 3 - GENERAL
            jobMessageDbo.Name = jobDataDetailDbo.Name;
            jobMessageDbo.Description = jobDataDetailDbo.Description;
            jobMessageDbo.GeneralData = jobDataDetailDbo.GeneralData;
            #endregion

            #region 6 - MESSAGE
            jobMessageDbo.JobMessageType = jobDataDetailDbo.MessageType;
            jobMessageDbo.Message = jobDataDetailDbo.Message;
            #endregion

            #region 8 - CONSUMER
            jobMessageDbo.JobTransportType = jobDataDetailDbo.TransportType;
            jobMessageDbo.EventName = jobDataDetailDbo.EventName;

            jobMessageDbo.RoutingType = jobDataDetailDbo.RoutingType;
            jobMessageDbo.ConsumerClientId = jobDataDetailDbo.ConsumerClientId;
            jobMessageDbo.ConsumerClientGroupKey = jobDataDetailDbo.ConsumerClientGroupKey;
            jobMessageDbo.QueueName = jobDataDetailDbo.QueueName;
            jobMessageDbo.QueueKey = jobDataDetailDbo.QueueKey;
            #endregion

            #region 10 - STATUS
            jobMessageDbo.IsError = false;
            jobMessageDbo.StatusTypeJobMessage = StatusTypeJobMessageEnum.None;
            #endregion

            #region 11 - CLONE CREATED
            jobMessageDbo.CreatedJobDataId = jobDataDbo.Id;
            jobMessageDbo.CreatedJobDataDetailId = jobDataDetailDbo.Id;
            jobMessageDbo.CreatedJobId = jobDbo.Id;
            jobMessageDbo.CreatedJobDetailId = jobDetailDbo.Id;
            #endregion

            #region 13 - GROUP
            jobMessageDbo.TriggerGroupsId = jobDbo.TriggerGroupsId;
            jobMessageDbo.EventGroupsId = eventGroupId;
            #endregion

            return jobMessageDbo;
        }
        internal static JobMessageLogDbo JobMessageLogDboCreate(Guid jobMessageId, string message, bool isError)
        {
            var jobLog = new JobMessageLogDbo();

            #region 7 - CHILD
            jobLog.JobMessageId = jobMessageId;
            #endregion

            #region 10 - STATUS
            jobLog.IsError = isError;
            #endregion

            #region 14 - MESSAGE INFO
            jobLog.Message = message;
            jobLog.MessageDate = DateHelper.GetUniversalNow();
            #endregion

            return jobLog;
        }
        internal static ErrorMessageDbo ErrorMessageDboCreate(JobDataDbo jobDataDbo, string errorMessage)
        {
            var errorMessageDbo = new ErrorMessageDbo();

            #region 0 - BASE
            errorMessageDbo.Id = Guid.NewGuid();
            #endregion

            #region 7 - CHILD
            errorMessageDbo.ErrorJobDataId = jobDataDbo.Id;
            #endregion

            #region 9 - CONSUMER ERROR
            var routing = new RoutingModel();
            if (jobDataDbo.ErrorMessageRoutingType == RoutingTypeEnum.Special)
                routing = RoutingOperation.GetRoutingSpecial(jobDataDbo.ProducerClientId);
            else if (jobDataDbo.ErrorMessageRoutingType == RoutingTypeEnum.Group)
                routing = RoutingOperation.GetRoutingGroup(jobDataDbo.ProducerClientId);

            errorMessageDbo.RoutingType = routing.RoutingType;
            errorMessageDbo.ConsumerClientId = routing.ClientId;
            errorMessageDbo.ConsumerClientGroupKey = routing.ClientGroupKey;
            errorMessageDbo.QueueName = routing.QueueName;
            errorMessageDbo.QueueKey = routing.Key;
            #endregion

            #region 14 - MESSAGE INFO
            errorMessageDbo.ErrorMessage = errorMessage;
            #endregion

            return errorMessageDbo;
        }
    }
}
