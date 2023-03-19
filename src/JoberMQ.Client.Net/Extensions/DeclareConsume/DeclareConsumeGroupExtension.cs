using JoberMQ.Client.Net.Enums.Declare;
using JoberMQ.Client.Net.Models.DeclareConsume;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeGroupExtension
    {
        public static DeclareConsumeGroupModel GroupAdd(this DeclareConsumeBuilderModel declareConsumeBuilder)
            => Add(declareConsumeBuilder.DeclareConsumeTransport, DeclareConsumeOperationTypeEnum.GroupAdd, null);

        public static DeclareConsumeGroupModel GroupRemove(this DeclareConsumeBuilderModel declareConsumeBuilder)
            => Add(declareConsumeBuilder.DeclareConsumeTransport, DeclareConsumeOperationTypeEnum.GroupRemove, null);

        private static DeclareConsumeGroupModel Add(DeclareConsumeTransportModel declareConsumeTransport, DeclareConsumeOperationTypeEnum declareConsumeOperationType, string declareKey)
        {
            var result = new DeclareConsumeGroupModel();
            result.DeclareConsumeTransport = declareConsumeTransport;
            result.DeclareConsumeTransport.DeclareConsumeOperationType = declareConsumeOperationType;
            result.DeclareConsumeTransport.DeclareKey = declareKey;
            return result;
        }
    }
}
