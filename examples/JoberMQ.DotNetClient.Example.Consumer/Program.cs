using JoberMQ.Client.DotNet;
using JoberMQ.Common.Enums.Endpoint;

#region belli bir nesneden kalıtım alan nesneler
//Assembly assembly = Assembly.GetExecutingAssembly(); // Bu programın derlenmiş assembly'sini alır

//Type baseType = typeof(IJoberMQRPCClient);
//var derivedTypes = assembly.GetTypes().Where(t => baseType.IsAssignableFrom(t) && t != baseType);

//foreach (var derivedType in derivedTypes)
//{
//    Console.WriteLine("Derived class found: " + derivedType.Name);
//}
#endregion










#region CLIENT
//var client = JoberMQClient.CreateClient("client2", UrlProtocolEnum.http, 5000);
//var connect = await client.ConnectAsync();
//Console.WriteLine("connect : " + connect);

var client = await JoberMQClient.CreateClientConnectAsync("client2", UrlProtocolEnum.http, 5000);
#endregion





#region MESSAGE FREE
//await client.Consume().SubFreeGroupAsync("group1");
//client.ReceiveFreeMessageText += Client_ReceiveFreeMessageText;
//void Client_ReceiveFreeMessageText(string obj)
//{
//    Console.WriteLine(obj);
//};
#endregion

#region RPC MESSAGE
client.ReceiveRpcMessageText += Client_ReceiveRpcMessageText;

void Client_ReceiveRpcMessageText(Guid arg1, string arg2)
{
    Console.WriteLine(arg2);

    string cevap = "cevap döndüm";
    bool isError = false;
    string errorMessage = "";

    _ = client.SendAsync("RpcMessageResponse", arg1, cevap, isError, errorMessage);
}
#endregion


Console.WriteLine("Hello, World!");
Console.ReadLine();
