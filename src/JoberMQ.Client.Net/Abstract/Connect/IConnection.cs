using JoberMQ.Client.Common.Models.Base;
using JoberMQ.Client.Common.Models.Login;
using JoberMQ.Client.Common.Models.Response;

namespace JoberMQ.Client.Net.Abstract.Connect
{
    public partial interface IConnection : IDisposable
    {
        #region PROPERTY
        public string Endpoint { get; }
        public string ClientId { get; }
        public string ClientGroupKey { get; }
        public string ConnectionId { get; }
        public bool IsConnect { get; }
        public bool IsServerActive { get; }
        //public bool IsNotConnectKeepTrying { get; }
        public bool AutomaticReconnect { get; }
        public int ConnectionRetryTimeout { get; }
        #endregion

        #region TOKEN
        public ResponseLoginModel GetToken(string endpoint);
        #endregion

        #region CONNECT
        public Task<bool> ConnectAsync();
        #endregion

        #region MESSAGE PUSH
        //public Task<T> PushData<T>(int pushType, string message) where T : IPushDataResponseModelBase, new();
        public Task<JobDataAddResponseModel> JobDataAdd(string data);
        public Task<ResponseBaseModel> JobMessageStarted(string data);
        public Task<ResponseBaseModel> JobMessageCompleted(string data);
        public Task<JobDataGetResponseModel> JobDataGet(string data);
        public Task<DeclareConsumeResponseModel> DeclareConsume(string data);
        public Task<EventSubscribeResponseModel> EventSubscribe(string data);
        public Task<ResponseBaseModel> DataProtection(string data);
        #endregion

        #region MESSAGE RECEIVE
        public event Action<string> ReceiveData;
        public event Action<string> ReceiveDataError;
        #endregion

        #region IS SERVER ACTIVE
        public event Action<bool> ReceiveServerActive;
        #endregion

        #region STATE
        public event Action<bool> ConnectState;
        #endregion

        #region Cluster LoadBalancing
        //public List<ServerEndpointModel> ServerEndpoints { get; }
        #endregion

        #region DISPOSE / DISCONNECT
        public void Disconnect();
        #endregion
    }
}
