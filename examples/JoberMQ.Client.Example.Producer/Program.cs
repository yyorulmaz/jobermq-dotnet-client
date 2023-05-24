using JoberMQ.Client.Net;
using JoberMQ.Client.Net.Extensions;
using JoberMQ.Client.Net.Extensions.Consume;
using JoberMQ.Client.Net.Extensions.Distributor;
using JoberMQ.Client.Net.Extensions.Job;
using JoberMQ.Client.Net.Extensions.Message;
using JoberMQ.Client.Net.Extensions.Publish;
using JoberMQ.Client.Net.Extensions.Queue;
using JoberMQ.Client.Net.Extensions.Routing;
using JoberMQ.Client.Net.Extensions.Rpc;

var config = JoberMQClient.GetConfiguration();
var client = JoberMQClient.CreateClient("client 4", config);
client.Connect.ConnectState += Client_ConnectState;
var connect = client.Connect.ConnectAsync().Result;

int aaa = 0;


#region Declare Distributor
//var distributor1 = client
//    .DistributorBuilder()
//    .Create("deneme Distributor 1")
//    .Build();
//var declareDistributor1Result = client.Publish(distributor1).Result;
//Console.WriteLine(declareDistributor1Result.Message);

//var distributor2 = client
//    .DistributorBuilder()
//    .Create("deneme Distributor 2")
//    .Build();
//var declareDistributorResult2 = client.Publish(distributor2).Result;
//Console.WriteLine(declareDistributorResult2.Message);

//var distributor1Remove = client
//    .DistributorBuilder()
//    .Remove("deneme Distributor 1")
//    .Build();
//var declareDistributorResult1Remove = client.Publish(distributor1Remove).Result;
//Console.WriteLine(declareDistributorResult1Remove.Message);
#endregion

#region Declare Queue
//var queue = client
//    .QueueBuilder()
//    .Create("deneme Queue 1")
//    .Build();
//var declareQueueResult = client.Publish(queue).Result;
//Console.WriteLine(declareQueueResult.Message);
#endregion

#region Message Rpc
//Task.Run(async () =>
//{
//    for (int i = 0; i < 100000; i++)
//    {
//        var message = client.CreateMessage("client 2", "");
//        var builder = client.RpcBuilder().Message(message).Build();
//        var result = await client.PublishAsync(builder);
//        Console.WriteLine(result.Result);
//    }
//});
#endregion

#region Message
//Task.Run(async () =>
//{
//    var startDate = DateTime.Now;
//    Console.WriteLine("startDate : " + startDate);

//    for (int i = 1; i < 1000001; i++)
//    {
//        //var message = client
//        //.MessageBuilder()
//        //.Message(client.CreateMessage("hey", client.CreateSpecial("client 1")))
//        //.Build();

//        ////var result = await client.Publish(message);
//        //var result = client.Publish(message);


//        if (client.Connect.IsConnect)
//        {
//            //var message = client.CreateMessage(i.ToString(), client.CreateRoutingSpecial("client 2"));
//            var message = client.CreateMessage(i.ToString(), client.CreateRoutingSpecial("client 2"));
//            var job = client
//            .JobBuilder()
//            .Message(message)
//            .Build();
//            var result = await client.PublishAsync(job);

//            //var result = client.PublishAsync(job);



//        }
//        else
//        {
//            Console.WriteLine("error");
//        }




//    }
//    var endDate = DateTime.Now;
//    Console.WriteLine("endDate : " + endDate);
//    Console.WriteLine("fark : " + (startDate-endDate));

//});
#endregion

#region Message
Task.Run(async () =>
{
    var startDate = DateTime.Now;
    Console.WriteLine("startDate : " + startDate);

    for (int i = 1; i < 11; i++)
    {
        if (client.Connect.IsConnect)
        {
            var message = client.CreateMessage(i.ToString(), client.CreateRoutingEvent("denemeEvent1"));
            
            
            
            //var job = client
            //.JobBuilder()
            //.Message(message, isDbTextSave:false)
            //.Build();
            //var result = await client.PublishAsync(job);


            var msg = client
            .MessageBuilder()
            .Message(message, isDbTextSave: false)
            .Build();
            //var result = await client.PublishAsync(msg);
            var result = await client.PublishAsync(msg);

        }
        else
        {
            Console.WriteLine("error");
        }
    }
});
#endregion






static void Client_ConnectState(bool obj)
{
    if (obj)
        Console.WriteLine("connected");
    else
        Console.WriteLine("disconnected");
}

Console.WriteLine("Hello, World!");
Console.ReadLine();