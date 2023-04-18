using JoberMQ.Library.Enums.Distributor;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Models.Distributor;

namespace JoberMQ.Client.Net.Extensions.Distributor
{
    public static class DistributorExtension
    {
        public static DistributorTransportModel Create(
            this DistributorBuilderModel distributorBuilder,
            string distributorKey,
            DistributorTypeEnum distributorType = DistributorTypeEnum.Direct,
            PermissionTypeEnum permissionType = PermissionTypeEnum.Group,
            bool isDurable = true)
            => Operation(distributorBuilder.DistributorTransport, DistributorOperationTypeEnum.Create, distributorKey, distributorType, permissionType, isDurable);

        public static DistributorTransportModel Edit(
            this DistributorBuilderModel distributorBuilder,
            string distributorKey,
            DistributorTypeEnum distributorType = DistributorTypeEnum.Direct,
            PermissionTypeEnum permissionType = PermissionTypeEnum.Group,
            bool isDurable = true)
            => Operation(distributorBuilder.DistributorTransport, DistributorOperationTypeEnum.Update, distributorKey, distributorType, permissionType, isDurable);

        public static DistributorTransportModel Remove(
            this DistributorBuilderModel distributorBuilder,
            string distributorKey)
            => Operation(distributorBuilder.DistributorTransport, DistributorOperationTypeEnum.Remove, distributorKey);

        private static DistributorTransportModel Operation(
            DistributorTransportModel distributorTransport,
            DistributorOperationTypeEnum distributorOperationType,
            string distributorKey,
            DistributorTypeEnum distributorType = DistributorTypeEnum.Direct,
            PermissionTypeEnum permissionType = PermissionTypeEnum.Group,
            bool isDurable = true)
        {
            distributorTransport.DistributorOperationType = distributorOperationType;
            distributorTransport.DistributorKey = distributorKey;
            distributorTransport.DistributorType = distributorType;
            distributorTransport.PermissionType = permissionType;
            distributorTransport.IsDurable = isDurable;
            return distributorTransport;
        }
    }
}
