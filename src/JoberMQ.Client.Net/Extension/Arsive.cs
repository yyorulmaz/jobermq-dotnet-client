// ----------------------------------------------MessageBuildExtension.cs-------------------------------------------------------------------

//using JoberMQ.Common.Dbos;
//using JoberMQ.Common.Models.Message;

//namespace JoberMQ.Client.Net.Extension.Message
//{
//    public static class MessageBuildExtension
//    {
//        public static MessageDbo Build(this MessageBuilderMessageExtensionModel messageBuilderMessage) => messageBuilderMessage.Message;
//        public static MessageDbo Build(this MessageBuilderResultMessageExtensionModel messageBuilderResultMessage) => messageBuilderResultMessage.Message;
//    }
//}



// ----------------------------------------------MessageMessageExtension.cs-------------------------------------------------------------------

//using JoberMQ.Client.Net.Abstraction.Message;
//using JoberMQ.Client.Net.Constant;
//using JoberMQ.Common.Dbos;
//using JoberMQ.Common.Models.Message;

//namespace JoberMQ.Client.Net.Extension.Message
//{
//    public static class MessageMessageExtension
//    {
//        public static MessageBuilderMessageExtensionModel Message(this MessageBuilderModel messageBuilder, IMessage message, IMessage resultMessage = null, bool isDbTextSave = ClientConst.IsDbTextSave)
//            => Add(messageBuilder.Message, message, resultMessage, isDbTextSave);

//        private static MessageBuilderMessageExtensionModel Add(MessageDbo builder, IMessage message, IMessage resultMessage, bool isDbTextSave)
//        {
//            builder.IsDbTextSave = isDbTextSave;

//            var msg = new MessageModel
//            {
//                MessageType = message.MessageType,
//                Message = message.Message,
//                Routing = message.Routing,
//                Info = message.Info,
//                GeneralData = message.GeneralData,
//                PriorityType = message.PriorityType,
//                MessageConsuming = message.MessageConsuming,
//            };
//            builder.Message = msg;


//            if (resultMessage != null)
//            {
//                var resultMsg = new MessageModel
//                {
//                    MessageType = resultMessage.MessageType,
//                    Message = resultMessage.Message,
//                    Routing = resultMessage.Routing,
//                    Info = resultMessage.Info,
//                    GeneralData = resultMessage.GeneralData,
//                    PriorityType = resultMessage.PriorityType,
//                    MessageConsuming = resultMessage.MessageConsuming,
//                };

//                builder.IsResult = true;
//                builder.ResultMessage = resultMsg;

//            }
//            else
//            {
//                builder.IsResult = false;
//            }

//            return new MessageBuilderMessageExtensionModel { Message = builder };
//        }
//    }




//    #region eski
//    //public static class MessageMessageExtension
//    //{
//    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, string messageText, RoutingSpecialModel routingSpecial, InfoModel info = null)
//    //        => MessageAdd(messageBuilder.Builder, messageText, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
//    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, string messageText, RoutingGroupModel routingGroup, InfoModel info = null)
//    //        => MessageAdd(messageBuilder.Builder, messageText, RoutingHelper.GetRoutingGroup(routingGroup), info);
//    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, string messageText, RoutingQueueModel routingQueue, InfoModel info = null)
//    //        => MessageAdd(messageBuilder.Builder, messageText, RoutingHelper.GetRoutingQueue(routingQueue), info);
//    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, string messageText, RoutingEventModel routingEvent, InfoModel info = null)
//    //        => MessageAdd(messageBuilder.Builder, messageText, RoutingHelper.GetRoutingEvent(routingEvent), info);





//    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, Expression<Action> methodCall, RoutingSpecialModel routingSpecial, InfoModel info = null)
//    //        => MethodAdd(messageBuilder.Builder, methodCall, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
//    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, Expression<Action> methodCall, RoutingGroupModel routingGroup, InfoModel info = null)
//    //        => MethodAdd(messageBuilder.Builder, methodCall, RoutingHelper.GetRoutingGroup(routingGroup), info);
//    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, Expression<Action> methodCall, RoutingQueueModel routingQueue, InfoModel info = null)
//    //        => MethodAdd(messageBuilder.Builder, methodCall, RoutingHelper.GetRoutingQueue(routingQueue), info);
//    //    public static MessageBuilderMessageModel Message(this MessageBuilderModel messageBuilder, Expression<Action> methodCall, RoutingEventModel routingEvent, InfoModel info = null)
//    //        => MethodAdd(messageBuilder.Builder, methodCall, RoutingHelper.GetRoutingEvent(routingEvent), info);





















//    //    private static MessageBuilderMessageModel MessageAdd(BuilderModel builder, string messageText, RoutingModel routing, InfoModel info = null)
//    //        => Add(builder, messageText, null, routing, info);


//    //    private static MessageBuilderMessageModel MethodAdd(BuilderModel builder, Expression<Action> methodCall, RoutingModel routing, InfoModel info = null)
//    //        => Add(builder, null, methodCall, routing, info);


