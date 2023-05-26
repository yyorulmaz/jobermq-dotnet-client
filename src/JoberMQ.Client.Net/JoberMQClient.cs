using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Factories.Client;
using JoberMQ.Client.Net.Factories.Configuration;
using JoberMQ.Common.Enums.Configuration;

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
