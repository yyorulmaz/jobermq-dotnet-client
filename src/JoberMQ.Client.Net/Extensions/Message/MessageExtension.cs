using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Enums.Operation;
using JoberMQ.Client.Net.Enums.Publisher;
using JoberMQ.Client.Net.Enums.Timing;
using JoberMQ.Client.Net.Models.Info;
using JoberMQ.Client.Net.Models.MessageBuilder;
using JoberMQ.Client.Net.Models.Operation;
using JoberMQ.Client.Net.Models.Publisher;
using JoberMQ.Client.Net.Models.Timing;
using Quartz;

namespace JoberMQ.Client.Net.Extensions.Message
{
    public static class MessageExtension
    {
        public static MessageBuilderExtensionModel MessageBuilder(this IClient client, InfoModel info = null)
            => Add(client, info);

        private static MessageBuilderExtensionModel Add(IClient client, InfoModel info = null)
        {
            var messageBuilder = new MessageBuilderExtensionModel();
            messageBuilder.Builder.Operation = new OperationModel { OperationType = OperationTypeEnum.Message };
            messageBuilder.Builder.Info = info;
            messageBuilder.Builder.ClientInfo = client.ClientInfo;
            messageBuilder.Builder.Publisher = new PublisherModel { PublisherType = PublisherTypeEnum.Standart };
            messageBuilder.Builder.Timing = new TimingModel { TimingType = TimingTypeEnum.Now };
            messageBuilder.Builder.IsResult = false;

            return messageBuilder;
        }
    }

    #region eski
    //public static class MessageExtension
    //{
    //    public static MessageBuilderModel JobBuilder(this IClient client, InfoModel info = null)
    //        => Add(client, info);

    //    private static MessageBuilderModel Add(IClient client, InfoModel info = null)
    //    {
    //        var messageBuilder = new MessageBuilderModel();
    //        messageBuilder.Builder.Operation = new OperationModel { OperationType = OperationTypeEnum.Message };
    //        messageBuilder.Builder.Info = info;
    //        messageBuilder.Builder.Producer = client.Producer;
    //        messageBuilder.Builder.Publisher = new PublisherModel { PublisherType = PublisherTypeEnum.Standart };
    //        messageBuilder.Builder.Timing = new TimingModel { TimingType = TimingTypeEnum.Now };

    //        return messageBuilder;
    //    }
    //} 
    #endregion
}
