using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.Net.Implementation.Client.Default
{
    public class DefaultClientInfo : IClientInfo
    {
        public DefaultClientInfo(ClientTypeEnum clientType, string clientKey, bool isOfflineClient)
        {
            ClientType = clientType;
            ClientKey = clientKey;
            IsOfflineClient = isOfflineClient;
        }

        public ClientTypeEnum ClientType { get; }
        public string ClientKey { get; }
        public bool IsOfflineClient { get; }

        public bool isClientActive = false;
        public bool IsClientActive => isClientActive;
    }
}
