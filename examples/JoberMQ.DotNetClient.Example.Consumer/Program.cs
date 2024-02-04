using JoberMQ.Client.DotNet;
using JoberMQ.Common.Enums.Endpoint;
using JoberMQ.Common.Models.General;
using JoberMQ.DotNetClient.Example.Consumer;

#region belli bir nesneden kalıtım alan nesneler
//Assembly assembly = Assembly.GetExecutingAssembly(); // Bu programın derlenmiş assembly'sini alır

//Type baseType = typeof(IJoberMQRPCClient);
//var derivedTypes = assembly.GetTypes().Where(t => baseType.IsAssignableFrom(t) && t != baseType);

//foreach (var derivedType in derivedTypes)
//{
//    Console.WriteLine("Derived class found: " + derivedType.Name);
//}
#endregion










#region CLIENT
////var client = JoberMQClient.CreateClient("client2", UrlProtocolEnum.http, 5000);
////var connect = await client.ConnectAsync();
////Console.WriteLine("connect : " + connect);

//var parameter = new JoberMQParameterModel
//{
//    //ClientKey = "INVEST.Trading.Strategy.BackgroundTask",
//    ClientKey = "FFFFFFFFFFFFFFF",
//    HostName = "localhost",
//    UrlProtocol = UrlProtocolEnum.http,
//    Port = 8654
//};

//var joberMQResult = await JoberMQClient.CreateClientAndConnectAsync(parameter);



//var client = await JoberMQClient.CreateClientConnectAsync("client2", UrlProtocolEnum.http, 8654);
//await client.Consume().MessageFreeGroupSub("CandleLiveData__FiveMinutes_Queue").SendAsync();
//await client.Consume().MessageFreeGroupSub("CandleLiveData__FifteenMinutes_Queue").SendAsync();
//await client.Consume().MessageFreeGroupSub("CandleLiveData__OneHour_Queue").SendAsync();
//await client.Consume().MessageFreeGroupSub("CandleLiveData__FourHour_Queue").SendAsync();
//await client.Consume().MessageFreeGroupSub("CandleLiveData__OneDay_Queue").SendAsync();
//client.ReceiveMessageFreeText +=Client_ReceiveMessageFreeText;

//void Client_ReceiveMessageFreeText(string obj)
//{
//    Console.WriteLine(obj);
//}

//int aaaa = 0;
#endregion



#region MESSAGE
////await client.Consume().FreeMessageGroupSub("group1").SendAsync();
//client.ReceiveMessage += Client_ReceiveMessage;

//void Client_ReceiveMessage(string obj)
//{
//    Console.WriteLine(obj);
//};
#endregion




#region MESSAGE FREE
////await client.Consume().FreeMessageGroupSub("group1").SendAsync();
//client.ReceiveFreeMessageText += Client_ReceiveFreeMessageText;
//void Client_ReceiveFreeMessageText(string obj)
//{
//    Console.WriteLine(obj);
//};
#endregion

#region RPC MESSAGE
//client.ReceiveRpcMessageText += Client_ReceiveRpcMessageText;

//void Client_ReceiveRpcMessageText(Guid arg1, string arg2)
//{
//    Console.WriteLine(arg2);

//    string cevap = "cevap döndüm";
//    bool isError = false;
//    string errorMessage = "";

//    _ = client.SendAsync("MessageRpcResponse", arg1, cevap, isError, errorMessage);
//}


#endregion

ITradingTool deneme = new TradingTool();
deneme.Setup(new JoberMQ.Common.Models.General.JoberMQParameterModel
{
    ClientKey = "client3",
    HostName = "localhost",
    UrlProtocol = UrlProtocolEnum.http,
    Port = 8654
});


Console.WriteLine("Hello, World!");
Console.ReadLine();
