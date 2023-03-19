using JoberMQ.Client.Net.Enums.Declare;
using JoberMQ.Client.Net.Models.DeclareConsume;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeSpecialExtension
    {
        public static DeclareConsumeSpecialModel SpecialAdd(this DeclareConsumeBuilderModel declareConsumeBuilder)
            => Add(declareConsumeBuilder.DeclareConsumeTransport, DeclareConsumeOperationTypeEnum.SpecialAdd, null);

        public static DeclareConsumeSpecialModel SpecialRemove(this DeclareConsumeBuilderModel declareConsumeBuilder)
            => Add(declareConsumeBuilder.DeclareConsumeTransport, DeclareConsumeOperationTypeEnum.SpecialRemove, null);

        private static DeclareConsumeSpecialModel Add(DeclareConsumeTransportModel declareConsumeTransport, DeclareConsumeOperationTypeEnum declareConsumeOperationType, string declareKey)
        {
            var result = new DeclareConsumeSpecialModel();
            result.DeclareConsumeTransport = declareConsumeTransport;
            result.DeclareConsumeTransport.DeclareConsumeOperationType = declareConsumeOperationType;
            result.DeclareConsumeTransport.DeclareKey = declareKey;
            return result;
        }
    }
}
