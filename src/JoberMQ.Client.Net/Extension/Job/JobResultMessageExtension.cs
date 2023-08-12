using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Constant;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Consuming;
using JoberMQ.Common.Models.Job;
using JoberMQ.Common.Models.Message;

public static class JobResultMessageExtension
{
    public static JobBuilderResultMessageExtensionModel ResultMessage(this JobBuilderMessageExtensionModel jobBuilderMessageExtension, IMessage resultMessage, bool isConsumingRetryPause = ClientConst.IsConsumingRetryPause, int consumingRetryMaxCount = ClientConst.ConsumingRetryMaxCount)
       => Add(jobBuilderMessageExtension.Job, resultMessage, isConsumingRetryPause, consumingRetryMaxCount);

    private static JobBuilderResultMessageExtensionModel Add(JobDbo builder, IMessage resultMessage, bool isConsumingRetryPause, int consumingRetryMaxCount)
    {
        builder.IsJobResultMessage = true;

        var message = new MessageModel()
        {
            MessageType = resultMessage.MessageType,
            Message = resultMessage.Message,
            Routing = resultMessage.Routing,
            Info = resultMessage.Info,
            GeneralData = resultMessage.GeneralData,
            PriorityType = resultMessage.PriorityType,
            MessageConsuming = resultMessage.MessageConsuming,
        };
        builder.JobResultMessage = message;

        builder.JobResultMessageConsuming = new ConsumingModel
        {
            IsConsumingRetryPause = isConsumingRetryPause,
            ConsumingRetryMaxCount = consumingRetryMaxCount
        };

        return new JobBuilderResultMessageExtensionModel { Job = builder };
    }
}