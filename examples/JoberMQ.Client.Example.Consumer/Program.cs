using JoberMQ.Client.Net;
using JoberMQ.Common.Enums.Endpoint;

//var client = JoberMQClient.CreateClient("client 10", UrlProtocolEnum.http, 5183);
//var client = JoberMQClient.CreateClient(Guid.NewGuid().ToString(), UrlProtocolEnum.http, 5183);
//var client = JoberMQClient.CreateClient("client2", UrlProtocolEnum.http, 5000);
var client = JoberMQClient.CreateClient("client2", UrlProtocolEnum.http, 7654);
client.Connect.ConnectState += Client_ConnectState;
client.ReceiveMessageText +=Client_ReceiveMessageText;

void Client_ReceiveMessageText(string obj)
{
    Console.WriteLine(obj);
}

var connect = client.ConnectAsync().Result;
//var consumeSub = await client.Consume().SubAsync("def.que.clientkey.free", true);
await client.Consume().SubFreeMessageGroup("group1");

static void Client_ConnectState(bool obj)
{
    if (obj)
        Console.WriteLine("connected");
    else
        Console.WriteLine("disconnected");
}





Console.WriteLine();
Console.ReadLine();