using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.Job;
using JoberMQ.Client.Net.Models.Message;
using JoberMQ.Client.Net.Models.MessageBuilder;

namespace JoberMQ.Client.Net.Extensions.Message
{
    public static class MessageResultMessageExtension
    {
        public static MessageBuilderResultMessageExtensionModel ResultMessage(this MessageBuilderMessageExtensionModel messageBuilderMessageExtension, IMessage resultMessage)
           => Add(messageBuilderMessageExtension.Builder, resultMessage);

        private static MessageBuilderResultMessageExtensionModel Add(MessageBuilderModel builder, IMessage resultMessage = null)
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

            return new MessageBuilderResultMessageExtensionModel { Builder = builder };
        }
    }
}
