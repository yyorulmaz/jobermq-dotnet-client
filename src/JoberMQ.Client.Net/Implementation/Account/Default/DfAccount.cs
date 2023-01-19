using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Endpoint;

namespace JoberMQ.Client.Net.Implementation.Account.Default
{
    internal class DfAccount : IAccount
    {
        public DfAccount(bool isMaster, bool isActive, string userName, string password, IEndpointDetail endpointDetail)
        {
            this.isMaster = isMaster;
            this.isActive = isActive;
            this.userName = userName;
            this.password = password;
            this.endpointDetail = endpointDetail;
        }

        public bool isMaster;
        public bool IsMaster { get => isMaster; set => isMaster = value; }

        public bool isActive;
        public bool IsActive { get => isActive; set => isActive = value; }

        public string EndpointLogin => endpointDetail.GetEndpointLogin();
        public string EndpointHub => endpointDetail.GetEndpointHub();

        public string userName;
        public string UserName => userName;

        public string password;
        public string Password => password;

        IEndpointDetail endpointDetail;
        public IEndpointDetail EndpointDetail => endpointDetail;
    }
}
