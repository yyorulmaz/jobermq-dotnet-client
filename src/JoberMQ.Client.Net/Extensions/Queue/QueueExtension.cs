using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Enums.Queue;
using JoberMQ.Library.Models.Queue;

namespace JoberMQ.Client.Net.Extensions.Queue
{
    public static class QueueExtension
    {
        public static QueueTransportModel Create(
            this QueueBuilderModel queueBuilder,
            string queueKey,
            QueueMatchTypeEnum matchType = QueueMatchTypeEnum.Free,
            QueueOrderOfSendingTypeEnum queueOrderOfSendingType = QueueOrderOfSendingTypeEnum.FIFO,
            PermissionTypeEnum permissionType = PermissionTypeEnum.All,
            bool isDurable = true)
            => Operation(queueBuilder.QueueTransport, QueueOperationTypeEnum.Create, null, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable);

        public static QueueTransportModel Edit(
            this QueueBuilderModel queueBuilder,
            string queueKey,
            QueueMatchTypeEnum matchType = QueueMatchTypeEnum.Free,
            QueueOrderOfSendingTypeEnum queueOrderOfSendingType = QueueOrderOfSendingTypeEnum.FIFO,
            PermissionTypeEnum permissionType = PermissionTypeEnum.All,
            bool isDurable = true)
            => Operation(queueBuilder.QueueTransport, QueueOperationTypeEnum.Update, null, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable);

        public static QueueTransportModel Remove(
            this QueueBuilderModel queueBuilder,
            string queueKey)
            => Operation(queueBuilder.QueueTransport, QueueOperationTypeEnum.Remove, null, queueKey);

        public static QueueTransportModel Merge(
            this QueueBuilderModel queueBuilder,
            string distributorKey,
            string queueKey)
            => Operation(queueBuilder.QueueTransport, QueueOperationTypeEnum.DistributorMerge, distributorKey, queueKey);

        private static QueueTransportModel Operation(
            QueueTransportModel queueTransport,
            QueueOperationTypeEnum queueOperationType,
            string distributorKey,
            string queueKey,
            QueueMatchTypeEnum matchType = QueueMatchTypeEnum.Free,
            QueueOrderOfSendingTypeEnum queueOrderOfSendingType = QueueOrderOfSendingTypeEnum.FIFO,
            PermissionTypeEnum permissionType = PermissionTypeEnum.All,
            bool isDurable = true)
        {
            queueTransport.QueueOperationType = queueOperationType;
            queueTransport.DistributorKey = distributorKey;
            queueTransport.QueueKey = queueKey;
            queueTransport.MatchType = matchType;
            queueTransport.QueueOrderOfSendingType = queueOrderOfSendingType;
            queueTransport.PermissionType = permissionType;
            queueTransport.IsDurable = isDurable;
            return queueTransport;
        }
    }
}
