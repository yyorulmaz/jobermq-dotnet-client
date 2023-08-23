using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Constant;
using JoberMQ.Client.DotNet.Extension.Consume;
using JoberMQ.Client.DotNet.Factory;
using JoberMQ.Common.Helpers;
using JoberMQ.Common.Method.Abstraction;
using JoberMQ.Common.Method.Enums;
using JoberMQ.Common.Method.Factories;
using JoberMQ.Common.Method.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace JoberMQ.Client.DotNet.Imp
{
    internal class ClientSocket : IClient
    {
        private readonly IConfiguration configuration;
        IConnect connect;
        public ClientSocket(string clientKey, IConfiguration configuration)
        {
            this.configuration = configuration;
            clientInfo = ClientInfoFactory.Create(ClientConst.ClientInfoFactory, ClientConst.ClientType, clientKey, ClientConst.IsOfflineClient);
            var account = AccountFactory.Create(ClientConst.AccountFactory, true, true, ClientConst.UserName, ClientConst.Password, configuration.EndpointLogin, configuration.EndpointHub);
            connect = ConnectFactory.Create(ClientConst.ConnectFactory, ClientConst.ConnectionRetryTimeout, ClientConst.AutoReconnect, account, clientInfo);
            method = MethodFactory.Create(MethodFactoryEnum.Default);

            connect.ReceiveFreeMessageText += Connect_ReceiveFreeMessageText;
            connect.ReceiveRpcMessageText += Connect_ReceiveRpcMessageText;
            connect.ReceiveRpcMessageFunction += Connect_ReceiveRpcMessageFunction;
        }



        IClientInfo clientInfo;
        public IClientInfo ClientInfo => clientInfo;

        IMethod method;
        public IMethod Method => method;


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
                await this.Consume().SubAsync(ClientConst.DefaultQueueClientKey, true);
            }

            return conn;
        }

        public event Action<string> ReceiveFreeMessageText;
        private void Connect_ReceiveFreeMessageText(string obj) => ReceiveFreeMessageText?.Invoke(obj);


        public event Action<Guid, string> ReceiveRpcMessageText;
        private void Connect_ReceiveRpcMessageFunction(Guid arg1, string arg2)
        {
            var returnData = new MethodReturnModel<byte[]>();

            try
            {
                var methodProperty = JsonConvert.DeserializeObject<MethodPropertyModel>(arg2);
                var type = Type.GetType(methodProperty.MethodName);


                MethodInfo methodInfo;
                try
                {
                    methodInfo = type.GetMethod(methodProperty.MethodName);
                }
                catch (Exception)
                {
                    methodInfo = type.GetMethod(methodProperty.MethodName, Type.EmptyTypes);
                }

                object[] setParameter = new object[methodProperty.ParemeterValues.Count];
                for (int i = 0; i < methodProperty.ParemeterValues.Count; i++)
                {
                    var propertyAssembly = Assembly.Load(new AssemblyName(methodProperty.ParemeterValues[i].ParameterAssemblyName));
                    var propertyType = propertyAssembly.GetType(methodProperty.ParemeterValues[i].ParameterTypeFullName);
                    setParameter[i] = JsonConvert.DeserializeObject(methodProperty.ParemeterValues[i].ParameterValue, propertyType);
                }

                Type typeAsync = typeof(AsyncStateMachineAttribute);
                var isAsync = (AsyncStateMachineAttribute)methodInfo.GetCustomAttribute(typeAsync);

                if (isAsync == null)
                {
                    var rtrnDt = methodInfo.Invoke(this, setParameter);

                    if (rtrnDt != null)
                    {
                        var rtrnTyp = rtrnDt.GetType();
                        returnData.TypeFullName = rtrnTyp.FullName;
                        returnData.Data = ByteHelper.ObjectToByteArray(rtrnDt);
                        returnData.StatusCode = "0";
                    }
                    else
                    {
                        returnData.TypeFullName = null;
                        returnData.Data = null;
                        returnData.StatusCode = "0";
                    }
                }
                else
                {
                    var task = (Task)methodInfo.Invoke(this, setParameter);
                    if (task != null)
                    {
                        //await task.ConfigureAwait(false);
                        task.ConfigureAwait(false).GetAwaiter();
                        var resultProperty = task.GetType().GetProperty("Result");


                        var rtrnDt = resultProperty.GetValue(task);
                        var rtrnTyp = rtrnDt.GetType();

                        if (rtrnDt != null)
                        {
                            returnData.TypeFullName = rtrnTyp.FullName;

                            if (returnData.TypeFullName == "System.Threading.Tasks.VoidTaskResult")
                                returnData.Data = null;
                            else
                                returnData.Data = ByteHelper.ObjectToByteArray(rtrnDt);

                            returnData.StatusCode = "0";
                        }
                        else
                        {
                            returnData.TypeFullName = null;
                            returnData.Data = null;
                            returnData.StatusCode = "0";
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                returnData.IsOperationSuccess = false;
                returnData.Data = null;
                returnData.StatusCode = "1";
                returnData.Message = ex.Message;
            }

            _ = SendAsync("RpcMessageResponse", arg1, returnData.Data.ToString(), returnData.IsOperationSuccess, returnData.Message);

        }

        private void Connect_ReceiveRpcMessageText(Guid id, string obj) => ReceiveRpcMessageText?.Invoke(id, obj);


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
