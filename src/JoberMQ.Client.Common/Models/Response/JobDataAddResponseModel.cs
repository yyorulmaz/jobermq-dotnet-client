﻿using JoberMQ.Client.Common.Models.Base;

namespace JoberMQ.Client.Common.Models.Response
{
    public class JobDataAddResponseModel : ResponseBaseModel
    {
        public Guid? JobId { get; set; }
    }
}
