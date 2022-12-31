using JoberMQ.Client.Common.Models.Config;
using JoberMQ.Client.Common.StatusCode.Factories;
using JoberMQ.Client.Net.Abstract.Client;
using JoberMQ.Client.Net.Abstract.Connect;
using JoberMQ.Client.Net.Abstract.Factory;
using JoberMQ.Client.Net.Concrete.Client;

namespace JoberMQ.Client.Net.Concrete.Factory
{
    internal class ClientFactory: IClientFactory
    {
        private readonly IConnection connection;
        private readonly ConfigClientModel config;
        public ClientFactory(ConfigClientModel config, IConnection connection)
        {
            this.connection = connection;
            this.config = config;
        }

        public IClient CreateClient()
        {
            var statusCodeService = StatusCodeFactory.Create(Common.StatusCode.Enums.StatusCodeFactoryEnum.Default,null, Common.StatusCode.Enums.StatusCodeMessageLanguageEnum.tr);
            //statusCodeService.Start();
            JoberMQ.Client.Net.Factory.StatusCodeService = statusCodeService;

            IClient client = new ClientBase(config, connection);

            return client;
        }
    }
}
