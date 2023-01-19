using JoberMQ.Client.Net.Models.Operation;
using JoberMQ.Client.Net.Models.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Dbos
{
    public class MessageDbo
    {
        public OperationModel Operation { get; set; }
        public ProducerModel Producer { get; set; }





        public string GeneralData { get; set; }
    }
}
