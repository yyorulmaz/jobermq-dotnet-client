using JoberMQ.Client.DotNet;
using JoberMQ.Common.Enums.Endpoint;

var startDate = DateTime.Now;




#region CLIENT
var client = JoberMQClient.CreateClient("client1", UrlProtocolEnum.http, 5000);
var connect = client.ConnectAsync().Result;
Console.WriteLine("connect : " + connect);
#endregion

#region MESSAGE FREE
//for (int i = 0; i < 10000000; i++)
//{
//    await client.Message().FreeMessageClient("client2", i.ToString()).SendAsync();
//    //await client.Message().FreeMessageGroup("group1", i.ToString()).SendAsync();
//}
#endregion

#region RPC MESSAGE
for (int i = 0; i < 1000; i++)
{
    var result = await client.Message().RpcMessage("client2", i.ToString()).SendAsync();
    Console.WriteLine(result.ResultData);
}

#endregion




var endDate = DateTime.Now;
Console.WriteLine("Zaman : " + (endDate - startDate));
Console.ReadLine();
