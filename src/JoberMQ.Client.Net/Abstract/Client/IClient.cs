using JoberMQ.Client.Net.Abstract.Connect;

namespace JoberMQ.Client.Net.Abstract.Client
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
