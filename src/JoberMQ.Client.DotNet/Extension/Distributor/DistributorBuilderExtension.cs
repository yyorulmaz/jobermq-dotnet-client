using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Models.Distributor;

public static class DistributorBuilderExtension
{
    public static DistributorBuilderModel Distributor(this IClient client)
        => new DistributorBuilderModel(client);
}

public class DistributorBuilderModel
{
    internal IClient client { get; set; }
    internal DistributorBuilderModel(IClient client)
        => this.client = client;
}

public class DistributorAddBuilderModel
{
    internal IClient client { get; set; }
    internal string distributorKey { get; set; }
    internal DistributorTypeEnum distributorType { get; set; }
    internal DistributorSearchSourceTypeEnum distributorSearchSourceType { get; set; }
    internal PermissionTypeEnum permissionType { get; set; }
    internal bool isDurable { get; set; }
    internal DistributorAddBuilderModel(
        IClient client,
        string distributorKey,
        DistributorTypeEnum distributorType,
        DistributorSearchSourceTypeEnum distributorSearchSourceType,
        PermissionTypeEnum permissionType,
        bool isDurable)
    {
        this.client = client;
        this.distributorKey = distributorKey;
        this.distributorType = distributorType;
        this.distributorSearchSourceType = distributorSearchSourceType;
        this.permissionType = permissionType;
        this.isDurable = isDurable;
    }

    internal DistributorModel GetDistributorModel()
        => new DistributorModel(distributorKey, distributorType, distributorSearchSourceType, permissionType, isDurable);
}

public class DistributorGetBuilderModel
{
    internal IClient client { get; set; }
    internal string distributorKey { get; set; }
    internal DistributorGetBuilderModel(IClient client, string distributorKey)
    {
        this.client = client;
        this.distributorKey = distributorKey;
    }
}

public class DistributorGetAllBuilderModel
{
    internal IClient client { get; set; }
    internal DistributorGetAllBuilderModel(IClient client)
    {
        this.client = client;
    }
}

public class DistributorEditBuilderModel
{
    internal IClient client { get; set; }
    internal string distributorKey { get; set; }
    internal PermissionTypeEnum permissionType { get; set; }
    internal bool isDurable { get; set; }
    internal DistributorEditBuilderModel(
        IClient client,
        string distributorKey,
        PermissionTypeEnum permissionType,
        bool isDurable)
    {
        this.client = client;
        this.distributorKey = distributorKey;
        this.permissionType = permissionType;
        this.isDurable = isDurable;
    }

    internal DistributorModel GetDistributorModel()
        => new DistributorModel(distributorKey, null, null, permissionType, isDurable);
}

public class DistributorRemoveBuilderModel 
{
    internal IClient client { get; set; }
    internal string distributorKey { get; set; }
    internal DistributorRemoveBuilderModel(IClient client, string distributorKey)
    {
        this.client = client;
        this.distributorKey = distributorKey;
    }
}




