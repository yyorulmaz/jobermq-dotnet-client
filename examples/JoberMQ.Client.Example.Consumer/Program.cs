using JoberMQ.Client.Net;
using JoberMQ.Client.Net.Extensions;
using JoberMQ.Client.Net.Extensions.Consume;
using JoberMQ.Client.Net.Extensions.Distributor;
using JoberMQ.Client.Net.Extensions.Job;
using JoberMQ.Client.Net.Extensions.Message;
using JoberMQ.Client.Net.Extensions.Publish;
using JoberMQ.Client.Net.Extensions.Queue;
using JoberMQ.Client.Net.Extensions.Routing;
using JoberMQ.Library.Dbos;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Text.RegularExpressions;




var config = JoberMQClient.GetConfiguration();
var client = JoberMQClient.CreateClient("client 2", config);
client.Connect.ConnectState += Client_ConnectState;
var connect = client.Connect.ConnectAsync().Result;


var consume = client.ConsumeBuilder().SpecialAdd().Build();
var resultConsume = client.PublishAsync(consume);



//client.Connect.ReceiveData +=Connect_ReceiveData;
//void Connect_ReceiveData(string obj)
//{
//    var msg = JsonConvert.DeserializeObject<MessageDbo>(obj);
//    Console.WriteLine(msg.Message.Message);

//}

client.ReceiveMessageText += Client_ReceiveMessageText;
void Client_ReceiveMessageText(string obj)
{
    Console.WriteLine(obj);
}

static void Client_ConnectState(bool obj)
{
    if (obj)
        Console.WriteLine("connected");
    else
        Console.WriteLine("disconnected");
}

Console.WriteLine("Hello, World!");
Console.ReadLine();










//deneme2(() => ddddd.AAAAAA());



//static void deneme(Action<string> action)
//{

//}
//static void deneme2(Expression<Action> action)
//{

//}
//static void deneme22(Expression<Func<Action>> action)
//{

//}
//static void deneme3(Task<Expression<Action>> action)
//{

//}


//static void deneme4(Func<string> action)
//{

//}
//static void deneme44(Func<Action> action)
//{

//}
//static void deneme4444(Func<Action, Task> action)
//{

//}
//static void deneme5(Func<string, Task> action)
//{

//}
//static void deneme6(Func<Expression<Action>, Task> action)
//{

//}

//static void BBBBBB()
//{

//}
//public class ddddd
//{
//    public static void AAAAAA()
//    {

//    }
//}


