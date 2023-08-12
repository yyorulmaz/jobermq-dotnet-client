using JoberMQ.Client.Net;
using JoberMQ.Common.Enums.Endpoint;

//var client = JoberMQClient.CreateClient("client 10", UrlProtocolEnum.http, 5183);
//var client = JoberMQClient.CreateClient(Guid.NewGuid().ToString(), UrlProtocolEnum.http, 5183);
var client = JoberMQClient.CreateClient(Guid.NewGuid().ToString(), UrlProtocolEnum.http, 5000);
client.Connect.ConnectState += Client_ConnectState;
client.ReceiveMessageText +=Client_ReceiveMessageText;

void Client_ReceiveMessageText(string obj)
{
    Console.WriteLine(obj);
}

var connect = client.Connect.ConnectAsync().Result;

static void Client_ConnectState(bool obj)
{
    if (obj)
        Console.WriteLine("connected");
    else
        Console.WriteLine("disconnected");
}

//var consumeSub = await client.ConsumeSubAsync("Binance-Futures-Usdt-FifteenMinutes-BTCUSDT", true);




Console.WriteLine();
Console.ReadLine();