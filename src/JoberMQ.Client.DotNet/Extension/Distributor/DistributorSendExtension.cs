using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Distributor;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class DistributorSendExtension
{
    public static async Task<ResponseBaseModel> SendAsync(this DistributorAddBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel>("DistributorAdd", builder.GetDistributorModel());

    public static async Task<ResponseBaseModel<DistributorModel>> SendAsync(this DistributorGetBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel<DistributorModel>>("DistributorGet", builder.distributorKey);

    public static async Task<ResponseBaseModel<List<DistributorModel>>> SendAsync(this DistributorGetAllBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel<List<DistributorModel>>>("DistributorGetAll");

    public static async Task<ResponseBaseModel> SendAsync(this DistributorEditBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel>("DistributorEdit", builder.GetDistributorModel());

    public static async Task<ResponseBaseModel> SendAsync(this DistributorRemoveBuilderModel builder)
        => await builder.client.InvokeAsync<ResponseBaseModel>("DistributorRemove", builder.distributorKey);
}