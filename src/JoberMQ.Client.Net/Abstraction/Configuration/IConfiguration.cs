using JoberMQ.Common.Enums.Client;
using JoberMQ.Common.Enums.Protocol;
using JoberMQ.Library.RoundRobin.Enums;
using JoberMQ.Library.StatusCode.Enums;
using JoberMQ.Library.StatusCode.Models;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Net.Abstraction.Configuration
{
    public interface IConfiguration
    {
        public ClientFactoryEnum ClientFactory { get; internal set; }
        public string ClientKey { get; internal set; }
        public string ClientGroupKey { get; internal set; }
        public bool IsOfflineMode { get; internal set; }
        public bool TextMessageReceiveAutoCompleted { get; internal set; }
        public StatusCodeFactoryEnum StatusCodeFactory { get; set; }
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage { get; set; }
        public ConcurrentDictionary<string, StatusCodeModel> StatusCodeDatas { get; set; }
        
        public ConnectProtocolEnum ConnectProtocol { get; internal set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public int PortSsl { get; set; }
        public bool IsSsl { get; set; }
        public bool AutomaticReconnect { get; set; }
        public int ConnectionRetryTimeout { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ClientTypeEnum ClientType { get; set; }


        public RoundRobinFactoryEnum RoundRobinFactory { get; internal set; }
        
    }
}
