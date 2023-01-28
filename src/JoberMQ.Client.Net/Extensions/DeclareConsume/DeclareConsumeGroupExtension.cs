using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Enums.Declare;
using JoberMQ.Client.Net.Models.DeclareConsume;
using System.ComponentModel;
using System.Linq;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeGroupExtension
    {
        public static DeclareConsumeBuilderGroupExtensionModel GroupAdd(this DeclareConsumeBuilderExtensionModel declareConsumeBuilderExtension)
            => Add(DeclareConsumeOperationTypeEnum.GroupAdd, null);

        public static DeclareConsumeBuilderGroupExtensionModel GroupRemove(this DeclareConsumeBuilderExtensionModel declareConsumeBuilderExtension)
            => Add(DeclareConsumeOperationTypeEnum.GroupRemove, null);

        private static DeclareConsumeBuilderGroupExtensionModel Add(DeclareConsumeOperationTypeEnum declareConsumeOperationType, string declareKey)
        {
            var declareConsumeBuilderGroupExtension = new DeclareConsumeBuilderGroupExtensionModel();
            declareConsumeBuilderGroupExtension.DeclareConsumeBuilder.DeclareConsumeOperationType = declareConsumeOperationType;
            declareConsumeBuilderGroupExtension.DeclareConsumeBuilder.DeclareKey = declareKey;
            return declareConsumeBuilderGroupExtension;
        }

    }
}
