using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.Models.Rpc;
using Newtonsoft.Json;
using System.Threading.Tasks;

public static class PublishExtension
{
    public static async Task<ResponseModel> PublishAsync(this IClient client, JobDbo job)
        => await client.Connect.InvokeAsync<ResponseModel>("Job", JsonConvert.SerializeObject(job));
    public static async Task<ResponseModel> PublishAsync(this IClient client, MessageDbo message)
        => await client.Connect.InvokeAsync<ResponseModel>("Message", JsonConvert.SerializeObject(message));
    public static async Task<RpcResponseModel> PublishAsync(this IClient client, RpcRequestModel message)
        => await client.Connect.InvokeAsync<RpcResponseModel>("Rpc", JsonConvert.SerializeObject(message));
}