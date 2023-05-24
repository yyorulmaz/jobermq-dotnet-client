using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Method.Abstraction;
using JoberMQ.Library.Models.Consume;
using JoberMQ.Library.Models.Distributor;
using JoberMQ.Library.Models.Job;
using JoberMQ.Library.Models.Message;
using JoberMQ.Library.Models.Queue;
using JoberMQ.Library.Models.Rpc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JoberMQ.Client.Net.Abstraction.Client
{
    public interface IClient : IDisposable
    {
        IClientInfo ClientInfo { get; }
        IConnect Connect { get; }


        DistributorBuilderModel DistributorBuilder();
        QueueBuilderModel QueueBuilder();
        ConsumeBuilderModel ConsumeBuilder();


        JobBuilderModel JobBuilder(string name = null, string description = null);
        MessageBuilderModel MessageBuilder();
        RpcBuilderModel RpcBuilder();



        event Action<string> ReceiveMessageText;




        IMethod Method { get; }
    }
}
