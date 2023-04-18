using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Constants;
using JoberMQ.Client.Net.Extensions.Routing;
using JoberMQ.Client.Net.Factories.Message;
using JoberMQ.Library.Enums.Message;
using JoberMQ.Library.Enums.Priority;
using JoberMQ.Library.Models;
using JoberMQ.Library.Models.Routing;
using System;
using System.Linq.Expressions;

namespace JoberMQ.Client.Net.Extensions
{
    public static class CreateMessageExtension
    {
        public static IMessage CreateMessage(this IClient client, string message, RoutingSpecialModel routingSpecial, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
            => MessageFactory.Create(MessageTypeEnum.Text, message, client.CreateRouting(routingSpecial), info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
        public static IMessage CreateMessage(this IClient client, string message, RoutingGroupModel routingGroup, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
            => MessageFactory.Create(MessageTypeEnum.Text, message, client.CreateRouting(routingGroup), info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
        public static IMessage CreateMessage(this IClient client, string message, RoutingQueueModel routingQueue, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
            => MessageFactory.Create(MessageTypeEnum.Text, message, client.CreateRouting(routingQueue), info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
        public static IMessage CreateMessage(this IClient client, string message, RoutingEventModel routingEvent, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
            => MessageFactory.Create(MessageTypeEnum.Text, message, client.CreateRouting(routingEvent), info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
        public static IMessage CreateMessage(this IClient client, string message, RoutingFilterModel routingFilter, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
            => MessageFactory.Create(MessageTypeEnum.Text, message, client.CreateRouting(routingFilter), info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);


        public static IMessage CreateMessage(this IClient client, Expression<Action> methodCall, RoutingSpecialModel routingSpecial, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
            => MessageFactory.Create(MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall), client.CreateRouting(routingSpecial), info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
        public static IMessage CreateMessage(this IClient client, Expression<Action> methodCall, RoutingGroupModel routingGroup, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
            => MessageFactory.Create(MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall), client.CreateRouting(routingGroup), info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
        public static IMessage CreateMessage(this IClient client, Expression<Action> methodCall, RoutingQueueModel routingQueue, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
            => MessageFactory.Create(MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall), client.CreateRouting(routingQueue), info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
        public static IMessage CreateMessage(this IClient client, Expression<Action> methodCall, RoutingEventModel routingEvent, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
            => MessageFactory.Create(MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall), client.CreateRouting(routingEvent), info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
        public static IMessage CreateMessage(this IClient client, Expression<Action> methodCall, RoutingFilterModel routingFilter, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
            => MessageFactory.Create(MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall), client.CreateRouting(routingFilter), info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);


        public static IMessageRpc CreateMessage(this IClient client, string consumerId, string message)
            => MessageRpcFactory.Create(consumerId, MessageTypeEnum.Text, message);
        public static IMessageRpc CreateMessage(this IClient client, string consumerId, Expression<Action> methodCall)
            => MessageRpcFactory.Create(consumerId, MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall));

    }
}
