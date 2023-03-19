using JoberMQ.Client.Net.Enums.Declare;
using JoberMQ.Client.Net.Models.DeclareConsume;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeEventExtension
    {
        public static DeclareConsumeEventModel EventSubscript(this DeclareConsumeBuilderModel declareConsumeBuilder, string eventKey)
            => Add(declareConsumeBuilder.DeclareConsumeTransport, DeclareConsumeOperationTypeEnum.EventSubscript, eventKey);

        public static DeclareConsumeEventModel EventUnSubscript(this DeclareConsumeBuilderModel declareConsumeBuilder, string eventKey)
            => Add(declareConsumeBuilder.DeclareConsumeTransport, DeclareConsumeOperationTypeEnum.EventUnSubscript, eventKey);

        private static DeclareConsumeEventModel Add(DeclareConsumeTransportModel declareConsumeTransport, DeclareConsumeOperationTypeEnum declareConsumeOperationType, string declareKey)
        {
            var result = new DeclareConsumeEventModel();
            result.DeclareConsumeTransport = declareConsumeTransport;
            result.DeclareConsumeTransport.DeclareConsumeOperationType = declareConsumeOperationType;
            result.DeclareConsumeTransport.DeclareKey = declareKey;
            return result;
        }
    }
}
