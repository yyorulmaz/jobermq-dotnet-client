using JoberMQ.Client.Common.Enums.Client;
using JoberMQ.Client.Common.Enums.Protocol;
using JoberMQ.Client.Common.Models.Base;
using JoberMQ.Client.Common.Models.Cluster;
using JoberMQ.Client.Common.Models.Config;
using JoberMQ.Client.Common.Models.Login;
using JoberMQ.Client.Common.Models.Response;
using JoberMQ.Client.Common.RoundRobin;
using JoberMQ.Client.Net.Abstract.Connect;
using JoberMQ.Client.Net.Operations;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace JoberMQ.Client.Net.Concrete.Connect
{
    internal class ConnectionSocket : IConnection
    {
        #region PROPERTY HELPER
        private string clientId;
        private string clientGroupKey;
        private int connectionRetryTimeout;
        private bool isOfflineClient;
        private HubConnection hubConn;
        private bool isServerActive = false;
        //private bool isNotConnectKeepTrying;
        private bool automaticReconnect;
        private int automaticReconnectTimeSecond;

        private string token;
        private string userName;
        private string password;
        private bool isReConnectStarted = false;
        #endregion

        #region PROPERTY
        public string Endpoint => roundRobin.GetEndRoundRobinData().HostName;
        public string ClientId => clientId;
        public string ClientGroupKey => clientGroupKey;
        public string ConnectionId => hubConn.ConnectionId;
        public bool IsConnect => hubConn == null ? false : hubConn.State == HubConnectionState.Connected ? true : false;
        public bool IsServerActive => isServerActive;
        //public bool IsNotConnectKeepTrying => isNotConnectKeepTrying;
        public bool AutomaticReconnect => automaticReconnect;
        public int ConnectionRetryTimeout => connectionRetryTimeout;
        #endregion

        #region CONSRUCTOR
        internal ConnectionSocket(ConfigClientModel config)
        {
            var roundRobinData = new ServerEndpointModel
            {
                IsSsl = config.IsSsl,
                HostName = config.HostName,
                Port = config.Port,
                ActionHub = "JoberHub",
                ActionLogin = "account/login",
                IsActive = true
            };

            roundRobin = JoberMQ.Client.Net.Factory.CreateRoundRobin<ServerEndpointModel>();
            roundRobin.Add(roundRobinData, 1);

            this.clientId = config.ClientId;
            this.clientGroupKey = config.ClientGroupKey;
            this.connectionRetryTimeout = config.ConnectionRetryTimeout;
            this.isOfflineClient = config.IsOfflineMode;
            this.userName = config.UserName;
            this.password = config.Password;
            automaticReconnect = config.AutomaticReconnect;
        }
        #endregion

        #region TOKEN
        public ResponseLoginModel GetToken(string endpoint) => new LoginOperation().AuthenticateAsync(endpoint, userName, password, ClientId).Result;
        #endregion

        #region CONNECT
        private (string endpointLogin, string endpointHub) GetEndpoint()
        {
            var activeServer = roundRobin.GetEndRoundRobinData();
            var urlProtocol = activeServer.IsSsl == true ? UrlProtocolEnum.https.ToString() : UrlProtocolEnum.http.ToString();
            var port = activeServer.Port <= 0 ? "" : $":{activeServer.Port}";
            var endpointLogin = $"{urlProtocol}://{activeServer.HostName}{port}/{activeServer.ActionLogin}";
            var endpointHub = $"{urlProtocol}://{activeServer.HostName}{port}/{activeServer.ActionHub}";

            return (endpointLogin, endpointHub);
        }
        public async Task<bool> ConnectAsync()
        {
            var endpoint = GetEndpoint();
            var getToken = GetToken(endpoint.endpointLogin);

            if (getToken != null && getToken.IsSuccess)
                token = getToken.Token;
            else
                throw new Exception(getToken.Message);

            hubConn = CreateHubConnection(endpoint.endpointHub);

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
        private async Task ReConnectAsync()
        {
            ReceiveServerActiveAction(false);
            var endpoint = GetEndpoint();

            Console.WriteLine("ReConnect 1");

            if (hubConn.State == HubConnectionState.Disconnected || hubConn.State == HubConnectionState.Reconnecting)
            {
                if (token == null)
                {
                    var getToken = GetToken(endpoint.endpointLogin);
                    if (getToken != null && getToken.IsSuccess)
                        token = getToken.Token;
                    else
                        Console.WriteLine("ReConnect 2");
                }

                if (token == null)
                {
                    Console.WriteLine("ReConnect 3");
                    _ = Task.Run(() =>
                    {
                        Thread.Sleep(ConnectionRetryTimeout);
                        _ = ReConnectAsync();
                    });
                }
                else
                {
                    hubConn = CreateHubConnection(endpoint.endpointHub);

                    try
                    {
                        await hubConn.StartAsync();

                        if (hubConn.State == HubConnectionState.Connected)
                        {
                            Console.WriteLine("ReConnect 4");
                            isReConnectStarted = false;
                            _ = Task.Run(() => ConnectState?.Invoke(true));
                        }
                        else
                        {
                            Console.WriteLine("ReConnect 4");

                            _ = Task.Run(() =>
                            {
                                Thread.Sleep(ConnectionRetryTimeout);
                                _ = ReConnectAsync();
                            });
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("ReConnect 5");
                        if (AutomaticReconnect)
                            _ = Task.Run(() =>
                            {
                                Thread.Sleep(ConnectionRetryTimeout);
                                _ = ReConnectAsync();
                            });
                    }
                }
            }




            //Console.WriteLine("ReConnect 1");











            //try
            //{
            //    ReceiveServerActiveAction(false);

            //    if (hubConn.State == HubConnectionState.Disconnected || hubConn.State == HubConnectionState.Reconnecting)
            //    {
            //        var endpoint = GetEndpoint();

            //        if (token == null)
            //        {
            //            var getToken = GetToken(endpoint.endpointLogin);
            //            if (getToken != null && getToken.IsSuccess)
            //                token = getToken.Token;
            //            else
            //            {
            //                Console.WriteLine(getToken.Message);

            //                _ = Task.Run(() =>
            //                {
            //                    Thread.Sleep(ConnectionRetryTimeout);
            //                    _ = ReConnectAsync();

            //                    return;
            //                });
            //            }

            //        }

            //        hubConn = CreateHubConnection(endpoint.endpointHub);

            //        try
            //        {
            //            await hubConn.StartAsync();

            //            if (hubConn.State == HubConnectionState.Connected)
            //            {
            //                isReConnectStarted = false;
            //                Console.WriteLine("connected");
            //                _ = Task.Run(() => ConnectState?.Invoke(true));
            //            }
            //            else
            //            {
            //                _ = Task.Run(() =>
            //                {
            //                    Thread.Sleep(ConnectionRetryTimeout);
            //                    _ = ReConnectAsync();
            //                });
            //            }
            //        }
            //        catch (Exception)
            //        {
            //            if (AutomaticReconnect)
            //                _ = Task.Run(() =>
            //                {
            //                    Thread.Sleep(ConnectionRetryTimeout);
            //                    _ = ReConnectAsync();
            //                });
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    _ = Task.Run(() =>
            //    {
            //        Thread.Sleep(ConnectionRetryTimeout);
            //        _ = ReConnectAsync();
            //    });
            //}
        }
        private HubConnection CreateHubConnection(string endpoint)
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
                    options.Headers.Add("ClientId", clientId);
                    options.Headers.Add("ClientGroupKey", clientGroupKey);
                    options.Headers.Add("IsOfflineClient", isOfflineClient.ToString());
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
                    options.Headers.Add("ClientId", clientId);
                    options.Headers.Add("ClientGroupKey", clientGroupKey);
                    options.Headers.Add("IsOfflineClient", isOfflineClient.ToString());
                })
               .Build();
            }

            hub.Reconnecting += Reconnecting;
            hub.Closed += HubConn_Closed;
            hub.Reconnected += HubConn_Reconnected;

            hub.On<string>("ReceiveData", (m) => ReceiveData?.Invoke(m));
            hub.On<string>("ReceiveDataError", (m) => ReceiveDataError?.Invoke(m));
            hub.On<string>("ClusterLoadBalancingEndpointReceive", (m) => ClusterLoadBalancingEndpointReceive(m));
            hub.On<bool>("ReceiveServerActive", (m) => ReceiveServerActiveAction(m));

            return hub;
        }


        private void ReceiveServerActiveAction(bool value)
        {
            isServerActive = value;
            ReceiveServerActive?.Invoke(value);
        }
        private Task Reconnecting(Exception arg)
        {
            //if (hubConn.State == HubConnectionState.Connected)
            Task.Run(() => ConnectState?.Invoke(true));

            return Task.CompletedTask;
        }
        private Task HubConn_Reconnected(string arg)
        {
            return Task.CompletedTask;
        }
        #endregion

        #region MESSAGE PUSH
        //public async Task<T> PushData<T>(int pushType, string message) where T : IPushDataResponseModelBase, new()
        //{
        //    try
        //    {
        //        return await hubConn.InvokeAsync<T>("PushData", pushType, message);
        //    }
        //    catch (Exception)
        //    {
        //        return new T { IsOnline = false, IsSuccess = false };
        //    }
        //}
        public async Task<JobDataAddResponseModel> JobDataAdd(string data)
        {
            try
            {
                return await hubConn.InvokeAsync<JobDataAddResponseModel>("JobDataAdd", data);
            }
            catch (Exception)
            {
                return new JobDataAddResponseModel { IsOnline = false, IsSuccess = false };
            }
        }
        public async Task<ResponseBaseModel> JobMessageStarted(string data)
        {
            try
            {
                return await hubConn.InvokeAsync<ResponseBaseModel>("JobMessageStarted", data);
            }
            catch (Exception)
            {
                return new ResponseBaseModel { IsOnline = false, IsSuccess = false };
            }
        }
        public async Task<ResponseBaseModel> JobMessageCompleted(string data)
        {
            try
            {
                return await hubConn.InvokeAsync<ResponseBaseModel>("JobMessageCompleted", data);
            }
            catch (Exception)
            {
                return new ResponseBaseModel { IsOnline = false, IsSuccess = false };
            }
        }
        public async Task<JobDataGetResponseModel> JobDataGet(string data)
        {
            try
            {
                return await hubConn.InvokeAsync<JobDataGetResponseModel>("JobDataGet", data);
            }
            catch (Exception)
            {
                return new JobDataGetResponseModel { IsOnline = false, IsSuccess = false };
            }
        }
        public async Task<DeclareConsumeResponseModel> DeclareConsume(string data)
        {
            try
            {
                return await hubConn.InvokeAsync<DeclareConsumeResponseModel>("DeclareConsume", data);
            }
            catch (Exception)
            {
                return new DeclareConsumeResponseModel { IsOnline = false, IsSuccess = false };
            }
        }
        public async Task<EventSubscribeResponseModel> EventSubscribe(string data)
        {
            try
            {
                return await hubConn.InvokeAsync<EventSubscribeResponseModel>("EventSubscribe", data);
            }
            catch (Exception)
            {
                return new EventSubscribeResponseModel { IsOnline = false, IsSuccess = false };
            }
        }
        public async Task<ResponseBaseModel> DataProtection(string data)
        {
            try
            {
                return await hubConn.InvokeAsync<ResponseBaseModel>("DataProtection", data);
            }
            catch (Exception)
            {
                return new ResponseBaseModel { IsOnline = false, IsSuccess = false };
            }
        }
        #endregion

        #region MESSAGE RECEIVE
        public event Action<string> ReceiveData;
        public event Action<string> ReceiveDataError;
        private void ClusterLoadBalancingEndpointReceive(string msg)
        {
            var endpointModel = JsonConvert.DeserializeObject<ServerEndpointModel>(msg);
            roundRobin.Add(endpointModel, 1);
        }
        #endregion

        #region IS SERVER ACTIVE
        public event Action<bool> ReceiveServerActive;
        #endregion

        #region STATE
        public event Action<bool> ConnectState;
        #endregion

        #region DISPOSE / DISCONNECT
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
        public void Disconnect() => Dispose();
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //isConnect = false;
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                disposedValue = true;
            }
        }

        // // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ConnectionSocket()
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

        #region ROUNDROBIN
        private IRoundRobin<ServerEndpointModel> roundRobin;
        #endregion
    }
}

