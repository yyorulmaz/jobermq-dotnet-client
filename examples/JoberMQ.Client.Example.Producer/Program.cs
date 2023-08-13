using JoberMQ.Client.Net;
using JoberMQ.Common.Enums.Endpoint;
using JoberMQ.Common.Enums.Publisher;

//var client = JoberMQClient.CreateClient(Guid.NewGuid().ToString(), UrlProtocolEnum.http, 7654);
var client = JoberMQClient.CreateClient("client1", UrlProtocolEnum.http, 5000);
client.Connect.ConnectState += Client_ConnectState;
var connect = client.ConnectAsync().Result;

static void Client_ConnectState(bool obj)
{
    if (obj)
        Console.WriteLine("connected");
    else
        Console.WriteLine("disconnected");
}


//#region Distributor
//var distributorGet = await client.DistributorGetAsync("def.dis.search.queuekey");
//var distributorAdd = await client.DistributorAddAsync("yenidistributor1", DistributorTypeEnum.Direct, DistributorSearchSourceTypeEnum.None, PermissionTypeEnum.All, true);
//#endregion

#region Queue
//var queueGet = await client.QueueGetAsync("def.que.clientkey.free");
//var queueCreate = await client.QueueCreateAsync("yeniqueue1", new string[] { "tag1", "tag2" }, QueueMatchTypeEnum.Tag, QueueOrderOfSendingTypeEnum.Free, PermissionTypeEnum.All, true, true);
#endregion

//#region Consume
////var consumeSub =   await client.ConsumeSubAsync("def.que.clientkey.free", true);
////var consumeUnSub = await client.ConsumeUnSubAsync("def.que.clientkey.free");
//#endregion

#region Queue
//var newQue = await client.QueueCreateAsync("que.all.free", new string[] { "tag1", "tag2" }, QueueMatchTypeEnum.All, QueueOrderOfSendingTypeEnum.Free, PermissionTypeEnum.All, true, true);
#endregion

#region Message
//var startDate = DateTime.Now;
//Task.Run(async () =>
//{
//    for (int i = 0; i < 100000001; i++)
//    {
//        Console.WriteLine(i);
//        //Thread.Sleep(1000);
//        //var message = client.Creator().Message("test message - " + i.ToString(), client.Creator().RoutingQueue("def.que.clientkey.free"));
//        var message = client.Creator().Message("test message - " + i.ToString(), client.Creator().RoutingClient("client 10"));
//        var result = await client.Message(message).SendAsync();

//        if (i == 100000000)
//        {
//            var endDate = DateTime.Now;

//            Console.WriteLine("mesaj eklemesi bitti : " + (endDate - startDate));
//        }
//    }
//});
#endregion

#region Message RPC
//int counter = 0;
//Task.Run(() => {
//    for (int i = 0; i < 100000; i++)
//    {
//        var messageRpc = client.CreateMessageRpc("trading", "deneme");
//        var messageBuilder = client.RpcBuilder().Message(messageRpc).Build();
//        var result = client.PublishAsync(messageBuilder).Result;
//        Console.WriteLine(result.Result + i);
//        counter++;
//    }
//});
#endregion

#region Job



var message = client.Creator().Message("test message", client.Creator().RoutingClient("client2"));
//var job =  client.JobBuilder()
//                 .Message(message)
//                 .Publisher(PublisherTypeEnum.Standart)
//                 .TimingNow()
//                 .ResultMessage(message)
//                 .Build();
var job = client.JobBuilder()
                 .Message(message)
                 //.TimingScheduleDelayed(20)
                 .TimingScheduleRecurrent("* * * ? * * *")
                 .Build();
var result = await client.PublishAsync(job);




#endregion




Console.WriteLine("Hello, World!");
Console.ReadLine();



