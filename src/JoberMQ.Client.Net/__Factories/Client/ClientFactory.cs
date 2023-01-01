using JoberMQ.Client.Net.Implementation.Client.Default;
using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Implementation.Configuration.Default;
using JoberMQ.Common.Enums.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.__Factories.Client
{
    internal class ClientFactory
    {
        public static IClient Create(IConfiguration configuration, IConnection connection)
        {
            IClient client;

            switch (configuration.ClientFactory)
            {
                case ClientFactoryEnum.Default:
                    client = new DfClient(configuration, connection);
                    break;
                default:
                    client = new DfClient(configuration, connection);
                    break;
            }

            return client;
        }
    }
}
