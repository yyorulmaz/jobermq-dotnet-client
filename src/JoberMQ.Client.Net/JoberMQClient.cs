using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Factories.Client;
using JoberMQ.Client.Net.Factories.Configuration;
using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Common.Enums.Endpoint;

namespace JoberMQ.Client.Net
{
    public class JoberMQClient
    {
        public static IConfiguration GetConfiguration()
            => ConfigurationFactory.Create(ConfigurationFactoryEnum.Default);

        public static IClient CreateClient(string clientKey)
            => DefaultCreateClient(clientKey, null);
        public static IClient CreateClient(string clientKey, IConfiguration configuration)
            => DefaultCreateClient(clientKey, configuration);
        public static IClient CreateClient(string clientKey, UrlProtocolEnum urlProtocol, int port)
            => DefaultCreateClient(clientKey, GetConfiguration(null, urlProtocol, port));
        public static IClient CreateClient(string clientKey, string hostName, UrlProtocolEnum urlProtocol, int port)
            => DefaultCreateClient(clientKey, GetConfiguration(hostName, urlProtocol, port));

        private static IConfiguration GetConfiguration(string hostName, UrlProtocolEnum urlProtocol, int port)
        {
            var configuration = GetConfiguration();

            string _hostName = hostName == null ? "localhost" : hostName;   
            bool _isSsl = urlProtocol == UrlProtocolEnum.https ? true : false;
            int _port = urlProtocol == UrlProtocolEnum.https ? 0 : port;
            int _portSsl = urlProtocol == UrlProtocolEnum.https ? port : 0;
            configuration.EndpointLogin = JoberMQ.Client.Net.Factories.Endpoint.EndpointFactory.Create(EndpointFactoryEnum.Default, _isSsl, _hostName, _port, _portSsl, "account/login");
            configuration.EndpointHub  = JoberMQ.Client.Net.Factories.Endpoint.EndpointFactory.Create(EndpointFactoryEnum.Default, _isSsl, _hostName, _port, _portSsl, "JoberHub");

            return configuration;
        }

        private static IClient DefaultCreateClient(string clientKey, IConfiguration configuration)
        {
            IConfiguration config;

            if (configuration == null)
                config = GetConfiguration();
            else
                config = configuration;

            return ClientFactory.Create(clientKey, config);
        }
    }
}
