using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Common.Models.Message;
using System.Threading.Tasks;

public static class FreeMessageExtension
{
    public static FreeMessageClientExtensionModel<IClient> FreeMessageToClient(this IClient client, string message, string clientKey)
        => new FreeMessageClientExtensionModel<IClient> { Client = client, Message = message, Key = clientKey };
    public static FreeMessageGroupExtensionModel<IClient> FreeMessageToGroup(this IClient client, string message, string groupKey)
        => new FreeMessageGroupExtensionModel<IClient> { Client = client, Message = message, Key = groupKey };

    public static async Task SendAsync(this FreeMessageClientExtensionModel<IClient> messageExtensionModel)
        => await messageExtensionModel.Client.Connect.SendAsync("FreeMessageToClient", messageExtensionModel.Message, messageExtensionModel.Key);
    public static async Task SendAsync(this FreeMessageGroupExtensionModel<IClient> messageExtensionModel)
        => await messageExtensionModel.Client.Connect.SendAsync("FreeMessageToGroup", messageExtensionModel.Message, messageExtensionModel.Key);



    public static async Task ConsumeSubAFreeMessageGroup(this IClient client, string groupKey)
        => await client.Connect.SendAsync("ConsumeSubAFreeMessageGroup", groupKey);
    public static async Task ConsumeUnSubAFreeMessageGroup(this IClient client, string groupKey)
        => await client.Connect.SendAsync("ConsumeUnSubAFreeMessageGroup", groupKey);
}