using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.StatusCode.Enums;
using JoberMQ.Common.StatusCode.Models;

namespace JoberMQ.Common.StatusCode.Abstraction
{
    internal interface IStatusCode
    {
        string GetStatusMessage(string statusCode);
        string GetStatusMessage(string statusCode, StatusCodeMessageLanguageEnum language);
    }
}
