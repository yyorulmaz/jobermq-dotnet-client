using JoberMQ.Client.Net;
using JoberMQ.Client.Net.Extension.Consume;
using JoberMQ.Common.Enums.Endpoint;
using Newtonsoft.Json;

var config = JoberMQClient.GetConfiguration();


//var endpointLogin = JoberMQ.Client.Net.Factories.Endpoint.EndpointFactory.Create(EndpointFactoryEnum.Default, true, "172.20.0.2", 7654, 7655, "account/login");
//var endpointJoberHub = JoberMQ.Client.Net.Factories.Endpoint.EndpointFactory.Create(EndpointFactoryEnum.Default, true, "172.20.0.2", 7654, 7655, "JoberHub");
//config.EndpointLogin = endpointLogin;
//config.EndpointHub = endpointJoberHub;


var endpointLogin = JoberMQ.Client.Net.Factories.Endpoint.EndpointFactory.Create(EndpointFactoryEnum.Default, false, "localhost", 7654, 7655, "account/login");
var endpointJoberHub = JoberMQ.Client.Net.Factories.Endpoint.EndpointFactory.Create(EndpointFactoryEnum.Default, false, "localhost", 7654, 7655, "JoberHub");
config.EndpointLogin = endpointLogin;
config.EndpointHub = endpointJoberHub;




var client = JoberMQClient.CreateClient("client 2", config);
//var client = JoberMQClient.CreateClient(Guid.NewGuid().ToString(), config);
//var client = JoberMQClient.CreateClient("trading", config);
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
//var consumeSub4 = await client.ConsumeSubAsync("def.que.clientkey.free", true);

Console.WriteLine();
Console.ReadLine();