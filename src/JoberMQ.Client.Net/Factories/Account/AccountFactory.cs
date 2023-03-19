using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Endpoint;
using JoberMQ.Client.Net.Enums.Account;
using JoberMQ.Client.Net.Implementation.Account.Default;

namespace JoberMQ.Client.Net.Factories.Account
{
    internal class AccountFactory
    {
        public static IAccount Create(IConfiguration configuration, bool isMaster, bool isActive, IEndpointDetail endpointDetail)
        {
            IAccount account;

            switch (configuration.AccountFactory)
            {
                case AccountFactoryEnum.Default:
                    account = new DfAccount(isMaster, isActive, configuration.UserName, configuration.Password, endpointDetail);
                    break;
                default:
                    account = new DfAccount(isMaster, isActive, configuration.UserName, configuration.Password, endpointDetail);
                    break;
            }

            return account;
        }

        public static IAccountInfo CreateAccountInfo(IConfiguration configuration, IEndpointDetail endpointDetail)
        {
            IAccountInfo accountInfo;

            switch (configuration.AccountInfoFactory)
            {
                case AccountInfoFactoryEnum.Default:
                    accountInfo = new DfAccountInfo(configuration, endpointDetail);
                    break;
                default:
                    accountInfo = new DfAccountInfo(configuration, endpointDetail);
                    break;
            }

            return accountInfo;
        }
    }
}
