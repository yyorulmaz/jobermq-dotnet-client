using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Constants;
using JoberMQ.Client.Net.Factories.Configuration;
using JoberMQ.Client.Net.Factories.Connection;
using JoberMQ.Client.Net.Implementation.Client.Default;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Library.StatusCode.Factories;

namespace JoberMQ.Client.Net
{
    public class JoberMQClient
    {
        internal static IConfiguration Configuration;
        internal static IStatusCode StatusCode;
        public static IConfiguration GetConfiguration()
            => Factories.Configuration.ConfigurationFactory.CreateConfiguration(ClientConst.ConfigurationFactory);

        public static IClient CreateClient()
            => DefaultCreateClient(null);
        public static IClient CreateClient(IConfiguration configuration)
            => DefaultCreateClient(configuration);

        private static IClient DefaultCreateClient(IConfiguration configuration)
        {
            if (configuration == null)
                Configuration = ConfigurationFactory.CreateConfiguration(ClientConst.ConfigurationFactory);
            else
                Configuration = configuration;

            StatusCode = StatusCodeFactory.Create(Configuration.StatusCodeFactory, Configuration.StatusCodeDatas, Configuration.StatusCodeMessageLanguage);

            IConnection connection = ConnectionFactory.Create(Configuration);
            IClient client = new DfClient(Configuration, connection);

            var connect = connection.ConnectAsync().Result;

            return client;
        }

    }
}
