using JoberMQ.Client.Net.Abstraction.Account;
using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Enums.Client;
using JoberMQ.Client.Net.Models.Login;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Implementation.Connect.Default
{
    public class DfConnect : IConnect
    {
        IAccountInfo accountInfo;
        IClientInfo clientInfo;
        event Action<string> receiveData;
        event Action<string> receiveDataError;
        event Action<bool> receiveServerActive;
        public DfConnect(
            IClientInfo clientInfo,
            IAccountInfo accountInfo,
            ref Action<string> receiveData,
            ref Action<string> receiveDataError,
            ref Action<bool> receiveServerActive,
            int connectionRetryTimeout)
        {
            this.clientInfo = clientInfo;
            this.accountInfo = accountInfo;
            this.receiveData = receiveData;
            this.receiveDataError = receiveDataError;
            this.receiveServerActive = receiveServerActive;
            this.connectionRetryTimeout = connectionRetryTimeout;
        }



        public event Action<bool> ConnectState;
        string token;
        public async Task<bool> ConnectAsync()
        {
            var account = accountInfo.Accounts.Get(x => x.IsMaster == true && x.IsActive == true);
            var getToken = await GetTokenAsync(account.EndpointLogin, account.UserName, account.Password, clientInfo.ClientKey);

            if (getToken != null && getToken.IsSuccess)
                token = getToken.Token;
            else
                throw new Exception(getToken.Message);


            hubConn = CreateHubConnection(account.EndpointHub);

            try
            {
                await hubConn.StartAsync();

                Console.WriteLine("connected");
            }
            catch (Exception ex)
            {
                if (AutomaticReconnect)
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

        HubConnection CreateHubConnection(string endpoint)
        {
            HubConnection hub;

            TimeSpan[] reconnectTime = new TimeSpan[1];
            reconnectTime[0] = TimeSpan.FromSeconds(1);

            if (AutomaticReconnect)
            {
                hub = new HubConnectionBuilder()
                .WithUrl(endpoint, options =>
                {
                    //options.DefaultTransferFormat = Microsoft.AspNetCore.Connections.TransferFormat.Text;
                    //options.DefaultTransferFormat = Microsoft.AspNetCore.Connections.TransferFormat.Binary;

                    options.AccessTokenProvider = () => Task.FromResult(token);

                    options.Headers.Add("ClientType", ClientTypeEnum.Normal.ToString());
                    options.Headers.Add("ClientKey", clientInfo.ClientKey);
                    options.Headers.Add("ClientGroupKey", clientInfo.ClientGroupKey);
                    options.Headers.Add("IsOfflineClient", clientInfo.IsOfflineClient.ToString());
                })
               //.WithAutomaticReconnect()
               .WithAutomaticReconnect(reconnectTime)
               .Build();
            }
            else
            {
                hub = new HubConnectionBuilder()
                .WithUrl(endpoint, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(token);

                    options.Headers.Add("ClientType", ClientTypeEnum.Normal.ToString());
                    options.Headers.Add("ClientKey", clientInfo.ClientKey);
                    options.Headers.Add("ClientGroupKey", clientInfo.ClientGroupKey);
                    options.Headers.Add("IsOfflineClient", clientInfo.IsOfflineClient.ToString());
                })
               .Build();
            }

            hub.Reconnecting += Reconnecting;
            hub.Closed += HubConn_Closed;
            hub.Reconnected += HubConn_Reconnected;





            hub.On<string>("ReceiveData", (m) => receiveData?.Invoke(m));
            hub.On<string>("ReceiveDataError", (m) => receiveDataError?.Invoke(m));
            hub.On<string>("ClusterLoadBalancingEndpointReceive", (m) => ClusterLoadBalancingEndpointReceive(m));
            hub.On<bool>("ReceiveServerActive", (m) => ReceiveServerActiveAction(m));

            return hub;
        }


        public async Task<ResponseLoginModel> GetTokenAsync(string endpoint, string user, string pass, string clientKey)
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
            catch (Exception)
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
        public bool IsConnect => hubConn == null ? false : hubConn.State == HubConnectionState.Connected ? true : false;

        HubConnection hubConn;
        public HubConnection HubConnection => hubConn;


        private int connectionRetryTimeout;
        public int ConnectionRetryTimeout => connectionRetryTimeout;
        private bool isReConnectStarted = false;
        private bool automaticReconnect;
        public bool AutomaticReconnect => automaticReconnect;


        bool isServerActive;
        public bool IsServerActive => isServerActive;

        private void ReceiveServerActiveAction(bool value)
        {
            isServerActive = value;
            receiveServerActive?.Invoke(value);
        }


        private async Task ReConnectAsync()
        {
            ReceiveServerActiveAction(false);
            var account = accountInfo.Accounts.Get(x => x.IsMaster == true && x.IsActive == true);

            if (hubConn.State == HubConnectionState.Disconnected || hubConn.State == HubConnectionState.Reconnecting)
            {
                if (token == null)
                {
                    var getToken = await GetTokenAsync(account.EndpointLogin, account.UserName, account.Password, clientInfo.ClientKey);
                    if (getToken != null && getToken.IsSuccess)
                        token = getToken.Token;
                }

                if (token == null)
                {
                    _ = Task.Run(() => ConnectState?.Invoke(false));

                    _ = Task.Run(() =>
                    {
                        Thread.Sleep(ConnectionRetryTimeout);
                        _ = ReConnectAsync();
                    });
                }
                else
                {
                    hubConn = CreateHubConnection(account.EndpointHub);

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
                                Thread.Sleep(ConnectionRetryTimeout);
                                _ = ReConnectAsync();
                            });
                        }
                    }
                    catch (Exception)
                    {
                        _ = Task.Run(() => ConnectState?.Invoke(false));

                        if (AutomaticReconnect)
                            _ = Task.Run(() =>
                            {
                                Thread.Sleep(ConnectionRetryTimeout);
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
            token = null;

            _ = Task.Run(() =>
            {
                ReceiveServerActiveAction(false);
                Thread.Sleep(ConnectionRetryTimeout);


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
    }
}
