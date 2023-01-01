using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Constants;
using JoberMQ.Client.Net.Implementation.Configuration.Default;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Client.Net.Factories.Configuration
{
    public class ConfigurationFactory
    {
        public static IConfiguration CreateConfiguration(ConfigurationFactoryEnum factory)
        {
            IConfiguration configuration;

            switch (factory)
            {
                case ConfigurationFactoryEnum.Default:
                    configuration = new DfConfiguration();
                    break;
                default:
                    configuration = new DfConfiguration();
                    break;
            }



            if (configuration.ConnectionRetryTimeout < ClientConst.ConnectionRetryTimeoutMin)
                configuration.ConnectionRetryTimeout = ClientConst.ConnectionRetryTimeout;



            return configuration;
        }
    }
}
