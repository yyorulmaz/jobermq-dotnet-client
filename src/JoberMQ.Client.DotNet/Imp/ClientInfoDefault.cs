using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.DotNet.Imp
{
    public class ClientInfoDefault : IClientInfo
    {
        public ClientInfoDefault(ClientTypeEnum clientType, string clientKey, bool isOfflineClient)
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
