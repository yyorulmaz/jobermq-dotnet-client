using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Common.Models.Base;
using System.Threading.Tasks;

public static class ConsumeExtension
{
    public static ConsumeBuilderModel Consume(this IClient client)
        => new ConsumeBuilderModel(ref client);



    public static async Task<ResponseBaseModel> SubAsync(this ConsumeBuilderModel consumeBuilder, string queueKey, bool isDurable)
        => await consumeBuilder.client.Connect.InvokeAsync<ResponseBaseModel>("ConsumeSub", consumeBuilder.client.ClientInfo.ClientKey, queueKey, isDurable);
    public static async Task<ResponseBaseModel> UnSubAsync(this ConsumeBuilderModel consumeBuilder, string queueKey)
        => await consumeBuilder.client.Connect.InvokeAsync<ResponseBaseModel>("ConsumeUnSub", consumeBuilder.client.ClientInfo.ClientKey, queueKey);


    public static async Task SubFreeMessageGroup(this ConsumeBuilderModel consumeBuilder, string groupKey)
        => await consumeBuilder.client.Connect.SendAsync("ConsumeSubFreeMessageGroup", groupKey);
    public static async Task UnSubFreeMessageGroup(this ConsumeBuilderModel consumeBuilder, string groupKey)
        => await consumeBuilder.client.Connect.SendAsync("ConsumeUnSubFreeMessageGroup", groupKey);
}

public class ConsumeBuilderModel
{
    internal IClient client { get; private set; }
    public ConsumeBuilderModel(ref IClient client)
        => this.client = client;
}
