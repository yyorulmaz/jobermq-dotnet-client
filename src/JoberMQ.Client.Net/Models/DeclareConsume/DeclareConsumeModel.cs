
using JoberMQ.Client.Net.Enums.Declare;

namespace JoberMQ.Client.Net.Models.DeclareConsume
{
    public class DeclareConsumeModel
    {
        public DeclareConsumeTypeEnum DeclareConsumeType { get; set; }
        public string DeclareKey { get; set; }
    }
}
