using JoberMQ.Client.Example.Producer;
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


#region Distributor
//var distributorGet = await client.DistributorGetAsync("def.dis.search.queuekey");
//var distributorAdd = await client.DistributorAddAsync("yenidistributor1", DistributorTypeEnum.Direct, DistributorSearchSourceTypeEnum.None, PermissionTypeEnum.All, true);
#endregion

#region Queue
//var queueGet = await client.QueueGetAsync("def.que.clientkey.free");
//var queueCreate = await client.QueueCreateAsync("yeniqueue1", new string[] { "tag1", "tag2" }, QueueMatchTypeEnum.Tag, QueueOrderOfSendingTypeEnum.Free, PermissionTypeEnum.All, true, true);
#endregion

#region Consume
//var consumeSub =   await client.ConsumeSubAsync("def.que.clientkey.free", true);
//var consumeUnSub = await client.ConsumeUnSubAsync("def.que.clientkey.free");
#endregion

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
//Task.Run(() =>
//{
//    for (int i = 0; i < 10; i++)
//    {
//        var messageRpc = client.Creator().MessageRpc("client2", "test");
//        var messageBuilder = client.RpcBuilder().Message(messageRpc).Build();
//        var result = client.PublishAsync(messageBuilder).Result;
//        Console.WriteLine(result.Result + i);
//        counter++;
//    }
//});
#endregion

#region Job



//var message = client.Creator().Message("test message", client.Creator().RoutingClient("client2"));
////var job =  client.JobBuilder()
////                 .Message(message)
////                 .Publisher(PublisherTypeEnum.Standart)
////                 .TimingNow()
////                 .ResultMessage(message)
////                 .Build();
//var job = client.JobBuilder()
//                 .Message(message)
//                 //.TimingScheduleDelayed(20)
//                 .TimingScheduleRecurrent("* * * ? * * *")
//                 .Build();
//var result = await client.PublishAsync(job);




#endregion







#region BELLİ DAKİKALARA, SAATLERE ABONE OLMA. YANİ SERVER ÜZERİNDEN TİMER YAPMAK
//ITest test = new Test();
//test.Islem();
#endregion



#region trigger
//var message = client.Creator().Message("test - 1", client.Creator().RoutingClient("client2"));
//var job = client.JobBuilder()
//                 .Message(message)
//                 .TimingScheduleRecurrent("*/15 * * ? * * *")
//                 .Build();
//Console.WriteLine(job.Id);
//var result = await client.PublishAsync(job);
//Console.WriteLine(result.Id);


////Thread.Sleep(10000);


//var messageTrigger1_1 = client.Creator().Message("test - 1.1", client.Creator().RoutingClient("client2"));
//var jobTrigger1_1 = client.JobBuilder()
//                 .Message(messageTrigger1_1)
//                 .TimingTriggerWhenDone(result.Id.Value, true)
//                 .Build();
//Console.WriteLine(jobTrigger1_1.Id);
//var jobTriggerResult1_1 = await client.PublishAsync(jobTrigger1_1);
//Console.WriteLine(jobTriggerResult1_1.Id);





//var messageTrigger1_2 = client.Creator().Message("test - 1.2", client.Creator().RoutingClient("client2"));
//var jobTrigger1_2 = client.JobBuilder()
//                 .Message(messageTrigger1_2)
//                 .TimingTriggerWhenDone(result.Id.Value, true)
//                 .Build();
//Console.WriteLine(jobTrigger1_2.Id);
//var jobTriggerResult1_2 = await client.PublishAsync(jobTrigger1_2);
//Console.WriteLine(jobTriggerResult1_2.Id);




//var messageTrigger1_2_1 = client.Creator().Message("test - 1.2.1", client.Creator().RoutingClient("client2"));
//var jobTrigger1_2_1 = client.JobBuilder()
//                 .Message(messageTrigger1_2_1)
//                 .TimingTriggerWhenDone(jobTriggerResult1_2.Id.Value, true)
//                 .Build();
//Console.WriteLine(jobTrigger1_2_1.Id);
//var jobTriggerResult1_2_1 = await client.PublishAsync(jobTrigger1_2_1);
//Console.WriteLine(jobTriggerResult1_2_1.Id);




//var messageTrigger1_2_2 = client.Creator().Message("test - 1.2.2", client.Creator().RoutingClient("client2"));
//var jobTrigger1_2_2 = client.JobBuilder()
//                 .Message(messageTrigger1_2_2)
//                 .TimingTriggerWhenDone(jobTriggerResult1_2.Id.Value, true)
//                 .Build();
//Console.WriteLine(jobTrigger1_2_2.Id);
//var jobTriggerResult1_2_2 = await client.PublishAsync(jobTrigger1_2_2);
//Console.WriteLine(jobTriggerResult1_2_2.Id);

#endregion


for (int i = 0; i < 1000000; i++)
{
    client.FreeMessageToClient(i.ToString(), "client2").SendAsync();
    //client.FreeMessageToGroup(i.ToString(), "group1").SendAsync();
    Console.WriteLine(i.ToString());

}



Console.WriteLine("Hello, World!");
Console.ReadLine();



