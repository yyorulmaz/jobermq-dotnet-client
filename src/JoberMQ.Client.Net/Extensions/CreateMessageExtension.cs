using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Enums.Priority;
using JoberMQ.Client.Net.Factories.Message;
using JoberMQ.Client.Net.Helpers;
using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.Routing;
using System.Linq.Expressions;
using System;
using JoberMQ.Client.Net.Enums.Message;

namespace JoberMQ.Client.Net.Extensions
{
    public static class CreateMessageExtension
    {
        public static IMessage CreateMessage(this IClient client, string message, RoutingSpecialModel routingSpecial, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None)
            => MessageFactory.Create(MessageTypeEnum.Text, message, RoutingHelper.GetRoutingSpecial(routingSpecial), info, generalData, priorityType);
        public static IMessage CreateMessage(this IClient client, string message, RoutingGroupModel routingGroup, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None)
            => MessageFactory.Create(MessageTypeEnum.Text, message, RoutingHelper.GetRoutingGroup(routingGroup), info, generalData, priorityType);
        public static IMessage CreateMessage(this IClient client, string message, RoutingQueueModel routingQueue, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None)
            => MessageFactory.Create(MessageTypeEnum.Text, message, RoutingHelper.GetRoutingQueue(routingQueue), info, generalData, priorityType);
        public static IMessage CreateMessage(this IClient client, string message, RoutingEventModel routingEvent, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None)
            => MessageFactory.Create(MessageTypeEnum.Text, message, RoutingHelper.GetRoutingEvent(routingEvent), info, generalData, priorityType);


        public static IMessage CreateMessage(this IClient client, Expression<Action> methodCall, RoutingSpecialModel routingSpecial, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None)
            => MessageFactory.Create(MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall), RoutingHelper.GetRoutingSpecial(routingSpecial), info, generalData, priorityType);
        public static IMessage CreateMessage(this IClient client, Expression<Action> methodCall, RoutingGroupModel routingGroup, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None)
            => MessageFactory.Create(MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall), RoutingHelper.GetRoutingGroup(routingGroup), info, generalData, priorityType);
        public static IMessage CreateMessage(this IClient client, Expression<Action> methodCall, RoutingQueueModel routingQueue, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None)
            => MessageFactory.Create(MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall), RoutingHelper.GetRoutingQueue(routingQueue), info, generalData, priorityType);
        public static IMessage CreateMessage(this IClient client, Expression<Action> methodCall, RoutingEventModel routingEvent, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None)
            => MessageFactory.Create(MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall), RoutingHelper.GetRoutingEvent(routingEvent), info, generalData, priorityType);
    }
}