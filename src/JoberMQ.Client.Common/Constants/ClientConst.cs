using JoberMQ.Client.Common.Enums.Client;
using JoberMQ.Client.Common.Enums.Protocol;
using JoberMQ.Client.Common.StatusCode.Enums;

namespace JoberMQ.Client.Common.Constants
{
    internal class ClientConst
    {
        internal const int DefaultPort = 7654;
        internal const int DefaultPortSsl = 7655;

        internal const string DefaultUserName = "jobermq";
        internal const string DefaultPassword = "jobermq";

        internal const UrlProtocolEnum DefaultUrlProtocol = UrlProtocolEnum.http;
        internal const string DefaultDomain = "localhost";
        internal const bool IsSsl = false;
        internal const ConnectProtocolEnum ConnectProtocol = ConnectProtocolEnum.Socket;
        internal const bool AutomaticReconnect = true;
        internal const int ConnectionRetryTimeoutMin = 2000;
        internal const int ConnectionRetryTimeout = 5000;
        internal const ClientTypeEnum ClientType = ClientTypeEnum.Normal;
        internal const StatusCodeMessageLanguageEnum StatusCodeMessageLanguage = StatusCodeMessageLanguageEnum.tr;

        internal const bool IsOfflineMode = true;
        internal const bool TextMessageReceiveAutoCompleted = true;
        //internal const bool IsNotConnectKeepTrying = false;
    }
}
