using JoberMQ.Client.Net.Abstraction.Connect;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IClient : IProducer, IConsumer, IOffline, IDisposable
    {
        public bool IsClientActive { get; }
        public string ClientKey { get; }
        public string ClientGroupKey { get; }
        public IConnection Connection { get; }
        public bool IsServerActive { get; }
    }
}
