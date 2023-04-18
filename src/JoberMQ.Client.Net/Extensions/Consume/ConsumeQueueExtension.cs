using JoberMQ.Library.Enums.Consume;
using JoberMQ.Library.Models.Consume;

namespace JoberMQ.Client.Net.Extensions.Consume
{
    public static class ConsumeQueueExtension
    {
        public static ConsumeQueueModel QueueAdd(this ConsumeBuilderModel consumeBuilder, string queueKey)
            => Add(consumeBuilder.ConsumeTransport, ConsumeOperationTypeEnum.QueueAdd, queueKey);

        public static ConsumeQueueModel QueueRemove(this ConsumeBuilderModel consumeBuilder, string queueKey)
            => Add(consumeBuilder.ConsumeTransport, ConsumeOperationTypeEnum.QueueRemove, queueKey);

        private static ConsumeQueueModel Add(ConsumeTransportModel consumeTransport, ConsumeOperationTypeEnum consumeOperationType, string declareKey)
        {
            var result = new ConsumeQueueModel();
            result.ConsumeTransport = consumeTransport;
            result.ConsumeTransport.ConsumeOperationType = consumeOperationType;
            result.ConsumeTransport.DeclareKey = declareKey;
            return result;
        }
    }
}
