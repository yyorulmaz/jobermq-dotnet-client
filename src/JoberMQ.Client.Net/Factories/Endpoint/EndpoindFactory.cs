using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Endpoint;
using JoberMQ.Client.Net.Enums.Endpoint;
using JoberMQ.Client.Net.Implementation.Endpoint.Default;

namespace JoberMQ.Client.Net.Factories.Endpoint
{
    internal class EndpoindFactory
    {
        public static IEndpointDetail Create(IConfiguration configuration)
        {
            IEndpointDetail endpointDetail;

            switch (configuration.EndpoindFactory)
            {
                case EndpoindFactoryEnum.Default:
                    endpointDetail = new DfEndpointDetail(configuration);
                    break;
                default:
                    endpointDetail = new DfEndpointDetail(configuration);
                    break;
            }

            return endpointDetail;
        }
    }
}
