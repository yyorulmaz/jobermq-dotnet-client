using JoberMQ.Library.Models.Distributor;

namespace JoberMQ.Client.Net.Extensions.Distributor
{
    public static class DistributorBuildExtension
    {
        public static DistributorModel Build(this DistributorTransportModel distributorTransport)
        {
            var result = new DistributorModel();
            result.DistributorOperationType = distributorTransport.DistributorOperationType;
            result.DistributorKey = distributorTransport.DistributorKey;
            result.DistributorType = distributorTransport.DistributorType;
            result.PermissionType = distributorTransport.PermissionType;
            result.IsDurable = distributorTransport.IsDurable;

            return result;
        }
    }
}
