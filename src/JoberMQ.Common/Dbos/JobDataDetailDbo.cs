using JoberMQ.Common.Database.Base;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Enums.Transport;
using System;

namespace JoberMQ.Common.Dbos
{
    internal class JobDataDetailDbo : DboPropertyGuidBase, IDboBase
    {
        #region CONSTRUCTOR
        public JobDataDetailDbo()
        {
            Name = "";
            Description = "";
            GeneralData = "";
            RoutingType = RoutingTypeEnum.Group;
        }
        #endregion

        #region 3 - GENERAL
        public string Name { get; set; }
        public string Description { get; set; }
        public string GeneralData { get; set; }
        #endregion

        #region 6 - MESSAGE
        public MessageTypeEnum MessageType { get; set; }
        public string Message { get; set; }
        #endregion

        #region 7 - CONSUMER
        public TransportTypeEnum TransportType { get; set; }
        public string EventName { get; set; }

        public RoutingTypeEnum RoutingType { get; set; } 
        public string ConsumerClientKey { get; set; }
        public string ConsumerClientGroupKey { get; set; }
        public string QueueName { get; set; }
        public string QueueKey { get; set; }
        #endregion

        #region 11 - CONSUMING
        public bool IsConsumingRetryPause { get; set; }
        public int ConsumingRetryMaxCount { get; set; }
        public int ConsumingRetryCounter { get; set; }
        #endregion

        #region 99 - CHILD PARENT
        //public JobDataDbo JobDataDbo { get; set; }
        public Guid? JobDataId { get; set; }
        #endregion
    }
}
