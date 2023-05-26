using JoberMQ.Client.Net.Abstraction.Endpoint;
using JoberMQ.Client.Net.Implementation.Endpoint.Default;
using JoberMQ.Common.Enums.Endpoint;

namespace JoberMQ.Client.Net.Factories.Endpoint
{
    public class EndpointFactory
    {
        public static IEndpoint Create(EndpointFactoryEnum endpointFactory, bool IsSsl, string HostName, int Port, int PortSsl, string Action)
        {
            IEndpoint endpoint;

            switch (endpointFactory)
            {
                case EndpointFactoryEnum.Default:
                    endpoint = new DefaultEndpoint(IsSsl, HostName, Port, PortSsl, Action);
                    break;
                default:
                    endpoint = new DefaultEndpoint(IsSsl, HostName, Port, PortSsl, Action);
                    break;
            }

            return endpoint;
        }
    }
}
