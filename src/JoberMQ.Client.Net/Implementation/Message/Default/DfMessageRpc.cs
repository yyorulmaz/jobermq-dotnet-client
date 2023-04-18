using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Library.Enums.Message;

namespace JoberMQ.Client.Net.Implementation.Message.Default
{
    public class DfMessageRpc : IMessageRpc
    {
        public DfMessageRpc(string consumerId, MessageTypeEnum messageType, string message)
        {
            this.consumerId = consumerId;
            this.messageType = messageType;
            this.message =  message;
        }

        string consumerId;
        public string ConsumerId => consumerId;

        MessageTypeEnum messageType;
        public MessageTypeEnum MessageType => MessageTypeEnum.Text;

        string message;
        public string Message => message;
    }
}
