using JoberMQ.Client.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Client.Common.StatusCode.Enums;
using JoberMQ.Client.Common.StatusCode.Models;

namespace JoberMQ.Client.Common.StatusCode.Abstraction
{
    internal interface IStatusCode
    {
        string GetStatusMessage(string statusCode);
        string GetStatusMessage(string statusCode, StatusCodeMessageLanguageEnum language);
    }
}
