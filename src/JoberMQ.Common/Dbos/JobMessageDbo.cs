using JoberMQ.Common.Database.Base;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Enums.Transport;
using System;

namespace JoberMQ.Common.Dbos
{
    internal class JobMessageDbo : DboPropertyGuidBase, IDboBase
    {
        #region CONSTRUCTOR
        public JobMessageDbo()
        {
        }
        #endregion

        #region 1 - PRODUCER
        public string ProducerClientId { get; set; }
        public string ProducerClientGroupKey { get; set; }
        #endregion

        #region 3 - GENERAL
        public string Name { get; set; }
        public string Description { get; set; }
        public string GeneralData { get; set; }
        #endregion

        #region 6 - MESSAGE
        public MessageTypeEnum JobMessageType { get; set; }
        public string Message { get; set; }
        #endregion

        #region 7 - CONSUMER
        public TransportTypeEnum JobTransportType { get; set; }
        public string EventName { get; set; }

        public RoutingTypeEnum RoutingType { get; set; }
        public string ConsumerClientId { get; set; }
        public string ConsumerClientGroupKey { get; set; }
        public string QueueName { get; set; }
        public string QueueKey { get; set; }
        #endregion

        #region 9 - STATUS
        public bool IsError { get; set; }
        public StatusTypeJobMessageEnum StatusTypeJobMessage { get; set; }
        public DateTime? TempAgainDate { get; set; }
        #endregion

        #region 10 - CLONE CREATED
        public Guid? CreatedJobDataId { get; set; }
        public Guid? CreatedJobDataDetailId { get; set; }
        public Guid? CreatedJobId { get; set; }
        public Guid? CreatedJobDetailId { get; set; }
        #endregion

        #region 11 - CONSUMING
        public string ConsumingConsumerId { get; set; }
        public string ConsumingConsumerGroupKey { get; set; }
        public bool IsConsumingRetryPause { get; set; }
        public int ConsumingRetryMaxCount { get; set; }
        public int ConsumingRetryCounter { get; set; }
        #endregion

        #region 12 - GROUP
        public Guid? TriggerGroupsId { get; set; }
        public Guid? EventGroupsId { get; set; }
        #endregion

        #region 13 - MESSAGE INFO
        public bool IsErrorMessageClientSend { get; set; }
        #endregion
    }
}
