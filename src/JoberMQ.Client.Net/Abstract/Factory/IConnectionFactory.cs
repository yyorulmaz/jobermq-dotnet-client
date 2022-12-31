using JoberMQ.Client.Net.Abstract.Connect;

namespace JoberMQ.Client.Net.Abstract.Factory
{
    internal interface IConnectionFactory
    {
        public IConnection CreateConnection();
    }
}
