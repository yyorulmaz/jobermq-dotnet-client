using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Job;

public static class JobBuildExtension
{
    public static JobDbo Build(this JobBuilderMessageExtensionModel jobBuilderMessageExtension) => jobBuilderMessageExtension.Job;
    public static JobDbo Build(this JobBuilderResultMessageExtensionModel jobBuilderResultMessageExtension) => jobBuilderResultMessageExtension.Job;
}
