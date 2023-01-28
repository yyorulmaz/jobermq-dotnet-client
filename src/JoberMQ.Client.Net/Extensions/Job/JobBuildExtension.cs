using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.Job;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobBuildExtension
    {
        public static JobBuilderModel Build(this JobBuilderMessageExtensionModel jobBuilderMessageExtension) => jobBuilderMessageExtension.Builder;
        public static JobBuilderModel Build(this JobBuilderResultMessageExtensionModel jobBuilderResultMessageExtension) => jobBuilderResultMessageExtension.Builder;
    }
}
