using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Library.Enums.Client;

namespace JoberMQ.Client.Net.Implementation.Client.Default
{
    public class DfClientInfo : IClientInfo
    {
        public DfClientInfo(ClientTypeEnum clientType, string clientKey, bool isOfflineClient)
        {
            this.ClientType = clientType;
            this.ClientKey = clientKey;
            this.IsOfflineClient = isOfflineClient;
        }

        public ClientTypeEnum ClientType { get; }
        public string ClientKey { get; }
        public bool IsOfflineClient { get; }

        public bool isClientActive = false;
        public bool IsClientActive => isClientActive;
    }
}
