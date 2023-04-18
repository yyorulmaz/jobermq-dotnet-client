using JoberMQ.Library.Dbos;
using JoberMQ.Library.Models.Job;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobBuildExtension
    {
        public static JobDbo Build(this JobBuilderMessageExtensionModel jobBuilderMessageExtension) => jobBuilderMessageExtension.Job;
        public static JobDbo Build(this JobBuilderResultMessageExtensionModel jobBuilderResultMessageExtension) => jobBuilderResultMessageExtension.Job;
    }
}
