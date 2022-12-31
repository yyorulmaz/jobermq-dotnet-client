using JoberMQ.Common.StatusCode.Enums;

namespace JoberMQ.Common.StatusCode.Models
{
    public class StatusCodeMessageModel
    {
        public StatusCodeMessageLanguageEnum Language { get; set; }
        public string Message { get; set; }
    }
}
