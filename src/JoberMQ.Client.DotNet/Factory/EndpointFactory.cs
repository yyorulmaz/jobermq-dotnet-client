using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Imp;
using JoberMQ.Common.Enums.Endpoint;

namespace JoberMQ.Client.DotNet.Factory
{
    internal class EndpointFactory
    {
        public static IEndpoint Create(EndpointFactoryEnum endpointFactory, bool IsSsl, string HostName, int Port, int PortSsl, string Action)
        {
            IEndpoint endpoint;

            switch (endpointFactory)
            {
                case EndpointFactoryEnum.Default:
                    endpoint = new EndpointDefault(IsSsl, HostName, Port, PortSsl, Action);
                    break;
                default:
                    endpoint = new EndpointDefault(IsSsl, HostName, Port, PortSsl, Action);
                    break;
            }

            return endpoint;
        }
    }
}
