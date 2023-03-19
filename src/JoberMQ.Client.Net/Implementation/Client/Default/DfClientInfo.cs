using JoberMQ.Client.Net.Abstraction.Client;

namespace JoberMQ.Client.Net.Implementation.Client.Default
{
    public class DfClientInfo : IClientInfo
    {
        public DfClientInfo(bool isOfflineClient)
        {
            this.isOfflineClient = isOfflineClient;
        }
        string clientKey;
        public string ClientKey { get => clientKey; set => clientKey = value; }
        string clientGroupKey;
        public string ClientGroupKey { get => clientGroupKey; set => clientGroupKey = value; }
        public bool isClientActive = false;
        public bool IsClientActive => isClientActive;
        public bool isOfflineClient;
        public bool IsOfflineClient => isOfflineClient;
    }
}
