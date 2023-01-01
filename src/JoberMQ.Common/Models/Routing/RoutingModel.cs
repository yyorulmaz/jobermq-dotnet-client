using JoberMQ.Common.Enums.Routing;

namespace JoberMQ.Common.Models.Routing
{
    public class RoutingModel
    {
        public RoutingTypeEnum RoutingType { get; set; } = RoutingTypeEnum.Group;
        public string ClientId { get; set; }
        public string ClientGroupKey { get; set; }
        public string QueueName { get; set; }
        public string Key { get; set; }
    }
}