//    //    private static MessageBuilderMessageModel Add(BuilderModel builder, string messageText, Expression<Action> methodCall, RoutingModel routing, InfoModel info = null)
//    //    {
//    //        if (messageText != null)
//    //            builder.MultipleMessages.Add(new MultipleMessageModel { Message = messageText, Routing = routing, Info = info });
//    //        else
//    //            builder.MultipleMethods.Add(new MultipleMethodModel { MethodCall = methodCall, Routing = routing, Info = info });

//    //        return new MessageBuilderMessageModel { Builder = builder };

//    //    }
//    //} 
//    #endregion
//}


// ----------------------------------------------------MessageResultMessageExtension.cs-------------------------------------------------------------

//using JoberMQ.Client.Net.Abstraction.Message;
//using JoberMQ.Common.Dbos;
//using JoberMQ.Common.Models.Message;

//namespace JoberMQ.Client.Net.Extension.Message
//{
//    public static class MessageResultMessageExtension
//    {
//        public static MessageBuilderResultMessageExtensionModel ResultMessage(this MessageBuilderMessageExtensionModel messageBuilderMessageExtension, IMessage resultMessage)
//           => Add(messageBuilderMessageExtension.Message, resultMessage);

//        private static MessageBuilderResultMessageExtensionModel Add(MessageDbo builder, IMessage resultMessage = null)
//        {
//            builder.IsResult = true;

//            var message = new MessageModel()
//            {
//                MessageType = resultMessage.MessageType,
//                Message = resultMessage.Message,
//                Routing = resultMessage.Routing,
//                Info = resultMessage.Info,
//                GeneralData = resultMessage.GeneralData,
//                PriorityType = resultMessage.PriorityType
//            };
//            builder.ResultMessage = message;

//            return new MessageBuilderResultMessageExtensionModel { Message = builder };
//        }
//    }
//}


// ------------------------------------------------RoutingExtension.cs-----------------------------------------------------------------

//using JoberMQ.Client.Net.Abstraction.Client;
//using JoberMQ.Client.Net.Constant;
//using JoberMQ.Common.Enums.Routing;
//using JoberMQ.Common.Models.Routing;

//namespace JoberMQ.Client.Net.Extension.Routing
//{
//    public static class RoutingExtension
//    {
//        public static CreatorRoutingModel CreatorRouting(this IClient client)
//            => new CreatorRoutingModel();

//        public static RoutingModel Client(this CreatorRoutingModel creatorRouting, string clientKey)
//            => new RoutingModel
//            {
//                DistributorKey = ClientConst.DefaultDistributorDirectKey,
//                RoutingKey = ClientConst.DefaultQueueClientKey,
//                QueueKey = ClientConst.DefaultQueueClientKey,
//                ClientKey = clientKey
//            };

//        public static RoutingModel Queue(this CreatorRoutingModel creatorRouting, string queueKey)
//            => new RoutingModel
//            {
//                DistributorKey = ClientConst.DefaultDistributorDirectKey,
//                RoutingKey = queueKey,
//                QueueKey= queueKey
//            };
//        public static RoutingModel Distirbutor(this CreatorRoutingModel creatorRouting, string distributorKey, string queueKey)
//            => new RoutingModel
//            {
//                DistributorKey = distributorKey,
//                RoutingKey = queueKey
//            };
//    }
//}

// ------------------------------------------------CreateMessageExtension.cs-----------------------------------------------------------------

//using JoberMQ.Client.Net.Abstraction.Client;
//using JoberMQ.Client.Net.Abstraction.Message;
//using JoberMQ.Client.Net.Constant;
//using JoberMQ.Client.Net.Factories.Message;
//using JoberMQ.Common.Enums.Message;
//using JoberMQ.Common.Enums.Priority;
//using JoberMQ.Common.Models.Info;
//using JoberMQ.Common.Models.Routing;
//using System;
//using System.Linq.Expressions;

//namespace JoberMQ.Client.Net.Extensions
//{
//    public static class CreateMessageExtension
//    {
//        public static IMessage CreateMessage(this IClient client, string message, RoutingModel routing, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
//            => MessageFactory.Create(MessageTypeEnum.Text, message, routing, info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);

//        public static IMessage CreateMessage(this IClient client, Expression<Action> methodCall, RoutingModel routing, InfoModel info = null, string generalData = "", PriorityTypeEnum priorityType = PriorityTypeEnum.None, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
//            => MessageFactory.Create(MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall), routing, info, generalData, priorityType, isConsumingRetryPause, consumingRetryMaxCount);

//        public static IMessageRpc CreateMessageRpc(this IClient client, string consumerId, string message)
//            => MessageRpcFactory.Create(consumerId, MessageTypeEnum.Text, message);
//        public static IMessageRpc CreateMessageRpc(this IClient client, string consumerId, Expression<Action> methodCall)
//            => MessageRpcFactory.Create(consumerId, MessageTypeEnum.Funtion, client.Method.MethodPropertySerialize(methodCall));

//    }
//}

// -----------------------------------------------------------------------------------------------------------------



// -----------------------------------------------------------------------------------------------------------------