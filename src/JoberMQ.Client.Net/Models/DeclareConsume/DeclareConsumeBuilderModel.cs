using JoberMQ.Client.Net.Enums.Declare;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Client.Net.Models.DeclareConsume
{
    public class DeclareConsumeBuilderModel
    {
        public DeclareConsumeOperationTypeEnum DeclareConsumeOperationType { get; set; }
        public string DeclareKey { get; set; }
    }
}
