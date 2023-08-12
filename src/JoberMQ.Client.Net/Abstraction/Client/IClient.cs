using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Method.Abstraction;
using JoberMQ.Common.Models.Distributor;
using JoberMQ.Common.Models.Job;
using JoberMQ.Common.Models.Message;
using JoberMQ.Common.Models.Rpc;
using System;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IClient : IDisposable
    {
        IClientInfo ClientInfo { get; }
        IConnect Connect { get; }


        JobBuilderModel JobBuilder(string name = null, string description = null);
        MessageBuilderModel MessageBuilder();
        RpcBuilderModel RpcBuilder();


        event Action<string> ReceiveMessageText;

        IMethod Method { get; }
    }

    /*
    
    Distributor().Get()
    Distributor().Create()
    Distributor().Edit()
    Distributor().Remove()

    Queue().Get()
    Queue().Create()
    Queue().Edit()
    Queue().Remove()

    Consume().Sub()
    Consume().UnSub()

    Message().Job()
    Message().Message()
    Message().Rpc()

    Receive
     
     */









    //public class MessageBuilder
    //{

    //}
    //public class TypeJobBuilder
    //{

    //}
    //public class TypeMessageBuilder
    //{

    //}
    //public class TypeRpcBuilder
    //{

    //}
    //public static class MessageExtensions
    //{
    //    public static MessageBuilder Message(this IClient client)
    //        => new MessageBuilder();

    //    public static TypeJobBuilder Job(this MessageBuilder messageBuilder)
    //    {
    //        return new JobBuilder();
    //    }
    //    public static TypeMessageBuilder Message(this MessageBuilder messageBuilder)
    //    {
    //        return new SingleBuilder();
    //    }
    //    public static TypeRpcBuilder Rpc(this MessageBuilder messageBuilder)
    //    {
    //        return new RpcBuilder();
    //    }
    //}



}
