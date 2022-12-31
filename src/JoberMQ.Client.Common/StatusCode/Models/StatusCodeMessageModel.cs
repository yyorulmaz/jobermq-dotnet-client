using JoberMQ.Client.Common.StatusCode.Enums;

namespace JoberMQ.Client.Common.StatusCode.Models
{
    public class StatusCodeMessageModel
    {
        public StatusCodeMessageLanguageEnum Language { get; set; }
        public string Message { get; set; }
    }
}
