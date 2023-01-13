using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Models.Routing;

namespace JoberMQ.Client.Net.Operations
{
    internal class RoutingOperation
    {
        internal static RoutingModel GetRoutingSpecial(string clientKey) => new RoutingModel
        {
            RoutingType = RoutingTypeEnum.Special,
            ClientKey = clientKey
        };
        internal static RoutingModel GetRoutingSpecial(RoutingSpecialModel routingSpecial) => new RoutingModel
        {
            RoutingType = RoutingTypeEnum.Special,
            ClientKey = routingSpecial.ClientKey
        };

        internal static RoutingModel GetRoutingGroup(string clientGroupKey) => new RoutingModel
        {
            RoutingType = RoutingTypeEnum.Group,
            ClientGroupKey = clientGroupKey
        };
        internal static RoutingModel GetRoutingGroup(RoutingGroupModel routingGroup) => new RoutingModel
        {
            RoutingType = RoutingTypeEnum.Group,
            ClientGroupKey = routingGroup.ClientGroupKey
        };

        internal static RoutingModel GetRoutingQueue(string queueName, string key) => new RoutingModel
        {
            RoutingType = RoutingTypeEnum.QueueKey,
            QueueName = queueName,
            Key = key
        };
        internal static RoutingModel GetRoutingQueue(RoutingQueueKeyModel routingQueue) => new RoutingModel
        {
            RoutingType = RoutingTypeEnum.QueueKey,
            QueueName = routingQueue.QueueName,
            Key = routingQueue.Key
        };
    }
}
