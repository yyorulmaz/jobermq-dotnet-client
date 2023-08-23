using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.DotNet.Abs
{
    public interface IClientInfo
    {
        ClientTypeEnum ClientType { get; }
        string ClientKey { get; }
        bool IsOfflineClient { get; }
        bool IsClientActive { get; }
    }
}
