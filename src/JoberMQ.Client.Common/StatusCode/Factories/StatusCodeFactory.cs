using JoberMQ.Client.Common.StatusCode.Abstraction;
using JoberMQ.Client.Common.StatusCode.Enums;
using JoberMQ.Client.Common.StatusCode.Implementation.Default;
using JoberMQ.Client.Common.StatusCode.Models;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Common.StatusCode.Factories
{
    internal class StatusCodeFactory
    {
        internal static IStatusCode Create(StatusCodeFactoryEnum factory, ConcurrentDictionary<string, StatusCodeModel> statusCodeData, StatusCodeMessageLanguageEnum defaultStatusCodeMessageLanguage)
        {
            IStatusCode statusCode;

            switch (factory)
            {
                case StatusCodeFactoryEnum.Default:
                    statusCode = new DfStatusCode(statusCodeData, defaultStatusCodeMessageLanguage);
                    break;
                default:
                    statusCode = new DfStatusCode(statusCodeData, defaultStatusCodeMessageLanguage);
                    break;
            }

            return statusCode;
        }
    }
}
