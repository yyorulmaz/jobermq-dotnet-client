using JoberMQ.Client.Net.Abstract.Client;

namespace JoberMQ.Client.Net.Abstract.Factory
{
    internal interface IClientFactory
    {
        public IClient CreateClient();
    }
}
