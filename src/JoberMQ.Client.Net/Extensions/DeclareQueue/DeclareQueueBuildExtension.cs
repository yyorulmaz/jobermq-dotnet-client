using JoberMQ.Client.Net.Models.DeclareQueue;

namespace JoberMQ.Client.Net.Extensions.DeclareQueue
{
    public static class DeclareQueueBuildExtension
    {
        public static DeclareQueueModel Build(this DeclareQueueTransportModel declareQueueTransport)
        {
            var result = new DeclareQueueModel();
            result.DeclareQueueOperationType = declareQueueTransport.DeclareQueueOperationType;
            result.DistributorKey = declareQueueTransport.DistributorKey;
            result.QueueKey = declareQueueTransport.QueueKey;
            result.MatchType = declareQueueTransport.MatchType;
            result.SendType = declareQueueTransport.SendType;
            result.PermissionType = declareQueueTransport.PermissionType;
            result.IsDurable = declareQueueTransport.IsDurable;

            return result;
        }
    }
}
