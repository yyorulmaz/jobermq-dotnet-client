using JoberMQ.Client.Net.Models.DeclareDistributor;

namespace JoberMQ.Client.Net.Extensions.DeclareDistributor
{
    public static class DeclareDistributorBuildExtension
    {
        public static DeclareDistributorModel Build(this DeclareDistributorTransportModel declareDistributorTransport)
        {
            var result = new DeclareDistributorModel();
            result.DeclareDistributorOperationType = declareDistributorTransport.DeclareDistributorOperationType;
            result.DistributorKey = declareDistributorTransport.DistributorKey;
            result.DistributorType = declareDistributorTransport.DistributorType;
            result.PermissionType = declareDistributorTransport.PermissionType;
            result.IsDurable = declareDistributorTransport.IsDurable;

            return result;
        }
    }
}
