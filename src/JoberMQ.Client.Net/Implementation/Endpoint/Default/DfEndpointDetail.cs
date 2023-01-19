using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Endpoint;
using JoberMQ.Client.Net.Constants;
using JoberMQ.Client.Net.Enums.Protocol;

namespace JoberMQ.Client.Net.Implementation.Endpoint.Default
{
    internal class DfEndpointDetail : IEndpointDetail
    {
        IConfiguration configuration;
        public DfEndpointDetail(IConfiguration configuration)
        {
            this.configuration = configuration;

            this.hostName = configuration.HostName;
            this.port = configuration.Port;
            this.portSsl = configuration.PortSsl;
            this.actionHub = ClientConst.ActionHub;
            this.actionLogin = ClientConst.ActionLogin;
        }

        bool isSsl;
        public bool IsSsl { get => isSsl; set => isSsl = value; }


        string hostName;
        public string HostName { get => hostName; set => hostName = value; }


        int port;
        public int Port { get => port; set => port = value; }

        int portSsl;
        public int PortSsl { get => portSsl; set => portSsl = value; }


        string actionHub;
        public string ActionHub { get => actionHub; set => actionHub = value; }


        string actionLogin;
        public string ActionLogin { get => actionLogin; set => actionLogin = value; }

        public string GetEndpointLogin()
        {
            var urlProtocol = isSsl == true ? UrlProtocolEnum.https.ToString() : UrlProtocolEnum.http.ToString();

            string portNo;
            if (isSsl)
                portNo = portSsl <= 0 ? "" : $":{portSsl}";
            else
                portNo = port <= 0 ? "" : $":{port}";

            return $"{urlProtocol}://{hostName}{portNo}/{actionLogin}";
        }
        public string GetEndpointHub()
        {
            var urlProtocol = isSsl == true ? UrlProtocolEnum.https.ToString() : UrlProtocolEnum.http.ToString();
            string portNo;
            if (isSsl)
                portNo = portSsl <= 0 ? "" : $":{portSsl}";
            else
                portNo = port <= 0 ? "" : $":{port}";

            return $"{urlProtocol}://{hostName}{portNo}/{actionHub}";
        }
    }
}
