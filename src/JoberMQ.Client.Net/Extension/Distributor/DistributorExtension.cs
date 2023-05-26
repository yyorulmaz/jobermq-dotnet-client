using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Distributor;
using System.Threading.Tasks;

public static class DistributorExtension
{
    public static async Task<ResponseBaseModel<DistributorModel>> DistributorGetAsync(this IClient client, string distributorKey)
    {
        return await client.Connect.InvokeAsync<ResponseBaseModel<DistributorModel>>("DistributorGet", distributorKey);
    }
    public static async Task<ResponseBaseModel> DistributorCreateAsync(this IClient client, string distributorKey, DistributorTypeEnum distributorType, DistributorSearchSourceTypeEnum distributorSearchSourceType, PermissionTypeEnum permissionType, bool isDurable)
    {
        var distributor = new DistributorModel(distributorKey, distributorType, distributorSearchSourceType, permissionType, isDurable);
        return await client.Connect.InvokeAsync<ResponseBaseModel>("DistributorCreate", distributor);
    }

    public static async Task<ResponseBaseModel> DistributorEditAsync(this IClient client, string distributorKey, PermissionTypeEnum permissionType = PermissionTypeEnum.All, bool isDurable = true)
    {
        var distributor = new DistributorModel(distributorKey, null, null, permissionType, isDurable);
        return await client.Connect.InvokeAsync<ResponseBaseModel>("DistributorEdit", distributor);
    }

    public static async Task<ResponseBaseModel> DistributorRemoveAsync(this IClient client, string distributorKey)
    {
        return await client.Connect.InvokeAsync<ResponseBaseModel>("DistributorRemove", distributorKey);
    }

}
