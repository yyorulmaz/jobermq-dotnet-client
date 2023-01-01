using JoberMQ.Common.Enums.Routing;
using System;

namespace JoberMQ.Common.Models.Started
{
    internal class JobStartedModel
    {
        public Guid JobMessageId { get; set; }
        public RoutingTypeEnum RoutingType { get; set; }
    }
}
