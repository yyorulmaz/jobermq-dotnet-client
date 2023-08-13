using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Constant;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Job;
using JoberMQ.Common.Models.Message;

public static class JobMessageExtension
{
    public static JobBuilderMessageExtensionModel Message(this JobBuilderModel jobBuilderExtension, IMessage message, IMessage resultMessage = null, bool isDbTextSave = ClientConst.IsDbTextSave)
        => Add(jobBuilderExtension.Job, message, resultMessage, isDbTextSave);
    //public static JobBuilderMessageExtensionModel Message(this JobBuilderPublisherExtensionModel jobBuilderPublisherExtension, IMessage message, IMessage resultMessage = null, bool isDbTextSave = ClientConst.IsDbTextSave)
    //    => Add(jobBuilderPublisherExtension.Job, message, resultMessage, isDbTextSave);
    //public static JobBuilderMessageExtensionModel Message(this JobBuilderTimingExtensionModel jobBuilderTimingExtension, IMessage message, IMessage resultMessage = null, bool isDbTextSave = ClientConst.IsDbTextSave)
    //    => Add(jobBuilderTimingExtension.Job, message, resultMessage, isDbTextSave);
    public static JobBuilderMessageExtensionModel Message(this JobBuilderMessageExtensionModel jobBuilderMessageExtension, IMessage message, IMessage resultMessage = null, bool isDbTextSave = ClientConst.IsDbTextSave)
        => Add(jobBuilderMessageExtension.Job, message, resultMessage, isDbTextSave);

    private static JobBuilderMessageExtensionModel Add(JobDbo builder, IMessage message, IMessage resultMessage, bool isDbTextSave)
    {
        builder.IsDbTextSave = isDbTextSave;

        var jobDetail = new JobDetailDbo();
        jobDetail.JobId = builder.Id;
        jobDetail.Message = new MessageModel
        {
            MessageType = message.MessageType,
            Message = message.Message,
            Routing = message.Routing,
            Info = message.Info,
            GeneralData = message.GeneralData,
            PriorityType = message.PriorityType,
            MessageConsuming = message.MessageConsuming,
        };
        jobDetail.IsDbTextSave = isDbTextSave;

        if (resultMessage != null)
        {
            jobDetail.ResultMessage = new MessageModel
            {
                MessageType = resultMessage.MessageType,
                Message = resultMessage.Message,
                Routing = resultMessage.Routing,
                Info = resultMessage.Info,
                GeneralData = resultMessage.GeneralData,
                PriorityType = resultMessage.PriorityType,
                MessageConsuming = resultMessage.MessageConsuming,
            };
        }

        builder.JobDetails.Add(jobDetail);
        return new JobBuilderMessageExtensionModel { Job = builder };
    }
}

#region eski
//public static class JobMessageExtension
//{
//    public static JobBuilderMessageModel Message(this JobBuilderModel jobBuilder, string messageText, RoutingSpecialModel routingSpecial, InfoModel info = null)
//        => MessageAdd(jobBuilder.Builder, messageText, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
//    public static JobBuilderMessageModel Message(this JobBuilderModel jobBuilder, string messageText, RoutingGroupModel routingGroup, InfoModel info = null)
//        => MessageAdd(jobBuilder.Builder, messageText, RoutingHelper.GetRoutingGroup(routingGroup), info);
//    public static JobBuilderMessageModel Message(this JobBuilderModel jobBuilder, string messageText, RoutingQueueModel routingQueue, InfoModel info = null)
//        => MessageAdd(jobBuilder.Builder, messageText, RoutingHelper.GetRoutingQueue(routingQueue), info);
//    public static JobBuilderMessageModel Message(this JobBuilderModel jobBuilder, string messageText, RoutingEventModel routingEvent, InfoModel info = null)
//       => MessageAdd(jobBuilder.Builder, messageText, RoutingHelper.GetRoutingEvent(routingEvent), info);

//    public static JobBuilderMessageModel Message(this JobBuilderPublisherModel jobBuilderPublisher, string messageText, RoutingSpecialModel routingSpecial, InfoModel info = null)
//        => MessageAdd(jobBuilderPublisher.Builder, messageText, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
//    public static JobBuilderMessageModel Message(this JobBuilderPublisherModel jobBuilderPublisher, string messageText, RoutingGroupModel routingGroup, InfoModel info = null)
//        => MessageAdd(jobBuilderPublisher.Builder, messageText, RoutingHelper.GetRoutingGroup(routingGroup), info);
//    public static JobBuilderMessageModel Message(this JobBuilderPublisherModel jobBuilderPublisher, string messageText, RoutingQueueModel routingQueue, InfoModel info = null)
//        => MessageAdd(jobBuilderPublisher.Builder, messageText, RoutingHelper.GetRoutingQueue(routingQueue), info);
//    public static JobBuilderMessageModel Message(this JobBuilderPublisherModel jobBuilderPublisher, string messageText, RoutingEventModel routingEvent, InfoModel info = null)
//        => MessageAdd(jobBuilderPublisher.Builder, messageText, RoutingHelper.GetRoutingEvent(routingEvent), info);

