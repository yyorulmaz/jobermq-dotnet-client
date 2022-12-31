using JoberMQ.Client.Common.Models.Config;
using JoberMQ.Client.Common.RoundRobin;
using JoberMQ.Client.Common.StatusCode.Abstraction;
using JoberMQ.Client.Net.Abstract.Client;
using JoberMQ.Client.Net.Abstract.Connect;
using JoberMQ.Client.Net.Concrete.Factory;

namespace JoberMQ.Client.Net
{
    public class Factory
    {
        internal static IStatusCode StatusCodeService { get; set; }
        public static IClient CreateClient(ConfigClientModel config) => GetClient(config);
        public static IClient CreateClient(string clientGroupKey)
        {
            var config = new ConfigClientModel();
            config.ClientGroupKey = clientGroupKey;
            return GetClient(config);
        }
        public static IClient CreateClient(string clientGroupKey, string hostName)
        {
            var config = new ConfigClientModel();
            config.ClientGroupKey = clientGroupKey;
            config.HostName = hostName;
            return GetClient(config);
        }
        private static IClient GetClient(ConfigClientModel config)
        {
            if (String.IsNullOrEmpty(config.ClientGroupKey))
                throw new ArgumentNullException(nameof(config.ClientGroupKey));

            var connFactory = new ConnectionFactory(config);
            IConnection connection = connFactory.CreateConnection();


            var clientFactory = new ClientFactory(config, connection);
            IClient client = clientFactory.CreateClient();


            var connect = connection.ConnectAsync().Result;

            return client;
        }


        internal static IRoundRobin<T> CreateRoundRobin<T>()
        {
            var roundRobin = new RoundRobin<T>();
            return roundRobin;
        }

    }
}
