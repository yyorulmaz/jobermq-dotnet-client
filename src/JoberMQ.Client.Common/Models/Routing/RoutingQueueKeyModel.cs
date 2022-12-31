using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Client.Common.Models.Routing
{
    public class RoutingQueueKeyModel
    {
        public string QueueName { get; set; }
        public string Key { get; set; }
    }
}
