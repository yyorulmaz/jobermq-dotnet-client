using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Enums.Message;
using JoberMQ.Client.Net.Enums.Priority;
using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.Routing;
using JoberMQ.Library.Method.Abstraction;
using System;
using System.Linq.Expressions;

namespace JoberMQ.Client.Net.Implementation.Message.Default
{
    public class DfMessage : IMessage
    {
        public DfMessage(MessageTypeEnum messageType, string message, RoutingModel routing, InfoModel info, string generalData, PriorityTypeEnum priorityType)
        {
            this.messageType = messageType;
            this.message =  message;
            this.routing = routing;
            this.info = info;
            this.generalData = generalData;
            this.priorityType = priorityType;
        }
        //public DfMessage(string message, RoutingModel routing, InfoModel info, string generalData, PriorityTypeEnum priorityType)
        //    => Creator(MessageTypeEnum.Text, message, routing, info, generalData, priorityType);

        //public DfMessage(Expression<Action> methodCall, RoutingModel routing, InfoModel info, string generalData, PriorityTypeEnum priorityType)
        //    => Creator(MessageTypeEnum.Funtion, method.MethodPropertySerialize(methodCall), routing, info, generalData, priorityType);
        //void Creator(MessageTypeEnum messageType, string message, RoutingModel routing, InfoModel info, string generalData, PriorityTypeEnum priorityType)
        //{
        //    this.messageType = messageType;
        //    this.message =  message;
        //    this.routing = routing;
        //    this.info = info;
        //    this.generalData = generalData;
        //    this.priorityType = priorityType;
        //}



        MessageTypeEnum messageType;
        public MessageTypeEnum MessageType => MessageTypeEnum.Text;

        string message;
        public string Message => message;

        RoutingModel routing;
        public RoutingModel Routing => routing;

        InfoModel info;
        public InfoModel Info => info;

        string generalData;
        public string GeneralData => generalData;

        PriorityTypeEnum priorityType;
        public PriorityTypeEnum PriorityType => priorityType;
    }
}
