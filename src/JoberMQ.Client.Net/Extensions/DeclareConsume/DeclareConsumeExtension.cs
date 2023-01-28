using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Enums.Declare;
using JoberMQ.Client.Net.Models.DeclareConsume;
using System.ComponentModel;
using System.Linq;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeExtension
    {
        public static DeclareConsumeBuilderExtensionModel DeclareConsumeBuilder(this IClient client)
            => new DeclareConsumeBuilderExtensionModel();
        
    }
}
