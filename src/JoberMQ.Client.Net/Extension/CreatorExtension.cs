using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Constant;
using JoberMQ.Client.Net.Factories.Message;
using JoberMQ.Common.Models;
using JoberMQ.Common.Models.Routing;
using System.Linq.Expressions;
using System;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Common.Models.Info;
using JoberMQ.Common.Enums.Priority;

public static class CreatorExtension
{
    public static CreatorExtensionModel<IClient> Creator(this IClient client)
        => new CreatorExtensionModel<IClient> { Client = client };


    #region Routing
    public static RoutingModel RoutingClient(this CreatorExtensionModel<IClient> creatorExtensionModel, string clientKey)
        => new RoutingModel
        {
            DistributorKey = ClientConst.DefaultDistributorDirectKey,
            RoutingKey = ClientConst.DefaultQueueClientKey,
            QueueKey = ClientConst.DefaultQueueClientKey,
            ClientKey = clientKey
        };
    public static RoutingModel RoutingQueue(this CreatorExtensionModel<IClient> creatorExtensionModel, string queueKey)
        => new RoutingModel
        {
            DistributorKey = ClientConst.DefaultDistributorDirectKey,
            RoutingKey = queueKey,
            QueueKey = queueKey
        };
    public static RoutingModel RoutingDistirbutor(this CreatorExtensionModel<IClient> creatorExtensionModel, string distributorKey, string queueKey)
        => new RoutingModel
        {
            DistributorKey = distributorKey,
            RoutingKey = queueKey
        };
    #endregion


    #region Message
    public static IMessage Message(this CreatorExtensionModel<IClient> creatorExtensionModel, string message, RoutingModel routing, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
        => MessageFactory.Create(MessageTypeEnum.Text, message, routing, info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
    public static IMessage Message(this CreatorExtensionModel<IClient> creatorExtensionModel, Expression<Action> methodCall, RoutingModel routing, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
        => MessageFactory.Create(MessageTypeEnum.Funtion, creatorExtensionModel.Client.Method.MethodPropertySerialize(methodCall), routing, info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);

    public static IMessageRpc MessageRpc(this CreatorExtensionModel<IClient> creatorExtensionModel, string consumerId, string message)
        => MessageRpcFactory.Create(consumerId, MessageTypeEnum.Text, message);
    public static IMessageRpc MessageRpc(this CreatorExtensionModel<IClient> creatorExtensionModel, string consumerId, Expression<Action> methodCall)
        => MessageRpcFactory.Create(consumerId, MessageTypeEnum.Funtion, creatorExtensionModel.Client.Method.MethodPropertySerialize(methodCall));
    #endregion
}
