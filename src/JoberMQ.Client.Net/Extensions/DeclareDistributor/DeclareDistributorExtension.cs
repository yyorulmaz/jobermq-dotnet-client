using JoberMQ.Client.Net.Enums.Distributor;
using JoberMQ.Client.Net.Enums.Permission;
using JoberMQ.Client.Net.Models.DeclareDistributor;

namespace JoberMQ.Client.Net.Extensions.DeclareDistributor
{
    public static class DeclareDistributorExtension
    {
        public static DeclareDistributorTransportModel Create(
            this DeclareDistributorBuilderModel declareDistributorBuilder, 
            string distributorKey, 
            DistributorTypeEnum distributorType = DistributorTypeEnum.Direct, 
            PermissionTypeEnum permissionType = PermissionTypeEnum.Group, 
            bool isDurable = true)
            => Operation(declareDistributorBuilder.DeclareDistributorTransport, DeclareDistributorOperationTypeEnum.Create, distributorKey, distributorType, permissionType, isDurable);

        public static DeclareDistributorTransportModel Update(
            this DeclareDistributorBuilderModel declareDistributorBuilder,
            string distributorKey,
            DistributorTypeEnum distributorType = DistributorTypeEnum.Direct,
            PermissionTypeEnum permissionType = PermissionTypeEnum.Group,
            bool isDurable = true)
            => Operation(declareDistributorBuilder.DeclareDistributorTransport, DeclareDistributorOperationTypeEnum.Update, distributorKey, distributorType, permissionType, isDurable);

        public static DeclareDistributorTransportModel Remove(
            this DeclareDistributorBuilderModel declareDistributorBuilder, 
            string distributorKey)
            => Operation(declareDistributorBuilder.DeclareDistributorTransport, DeclareDistributorOperationTypeEnum.Remove, distributorKey);

        private static DeclareDistributorTransportModel Operation(
            DeclareDistributorTransportModel declareDistributorTransport,
            DeclareDistributorOperationTypeEnum declareDistributorOperationType,
            string distributorKey,
            DistributorTypeEnum distributorType = DistributorTypeEnum.Direct,
            PermissionTypeEnum permissionType = PermissionTypeEnum.Group,
            bool isDurable = true)
        {
            declareDistributorTransport.DeclareDistributorOperationType = declareDistributorOperationType;
            declareDistributorTransport.DistributorKey = distributorKey;
            declareDistributorTransport.DistributorType = distributorType;
            declareDistributorTransport.PermissionType = permissionType;
            declareDistributorTransport.IsDurable = isDurable;
            return declareDistributorTransport;
        }
    }
}
