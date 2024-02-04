using JoberMQ.Client.DotNet.Abs;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JoberMQ.Client.DotNet.Imp
{
    internal class ConnectSocket : IConnect
    {
        private readonly int retryTimeout;
        private readonly bool autoReconnect;
        private ConcurrentDictionary<Guid, IAccount> accounts = new ConcurrentDictionary<Guid, IAccount>();
        private IAccount masterAccount;
        private IClientInfo clientInfo;
        private HubConnection hubConnection;
        private bool isServerActive;
        private bool isReconnectStarted = false;

        public ConnectSocket(int retryTimeout, bool autoReconnect, IAccount account, IClientInfo clientInfo)
        {
            this.retryTimeout = retryTimeout;
            this.autoReconnect = autoReconnect;
            accounts.TryAdd(Guid.NewGuid(), account);
            this.clientInfo = clientInfo;
        }



        public HubConnection HubConn => hubConnection;
        public bool IsConnect => hubConnection == null ? false : hubConnection.State == HubConnectionState.Connected ? true : false;
        public event Action<bool> ConnectState;

        public bool IsServerActive => isServerActive;


        public event Action<bool> ServerActiveState;

        public event Action<string> ReceiveMessageFreeText;
        public event Action<Guid, string> ReceiveMessageRpcText;
        public event Action<Guid, string> ReceiveMessageRpcFunction;
        public event Action<MessageDbo> ReceiveMessage;

        public async Task<bool> ConnectAsync()
        {
            try
            {
                var account = accounts.FirstOrDefault(x => x.Value.IsMaster == true && x.Value.IsActive == true);
                masterAccount = account.Value;
                var responseLogin = await LoginAsync(masterAccount.EndpointLogin.GetEndpoint(), masterAccount.UserName, masterAccount.Password, clientInfo.ClientKey);

                if (responseLogin != null && responseLogin.IsSuccess)
                {
                    masterAccount.Token = responseLogin.Token;
                    accounts.TryUpdate(account.Key, masterAccount, null);
                }
                else
                    return false;


                hubConnection = CreateHubConnection();
                await hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (autoReconnect)
                {
                    isReconnectStarted = true;
                    _ = ReConnectAsync();
                    return false;
                }
                else
                    throw new Exception(ex.Message);
            }

            return true;
        }
        Task Reconnecting(Exception arg)
        {
            Task.Run(() => ConnectState?.Invoke(true));

            return Task.CompletedTask;
        }
        private Task HubConn_Reconnected(string arg)
        {
            return Task.CompletedTask;
        }
        private async Task ReConnectAsync()
        {
            ServerActiveStateAction(false);
            var account = accounts.FirstOrDefault(x => x.Value.IsMaster == true && x.Value.IsActive == true);
            masterAccount = account.Value;

            if (hubConnection.State == HubConnectionState.Disconnected || hubConnection.State == HubConnectionState.Reconnecting)
            {
                if (masterAccount.Token == null)
                {
                    var responseLogin = await LoginAsync(masterAccount.EndpointLogin.GetEndpoint(), masterAccount.UserName, masterAccount.Password, clientInfo.ClientKey);
                    if (responseLogin != null && responseLogin.IsSuccess)
                    {
                        masterAccount.Token = responseLogin.Token;
                        accounts.TryUpdate(account.Key, masterAccount, null);
                    }
                }

                if (masterAccount.Token == null)
                {
                    _ = Task.Run(() => ConnectState?.Invoke(false));

                    _ = Task.Run(() =>
                    {
                        Thread.Sleep(retryTimeout);
                        _ = ReConnectAsync();
                    });
                }
                else
                {
                    hubConnection = CreateHubConnection();

                    try
                    {
                        await hubConnection.StartAsync();

                        if (hubConnection.State == HubConnectionState.Connected)
                        {
                            isReconnectStarted = false;
                            _ = Task.Run(() => ConnectState?.Invoke(true));
                        }
                        else
                        {
                            _ = Task.Run(() => ConnectState?.Invoke(false));

                            _ = Task.Run(() =>
                            {
                                Thread.Sleep(retryTimeout);
                                _ = ReConnectAsync();
                            });
                        }
                    }
                    catch (Exception)
                    {
                        _ = Task.Run(() => ConnectState?.Invoke(false));

                        if (autoReconnect)
                            _ = Task.Run(() =>
                            {
                                Thread.Sleep(retryTimeout);
                                _ = ReConnectAsync();
                            });
                    }
                }
            }
        }
        Task HubConn_Closed(Exception arg)
        {
            var account = accounts.FirstOrDefault(x => x.Value.IsMaster == true && x.Value.IsActive == true);
            masterAccount.Token = null;
            accounts.TryUpdate(account.Key, masterAccount, null);

            _ = Task.Run(() =>
            {
                ServerActiveStateAction(false);
                Thread.Sleep(retryTimeout);


                if (isReconnectStarted == false)
                    _ = ReConnectAsync();
            });

            return Task.CompletedTask;
        }
        private void ServerActiveStateAction(bool value)
        {
            isServerActive = value;
            ServerActiveState?.Invoke(value);
        }

        HubConnection CreateHubConnection()
        {
            HubConnection hub;

            TimeSpan[] reconnectTime = new TimeSpan[1];
            reconnectTime[0] = TimeSpan.FromSeconds(1);

            if (autoReconnect)
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



            hub.On<string>("ReceiveMessageFreeText", (m) => ReceiveMessageFreeText?.Invoke(m));
            hub.On<Guid, string>("ReceiveMessageRpcText", (i, m) => ReceiveMessageRpcText?.Invoke(i, m));
            hub.On<Guid, string>("ReceiveMessageRpcFunction", (i, m) => ReceiveMessageRpcFunction?.Invoke(i, m));
            hub.On<MessageDbo>("ReceiveMessage", (m) => ReceiveMessage?.Invoke(m));


            hub.On<bool>("ServerActiveState", (m) => ServerActiveStateAction(m));



            return hub;
        }
        HttpConnectionOptions CreateHttpConnectionOptions(HttpConnectionOptions options)
        {
            options.AccessTokenProvider = () => Task.FromResult(masterAccount.Token);
            var clientInfoData = new ClientInfoDataModel(clientInfo.ClientType, clientInfo.ClientKey, clientInfo.IsOfflineClient, clientInfo.IsClientActive);
            options.Headers.Add("ClientInfoData", JsonConvert.SerializeObject(clientInfoData));
            return options;
        }
        public async Task<ResponseLoginModel> LoginAsync(string endpoint, string user, string pass, string clientKey)
        {
            HttpClient httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(endpoint));
            var encoding = Encoding.GetEncoding("iso-8859-1");
            httpClient.DefaultRequestHeaders.Add("clientKey", clientKey);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(encoding.GetBytes($"{user}:{pass}")));

            try
            {
                var response = await httpClient.SendAsync(request);
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
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ConnectSocket()
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
