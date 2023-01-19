using JoberMQ.Client.Net.Models.Consuming;
using JoberMQ.Client.Net.Models.Message;
using JoberMQ.Library.Database.Base;
using System;

namespace JoberMQ.Client.Net.Dbos
{
    public class JobDetailDbo : DboPropertyGuidBase, IDboBase
    {
        public Guid? JobId { get; set; }

        public MessageModel Message { get; set; }
        public bool IsResult { get; set; }
        public MessageModel ResultMessage { get; set; }

       
        public ConsumingModel Consuming { get; set; }
    }
}
