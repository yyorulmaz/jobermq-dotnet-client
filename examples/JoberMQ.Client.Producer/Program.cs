using JoberMQ.Client.Net;
using JoberMQ.Client.Net.Extensions;
using JoberMQ.Client.Net.Extensions.DeclareConsume;
using JoberMQ.Client.Net.Extensions.DeclareDistributor;
using JoberMQ.Client.Net.Extensions.Job;
using JoberMQ.Client.Net.Extensions.Message;
using JoberMQ.Client.Net.Models.Routing;

var configuration = JoberMQClient.GetConfiguration();
var client = JoberMQClient.CreateClient("client1", "clientGroup1", configuration);
client.Connect.ConnectState += Client_ConnectState;
var ssss = client.Connect.ConnectAsync().Result;

#region Declare Distributor
var declareDistributor = client
    .DeclareDistributor()
    .Create("deneme Distributor 1")
    .Build();
var declareDistributorResult = client.Publish(declareDistributor).Result;
#endregion









////for (int i = 0; i < 1000000; i++)
////{
////    var message = client
////        .JobBuilder()
////        .Message(client.CreateMessage("1111", new RoutingSpecialModel { ClientKey = "client2" }))
////        .Build();

////    var ddddddddd = client.Publish(message).Result;
////}

//for (int i = 0; i < 100000; i++)
//{
//    var message = client
//    .MessageBuilder()
//    .Message(client.CreateMessage("1111", new RoutingSpecialModel { ClientKey = "client2" }))
//    .Build();
//    var ddddddddd = client.Publish(message).Result;
//}


//var declareConsume = client
//    .DeclareConsumeBuilder()
//    .SpecialAdd()
//    .Build();
//var ddddddddd = client.Publish(declareConsume).Result;



Console.ReadLine();


static void Client_ConnectState(bool obj)
{
    if (obj)
        Console.WriteLine("connected");
    else
        Console.WriteLine("disconnected");


}
