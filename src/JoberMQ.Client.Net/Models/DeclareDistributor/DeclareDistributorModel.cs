using JoberMQ.Client.Net.Enums.Distributor;
using JoberMQ.Client.Net.Enums.Permission;

namespace JoberMQ.Client.Net.Models.DeclareDistributor
{
    public class DeclareDistributorModel
    {
        public DeclareDistributorOperationTypeEnum DeclareDistributorOperationType { get; internal set; }
        public string DistributorKey { get; set; }
        public DistributorTypeEnum DistributorType { get; set; }
        public PermissionTypeEnum PermissionType { get; set; }
        public bool IsDurable { get; set; }
    }
}
