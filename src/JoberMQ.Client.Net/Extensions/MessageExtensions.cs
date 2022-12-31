using JoberMQ.Client.Common.Enums.Transport;
using JoberMQ.Client.Common.Models.Builder;
using JoberMQ.Client.Common.Models.Message;
using JoberMQ.Client.Common.Models.Option;
using JoberMQ.Client.Common.Models.Routing;
using JoberMQ.Client.Net.Operations;
using System.Linq.Expressions;

namespace JoberMQ.Client.Net.Extensions
{
    public static class MessageExtensions
    {
        public static JobBuilderMessageDataModel Message(this JobBuilderMessageDataModel builderData, string messageText, RoutingSpecialModel routingSpecial, OptionModel option = null)
            => MessageAdd(builderData, messageText, RoutingOperation.GetRoutingSpecial(routingSpecial), option);
        public static JobBuilderMessageDataModel Message(this JobBuilderMessageDataModel builderData, string messageText, RoutingGroupModel routingGroup, OptionModel option = null)
            => MessageAdd(builderData, messageText, RoutingOperation.GetRoutingGroup(routingGroup), option);
        public static JobBuilderMessageDataModel Message(this JobBuilderMessageDataModel builderData, string messageText, RoutingQueueKeyModel routingQueue, OptionModel option = null)
            => MessageAdd(builderData, messageText, RoutingOperation.GetRoutingQueue(routingQueue), option);


        public static JobBuilderMessageDataModel Message(this JobBuilderMessageDataModel builderData, Expression<Action> methodCall, RoutingSpecialModel routingSpecial, OptionModel option = null)
            => MethodAdd(builderData, methodCall, RoutingOperation.GetRoutingSpecial(routingSpecial), option);
        public static JobBuilderMessageDataModel Message(this JobBuilderMessageDataModel builderData, Expression<Action> methodCall, RoutingGroupModel routingGroup, OptionModel option = null)
            => MethodAdd(builderData, methodCall, RoutingOperation.GetRoutingGroup(routingGroup), option);
        public static JobBuilderMessageDataModel Message(this JobBuilderMessageDataModel builderData, Expression<Action> methodCall, RoutingQueueKeyModel routingQueue, OptionModel option = null)
            => MethodAdd(builderData, methodCall, RoutingOperation.GetRoutingQueue(routingQueue), option);


        public static JobBuilderMessageDataModel Message(this JobBuilderMessageDataModel builderData, string messageText, string eventName, OptionModel option = null)
            => MessageAdd(builderData, messageText, eventName, option);
        public static JobBuilderMessageDataModel Message(this JobBuilderMessageDataModel builderData, Expression<Action> methodCall, string eventName, OptionModel option = null)
            => MethodAdd(builderData, methodCall, eventName, option);



        private static JobBuilderMessageDataModel MessageAdd(JobBuilderMessageDataModel builderData, string messageText, RoutingModel routing, OptionModel option = null)
            => MessageAdd(builderData, messageText, TransportTypeEnum.Route, routing, null, option);
        private static JobBuilderMessageDataModel MessageAdd(JobBuilderMessageDataModel builderData, string messageText, string eventName, OptionModel option = null)
            => MessageAdd(builderData, messageText, TransportTypeEnum.Event, null, eventName, option);
        private static JobBuilderMessageDataModel MessageAdd(JobBuilderMessageDataModel builderData, string messageText, TransportTypeEnum jobTransportType, RoutingModel routing, string eventName, OptionModel option = null)
            => Add(builderData, messageText, null, jobTransportType, routing, eventName, option);



        private static JobBuilderMessageDataModel MethodAdd(JobBuilderMessageDataModel builderData, Expression<Action> methodCall, RoutingModel routing, OptionModel option = null)
            => MethodAdd(builderData, methodCall, TransportTypeEnum.Route, routing, null, option);
        private static JobBuilderMessageDataModel MethodAdd(JobBuilderMessageDataModel builderData, Expression<Action> methodCall, string eventName, OptionModel option = null)
            => MethodAdd(builderData, methodCall, TransportTypeEnum.Event, null, eventName, option);
        private static JobBuilderMessageDataModel MethodAdd(JobBuilderMessageDataModel builderData, Expression<Action> methodCall, TransportTypeEnum jobTransportType, RoutingModel routing, string eventName, OptionModel option = null)
            => Add(builderData, null, methodCall, jobTransportType, routing, eventName, option);


        private static JobBuilderMessageDataModel Add(JobBuilderMessageDataModel builderData, string messageText, Expression<Action> methodCall, TransportTypeEnum jobTransportType, RoutingModel routing, string eventName, OptionModel option = null)
        {
            if (messageText != null)
                builderData.JobBuilder.MultipleMessages.Add(new MultipleMessageModel { Message = messageText, JobTransportType = jobTransportType, Routing = routing, EventName = eventName, Option = option });
            else
                builderData.JobBuilder.MultipleMethods.Add(new MultipleMethodModel { MethodCall = methodCall, JobTransportType = jobTransportType, Routing = routing, EventName = eventName, Option = option });

            return builderData;
        }
    }
}
