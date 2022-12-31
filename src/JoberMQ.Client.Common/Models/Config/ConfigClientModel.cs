using JoberMQ.Client.Common.Constants;
using JoberMQ.Client.Common.Enums.Client;
using JoberMQ.Client.Common.Enums.Protocol;
using JoberMQ.Client.Common.StatusCode.Enums;

namespace JoberMQ.Client.Common.Models.Config
{
    public class ConfigClientModel
    {
        public string ClientId { get; set; } = Guid.NewGuid().ToString();
        public string ClientGroupKey { get; set; }
        public bool IsOfflineMode { get; set; } = ClientConst.IsOfflineMode;
        public bool TextMessageReceiveAutoCompleted { get; set; } = ClientConst.TextMessageReceiveAutoCompleted;
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage { get; set; } = ClientConst.StatusCodeMessageLanguage;

        public ConnectProtocolEnum ConnectProtocol { get; set; } = ClientConst.ConnectProtocol;
        public string HostName { get; set; } = ClientConst.DefaultDomain;
        public int Port { get; set; } = ClientConst.DefaultPort;
        public bool IsSsl { get; set; } = ClientConst.IsSsl;
        public bool AutomaticReconnect { get; set; } = ClientConst.AutomaticReconnect;
        public int ConnectionRetryTimeout { get; set; } = ClientConst.ConnectionRetryTimeout;
        public string UserName { get; set; } = ClientConst.DefaultUserName;
        public string Password { get; set; } = ClientConst.DefaultPassword;
        public ClientTypeEnum ClientType { get; set; } = ClientConst.ClientType;
        //public bool IsNotConnectKeepTrying { get; set; } = ClientConst.IsNotConnectKeepTrying;
    }
}
