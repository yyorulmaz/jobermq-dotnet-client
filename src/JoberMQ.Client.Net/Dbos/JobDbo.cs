using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.Message;
using JoberMQ.Client.Net.Models.Operation;
using JoberMQ.Client.Net.Models.Producer;
using JoberMQ.Client.Net.Models.Publisher;
using JoberMQ.Client.Net.Models.Routing;
using JoberMQ.Client.Net.Models.Status;
using JoberMQ.Client.Net.Models.Timing;
using JoberMQ.Library.Database.Base;
using System.Collections.Generic;

namespace JoberMQ.Client.Net.Dbos
{
    public class JobDbo : DboPropertyGuidBase, IDboBase
    {
        public OperationModel Operation { get; set; }
        public ProducerModel Producer { get; set; }
        public InfoModel Info { get; set; }
        public PublisherModel Publisher { get; set; }
        public TimingModel Timing { get; set; }
        
        public bool IsResult { get; set; }
        public MessageModel ResultMessage { get; set; }
        public StatusModel Status { get; set; }
        public int Version { get; set; }
        public List<JobDetailDbo> JobDetails { get; set; }

    }
}