//    public static JobBuilderMessageModel Message(this JobBuilderTimingModel jobBuilderTiming, string messageText, RoutingSpecialModel routingSpecial, InfoModel info = null)
//        => MessageAdd(jobBuilderTiming.Builder, messageText, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
//    public static JobBuilderMessageModel Message(this JobBuilderTimingModel jobBuilderTiming, string messageText, RoutingGroupModel routingGroup, InfoModel info = null)
//        => MessageAdd(jobBuilderTiming.Builder, messageText, RoutingHelper.GetRoutingGroup(routingGroup), info);
//    public static JobBuilderMessageModel Message(this JobBuilderTimingModel jobBuilderTiming, string messageText, RoutingQueueModel routingQueue, InfoModel info = null)
//        => MessageAdd(jobBuilderTiming.Builder, messageText, RoutingHelper.GetRoutingQueue(routingQueue), info);
//    public static JobBuilderMessageModel Message(this JobBuilderTimingModel jobBuilderTiming, string messageText, RoutingEventModel routingEvent, InfoModel info = null)
//        => MessageAdd(jobBuilderTiming.Builder, messageText, RoutingHelper.GetRoutingEvent(routingEvent), info);

//    public static JobBuilderMessageModel Message(this JobBuilderMessageModel jobBuilderMessage, string messageText, RoutingSpecialModel routingSpecial, InfoModel info = null)
//        => MessageAdd(jobBuilderMessage.Builder, messageText, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
//    public static JobBuilderMessageModel Message(this JobBuilderMessageModel jobBuilderMessage, string messageText, RoutingGroupModel routingGroup, InfoModel info = null)
//        => MessageAdd(jobBuilderMessage.Builder, messageText, RoutingHelper.GetRoutingGroup(routingGroup), info);
//    public static JobBuilderMessageModel Message(this JobBuilderMessageModel jobBuilderMessage, string messageText, RoutingQueueModel routingQueue, InfoModel info = null)
//        => MessageAdd(jobBuilderMessage.Builder, messageText, RoutingHelper.GetRoutingQueue(routingQueue), info);
//    public static JobBuilderMessageModel Message(this JobBuilderMessageModel jobBuilderMessage, string messageText, RoutingEventModel routingEvent, InfoModel info = null)
//        => MessageAdd(jobBuilderMessage.Builder, messageText, RoutingHelper.GetRoutingEvent(routingEvent), info);




//    public static JobBuilderMessageModel Message(this JobBuilderModel jobBuilder, Expression<Action> methodCall, RoutingSpecialModel routingSpecial, InfoModel info = null)
//        => MethodAdd(jobBuilder.Builder, methodCall, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
//    public static JobBuilderMessageModel Message(this JobBuilderModel jobBuilder, Expression<Action> methodCall, RoutingGroupModel routingGroup, InfoModel info = null)
//        => MethodAdd(jobBuilder.Builder, methodCall, RoutingHelper.GetRoutingGroup(routingGroup), info);
//    public static JobBuilderMessageModel Message(this JobBuilderModel jobBuilder, Expression<Action> methodCall, RoutingQueueModel routingQueue, InfoModel info = null)
//        => MethodAdd(jobBuilder.Builder, methodCall, RoutingHelper.GetRoutingQueue(routingQueue), info);
//    public static JobBuilderMessageModel Message(this JobBuilderModel jobBuilder, Expression<Action> methodCall, RoutingEventModel routingEvent, InfoModel info = null)
//        => MethodAdd(jobBuilder.Builder, methodCall, RoutingHelper.GetRoutingEvent(routingEvent), info);

