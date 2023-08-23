using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Factory;
using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Common.Enums.Endpoint;
using JoberMQ.Common.Models.General;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Client.DotNet
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
        public static IClient CreateClient(JoberMQParameterModel joberMQParameter)
            => DefaultCreateClient(joberMQParameter.ClientKey, GetConfiguration(joberMQParameter.HostName, joberMQParameter.UrlProtocol, joberMQParameter.Port));

        private static IConfiguration GetConfiguration(string hostName, UrlProtocolEnum urlProtocol, int port)
        {
            var configuration = GetConfiguration();

            string _hostName = hostName == null ? "localhost" : hostName;
            bool _isSsl = urlProtocol == UrlProtocolEnum.https ? true : false;
            int _port = urlProtocol == UrlProtocolEnum.https ? 0 : port;
            int _portSsl = urlProtocol == UrlProtocolEnum.https ? port : 0;
            configuration.EndpointLogin = EndpointFactory.Create(EndpointFactoryEnum.Default, _isSsl, _hostName, _port, _portSsl, "account/login");
            configuration.EndpointHub = EndpointFactory.Create(EndpointFactoryEnum.Default, _isSsl, _hostName, _port, _portSsl, "JoberHub");

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


        public static async Task<IClient> CreateClientConnectAsync(string clientKey)
        {
            var client = CreateClient(clientKey);
            var connect = await client.ConnectAsync();

            if (!connect)
                throw new ArgumentException("CONNECTOIN ERROR");
            return client;
        }
        public static async Task<IClient> CreateClientConnectAsync(string clientKey, IConfiguration configuration)
        {
            var client = CreateClient(clientKey, configuration);
            var connect = await client.ConnectAsync();

            if (!connect)
                throw new ArgumentException("CONNECTOIN ERROR");
            return client;
        }
        public static async Task<IClient> CreateClientConnectAsync(string clientKey, UrlProtocolEnum urlProtocol, int port)
        {
            var client = CreateClient(clientKey, urlProtocol, port);
            var connect = await client.ConnectAsync();

            if (!connect)
                throw new ArgumentException("CONNECTOIN ERROR");
            return client;
        }
        public static async Task<IClient> CreateClientConnectAsync(string clientKey, string hostName, UrlProtocolEnum urlProtocol, int port)
        {
            var client = CreateClient(clientKey, hostName, urlProtocol, port);
            var connect = await client.ConnectAsync();

            if (!connect)
                throw new ArgumentException("CONNECTOIN ERROR");
            return client;
        }
        public static async Task<IClient> CreateClientConnectAsync(JoberMQParameterModel joberMQParameter)
        {
            var client = CreateClient(joberMQParameter);
            var connect = await client.ConnectAsync();

            if (!connect)
                throw new ArgumentException("CONNECTOIN ERROR");
            return client;
        }


        public static async Task<(IClient client, bool connect)> CreateClientAndConnectAsync(string clientKey)
        {
            var client = CreateClient(clientKey);
            var connect = await client.ConnectAsync();

            return (client, connect);
        }
        public static async Task<(IClient client, bool connect)> CreateClientAndConnectAsync(string clientKey, IConfiguration configuration)
        {
            var client = CreateClient(clientKey, configuration);
            var connect = await client.ConnectAsync();

            return (client, connect);
        }
        public static async Task<(IClient client, bool connect)> CreateClientAndConnectAsync(string clientKey, UrlProtocolEnum urlProtocol, int port)
        {
            var client = CreateClient(clientKey, urlProtocol, port);
            var connect = await client.ConnectAsync();

            return (client, connect);
        }
        public static async Task<(IClient client, bool connect)> CreateClientAndConnectAsync(string clientKey, string hostName, UrlProtocolEnum urlProtocol, int port)
        {
            var client = CreateClient(clientKey, hostName, urlProtocol, port);
            var connect = await client.ConnectAsync();

            return (client, connect);
        }
        public static async Task<(IClient client, bool connect)> CreateClientAndConnectAsync(JoberMQParameterModel joberMQParameter)
        {
            var client = CreateClient(joberMQParameter);
            var connect = await client.ConnectAsync();

            return (client, connect);
        }
    }
}
