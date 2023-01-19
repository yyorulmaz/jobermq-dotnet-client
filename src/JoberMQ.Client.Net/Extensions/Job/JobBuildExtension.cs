using JoberMQ.Client.Net.Models.Builder;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobBuildExtension
    {
        public static BuilderModel Build(this JobBuilderMessageModel jobBuilderMessage) => jobBuilderMessage.Builder;
        public static BuilderModel Build(this JobBuilderResultMessageModel jobBuilderResultMessage) => jobBuilderResultMessage.Builder;
    }
}
