using JoberMQ.Client.Net.Enums.Declare;
using JoberMQ.Client.Net.Models.DeclareConsume;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeQueueExtension
    {
        public static DeclareConsumeQueueModel QueueAdd(this DeclareConsumeBuilderModel declareConsumeBuilder, string queueKey)
            => Add(declareConsumeBuilder.DeclareConsumeTransport, DeclareConsumeOperationTypeEnum.QueueAdd, queueKey);

        public static DeclareConsumeQueueModel QueueRemove(this DeclareConsumeBuilderModel declareConsumeBuilder, string queueKey)
            => Add(declareConsumeBuilder.DeclareConsumeTransport, DeclareConsumeOperationTypeEnum.QueueRemove, queueKey);

        private static DeclareConsumeQueueModel Add(DeclareConsumeTransportModel declareConsumeTransport, DeclareConsumeOperationTypeEnum declareConsumeOperationType, string declareKey)
        {
            var result = new DeclareConsumeQueueModel();
            result.DeclareConsumeTransport = declareConsumeTransport;
            result.DeclareConsumeTransport.DeclareConsumeOperationType = declareConsumeOperationType;
            result.DeclareConsumeTransport.DeclareKey = declareKey;
            return result;
        }
    }
}
