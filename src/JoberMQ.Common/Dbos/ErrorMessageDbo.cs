using JoberMQ.Common.Database.Base;
using JoberMQ.Common.Enums.Routing;
using System;

namespace JoberMQ.Common.Dbos
{
    public class ErrorMessageDbo : DboPropertyGuidBase, IDboBase
    {
        #region CONSTRUCTOR
        public ErrorMessageDbo()
        {
        }
        #endregion

        #region 7 - CHILD
        public Guid ErrorJobDataId { get; set; }
        public Guid ErrorJobId { get; set; }
        public Guid ErrorJobMessageId { get; set; }
        #endregion

        #region 8 - CONSUMER ERROR
        internal RoutingTypeEnum RoutingType { get; set; }
        internal string ConsumerClientId { get; set; }
        internal string ConsumerClientGroupKey { get; set; }
        internal string QueueName { get; set; }
        internal string QueueKey { get; set; }
        #endregion

        #region 13 - MESSAGE INFO
        public string ErrorMessage { get; set; }
        #endregion
    }
}
