using JoberMQ.Library.Enums.Consume;
using JoberMQ.Library.Models.Consume;

namespace JoberMQ.Client.Net.Extensions.Consume
{
    public static class ConsumeEventExtension
    {
        public static ConsumeEventModel EventSubscript(this ConsumeBuilderModel consumeBuilder, string eventKey)
            => Add(consumeBuilder.ConsumeTransport, ConsumeOperationTypeEnum.EventSubscript, eventKey);

        public static ConsumeEventModel EventUnSubscript(this ConsumeBuilderModel consumeBuilder, string eventKey)
            => Add(consumeBuilder.ConsumeTransport, ConsumeOperationTypeEnum.EventUnSubscript, eventKey);

        private static ConsumeEventModel Add(ConsumeTransportModel consumeTransport, ConsumeOperationTypeEnum consumeOperationType, string declareKey)
        {
            var result = new ConsumeEventModel();
            result.ConsumeTransport = consumeTransport;
            result.ConsumeTransport.ConsumeOperationType = consumeOperationType;
            result.ConsumeTransport.DeclareKey = declareKey;
            return result;
        }
    }
}
