using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.Job;
using JoberMQ.Client.Net.Models.Message;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobResultMessageExtension
    {
        public static JobBuilderResultMessageExtensionModel ResultMessage(this JobBuilderMessageExtensionModel jobBuilderMessageExtension, IMessage resultMessage)
           => Add(jobBuilderMessageExtension.Builder, resultMessage);

        private static JobBuilderResultMessageExtensionModel Add(JobBuilderModel builder, IMessage resultMessage = null)
        {
            builder.IsResult = true;

            var message = new MessageModel()
            {
                MessageType = resultMessage.MessageType,
                Message = resultMessage.Message,
                Routing = resultMessage.Routing,
                Info = resultMessage.Info,
                GeneralData = resultMessage.GeneralData,
                PriorityType = resultMessage.PriorityType
            };
            builder.ResultMessage = message;

            return new JobBuilderResultMessageExtensionModel { Builder = builder };
        }
    }
}
