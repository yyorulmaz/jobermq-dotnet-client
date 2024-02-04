using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;

public static class DistributorExtension
{
    public static DistributorAddBuilderModel Add(this DistributorBuilderModel builder, string distributorKey, DistributorTypeEnum distributorType, DistributorSearchSourceTypeEnum distributorSearchSourceType, PermissionTypeEnum permissionType, bool isDurable)
        => new DistributorAddBuilderModel(builder.client, distributorKey, distributorType, distributorSearchSourceType, permissionType, isDurable);

    public static DistributorGetBuilderModel Get(this DistributorBuilderModel builder, string distributorKey)
        => new DistributorGetBuilderModel(builder.client, distributorKey);

    public static DistributorGetAllBuilderModel GetAll(this DistributorBuilderModel builder)
        => new DistributorGetAllBuilderModel(builder.client);

    public static DistributorEditBuilderModel Edit(this DistributorBuilderModel builder, string distributorKey, PermissionTypeEnum permissionType = PermissionTypeEnum.All, bool isDurable = true)
        => new DistributorEditBuilderModel(builder.client, distributorKey, permissionType, isDurable);

    public static DistributorRemoveBuilderModel Remove(this DistributorBuilderModel builder, string distributorKey)
        => new DistributorRemoveBuilderModel(builder.client, distributorKey);
}

