using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Imp;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Client.DotNet.Factory
{
    internal class ConfigurationFactory
    {
        public static IConfiguration Create(ConfigurationFactoryEnum factory)
        {
            IConfiguration configuration;

            switch (factory)
            {
                case ConfigurationFactoryEnum.Default:
                    configuration = new ConfigurationDefault();
                    break;
                default:
                    configuration = new ConfigurationDefault();
                    break;
            }

            return configuration;
        }
    }
}
