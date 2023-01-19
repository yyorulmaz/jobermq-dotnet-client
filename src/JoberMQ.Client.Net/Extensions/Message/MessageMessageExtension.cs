using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.Multiple;

namespace JoberMQ.Client.Net.Extensions.Message
{
    public static class MessageMessageExtension
    {
        public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, IMessage message, IMessage resultMessage = null)
            => Add(messageBuilder.Builder, message, resultMessage);

        private static MessageBuilderMessageModel Add(BuilderModel builder, IMessage message, IMessage resultMessage = null)
        {
            var multipleMessage = new MultipleMessageModel();
            multipleMessage.Message = new Models.Message.MessageModel
            {
                MessageType = message.MessageType,
                Message = message.Message,
                Routing = message.Routing,
                Info = message.Info,
                GeneralData = message.GeneralData,
                PriorityType = message.PriorityType
            };
            multipleMessage.ResultMessage = new Models.Message.MessageModel
            {
                MessageType = resultMessage.MessageType,
                Message = resultMessage.Message,
                Routing = resultMessage.Routing,
                Info = resultMessage.Info,
                GeneralData = resultMessage.GeneralData,
                PriorityType = resultMessage.PriorityType
            };

            builder.MultipleMessages.Add(multipleMessage);
            return new MessageBuilderMessageModel { Builder = builder };
        }
    }




    #region eski
    //public static class MessageMessageExtension
    //{
    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, string messageText, RoutingSpecialModel routingSpecial, InfoModel info = null)
    //        => MessageAdd(messageBuilder.Builder, messageText, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, string messageText, RoutingGroupModel routingGroup, InfoModel info = null)
    //        => MessageAdd(messageBuilder.Builder, messageText, RoutingHelper.GetRoutingGroup(routingGroup), info);
    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, string messageText, RoutingQueueModel routingQueue, InfoModel info = null)
    //        => MessageAdd(messageBuilder.Builder, messageText, RoutingHelper.GetRoutingQueue(routingQueue), info);
    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, string messageText, RoutingEventModel routingEvent, InfoModel info = null)
    //        => MessageAdd(messageBuilder.Builder, messageText, RoutingHelper.GetRoutingEvent(routingEvent), info);





    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, Expression<Action> methodCall, RoutingSpecialModel routingSpecial, InfoModel info = null)
    //        => MethodAdd(messageBuilder.Builder, methodCall, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, Expression<Action> methodCall, RoutingGroupModel routingGroup, InfoModel info = null)
    //        => MethodAdd(messageBuilder.Builder, methodCall, RoutingHelper.GetRoutingGroup(routingGroup), info);
    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, Expression<Action> methodCall, RoutingQueueModel routingQueue, InfoModel info = null)
    //        => MethodAdd(messageBuilder.Builder, methodCall, RoutingHelper.GetRoutingQueue(routingQueue), info);
    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, Expression<Action> methodCall, RoutingEventModel routingEvent, InfoModel info = null)
    //        => MethodAdd(messageBuilder.Builder, methodCall, RoutingHelper.GetRoutingEvent(routingEvent), info);





















    //    private static MessageBuilderMessageModel MessageAdd(BuilderModel builder, string messageText, RoutingModel routing, InfoModel info = null)
    //        => Add(builder, messageText, null, routing, info);


    //    private static MessageBuilderMessageModel MethodAdd(BuilderModel builder, Expression<Action> methodCall, RoutingModel routing, InfoModel info = null)
    //        => Add(builder, null, methodCall, routing, info);


    //    private static MessageBuilderMessageModel Add(BuilderModel builder, string messageText, Expression<Action> methodCall, RoutingModel routing, InfoModel info = null)
    //    {
    //        if (messageText != null)
    //            builder.MultipleMessages.Add(new MultipleMessageModel { Message = messageText, Routing = routing, Info = info });
    //        else
    //            builder.MultipleMethods.Add(new MultipleMethodModel { MethodCall = methodCall, Routing = routing, Info = info });

    //        return new MessageBuilderMessageModel { Builder = builder };

    //    }
    //} 
    #endregion
}
