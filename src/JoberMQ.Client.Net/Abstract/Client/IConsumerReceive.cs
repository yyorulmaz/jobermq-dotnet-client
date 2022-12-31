namespace JoberMQ.Client.Net.Abstract.Client
{
    public interface IConsumerReceive : IDisposable
    {
        public event Action<string> MessageReceive;
        public bool TextMessageReceiveAutoCompleted { get; }
    }
}
