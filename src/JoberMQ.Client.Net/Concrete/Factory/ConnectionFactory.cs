using JoberMQ.Client.Common.Enums.Protocol;
using JoberMQ.Client.Common.Models.Config;
using JoberMQ.Client.Net.Abstract.Connect;
using JoberMQ.Client.Net.Abstract.Factory;
using JoberMQ.Client.Net.Concrete.Connect;
using JoberMQ.Client.Net.Constants;

namespace JoberMQ.Client.Net.Concrete.Factory
{
    internal class ConnectionFactory : IConnectionFactory
    {
        private readonly ConfigClientModel config;
        public ConnectionFactory(ConfigClientModel config) => this.config = config;

        public IConnection CreateConnection()
        {
            if (config.ConnectionRetryTimeout < ClientConst.ConnectionRetryTimeoutMin)
                config.ConnectionRetryTimeout = ClientConst.ConnectionRetryTimeout;

            IConnection conn;

            switch (config.ConnectProtocol)
            {
                case ConnectProtocolEnum.Socket:
                    conn = new ConnectionSocket(config);
                    break;
                default:
                    conn = new ConnectionSocket(config);
                    break;
            }

            return conn;
        }

        //private string clientGroupKey;
        //public string ClientGroupKey
        //{
        //    get => clientGroupKey;
        //    set => clientGroupKey = value;
        //}
    }
}
