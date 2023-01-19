using JoberMQ.Client.Net.Models.Routing;

namespace JoberMQ.Client.Net.Helpers
{
    internal class RoutingHelper
    {
        internal static RoutingModel GetRoutingSpecial(string clientKey) => new RoutingModel
        {
            ClientKey = clientKey
        };
        internal static RoutingModel GetRoutingSpecial(RoutingSpecialModel routingSpecial) => new RoutingModel
        {
            ClientKey = routingSpecial.ClientKey
        };

        internal static RoutingModel GetRoutingGroup(string clientGroupKey) => new RoutingModel
        {
            ClientGroupKey = clientGroupKey
        };
        internal static RoutingModel GetRoutingGroup(RoutingGroupModel routingGroup) => new RoutingModel
        {
            ClientGroupKey = routingGroup.ClientGroupKey
        };

        internal static RoutingModel GetRoutingQueue(string queueKey) => new RoutingModel
        {
            QueueKey = queueKey,
        };
        internal static RoutingModel GetRoutingQueue(RoutingQueueModel routingQueue) => new RoutingModel
        {
            QueueKey = routingQueue.QueueKey,
        };

        internal static RoutingModel GetRoutingEvent(string eventName) => new RoutingModel
        {
            EventName = eventName,
        };
        internal static RoutingModel GetRoutingEvent(RoutingEventModel routingEvent) => new RoutingModel
        {
            EventName = routingEvent.EventName,
        };
    }
}
