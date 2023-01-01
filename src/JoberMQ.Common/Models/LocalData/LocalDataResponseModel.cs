using System;

namespace JoberMQ.Common.Models.LocalData
{
    internal class LocalDataResponseModel
    {
        public bool? IsServerActive { get; set; }
        public bool? IsOnline { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Guid? JobId { get; set; }
    }
}
