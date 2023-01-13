using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Implementation.Client.Default;
using JoberMQ.Client.Net.Implementation.Connect.Default;
using JoberMQ.Common.Enums.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Factories.Connection
{
    internal class ConnectionFactory
    {
        public static IConnection Create(IConfiguration configuration)
        {
            IConnection connection;

            switch (configuration.ConnectProtocol)
            {
                case ConnectProtocolEnum.Socket:
                    connection = new DfConnectionSocket(configuration);
                    break;
                default:
                    connection = new DfConnectionSocket(configuration);
                    break;
            }

            return connection;
        }
    }
}
