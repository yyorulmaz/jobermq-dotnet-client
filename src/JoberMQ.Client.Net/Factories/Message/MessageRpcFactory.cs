using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Implementation.Message.Default;
using JoberMQ.Library.Enums.Message;

namespace JoberMQ.Client.Net.Factories.Message
{
    internal class MessageRpcFactory
    {
        public static IMessageRpc Create(string consumerId, MessageTypeEnum messageType, string message)
            => new DfMessageRpc(consumerId, messageType, message);
    }
}
