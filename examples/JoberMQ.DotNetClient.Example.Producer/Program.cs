using JoberMQ.Client.DotNet;
using JoberMQ.Common.Enums.Endpoint;
using JoberMQ.DotNetClient.Example.Producer;







#region CLIENT
var client = JoberMQClient.CreateClient("client1", UrlProtocolEnum.http, 8654);
var connect = client.ConnectAsync().Result;
Console.WriteLine("connect : " + connect);
#endregion

var startDate = DateTime.Now;

#region MESSAGE
//for (int i = 0; i < 100000; i++)
//{
//    Console.WriteLine("aaa-" + i.ToString());
//    var msg = client.Creator().Message("aaa-" + i.ToString(), client.Creator().RoutingClient("client2"), consumingRetryMaxCount : 1);
//    await client.Message().Message(msg).SendAsync();
//}
#endregion

#region MESSAGE FREE
//for (int i = 0; i < 100000; i++)
//{
//    //Console.WriteLine("aaa-" + i.ToString());
//    await client.Message().FreeClient("client2", i.ToString()).SendAsync();
//    //await client.Message().FreeMessageGroup("group1", i.ToString()).SendAsync();
//}
#endregion









#region RPC MESSAGE
////for (int i = 0; i < 1000; i++)
////{
////    var result = await client.Message().RpcMessage("client2", i.ToString()).SendAsync();
////    Console.WriteLine(result.ResultData);
////}

for (int i = 0; i < 100; i++)
{
    ////var result = await client.Message()..Message().RpcMessage("client3", () => DENEME.MyProperty.Topla(i, 5)).SendAsync<bool>();
    //var result = await client.Message().Rpc("client3", () => DENEME.MyProperty.Topla(i, 5)).SendAsync<bool>();
    //Console.WriteLine(result.ResultData);


    var result = await client.Message().Rpc("RPC_DataPool_TradingTool_Client", () => DENEME.TradingToolEventAction.TradingToolAdd(new TradingToolDbo { Id = 15524})).SendAsync<bool>();



    Console.WriteLine(result.ResultData);
}
#endregion




var endDate = DateTime.Now;
Console.WriteLine("Zaman : " + (endDate - startDate));
Console.ReadLine();
