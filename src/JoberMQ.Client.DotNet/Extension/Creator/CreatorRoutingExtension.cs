using JoberMQ.Client.DotNet.Constant;
using JoberMQ.Common.Models.Routing;

public static class CreatorRoutingExtension
{
    public static RoutingModel RoutingClient(this CreatorBuilderModel builder, string clientKey)
        => new RoutingModel
        {
            DistributorKey = ClientConst.DefaultDistributorDirectKey,
            RoutingKey = ClientConst.DefaultQueueClientKey,
            QueueKey = ClientConst.DefaultQueueClientKey,
            ClientKey = clientKey
        };
    public static RoutingModel RoutingQueue(this CreatorBuilderModel builder, string queueKey)
        => new RoutingModel
        {
            DistributorKey = ClientConst.DefaultDistributorDirectKey,
            RoutingKey = queueKey,
            QueueKey = queueKey
        };
    public static RoutingModel RoutingDistirbutor(this CreatorBuilderModel builder, string distributorKey, string queueKey)
        => new RoutingModel
        {
            DistributorKey = distributorKey,
            RoutingKey = queueKey
        };
}
