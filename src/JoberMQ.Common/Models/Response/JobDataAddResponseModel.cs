using JoberMQ.Common.Models.Base;
using System;

namespace JoberMQ.Common.Models.Response
{
    public class JobDataAddResponseModel : ResponseBaseModel
    {
        public Guid? JobId { get; set; }
    }
}
