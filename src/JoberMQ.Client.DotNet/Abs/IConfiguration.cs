using JoberMQ.Common.Enums.Client;
using JoberMQ.Common.Enums.Connect;

namespace JoberMQ.Client.DotNet.Abs
{
    public interface IConfiguration
    {
        ClientFactoryEnum ClientFactory { get; }
        ConnectProtocolEnum ConnectProtocol { get; }
        ClientTypeEnum ClientType { get; }
        bool IsOfflineClient { get; }
        int ConnectionRetryTimeoutMin { get; }
        int ConnectionRetryTimeout { get; set; }
        bool AutoReconnect { get; }
        IEndpoint EndpointLogin { get; set; }
        IEndpoint EndpointHub { get; set; }
        bool TextMessageReceiveAutoCompleted { get; set; }
    }
}
