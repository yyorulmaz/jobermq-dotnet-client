using JoberMQ.Common.Models.Rpc;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

public static class MessageRpcSendExtension
{
    public static async Task<RpcResponseModel> SendAsync(this MessageRpcTextBuilderModel builder)
        => await builder.client.InvokeAsync<RpcResponseModel>("MessageRpcText", builder.transactionId, builder.key, builder.message);
    public static async Task<RpcResponseModel<T>> SendAsync<T>(this MessageRpcTextBuilderModel builder)
    {
        var rpcResponse = await builder.client.InvokeAsync<RpcResponseModel>("MessageRpcText", builder.transactionId, builder.key, builder.message);

        T resultData;
        using (MemoryStream memoryStream = new MemoryStream(rpcResponse.ResultData))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            object obj = binaryFormatter.Deserialize(memoryStream);
            resultData = (T)obj;
        }

        return new RpcResponseModel<T>
        {
            Id = rpcResponse.Id,
            ErrorMessage = rpcResponse.ErrorMessage,
            IsError = rpcResponse.IsError,
            //ResultData = (T)Convert.ChangeType(rpcResponse.ResultData, typeof(T))
            ResultData = resultData
        };
    }
    public static async Task<RpcResponseModel> SendAsync(this MessageRpcFunctionBuilderModel builder)
        => await builder.client.InvokeAsync<RpcResponseModel>("MessageRpcFunction", builder.transactionId, builder.key, builder.message);
    public static async Task<RpcResponseModel<T>> SendAsync<T>(this MessageRpcFunctionBuilderModel builder)
    {
        var rpcResponse = await builder.client.InvokeAsync<RpcResponseModel>("MessageRpcFunction", builder.transactionId, builder.key, builder.message);

        T resultData;
        using (MemoryStream memoryStream = new MemoryStream(rpcResponse.ResultData))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            object obj = binaryFormatter.Deserialize(memoryStream);
            resultData = (T)obj;
        }

        return new RpcResponseModel<T>
        {
            Id = rpcResponse.Id,
            ErrorMessage = rpcResponse.ErrorMessage,
            IsError = rpcResponse.IsError,
            //ResultData = (T)Convert.ChangeType(rpcResponse.ResultData, typeof(T))
            ResultData = resultData
        };
    }
}
