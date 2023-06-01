using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Models.Account;
using JoberMQ.Common.Models.Client;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Implementation.Connect.Default
{
    internal class DefaultConnect : IConnect
    {
        private bool isReConnectStarted = false;
        private ConcurrentDictionary<Guid, IAccount> Accounts = new ConcurrentDictionary<Guid, IAccount>();
        private IAccount masterAccount;
        IClientInfo clientInfo;

        public DefaultConnect(
            int retryTimeout,
            bool autoReconnect,
            IAccount account,
            IClientInfo clientInfo)
        {
            this.retryTimeout = retryTimeout;
            this.autoReconnect = autoReconnect;
            Accounts.TryAdd(Guid.NewGuid(), account);
            this.clientInfo = clientInfo;
        }


        //public event Action<string> ReceiveData; 
        public event Action<MessageDbo> ReceiveData;
        public event Action<string> ReceiveDataError;
        public event Action<bool> ReceiveServerActive;
        public event Action<string> ReceiveRpc;


        private int retryTimeout;
        public int RetryTimeout => retryTimeout;

        private bool autoReconnect;
        public bool AutoReconnect => autoReconnect;

        HubConnection hubConn;
        public async Task<R> InvokeAsync<R>(string methodName, object arg)
            => await hubConn.InvokeAsync<R>(methodName, arg);
        public async Task<R> InvokeAsync<R>(string methodName, object arg1, object arg2)
            => await hubConn.InvokeAsync<R>(methodName, arg1, arg2);
        //=> await hubConn.InvokeCoreAsync<R>(methodName, new[] { arg1, arg2});
        public async Task<R> InvokeAsync<R>(string methodName, object arg1, object arg2, object arg3)
            => await hubConn.InvokeAsync<R>(methodName, arg1, arg2, arg3);
        //=> await hubConn.InvokeCoreAsync<R>(methodName, new[] { arg1, arg2, arg3} );

        public async Task<bool> ConnectAsync()
        {
            try
            {
                var account = Accounts.FirstOrDefault(x => x.Value.IsMaster == true && x.Value.IsActive == true);
                masterAccount = account.Value;
                var responseLogin = await LoginAsync(masterAccount.EndpointLogin.GetEndpoint(), masterAccount.UserName, masterAccount.Password, clientInfo.ClientKey);

                Console.WriteLine("IsSuccess : " + responseLogin.IsSuccess);

                if (responseLogin != null && responseLogin.IsSuccess)
                {
                    masterAccount.Token = responseLogin.Token;
                    Console.WriteLine("Token : " + responseLogin.Token);
                    Accounts.TryUpdate(account.Key, masterAccount, null);
                }
                else
                    //throw new Exception(getToken.Message);
                    return false;


                hubConn = CreateHubConnection();


                await hubConn.StartAsync();

                Console.WriteLine("connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (AutoReconnect)
                {
                    isReConnectStarted = true;
                    _ = ReConnectAsync();
                    return false;
                }
                else
                    throw new Exception(ex.Message);
            }

            return true;
        }
        public async Task<ResponseLoginModel> LoginAsync(string endpoint, string user, string pass, string clientKey)
        {
            HttpClient HttpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(endpoint));
            var encoding = Encoding.GetEncoding("iso-8859-1");
            HttpClient.DefaultRequestHeaders.Add("clientKey", clientKey);
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(encoding.GetBytes($"{user}:{pass}")));

            try
            {
                var response = await HttpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ResponseLoginModel>(content);
            }
            catch (Exception ex)
            {
                return new ResponseLoginModel
                {
                    IsSuccess = false,
                    StatusCode = "0.0.12",
                    //Message = JoberMQClient.StatusCode.GetStatusMessage("0.0.12"),
                    Message = "",
                    Token = null
                };
            }
        }
        HubConnection CreateHubConnection()
        {
            HubConnection hub;

            TimeSpan[] reconnectTime = new TimeSpan[1];
            reconnectTime[0] = TimeSpan.FromSeconds(1);

            if (AutoReconnect)
            {
                hub = new HubConnectionBuilder()
                .WithUrl(masterAccount.EndpointHub.GetEndpoint(), options =>
                {
                    options = CreateHttpConnectionOptions(options);
                })
               //.WithAutomaticReconnect()
               .WithAutomaticReconnect(reconnectTime)
               .Build();
            }
            else
            {
                hub = new HubConnectionBuilder()
                .WithUrl(masterAccount.EndpointHub.GetEndpoint(), options =>
                {
                    options = CreateHttpConnectionOptions(options);
                })
               .Build();
            }

            hub.Reconnecting += Reconnecting;
            hub.Closed += HubConn_Closed;
            hub.Reconnected += HubConn_Reconnected;





            //hub.On<string>("ReceiveData", (m) => ReceiveData?.Invoke(m));
            hub.On<MessageDbo>("ReceiveData", (m) => ReceiveData?.Invoke(m));
            hub.On<string>("ReceiveDataError", (m) => ReceiveDataError?.Invoke(m));
            hub.On<string>("ClusterLoadBalancingEndpointReceive", (m) => ClusterLoadBalancingEndpointReceive(m));
            hub.On<bool>("ReceiveServerActive", (m) => ReceiveServerActiveAction(m));
            hub.On<string>("ReceiveRpc", (m) => ReceiveRpc?.Invoke(m));

            return hub;
        }
        HttpConnectionOptions CreateHttpConnectionOptions(HttpConnectionOptions options)
        {
            //httpConnectionOptions.DefaultTransferFormat = Microsoft.AspNetCore.Connections.TransferFormat.Text;
            //httpConnectionOptions.DefaultTransferFormat = Microsoft.AspNetCore.Connections.TransferFormat.Binary;

            options.AccessTokenProvider = () => Task.FromResult(masterAccount.Token);

            Console.WriteLine("AccessTokenProvider : " + options.AccessTokenProvider);


            var clientInfoData = new ClientInfoDataModel
            {
                ClientType = clientInfo.ClientType,
                ClientKey = clientInfo.ClientKey,
                IsOfflineClient = clientInfo.IsOfflineClient,
                IsClientActive = clientInfo.IsClientActive
            };

            options.Headers.Add("ClientInfoData", JsonConvert.SerializeObject(clientInfoData));

            //options.Headers.Add("ClientType", clientInfo.ClientType.ToString());
            //options.Headers.Add("ClientKey", clientInfo.ClientKey);
            //options.Headers.Add("ClientGroupKey", clientInfo.ClientGroupKey);
            //options.Headers.Add("IsOfflineClient", clientInfo.IsOfflineClient.ToString());

            return options;
        }



        public bool IsConnect => hubConn == null ? false : hubConn.State == HubConnectionState.Connected ? true : false;
        public event Action<bool> ConnectState;

        bool isServerActive;
        public bool IsServerActive => isServerActive;
        private void ReceiveServerActiveAction(bool value)
        {
            isServerActive = value;
            ReceiveServerActive?.Invoke(value);
        }





        private async Task ReConnectAsync()
        {
            ReceiveServerActiveAction(false);
            var account = Accounts.FirstOrDefault(x => x.Value.IsMaster == true && x.Value.IsActive == true);
            masterAccount = account.Value;

            if (hubConn.State == HubConnectionState.Disconnected || hubConn.State == HubConnectionState.Reconnecting)
            {
                if (masterAccount.Token == null)
                {
                    var responseLogin = await LoginAsync(masterAccount.EndpointLogin.GetEndpoint(), masterAccount.UserName, masterAccount.Password, clientInfo.ClientKey);
                    if (responseLogin != null && responseLogin.IsSuccess)
                    {
                        masterAccount.Token = responseLogin.Token;
                        Accounts.TryUpdate(account.Key, masterAccount, null);
                    }
                }

                if (masterAccount.Token == null)
                {
                    _ = Task.Run(() => ConnectState?.Invoke(false));

                    _ = Task.Run(() =>
                    {
                        Thread.Sleep(RetryTimeout);
                        _ = ReConnectAsync();
                    });
                }
                else
                {
                    hubConn = CreateHubConnection();

                    try
                    {
                        await hubConn.StartAsync();

                        if (hubConn.State == HubConnectionState.Connected)
                        {
                            isReConnectStarted = false;
                            _ = Task.Run(() => ConnectState?.Invoke(true));
                        }
                        else
                        {
                            _ = Task.Run(() => ConnectState?.Invoke(false));

                            _ = Task.Run(() =>
                            {
                                Thread.Sleep(RetryTimeout);
                                _ = ReConnectAsync();
                            });
                        }
                    }
                    catch (Exception)
                    {
                        _ = Task.Run(() => ConnectState?.Invoke(false));

                        if (AutoReconnect)
                            _ = Task.Run(() =>
                            {
                                Thread.Sleep(RetryTimeout);
                                _ = ReConnectAsync();
                            });
                    }
                }
            }
        }
        private Task Reconnecting(Exception arg)
        {
            Task.Run(() => ConnectState?.Invoke(true));

            return Task.CompletedTask;
        }
        private Task HubConn_Reconnected(string arg)
        {
            return Task.CompletedTask;
        }
        private Task HubConn_Closed(Exception arg)
        {
            var account = Accounts.FirstOrDefault(x => x.Value.IsMaster == true && x.Value.IsActive == true);
            masterAccount.Token = null;
            Accounts.TryUpdate(account.Key, masterAccount, null);

            _ = Task.Run(() =>
            {
                ReceiveServerActiveAction(false);
                Thread.Sleep(RetryTimeout);


                if (isReConnectStarted == false)
                    _ = ReConnectAsync();
            });

            return Task.CompletedTask;
        }

        private void ClusterLoadBalancingEndpointReceive(string msg)
        {
            //todo yap
            //var endpointModel = JsonConvert.DeserializeObject<ServerEndpointModel>(msg);
            //roundRobin.Add(endpointModel, 1);
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
                    masterAccount.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DfConnect()
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
