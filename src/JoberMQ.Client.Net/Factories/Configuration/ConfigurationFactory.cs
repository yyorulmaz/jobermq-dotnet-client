using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Implementation.Configuration;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Client.Net.Factories.Configuration
{
    internal class ConfigurationFactory
    {
        public static IConfiguration Create(ConfigurationFactoryEnum factory)
        {
            IConfiguration configuration;

            switch (factory)
            {
                case ConfigurationFactoryEnum.Default:
                    configuration = new DefaultConfiguration();
                    break;
                default:
                    configuration = new DefaultConfiguration();
                    break;
            }

            return configuration;
        }
    }
}