//    public static JobBuilderMessageModel Message(this JobBuilderPublisherModel jobBuilderPublisher, Expression<Action> methodCall, RoutingSpecialModel routingSpecial, InfoModel info = null)
//        => MethodAdd(jobBuilderPublisher.Builder, methodCall, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
//    public static JobBuilderMessageModel Message(this JobBuilderPublisherModel jobBuilderPublisher, Expression<Action> methodCall, RoutingGroupModel routingGroup, InfoModel info = null)
//        => MethodAdd(jobBuilderPublisher.Builder, methodCall, RoutingHelper.GetRoutingGroup(routingGroup), info);
//    public static JobBuilderMessageModel Message(this JobBuilderPublisherModel jobBuilderPublisher, Expression<Action> methodCall, RoutingQueueModel routingQueue, InfoModel info = null)
//        => MethodAdd(jobBuilderPublisher.Builder, methodCall, RoutingHelper.GetRoutingQueue(routingQueue), info);
//    public static JobBuilderMessageModel Message(this JobBuilderPublisherModel jobBuilderPublisher, Expression<Action> methodCall, RoutingEventModel routingEvent, InfoModel info = null)
//        => MethodAdd(jobBuilderPublisher.Builder, methodCall, RoutingHelper.GetRoutingEvent(routingEvent), info);

//    public static JobBuilderMessageModel Message(this JobBuilderTimingModel jobBuilderTiming, Expression<Action> methodCall, RoutingSpecialModel routingSpecial, InfoModel info = null)
//        => MethodAdd(jobBuilderTiming.Builder, methodCall, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
//    public static JobBuilderMessageModel Message(this JobBuilderTimingModel jobBuilderTiming, Expression<Action> methodCall, RoutingGroupModel routingGroup, InfoModel info = null)
//        => MethodAdd(jobBuilderTiming.Builder, methodCall, RoutingHelper.GetRoutingGroup(routingGroup), info);
//    public static JobBuilderMessageModel Message(this JobBuilderTimingModel jobBuilderTiming, Expression<Action> methodCall, RoutingQueueModel routingQueue, InfoModel info = null)
//        => MethodAdd(jobBuilderTiming.Builder, methodCall, RoutingHelper.GetRoutingQueue(routingQueue), info);
//    public static JobBuilderMessageModel Message(this JobBuilderTimingModel jobBuilderTiming, Expression<Action> methodCall, RoutingEventModel routingEvent, InfoModel info = null)
//        => MethodAdd(jobBuilderTiming.Builder, methodCall, RoutingHelper.GetRoutingEvent(routingEvent), info);

//    public static JobBuilderMessageModel Message(this JobBuilderMessageModel jobBuilderMessage, Expression<Action> methodCall, RoutingSpecialModel routingSpecial, InfoModel info = null)
//        => MethodAdd(jobBuilderMessage.Builder, methodCall, RoutingHelper.GetRoutingSpecial(routingSpecial), info);
//    public static JobBuilderMessageModel Message(this JobBuilderMessageModel jobBuilderMessage, Expression<Action> methodCall, RoutingGroupModel routingGroup, InfoModel info = null)
//        => MethodAdd(jobBuilderMessage.Builder, methodCall, RoutingHelper.GetRoutingGroup(routingGroup), info);
//    public static JobBuilderMessageModel Message(this JobBuilderMessageModel jobBuilderMessage, Expression<Action> methodCall, RoutingQueueModel routingQueue, InfoModel info = null)
//        => MethodAdd(jobBuilderMessage.Builder, methodCall, RoutingHelper.GetRoutingQueue(routingQueue), info);
//    public static JobBuilderMessageModel Message(this JobBuilderMessageModel jobBuilderMessage, Expression<Action> methodCall, RoutingEventModel routingEvent, InfoModel info = null)
//        => MethodAdd(jobBuilderMessage.Builder, methodCall, RoutingHelper.GetRoutingEvent(routingEvent), info);





















//    private static JobBuilderMessageModel MessageAdd(BuilderModel builder, string messageText, RoutingModel routing, InfoModel info = null)
//        => Add(builder, messageText, null, routing, info);


//    private static JobBuilderMessageModel MethodAdd(BuilderModel builder, Expression<Action> methodCall, RoutingModel routing, InfoModel info = null)
//        => Add(builder, null, methodCall, routing, info);


//    private static JobBuilderMessageModel Add(BuilderModel builder, string messageText, Expression<Action> methodCall, RoutingModel routing, InfoModel info = null)
//    {
//        routing.IsResult = false;

//        if (messageText != null)
//            builder.MultipleMessages.Add(new MultipleMessageModel { Message = messageText, Routing = routing, Info = info });
//        else
//            builder.MultipleMethods.Add(new MultipleMethodModel { MethodCall = methodCall, Routing = routing, Info = info });

//        return new JobBuilderMessageModel { Builder = builder };

//    }
//}
#endregion
