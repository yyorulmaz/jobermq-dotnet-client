using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Constants;
using JoberMQ.Client.Net.Factories.Account;
using JoberMQ.Client.Net.Factories.Client;
using JoberMQ.Client.Net.Factories.Connect;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Message;
using JoberMQ.Library.Enums.Operation;
using JoberMQ.Library.Enums.Publisher;
using JoberMQ.Library.Enums.Status;
using JoberMQ.Library.Enums.Timing;
using JoberMQ.Library.Method.Abstraction;
using JoberMQ.Library.Method.Enums;
using JoberMQ.Library.Method.Factories;
using JoberMQ.Library.Models;
using JoberMQ.Library.Models.Client;
using JoberMQ.Library.Models.Consume;
using JoberMQ.Library.Models.Distributor;
using JoberMQ.Library.Models.Job;
using JoberMQ.Library.Models.Message;
using JoberMQ.Library.Models.Queue;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.Models.Rpc;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JoberMQ.Client.Net.Implementation.Client.Default
{
    public class DfClientSocket : IClient
    {
        public DfClientSocket(string clientKey, IConfiguration configuration)
        {
            this.configuration = configuration;
            //consuming = new ConcurrentDictionary<Guid, ConsumeModel>();

            var account = AccountFactory.Create(ClientConst.AccountFactory, true, true, ClientConst.UserName, ClientConst.Password, configuration.EndpointLogin, configuration.EndpointHub);
            clientInfo = ClientInfoFactory.Create(ClientConst.ClientInfoFactory, ClientConst.ClientType, clientKey, ClientConst.IsOfflineClient);
            connect = ConnectFactory.Create(ClientConst.ConnectFactory, ClientConst.ConnectionRetryTimeout, ClientConst.AutoReconnect, account, clientInfo);
            method = MethodFactory.Create(MethodFactoryEnum.Default);

            connect.ReceiveData += Connect_ReceiveData;
            connect.ReceiveRpc +=Connect_ReceiveRpc;
            connect.ConnectState +=Connect_ConnectState;
        }



        #region reconnect Consuming
        //bu yapıyı şundan dolayı kurdum. client bağlantı kopmuş ve tekrar bağlanmışsa 
        //consuming bilgilerini tekrar sunucuya göndermek için
        bool isConsumingRefresh = false;
        private void Connect_ConnectState(bool obj)
        {
            //if (obj == false)
            //{
            //    isConsumingRefresh = true;
            //}
            //else
            //{
            //    if (isConsumingRefresh == true && connect.IsConnect)
            //    {
            //        var consumeRequest = new ConsumeRequestModel();
            //        consumeRequest.Consuming = Consuming;

            //        var serialize = JsonConvert.SerializeObject(consumeRequest);
            //        var result = connect.InvokeAsync<ResponseModel>("Consume", serialize).Result;
            //        if (result.IsSucces == true)
            //            isConsumingRefresh = false;
            //    }
            //}

        }
        #endregion

        IConfiguration configuration;

        IClientInfo clientInfo;
        IClientInfo IClient.ClientInfo => clientInfo;

        IConnect connect;
        IConnect IClient.Connect => connect;

        IMethod method;
        public IMethod Method => method;

        public DistributorBuilderModel DistributorBuilder()
          => new DistributorBuilderModel { DistributorTransport = new DistributorTransportModel() };
        public ConsumeBuilderModel ConsumeBuilder()
            => new ConsumeBuilderModel { ConsumeTransport = new ConsumeTransportModel() };
        public QueueBuilderModel QueueBuilder()
            => new QueueBuilderModel { QueueTransport = new QueueTransportModel() };



        public JobBuilderModel JobBuilder(string name = null, string description = null)
           => JobBuilderDefault(name, description);
        private JobBuilderModel JobBuilderDefault(string name, string description)
            => new JobBuilderModel
            {
                Job = new JobDbo
                {
                    Id = Guid.NewGuid(),
                    Operation = new OperationModel
                    {
                        Version = 0,
                        OperationType = OperationTypeEnum.Job
                    },
                    Producer = new ProducerModel
                    {
                        ClientKey = clientInfo.ClientKey
                    },
                    Info = new InfoModel
                    {
                        Name = name,
                        Description = description
                    },
                    Publisher = new PublisherModel
                    {
                        PublisherType = PublisherTypeEnum.Standart
                    },
                    Timing = new TimingModel
                    {
                        TimingType = TimingTypeEnum.Now
                    },
                    JobDetails = new List<JobDetailDbo>(),
                    IsJobResultMessage = false,
                    JobResultMessage = null,
                    JobResultMessageConsuming = null,
                    Status = new StatusModel
                    {
                        IsCompleted = false,
                        IsError = false,
                        StatusTypeMessage = StatusTypeMessageEnum.None,
                        TempAgainDate = null
                    }
                }
            };

        public MessageBuilderModel MessageBuilder()
            => MessageBuilderDefault();
        private MessageBuilderModel MessageBuilderDefault()
            => new MessageBuilderModel
            {
                Message = new MessageDbo
                {
                    Id = Guid.NewGuid(),
                    Operation = new OperationModel
                    {
                        Version = 0,
                        OperationType = OperationTypeEnum.Message
                    },
                    Producer = new ProducerModel
                    {
                        ClientKey = clientInfo.ClientKey,
                    },
                    Message = null,
                    IsResult = false,
                    ResultMessage = null,
                    TriggerGroupsId = null,
                    CreatedJobId = null,
                    CreatedJobDetailId = null,
                    CreatedJobTransactionId = null,
                    CreatedJobTransactionDetailId = null,
                    EventGroupsId = null,
                    Status = new StatusModel
                    {
                        IsCompleted = false,
                        IsError = false,
                        StatusTypeMessage = StatusTypeMessageEnum.None,
                        TempAgainDate = null
                    },
                }
            };
        public RpcBuilderModel RpcBuilder()
           => RpcBuilderDefault();
        private RpcBuilderModel RpcBuilderDefault()
            => new RpcBuilderModel
            {
                RpcMessage = new RpcRequestModel
                {
                    Id = Guid.NewGuid(),
                    ProducerId = clientInfo.ClientKey,
                }
            };


        public event Action<string> ReceiveMessageText;
        //private async void Connect_ReceiveData(string obj)
        private void Connect_ReceiveData(string obj)
        {
            if (String.IsNullOrEmpty(obj))
                return;

            var messageDbo = JsonConvert.DeserializeObject<MessageDbo>(obj);

            // Message Started
            var messageStarted = new MessageStartedModel();
            messageStarted.MessageId = messageDbo.Id;
            messageStarted.IsError = false;

            // todo kontrol et. veritabanına kaydetmediğim için started gönderirsem patlıyor. buna çözüm düşünebilirsin yada hiçbirşey yapma
            if (messageDbo.IsDbTextSave)
                _ = connect.InvokeAsync<ResponseModel>("MessageStarted", JsonConvert.SerializeObject(messageStarted));

            var messageCompleted = new MessageCompletedModel();
            messageCompleted.MessageId = messageDbo.Id;


            if (messageDbo.Message.MessageType == MessageTypeEnum.Text)
            {
                ReceiveMessageText?.Invoke(messageDbo.Message.Message);
                if (configuration.TextMessageReceiveAutoCompleted)
                {
                    messageCompleted.IsError = false;
                    messageCompleted.Message = "";
                    if (messageDbo.IsDbTextSave)
                        _ = connect.InvokeAsync<ResponseModel>("MessageCompleted", JsonConvert.SerializeObject(messageCompleted));
                }
            }
            else if (messageDbo.Message.MessageType == MessageTypeEnum.Funtion)
            {
                var returnData = method.MethodRun(messageDbo.Message.Message).Result;

                if (returnData.StatusCode == "0")
                {
                    messageCompleted.IsError = false;
                    messageCompleted.Message = "";
                }
                else
                {
                    messageCompleted.IsError = true;
                    messageCompleted.Message = returnData.Message;
                }
                messageCompleted.Type = returnData.TypeFullName;
                messageCompleted.ReturnData = returnData.Data;
                //messageCompleted.RoutingType = consumerMessage.RoutingType;

                if (messageDbo.IsDbTextSave)
                    _ = connect.InvokeAsync<ResponseModel>("MessageCompleted", JsonConvert.SerializeObject(messageCompleted));

            }


        }

        static int counter = 0;
        private void Connect_ReceiveRpc(string obj)
        {
            if (String.IsNullOrEmpty(obj))
                return;

            var message = JsonConvert.DeserializeObject<RpcResponseModel>(obj);



            //işlemler
            //
            message.Result = "hayde - " + counter.ToString();
            counter++;


            _ = connect.InvokeAsync<ResponseModel>("RpcResponse", JsonConvert.SerializeObject(message));
        }

        


        #region Dispose
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    connect.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue=true;
            }
        }
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DfClientSocket()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
        #endregion
    }
}
