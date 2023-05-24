using JoberMQ.Library.Models.Queue;

namespace JoberMQ.Client.Net.Extensions.Queue
{
    public static class QueueBuildExtension
    {
        public static QueueModel Build(this QueueTransportModel queueTransport)
        {
            var result = new QueueModel();
            result.QueueOperationType = queueTransport.QueueOperationType;
            result.DistributorKey = queueTransport.DistributorKey;
            result.QueueKey = queueTransport.QueueKey;
            result.MatchType = queueTransport.MatchType;
            result.QueueOrderOfSendingType = queueTransport.QueueOrderOfSendingType;
            result.PermissionType = queueTransport.PermissionType;
            result.IsDurable = queueTransport.IsDurable;

            return result;
        }
    }
}
