using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.Message;

namespace JoberMQ.Client.Net.Extensions.Message
{
    public static class MessageResultMessageExtension
    {
        public static MessageBuilderResultMessageModel ResultMessage(this JobBuilderMessageModel jobBuilderMessage, IMessage resultMessage)
           => Add(jobBuilderMessage.Builder, resultMessage);

        private static MessageBuilderResultMessageModel Add(BuilderModel builder, IMessage resultMessage = null)
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

            return new MessageBuilderResultMessageModel { Builder = builder };
        }
    }
}
