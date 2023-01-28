using JoberMQ.Client.Net.Models.Client;
using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.Message;
using JoberMQ.Client.Net.Models.Multiple;
using JoberMQ.Client.Net.Models.Operation;
using JoberMQ.Client.Net.Models.Publisher;
using JoberMQ.Client.Net.Models.Timing;
using System.Collections.Generic;

namespace JoberMQ.Client.Net.Models.Builder
{
    public class JobBuilderModel
    {
        public OperationModel Operation { get; set; } = new OperationModel();
        public ClientInfoModel ClientInfo { get; set; } = new ClientInfoModel();
        public InfoModel Info { get; set; } = new InfoModel();
        public PublisherModel Publisher { get; set; } = new PublisherModel();
        public TimingModel Timing { get; set; } = new TimingModel();

        public bool IsResult { get; set; }
        public MessageModel ResultMessage { get; set; }

        public List<MultipleMessageModel> MultipleMessages { get; set; } = new List<MultipleMessageModel>();
    }
}
