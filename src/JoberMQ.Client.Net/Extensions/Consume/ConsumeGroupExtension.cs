using JoberMQ.Library.Enums.Consume;
using JoberMQ.Library.Models.Consume;

namespace JoberMQ.Client.Net.Extensions.Consume
{
    public static class ConsumeGroupExtension
    {
        public static ConsumeGroupModel GroupAdd(this ConsumeBuilderModel consumeBuilder)
            => Add(consumeBuilder.ConsumeTransport, ConsumeOperationTypeEnum.GroupAdd, null);

        public static ConsumeGroupModel GroupRemove(this ConsumeBuilderModel consumeBuilder)
            => Add(consumeBuilder.ConsumeTransport, ConsumeOperationTypeEnum.GroupRemove, null);

        private static ConsumeGroupModel Add(ConsumeTransportModel consumeTransport, ConsumeOperationTypeEnum consumeOperationType, string declareKey)
        {
            var result = new ConsumeGroupModel();
            result.ConsumeTransport = consumeTransport;
            result.ConsumeTransport.ConsumeOperationType = consumeOperationType;
            result.ConsumeTransport.DeclareKey = declareKey;
            return result;
        }
    }
}
