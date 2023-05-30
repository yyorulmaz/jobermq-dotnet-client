﻿using JoberMQ.Client.Net;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;

var config = JoberMQClient.GetConfiguration();
//var client = JoberMQClient.CreateClient("client 4", config);
var client = JoberMQClient.CreateClient(Guid.NewGuid().ToString(), config);
client.Connect.ConnectState += Client_ConnectState;
var connect = client.Connect.ConnectAsync().Result;

static void Client_ConnectState(bool obj)
{
    if (obj)
        Console.WriteLine("connected");
    else
        Console.WriteLine("disconnected");
}


//#region Distributor
//var distributorGet = await client.DistributorGetAsync("def.dis.search.queuekey");
//var distributorCreate = await client.DistributorCreateAsync("yenidistributor1", DistributorTypeEnum.Direct, DistributorSearchSourceTypeEnum.None, PermissionTypeEnum.All, true);
//#endregion

#region Queue
var queueGet = await client.QueueGetAsync("def.que.clientkey.free");
var queueCreate = await client.QueueCreateAsync("yeniqueue1", new string[] { "tag1", "tag2" }, QueueMatchTypeEnum.Tag, QueueOrderOfSendingTypeEnum.Free, PermissionTypeEnum.All, true, true);
#endregion

//#region Consume
//var consumeSub = await client.ConsumeSubAsync("def.que.clientkey.free", true);
////var consumeUnSub = await client.ConsumeUnSubAsync("def.que.clientkey.free");
//#endregion

#region Queue
//var newQue = await client.QueueCreateAsync("que.all.free", new string[] { "tag1", "tag2" }, QueueMatchTypeEnum.All, QueueOrderOfSendingTypeEnum.Free, PermissionTypeEnum.All, true, true);
#endregion

#region Message
var startDate = DateTime.Now;
Task.Run(async () =>
{
    for (int i = 0; i < 100001; i++)
    {
        Console.WriteLine(i);
        //Thread.Sleep(1000);
        //var message = client.Creator().Message("test message - " + i.ToString(), client.Creator().RoutingQueue("def.que.clientkey.free"));
        var message = client.Creator().Message("test message - " + i.ToString(), client.Creator().RoutingClient("client 2"));
        var result = await client.Message(message).SendAsync();

        if (i == 100000)
        {
            var endDate = DateTime.Now;

            Console.WriteLine("mesaj eklemesi bitti : " + endDate + startDate);
        }
    }
});
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






Console.WriteLine("Hello, World!");
Console.ReadLine();



