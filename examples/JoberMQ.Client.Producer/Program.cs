using JoberMQ.Client.Net;
using JoberMQ.Client.Net.Extensions;
using JoberMQ.Client.Net.Extensions.Job;
using JoberMQ.Client.Net.Models.Routing;

var configuration = JoberMQClient.GetConfiguration();
var client = JoberMQClient.CreateClient("client1", "clientGroup1", configuration);
client.ConnectState += Client_ConnectState;
var ssss = client.ConnectAsync().Result;



for (int i = 0; i < 1000000; i++)
{
    var message = client
        .JobBuilder()
        .Message(client.CreateMessage("1111", new RoutingSpecialModel { ClientKey = "client2" }))
        .Build();

    var ddddddddd = client.Publish(message).Result;
}



Console.ReadLine();


static void Client_ConnectState(bool obj)
{
    if (obj)
        Console.WriteLine("connected");
    else
        Console.WriteLine("disconnected");


}
