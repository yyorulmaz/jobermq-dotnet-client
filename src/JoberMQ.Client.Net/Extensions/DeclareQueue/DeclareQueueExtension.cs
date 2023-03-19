using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Enums.Permission;
using JoberMQ.Client.Net.Enums.Queue;
using JoberMQ.Client.Net.Models.DeclareQueue;

namespace JoberMQ.Client.Net.Extensions.DeclareQueue
{
    public static class DeclareQueueExtension
    {
        public static DeclareQueueTransportModel Create(
            this DeclareQueueBuilderModel declareQueueBuilder,
            string queueKey,
            MatchTypeEnum matchType = MatchTypeEnum.Free,
            SendTypeEnum sendType = SendTypeEnum.FIFO,
            PermissionTypeEnum permissionType = PermissionTypeEnum.All,
            bool isDurable = true)
            => Operation(declareQueueBuilder.DeclareQueueTransport, DeclareQueueOperationTypeEnum.Create, null, queueKey, matchType, sendType, permissionType, isDurable);

        public static DeclareQueueTransportModel Update(
            this DeclareQueueBuilderModel declareQueueBuilder,
            string queueKey,
            MatchTypeEnum matchType = MatchTypeEnum.Free,
            SendTypeEnum sendType = SendTypeEnum.FIFO,
            PermissionTypeEnum permissionType = PermissionTypeEnum.All,
            bool isDurable = true)
            => Operation(declareQueueBuilder.DeclareQueueTransport, DeclareQueueOperationTypeEnum.Update, null, queueKey, matchType, sendType, permissionType, isDurable);

        public static DeclareQueueTransportModel Remove(
            this DeclareQueueBuilderModel declareQueueBuilder,
            string queueKey)
            => Operation(declareQueueBuilder.DeclareQueueTransport, DeclareQueueOperationTypeEnum.Remove, null, queueKey);

        public static DeclareQueueTransportModel Bind(
            this DeclareQueueBuilderModel declareQueueBuilder,
            string distributorKey,
            string queueKey)
            => Operation(declareQueueBuilder.DeclareQueueTransport, DeclareQueueOperationTypeEnum.DistributorBind, distributorKey, queueKey);

        private static DeclareQueueTransportModel Operation(
            DeclareQueueTransportModel declareQueueTransport,
            DeclareQueueOperationTypeEnum declareQueueOperationType,
            string distributorKey,
            string queueKey,
            MatchTypeEnum matchType = MatchTypeEnum.Free,
            SendTypeEnum sendType = SendTypeEnum.FIFO,
            PermissionTypeEnum permissionType = PermissionTypeEnum.All,
            bool isDurable = true)
        {
            declareQueueTransport.DeclareQueueOperationType = declareQueueOperationType;
            declareQueueTransport.DistributorKey = distributorKey;
            declareQueueTransport.QueueKey = queueKey;
            declareQueueTransport.MatchType = matchType;
            declareQueueTransport.SendType = sendType;
            declareQueueTransport.PermissionType = permissionType;
            declareQueueTransport.IsDurable = isDurable;
            return declareQueueTransport;
        }
    }
}
