﻿using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Implementation.Configuration.Default;
using JoberMQ.Library.Enums.Configuration;

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
                    configuration = new DfConfiguration();
                    break;
                default:
                    configuration = new DfConfiguration();
                    break;
            }

            return configuration;
        }
    }
}
