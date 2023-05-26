using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Priority;
using JoberMQ.Common.Models.Consuming;
using JoberMQ.Common.Models.Info;
using JoberMQ.Common.Models.Routing;

namespace JoberMQ.Client.Net.Implementation.Message.Default
{
    public class DfMessage : IMessage
    {
        public DfMessage(MessageTypeEnum messageType, string message, RoutingModel routing, InfoModel info, string generalData, PriorityTypeEnum priorityType, bool isConsumingRetryPause, int consumingRetryMaxCount)
        {
            this.messageType = messageType;
            this.message =  message;
            this.routing = routing;
            this.info = info;
            this.generalData = generalData;
            this.priorityType = priorityType;
            this.MessageConsuming = new ConsumingModel
            {
                IsConsumingRetryPause = isConsumingRetryPause,
                ConsumingRetryMaxCount = consumingRetryMaxCount
            };
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

        ConsumingModel messageConsuming;
        public ConsumingModel MessageConsuming { get => messageConsuming; set => messageConsuming = value; }
    }
}
