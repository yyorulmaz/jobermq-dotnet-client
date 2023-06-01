using JoberMQ.Client.Net;
using JoberMQ.Client.Net.Extension.Consume;
using JoberMQ.Common.Enums.Endpoint;
using Newtonsoft.Json;

var client = JoberMQClient.CreateClient("client 10", UrlProtocolEnum.http, 7654);
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

var consumeSub = await client.ConsumeSubAsync("Binance-Futures-Usdt-FifteenMinutes-BTCUSDT", true);
var consumeSub2 = await client.ConsumeSubAsync("Binance-Futures-Usdt-FifteenMinutes-ETHUSDT", true);
var consumeSub3 = await client.ConsumeSubAsync("Binance-Futures-Usdt-FifteenMinutes", true);
var consumeSub4 = await client.ConsumeSubAsync("def.que.clientkey.free", true);

Console.WriteLine();
Console.ReadLine();