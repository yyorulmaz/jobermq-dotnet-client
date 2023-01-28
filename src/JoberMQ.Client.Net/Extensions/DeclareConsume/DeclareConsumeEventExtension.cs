using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Enums.Declare;
using JoberMQ.Client.Net.Models.DeclareConsume;
using System.ComponentModel;
using System.Linq;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeEventExtension
    {
        public static DeclareConsumeBuilderEventExtensionModel EventSubscript(this DeclareConsumeBuilderExtensionModel declareConsumeBuilderExtension, string eventKey)
            => Add(DeclareConsumeOperationTypeEnum.EventSubscript, eventKey);

        public static DeclareConsumeBuilderEventExtensionModel EventUnSubscript(this DeclareConsumeBuilderExtensionModel declareConsumeBuilderExtension, string eventKey)
            => Add(DeclareConsumeOperationTypeEnum.EventUnSubscript, eventKey);

        private static DeclareConsumeBuilderEventExtensionModel Add(DeclareConsumeOperationTypeEnum declareConsumeOperationType, string declareKey)
        {
            var declareConsumeBuilderEventExtension = new DeclareConsumeBuilderEventExtensionModel();
            declareConsumeBuilderEventExtension.DeclareConsumeBuilder.DeclareConsumeOperationType = declareConsumeOperationType;
            declareConsumeBuilderEventExtension.DeclareConsumeBuilder.DeclareKey = declareKey;
            return declareConsumeBuilderEventExtension;
        }

    }
}
