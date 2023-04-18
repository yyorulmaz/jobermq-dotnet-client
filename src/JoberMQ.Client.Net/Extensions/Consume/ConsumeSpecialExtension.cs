using JoberMQ.Client.Net.Constants;
using JoberMQ.Library.Enums.Consume;
using JoberMQ.Library.Models.Consume;

namespace JoberMQ.Client.Net.Extensions.Consume
{
    public static class ConsumeSpecialExtension
    {
        public static ConsumeSpecialModel SpecialAdd(this ConsumeBuilderModel consumeBuilder)
            => Add(consumeBuilder.ConsumeTransport, ConsumeOperationTypeEnum.SpecialAdd, null);

        public static ConsumeSpecialModel SpecialRemove(this ConsumeBuilderModel consumeBuilder)
            => Add(consumeBuilder.ConsumeTransport, ConsumeOperationTypeEnum.SpecialRemove, null);

        private static ConsumeSpecialModel Add(ConsumeTransportModel consumeTransport, ConsumeOperationTypeEnum consumeOperationType, string declareKey)
        {
            var result = new ConsumeSpecialModel();
            result.ConsumeTransport = consumeTransport;
            result.ConsumeTransport.ConsumeOperationType = consumeOperationType;
            result.ConsumeTransport.DeclareKey = declareKey;
            return result;
        }
    }
}
