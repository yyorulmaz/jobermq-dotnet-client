namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IConsumerReceive : IDisposable
    {
        public event Action<string> MessageReceive;
        public bool TextMessageReceiveAutoCompleted { get; }
    }
}
