using JoberMQ.Library.Enums.Message;

namespace JoberMQ.Client.Net.Abstraction.Message
{
    public interface IMessageRpc
    {
        string ConsumerId { get; }
        MessageTypeEnum MessageType { get; }
        string Message { get; }
    }
}
