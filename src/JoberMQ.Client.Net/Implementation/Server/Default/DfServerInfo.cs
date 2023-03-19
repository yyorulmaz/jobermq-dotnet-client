using JoberMQ.Client.Net.Abstraction.Server;

namespace JoberMQ.Client.Net.Implementation.Server.Default
{
    public class DfServerInfo : IServerInfo
    {
        bool isServerActive;
        public bool IsServerActive => isServerActive;
    }
}
