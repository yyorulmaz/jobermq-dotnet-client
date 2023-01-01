using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Constants;
using JoberMQ.Common.Enums.Client;
using JoberMQ.Common.Enums.Protocol;
using JoberMQ.Common.RoundRobin.Enums;
using JoberMQ.Common.StatusCode.Enums;
using JoberMQ.Common.StatusCode.Models;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Net.Implementation.Configuration.Default
{
    public class DfConfiguration : IConfiguration
    {
        ClientFactoryEnum clientFactory = ClientConst.ClientFactory;
        ClientFactoryEnum IConfiguration.ClientFactory { get => clientFactory; set => clientFactory = value; }

        string clientKey = Guid.NewGuid().ToString();
        string IConfiguration.ClientKey { get => clientKey; set => clientKey = value; }

        string clientGroupKey;
        string IConfiguration.ClientGroupKey { get => clientGroupKey; set => clientGroupKey = value; }

        bool isOfflineMode = ClientConst.IsOfflineMode;
        bool IConfiguration.IsOfflineMode { get => isOfflineMode; set => isOfflineMode = value; }

        bool textMessageReceiveAutoCompleted = ClientConst.TextMessageReceiveAutoCompleted;
        bool IConfiguration.TextMessageReceiveAutoCompleted { get => textMessageReceiveAutoCompleted; set => textMessageReceiveAutoCompleted = value; }



        StatusCodeFactoryEnum statusCodeFactory = ClientConst.StatusCodeFactory;
        StatusCodeFactoryEnum IConfiguration.StatusCodeFactory { get => statusCodeFactory; set => statusCodeFactory = value; }

        StatusCodeMessageLanguageEnum statusCodeMessageLanguage = ClientConst.StatusCodeMessageLanguage;
        StatusCodeMessageLanguageEnum IConfiguration.StatusCodeMessageLanguage { get => statusCodeMessageLanguage; set => statusCodeMessageLanguage = value; }

        ConcurrentDictionary<string, StatusCodeModel> statusCodeDatas = ClientConst.DefaultStatusCodeDatas;
        public ConcurrentDictionary<string, StatusCodeModel> StatusCodeDatas { get => statusCodeDatas; set => statusCodeDatas = value; }



        ConnectProtocolEnum connectProtocol = ClientConst.ConnectProtocol;
        ConnectProtocolEnum IConfiguration.ConnectProtocol { get => connectProtocol; set => connectProtocol = value; }


        string hostName = ClientConst.HostName;
        string IConfiguration.HostName { get => hostName; set => hostName = value; }

        int port = ClientConst.Port;
        int IConfiguration.Port { get => port; set => port = value; }

        int portSsl = ClientConst.PortSsl;
        int IConfiguration.PortSsl { get => portSsl; set => portSsl = value; }

        bool isSsl = ClientConst.IsSsl;
        bool IConfiguration.IsSsl { get => isSsl; set => isSsl = value; }

        bool automaticReconnect = ClientConst.AutomaticReconnect;
        bool IConfiguration.AutomaticReconnect { get => automaticReconnect; set => automaticReconnect = value; }

        int connectionRetryTimeout = ClientConst.ConnectionRetryTimeout;
        int IConfiguration.ConnectionRetryTimeout { get => connectionRetryTimeout; set => connectionRetryTimeout = value; }

        string userName = ClientConst.UserName;
        string IConfiguration.UserName { get => userName; set => userName = value; }

        string password = ClientConst.Password;
        string IConfiguration.Password { get => password; set => password = value; }

        ClientTypeEnum clientType = ClientConst.ClientType;
        ClientTypeEnum IConfiguration.ClientType { get => clientType; set => clientType = value; }

        RoundRobinFactoryEnum roundRobinFactory = ClientConst.RoundRobinFactory;
        RoundRobinFactoryEnum IConfiguration.RoundRobinFactory { get => roundRobinFactory; set => roundRobinFactory = value; }
    }
}
