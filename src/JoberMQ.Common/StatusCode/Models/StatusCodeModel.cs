using JoberMQ.Common.StatusCode.Enums;
using System.Collections.Generic;

namespace JoberMQ.Common.StatusCode.Models
{
    public class StatusCodeModel
    {
        public StatusCodeTypeEnum StatusCodeType { get; set; }
        public string StatusCode { get; set; }
        public List<StatusCodeMessageModel> StatusCodeMessages { get; set; }
    }
}
