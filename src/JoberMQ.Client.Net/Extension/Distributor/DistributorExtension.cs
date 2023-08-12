using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Distributor;
using System.Net;
using System.Threading.Tasks;

public static class DistributorExtension
{
    public static async Task<ResponseBaseModel<DistributorModel>> DistributorGetAsync(this IClient client, string distributorKey)
        => await client.Connect.InvokeAsync<ResponseBaseModel<DistributorModel>>("DistributorGet", distributorKey);
    public static async Task<ResponseBaseModel> DistributorAddAsync(this IClient client, string distributorKey, DistributorTypeEnum distributorType, DistributorSearchSourceTypeEnum distributorSearchSourceType, PermissionTypeEnum permissionType, bool isDurable)
        => await client.Connect.InvokeAsync<ResponseBaseModel>("DistributorAdd", new DistributorModel(distributorKey, distributorType, distributorSearchSourceType, permissionType, isDurable));

    public static async Task<ResponseBaseModel> DistributorEditAsync(this IClient client, string distributorKey, PermissionTypeEnum permissionType = PermissionTypeEnum.All, bool isDurable = true)
        => await client.Connect.InvokeAsync<ResponseBaseModel>("DistributorEdit", new DistributorModel(distributorKey, null, null, permissionType, isDurable));

    public static async Task<ResponseBaseModel> DistributorRemoveAsync(this IClient client, string distributorKey)
        => await client.Connect.InvokeAsync<ResponseBaseModel>("DistributorRemove", distributorKey);






    //public static DistributorBuilderModel Distributor(this IClient client)
    //    => new DistributorBuilderModel(ref client);


    //public static async Task<ResponseBaseModel<DistributorModel>> GetAsync(this DistributorBuilderModel distributorBuilder, string distributorKey)
    //    => await distributorBuilder.client.Connect.InvokeAsync<ResponseBaseModel<DistributorModel>>("DistributorGet", distributorKey);
    //public static async Task<ResponseBaseModel> AddAsync(this DistributorBuilderModel distributorBuilder, string distributorKey, DistributorTypeEnum distributorType, DistributorSearchSourceTypeEnum distributorSearchSourceType, PermissionTypeEnum permissionType, bool isDurable)
    //    => await distributorBuilder.client.Connect.InvokeAsync<ResponseBaseModel>("DistributorAdd", new DistributorModel(distributorKey, distributorType, distributorSearchSourceType, permissionType, isDurable));
    //public static async Task<ResponseBaseModel> EditAsync(this DistributorBuilderModel distributorBuilder, string distributorKey, PermissionTypeEnum permissionType = PermissionTypeEnum.All, bool isDurable = true)
    //    => await distributorBuilder.client.Connect.InvokeAsync<ResponseBaseModel>("DistributorEdit", new DistributorModel(distributorKey, null, null, permissionType, isDurable));
    //public static async Task<ResponseBaseModel> RemoveAsync(this DistributorBuilderModel distributorBuilder, string distributorKey)
    //    => await distributorBuilder.client.Connect.InvokeAsync<ResponseBaseModel>("DistributorRemove", distributorKey);
}
//public class DistributorBuilderModel
//{
//    internal IClient client { get; private set; }
//    public DistributorBuilderModel(ref IClient client)
//        => this.client = client;
//}
