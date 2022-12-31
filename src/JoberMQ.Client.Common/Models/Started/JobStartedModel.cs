using JoberMQ.Client.Common.Enums.Routing;

namespace JoberMQ.Client.Common.Models.Started
{
    internal class JobStartedModel
    {
        public Guid JobMessageId { get; set; }
        public RoutingTypeEnum RoutingType { get; set; }
    }
}
