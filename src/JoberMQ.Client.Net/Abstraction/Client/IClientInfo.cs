using JoberMQ.Library.Enums.Client;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IClientInfo
    {
        ClientTypeEnum ClientType { get; }
        string ClientKey { get; }
        string ClientGroupKey { get; }
        bool IsOfflineClient { get; }
        bool IsClientActive { get; }
    }
}
