using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Common.Models.Base;
using System.Threading.Tasks;

namespace JoberMQ.Client.DotNet.Extension.Consume
{
    public static class ConsumeExtension
    {
        public static ConsumeBuilderModel Consume(this IClient client)
            => new ConsumeBuilderModel(client);

        public static async Task<ResponseBaseModel> SubAsync(this ConsumeBuilderModel consumeBuilder, string queueKey, bool isDurable)
            => await consumeBuilder.client.InvokeAsync<ResponseBaseModel>("ConsumeSub", consumeBuilder.client.ClientInfo.ClientKey, queueKey, isDurable);
        public static async Task<ResponseBaseModel> UnSubAsync(this ConsumeBuilderModel consumeBuilder, string queueKey)
            => await consumeBuilder.client.InvokeAsync<ResponseBaseModel>("ConsumeUnSub", consumeBuilder.client.ClientInfo.ClientKey, queueKey);

        public static async Task SubFreeGroupAsync(this ConsumeBuilderModel consumeBuilder, string groupKey)
            => await consumeBuilder.client.SendAsync("ConsumeSubFreeGroup", groupKey);
        public static async Task UnSubFreeGroupAsync(this ConsumeBuilderModel consumeBuilder, string groupKey)
            => await consumeBuilder.client.SendAsync("ConsumeUnSubFreeGroup", groupKey);
    }
    public class ConsumeBuilderModel
    {
        internal IClient client { get; private set; }
        public ConsumeBuilderModel(IClient client)
            => this.client = client;
    }
}
