using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Constant;
using JoberMQ.Client.DotNet.Factory;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Method.Abstraction;
using JoberMQ.Common.Method.Enums;
using JoberMQ.Common.Method.Factories;
using JoberMQ.Common.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace JoberMQ.Client.DotNet.Imp
{
    internal class ClientSocket : IClient
    {
        private readonly IConfiguration configuration;
        public ClientSocket(string clientKey, IConfiguration configuration)
        {
            this.configuration = configuration;
            clientInfo = ClientInfoFactory.Create(ClientConst.ClientInfoFactory, ClientConst.ClientType, clientKey, ClientConst.IsOfflineClient);
            var account = AccountFactory.Create(ClientConst.AccountFactory, true, true, ClientConst.UserName, ClientConst.Password, configuration.EndpointLogin, configuration.EndpointHub);
            connect = ConnectFactory.Create(ClientConst.ConnectFactory, ClientConst.ConnectionRetryTimeout, ClientConst.AutoReconnect, account, clientInfo);
            method = MethodFactory.Create(MethodFactoryEnum.Default);

            connect.ReceiveMessageFreeText += Connect_ReceiveMessageFreeText;
            connect.ReceiveMessageRpcText += Connect_ReceiveMessageRpcText;
            //connect.ReceiveRpcMessageFunction += Connect_ReceiveRpcMessageFunction;
            connect.ReceiveMessage +=Connect_ReceiveMessage;
        }

        

        IConnect connect;
        public IConnect Connect => connect;
        IClientInfo clientInfo;
        public IClientInfo ClientInfo => clientInfo;

        IMethod method;
        public IMethod Method => method;


        public async Task<R> InvokeAsync<R>(string methodName)
            => await connect.HubConn.InvokeAsync<R>(methodName);
        public async Task<R> InvokeAsync<R>(string methodName, object arg1)
            => await connect.HubConn.InvokeAsync<R>(methodName, arg1);
        public async Task<R> InvokeAsync<R>(string methodName, object arg1, object arg2)
            => await connect.HubConn.InvokeAsync<R>(methodName, arg1, arg2);
        public async Task<R> InvokeAsync<R>(string methodName, object arg1, object arg2, object arg3)
            => await connect.HubConn.InvokeAsync<R>(methodName, arg1, arg2, arg3);
        public async Task SendAsync(string methodName, object arg1)
            => await connect.HubConn.SendAsync(methodName, arg1);
        public async Task SendAsync(string methodName, object arg1, object arg2)
            => await connect.HubConn.SendAsync(methodName, arg1, arg2);
        public async Task SendAsync(string methodName, object arg1, object arg2, object arg3, object arg4)
            => await connect.HubConn.SendAsync(methodName, arg1, arg2, arg3, arg4);

        public async Task<bool> ConnectAsync()
        {
            var conn = await connect.ConnectAsync();

            if (conn)
            {
                await this.Consume().QueueSub(ClientConst.DefaultQueueClientKey, true).SendAsync();
            }

            return conn;
        }





        public event Action<string> ReceiveMessageFreeText;
        private void Connect_ReceiveMessageFreeText(string obj) => ReceiveMessageFreeText?.Invoke(obj);


        public event Action<Guid, string> ReceiveMessageRpcText;
        private void Connect_ReceiveMessageRpcText(Guid id, string obj) => ReceiveMessageRpcText?.Invoke(id, obj);

        public event Action<string> ReceiveMessage;
        private void Connect_ReceiveMessage(MessageDbo obj)
        {
            if (obj == null)
                return;


            _ = Task.Run(async () =>
            {
                #region STARTED
                //var jobStartedModel = new StartedModel();
                //jobStartedModel.MessageId = obj.Id;
                //await connect.HubConn.SendAsync("Started", JsonConvert.SerializeObject(jobStartedModel));
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

                    //_ = connect.HubConn.SendAsync("Completed", JsonConvert.SerializeObject(jobCompleted));
                }
                else if (obj.Message.MessageType == MessageTypeEnum.Text)
                {
                    ReceiveMessage?.Invoke(obj.Message.Message);
                    if (configuration.TextMessageReceiveAutoCompleted == true)
                    {
                        var jobCompleted = new CompletedModel();
                        jobCompleted.MessageId = obj.Id;
                        jobCompleted.IsError = false;
                        jobCompleted.Message = "";

                        //_ = connect.HubConn.SendAsync("Completed", JsonConvert.SerializeObject(jobCompleted));
                    }
                }
            });
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
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ClientSocket()
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
