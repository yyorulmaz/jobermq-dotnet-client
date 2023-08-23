using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Constant;
using JoberMQ.Client.Net.Factories.Account;
using JoberMQ.Client.Net.Factories.Client;
using JoberMQ.Client.Net.Factories.Connect;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Operation;
using JoberMQ.Common.Enums.Publisher;
using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Enums.Timing;
using JoberMQ.Common.Method.Abstraction;
using JoberMQ.Common.Method.Enums;
using JoberMQ.Common.Method.Factories;
using JoberMQ.Common.Models;
using JoberMQ.Common.Models.Info;
using JoberMQ.Common.Models.Job;
using JoberMQ.Common.Models.Message;
using JoberMQ.Common.Models.Operation;
using JoberMQ.Common.Models.Producer;
using JoberMQ.Common.Models.Publisher;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.Models.Rpc;
using JoberMQ.Common.Models.Status;
using JoberMQ.Common.Models.Timing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Implementation.Client.Default
{
    internal class DefaultClientSocket : IClient
    {
        public IConfiguration configuration;
        public DefaultClientSocket(string clientKey, IConfiguration configuration)
        {
            this.configuration = configuration;

            clientInfo = ClientInfoFactory.Create(ClientConst.ClientInfoFactory, ClientConst.ClientType, clientKey, ClientConst.IsOfflineClient);
            var account = AccountFactory.Create(ClientConst.AccountFactory, true, true, ClientConst.UserName, ClientConst.Password, configuration.EndpointLogin, configuration.EndpointHub);
            connect = ConnectFactory.Create(ClientConst.ConnectFactory, ClientConst.ConnectionRetryTimeout, ClientConst.AutoReconnect, account, clientInfo);
            method = MethodFactory.Create(MethodFactoryEnum.Default);

            connect.ReceiveData += Connect_ReceiveData;
            connect.ReceiveDataText += Connect_ReceiveDataText;
            connect.ReceiveRpc += Connect_ReceiveRpc;
            connect.ConnectState += Connect_ConnectState;
        }

        IClientInfo clientInfo;
        public IClientInfo ClientInfo => clientInfo;
        IConnect connect;
        public IConnect Connect => connect;
        IMethod method;
        public IMethod Method => method;




        public async Task<bool> ConnectAsync()
        {
            var conn = await connect.ConnectAsync();

            if (conn)
            {
                await this.Consume().SubAsync(ClientConst.DefaultQueueClientKey, true);
            }

            return conn;
        }




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




        #region Receive
        public event Action<string> ReceiveMessageText;
        //private void Connect_ReceiveData(string obj)
        private void Connect_ReceiveData(MessageDbo obj)
        {
            if (obj == null)
                return;


            _ = Task.Run(() =>
            {
                #region STARTED
                var jobStartedModel = new StartedModel();
                jobStartedModel.MessageId = obj.Id;
                _ = connect.SendAsync("Started", JsonConvert.SerializeObject(jobStartedModel));
                #endregion

                if (obj.Message.MessageType == MessageTypeEnum.Funtion)
                {
                    var returnData = method.MethodRun(obj.Message.Message).Result;

                    var jobCompleted = new CompletedModel();
                    jobCompleted.MessageId = obj.Id;

                    if (returnData.StatusCode == "0")
                    {
                        jobCompleted.IsError = false;
                        jobCompleted.Message = "";
                    }
                    else
                    {
                        jobCompleted.IsError = true;
                        jobCompleted.Message = returnData.Message;
                    }
                    jobCompleted.Type = returnData.TypeFullName;
                    jobCompleted.ReturnData = returnData.Data;
                    jobCompleted.RoutingType = obj.Message.Routing.RoutingType;

                    _ = connect.SendAsync("Completed", JsonConvert.SerializeObject(jobCompleted));
                }
                else if (obj.Message.MessageType == MessageTypeEnum.Text)
                {
                    ReceiveMessageText?.Invoke(obj.Message.Message);
                    if (configuration.TextMessageReceiveAutoCompleted == true)
                    {
                        var jobCompleted = new CompletedModel();
                        jobCompleted.MessageId = obj.Id;
                        jobCompleted.IsError = false;
                        jobCompleted.Message = "";

                        _ = connect.SendAsync("Completed", JsonConvert.SerializeObject(jobCompleted));
                    }
                }
            });







        }
        private void Connect_ReceiveDataText(string obj)
        {
            // burası freemessage için kuyllanılıyor

            ReceiveMessageText?.Invoke(obj);
        }
        private void Connect_ReceiveRpc(string obj)
        {
            if (String.IsNullOrEmpty(obj))
                return;

            var message = JsonConvert.DeserializeObject<RpcRequestModel>(obj);
            if (message.MessageType == MessageTypeEnum.Text)
            {

            }
            else if (message.MessageType == MessageTypeEnum.Funtion)
            {

            }

            //işlemler
            //


            var response = new RpcResponseModel();
            response.ResultData = "hayde - ";

            _ = connect.InvokeAsync<ResponseModel>("RpcResponse", JsonConvert.SerializeObject(response));
        }
        private void Connect_ConnectState(bool obj)
        {
        }
        #endregion

        #region Dispose
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue=true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DefaultClientSocket()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
