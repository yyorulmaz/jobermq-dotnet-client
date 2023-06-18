using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Common.Models.Base;
using System.Threading.Tasks;

    public static class ConsumeExtension
    {
        public static async Task<ResponseBaseModel> ConsumeSubAsync(this IClient client, string queueKey, bool isDurable)
            => await client.Connect.InvokeAsync<ResponseBaseModel>("ConsumeSub", client.ClientInfo.ClientKey, queueKey, isDurable);

        public static async Task<ResponseBaseModel> ConsumeUnSubAsync(this IClient client, string queueKey)
            => await client.Connect.InvokeAsync<ResponseBaseModel>("ConsumeUnSub", client.ClientInfo.ClientKey, queueKey);
    }
