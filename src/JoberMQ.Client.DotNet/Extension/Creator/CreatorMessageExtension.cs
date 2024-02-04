using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Constant;
using JoberMQ.Client.DotNet.Factory;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Priority;
using JoberMQ.Common.Models.Info;
using JoberMQ.Common.Models.Routing;
using System;
using System.Linq.Expressions;

public static class CreatorMessageExtension
{
    public static IMessage Message(this CreatorBuilderModel builder, string message, RoutingModel routing, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
        => MessageFactory.Create(MessageTypeEnum.Text, message, routing, info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);

    public static IMessage Message(this CreatorBuilderModel builder, Expression<Action> methodCall, RoutingModel routing, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
        => MessageFactory.Create(MessageTypeEnum.Funtion, builder.client.Method.MethodPropertySerialize(methodCall), routing, info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);
}