using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Enums.Declare;
using JoberMQ.Client.Net.Models.DeclareConsume;
using System.ComponentModel;
using System.Linq;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeQueueExtension
    {
        public static DeclareConsumeBuilderQueueExtensionModel QueueAdd(this DeclareConsumeBuilderExtensionModel declareConsumeBuilderExtension, string queueKey)
            => Add(DeclareConsumeOperationTypeEnum.QueueAdd, queueKey);

        public static DeclareConsumeBuilderQueueExtensionModel QueueRemove(this DeclareConsumeBuilderExtensionModel declareConsumeBuilderExtension, string queueKey)
            => Add(DeclareConsumeOperationTypeEnum.QueueRemove, queueKey);

        private static DeclareConsumeBuilderQueueExtensionModel Add(DeclareConsumeOperationTypeEnum declareConsumeOperationType, string declareKey)
        {
            var declareConsumeBuilderQueueExtension = new DeclareConsumeBuilderQueueExtensionModel();
            declareConsumeBuilderQueueExtension.DeclareConsumeBuilder.DeclareConsumeOperationType = declareConsumeOperationType;
            declareConsumeBuilderQueueExtension.DeclareConsumeBuilder.DeclareKey = declareKey;
            return declareConsumeBuilderQueueExtension;
        }



    }
}
