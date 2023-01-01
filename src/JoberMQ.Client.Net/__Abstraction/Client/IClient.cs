using JoberMQ.Client.Net.Abstraction.Connect;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IClient : IProducer, IConsumer, IClientOffline, IDisposable
    {
        public bool IsClientActive { get; }
        public string ClientId { get; }
        public string ClientGroupKey { get; }
        public IConnection Connection { get; }
        public bool IsServerActive { get; }
    }
}
