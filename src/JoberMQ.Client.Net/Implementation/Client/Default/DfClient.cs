using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Configuration;
using JoberMQ.Client.Net.Abstraction.Connect;
using JoberMQ.Client.Net.Helpers;
using JoberMQ.Client.Net.Operations;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Data;
using JoberMQ.Common.Enums.Declare;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Enums.Timing;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Builder;
using JoberMQ.Common.Models.Completed;
using JoberMQ.Common.Models.DataProtection;
using JoberMQ.Common.Models.Declare;
using JoberMQ.Common.Models.Option;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.Models.Started;
using JoberMQ.Library.Database.Enums;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using TimerFramework;
using JoberMQ.Common.Enums.Publisher;

namespace JoberMQ.Client.Net.Implementation.Client.Default
{
    internal class DfClient : IClient
    {
        #region PROPERTY HELPER
        private ConcurrentDictionary<string, DeclareConsumeModel> ConsumeDatas = new ConcurrentDictionary<string, DeclareConsumeModel>();
        //private ConcurrentQueue<DataProtectionModel> DataProtections = new ConcurrentQueue<DataProtectionModel>();
        private DataProtectionModel[] DataProtections = new DataProtectionModel[100];
        private int protectionCounter = 0;
        private readonly IConnection connection;
        private readonly bool isOfflineMode;
        private readonly ITimer offlineTimer;
        private readonly string clientKey;
        private readonly string clientGroupKey;
        private readonly bool textMessageReceiveAutoCompleted;
        private bool isClientActive = false;
        private bool isServerActive = false;
        #endregion

        #region PROPERTY
        public string ClientKey => clientKey;
        public string ClientGroupKey => clientGroupKey;

        public IConnection Connection => connection;
        public bool IsClientActive => isClientActive;
        public bool IsServerActive => isServerActive;
        #endregion

        #region CONSRUCTOR
        internal DfClient(IConfiguration config, IConnection connection)
        {
            this.clientKey = config.ClientKey;
            this.clientGroupKey = config.ClientGroupKey;
            this.connection = connection;
            this.isOfflineMode = config.IsOfflineMode;
            this.textMessageReceiveAutoCompleted = config.TextMessageReceiveAutoCompleted;
            this.connection.ReceiveData += ReceiveDataAction;
            this.connection.ReceiveDataError += ReceiveDataActionError;
            this.connection.ConnectState += _connection_IsConnectState;
            this.connection.ReceiveServerActive += Connection_ReceiveServerActive;



            //_connection.ReceiveData += async (v) =>
            //{
            //    ReceiveDataAction(v);
            //};


            if (this.isOfflineMode)
            {
                offlineTimer = new TimerFactory().CreateTimer();
                offlineTimer.Receive += OfflineTimer_Receive;
                //Task.Factory.StartNew(() => OfflineMode());
                OfflineMode();
            }
        }

        private void Connection_ReceiveServerActive(bool obj)
        {
            isServerActive = obj;

            #region MyRegion
            //if (obj)
            //{
            //    foreach (var item in ConsumeDatas)
            //    {
            //        var result = DeclareConsumeAsync(item.Value.DeclareConsumeType, item.Value.QueueName, item.Value.Key).Result;
            //    }

            //    foreach (var item in DataProtections)
            //    {
            //        try
            //        {
            //            DataProtectionsCounter++;

            //            var serialize = JsonConvert.SerializeObject(item);

            //            var result = connection.PushData<PushDataResponseJobAddModel>(((int)PushDataTypeEnum.DataProtection), serialize).Result;
            //            if (result.IsOnline == true && result.IsSuccess == true)
            //            {
            //                DataProtections.TryDequeue(out var aaaaaaa);
            //            }
            //        }
            //        catch (Exception)
            //        {
            //            DataProtectionsCounterError++;
            //        }
            //    }

            //    if (DataProtections.Length == 0)
            //    {
            //        isClientActive = true;
            //    }
            //} 
            #endregion


            if (obj)
            {
                foreach (var item in ConsumeDatas)
                {
                    var result = DeclareConsumeAsync(item.Value.DeclareConsumeType, item.Value.QueueKey, item.Value.Key).Result;
                }

                for (int i = 0; i < DataProtections.Length; i++)
                {
                    try
                    {
                        var row = DataProtections[i];
                        if (row != null)
                        {
                            var serialize = JsonConvert.SerializeObject(row);
                            var result = connection.DataProtection(serialize).Result;

                            if (result.IsOnline == true && result.IsSuccess == true)
                                row = null;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            isClientActive = true;
        }

        //int DataProtectionsCounter = 1;
        //int DataProtectionsCounterError = 1;



        private void OfflineTimer_Receive(TimerModel obj)
        {
            LocalDataPush();
        }
        #endregion

        #region RECONNECTED CONSUME DATA PUSH and 
        private void _connection_IsConnectState(bool obj)
        {
            if (obj == true)
            {
                //BURADAN TAŞIDIM  _connection_ReceiveServerActive  YAPISINA
                //foreach (var item in ConsumeDatas)
                //{
                //    var result = DeclareConsumeAsync(item.Value.DeclareConsumeType, item.Value.QueueName, item.Value.Key).Result;
                //}

                //foreach (var item in DataProtections)
                //{
                //    try
                //    {
                //        var serialize = JsonConvert.SerializeObject(item);

                //        var result = _connection.PushData<PushDataResponseJobAddModel>(((int)PushDataTypeEnum.DataProtection), serialize).Result;
                //        if (result.IsOnline == true && result.IsSuccess == true)
                //            DataProtections.TryDequeue(out var aaaaaaa);
                //    }
                //    catch (Exception)
                //    {

                //    }
                //}

                //if (DataProtections.Count == 0)
                //{
                //    isClientActive = true;
                //    Console.WriteLine("isClientActive : " + isClientActive);
                //}

            }

        }
        #endregion

        #region OFFLINE
        public bool IsOfflineMode => isOfflineMode;
        void OfflineMode()
        {
            if (OfflineOperation.IsOfflineModeStarted == false)
            {
                var aaaa = OfflineOperation.DatabaseCreate();
                var bbbb = OfflineOperation.DependencyCreate();

                OfflineOperation.IsOfflineModeStarted = true;
            }

            var timer = new TimerModel();
            timer.Id = Guid.NewGuid();
            timer.CronTime = "15 * * ? * *";
            timer.TimerGroup = "ClientLocalDataPush";
            offlineTimer.Add(timer);
        }

        private async void LocalDataPush()
        {
            if (OfflineOperation.IsOfflineModeRuning == true)
                return;

            OfflineOperation.IsOfflineModeRuning = true;

            if (connection.IsConnect == true && IsServerActive == true && isClientActive == true)
            {
                var localDatas = await OfflineOperation.GetLocalDatas();

                if (localDatas != null)
                {
                    int checkCounter = 1;
                    int checkCount = 10;
                    foreach (var localData in localDatas.OrderBy(o => o.ProcessTime))
                    {
                        if (checkCount != checkCounter)
                        {
                            var deserialize = JsonConvert.DeserializeObject<JobDbo>(localData.Data);
                            var checkJob = await JobGetAsync(deserialize.Id);
                            //if (checkJob.JobData != null)
                            if (checkJob.IsSuccess == true)
                                await OfflineOperation.RemoveLocalData(localData);
                            else
                            {
                                var result = await JobAddAsyncOffline(localData.Data);
                                if (result.IsSuccess == true)
                                    await OfflineOperation.RemoveLocalData(localData);
                                else
                                {
                                    OfflineOperation.IsOfflineModeRuning = false;
                                    OfflineOperation.IsOfflineModeData = true;
                                    return;
                                }
                            }

                            checkCounter++;
                        }
                        else
                        {
                            var result = await JobAddAsyncOffline(localData.Data);
                            if (result.IsSuccess == true)
                                await OfflineOperation.RemoveLocalData(localData);
                            else
                            {
                                OfflineOperation.IsOfflineModeRuning = false;
                                OfflineOperation.IsOfflineModeData = true;
                                return;
                            }
                        }
                    }
                }
            }

            //todo YUKARIDA  JobAddAsyncOffline  İÇİN İŞLEM YAPMIŞIM AMA strarted, completed gibi LOCAL olarak kaydedilen işlemler içinde yapmalıyım

            OfflineOperation.IsOfflineModeRuning = false;
            OfflineOperation.IsOfflineModeData = false;
        }
        #endregion

        #region PRODUCER
        public JobBuilderMessageDataModel MessageData(OptionModel option = null)
        {
            #region JOB BUILDER DATA
            var jobTimingType = TimingTypeEnum.Now;
            var jobRunType = PublisherTypeEnum.Standart;
            var ErrorMessageRoutingType = RoutingTypeEnum.None;

            var builder = new JobBuilderMessageDataModel();
            builder.JobBuilder.TimingType = jobTimingType;
            builder.JobBuilder.PublisherType = jobRunType;
            builder.JobBuilder.Option = option;
            builder.JobBuilder.ErrorMessageRoutingType = ErrorMessageRoutingType;
            #endregion

            return builder;
        }
        public async Task<JobDataAddResponseModel> Publish(JobBuilderModel JobBuilder)
        {
            JobBuilder.ProducerClientKey = this.ClientKey;
            JobBuilder.ProducerClientGroupKey = this.ClientGroupKey;

            if (JobBuilder.TimingType != TimingTypeEnum.Trigger)
            {
                var dbo = DboCreateOperation.JobDataDboCreate(JobBuilder);
                dbo.DataStatusType = DataStatusTypeEnum.None;

                var jobDataSerialize = JsonConvert.SerializeObject(dbo);
                return await JobAddAsync(dbo.Id, jobDataSerialize);
            }
            else
            {
                var triggerJobInfo = await JobGetAsync(JobBuilder.TriggerJobId.Value);
                if (triggerJobInfo.IsSuccess == false)
                    return new JobDataAddResponseModel
                    {
                        IsOnline = triggerJobInfo.IsOnline,
                        IsSuccess = triggerJobInfo.IsSuccess,
                        Message = triggerJobInfo.Message
                    };
                else
                {
                    var dbo = DboCreateOperation.JobDataDboCreate(JobBuilder);
                    dbo.DataStatusType = DataStatusTypeEnum.None;

                    var jobDataSerialize = JsonConvert.SerializeObject(dbo);
                    return await JobAddAsync(dbo.Id, jobDataSerialize);
                }
            }
        }
        #endregion

        #region CONSUMER
        public DeclareConsumeModel DeclareConsume()
            => new DeclareConsumeModel();

        // DECLARE CONSUME
        public async Task<ResponseBaseModel> DeclareConsumeSpecialAddAsync() => await DeclareConsumeAsync(DeclareConsumeTypeEnum.SpecialAdd);
        public async Task<ResponseBaseModel> DeclareConsumeSpecialRemoveAsync() => await DeclareConsumeAsync(DeclareConsumeTypeEnum.SpecialRemove);
        public async Task<ResponseBaseModel> DeclareConsumeGroupAddAsync() => await DeclareConsumeAsync(DeclareConsumeTypeEnum.GroupAdd);
        public async Task<ResponseBaseModel> DeclareConsumeGroupRemoveAsync() => await DeclareConsumeAsync(DeclareConsumeTypeEnum.GroupRemove);
        public async Task<ResponseBaseModel> DeclareConsumeQueueKeyAddAsync(string queueName, string key) => await DeclareConsumeAsync(DeclareConsumeTypeEnum.QueueAdd, queueName, key);
        public async Task<ResponseBaseModel> DeclareConsumeQueueKeyRemoveAsync(string queueName, string key) => await DeclareConsumeAsync(DeclareConsumeTypeEnum.QueueRemove, queueName, key);


        // DECLARE CONSUME ERROR
        public async Task<ResponseBaseModel> DeclareConsumeErrorSpecialAddAsync() => await DeclareConsumeAsync(DeclareConsumeTypeEnum.ErrorSpecialAdd);
        public async Task<ResponseBaseModel> DeclareConsumeErrorSpecialRemoveAsync() => await DeclareConsumeAsync(DeclareConsumeTypeEnum.ErrorSpecialRemove);
        public async Task<ResponseBaseModel> DeclareConsumeErrorGroupAddAsync() => await DeclareConsumeAsync(DeclareConsumeTypeEnum.ErrorGroupAdd);
        public async Task<ResponseBaseModel> DeclareConsumeErrorGroupRemoveAsync() => await DeclareConsumeAsync(DeclareConsumeTypeEnum.ErrorGroupRemove);
        public async Task<ResponseBaseModel> DeclareConsumeErrorQueueKeyAddAsync(string queueName, string key) => await DeclareConsumeAsync(DeclareConsumeTypeEnum.ErrorQueueAdd, queueName, key);
        public async Task<ResponseBaseModel> DeclareConsumeErrorQueueKeyRemoveAsync(string queueName, string key) => await DeclareConsumeAsync(DeclareConsumeTypeEnum.ErrorQueueRemove, queueName, key);



        // EVENT SUB
        public async Task<ResponseBaseModel> EventSubscribeClientAsync(string eventName) => await EventSubAsync(DeclareEventTypeEnum.EventSubscribeClient, eventName);
        public async Task<ResponseBaseModel> EventUnSubscribeClientAsync(string eventName) => await EventSubAsync(DeclareEventTypeEnum.EventUnSubscribeClient, eventName);
        public async Task<ResponseBaseModel> EventSubscribeClientGroupKeyAsync(string eventName) => await EventSubAsync(DeclareEventTypeEnum.EventSubscribeClientGroupKey, eventName);
        public async Task<ResponseBaseModel> EventUnSubscribeClientGroupKeyAsync(string eventName) => await EventSubAsync(DeclareEventTypeEnum.EventUnSubscribeClientGroupKey, eventName);



        // RECEIVE MESSAGE
        public event Action<string> MessageReceive;
        public event Action<ErrorMessageDbo> MessageReceiveError;
        public bool TextMessageReceiveAutoCompleted => textMessageReceiveAutoCompleted;

        private void ReceiveDataAction(string obj)
        {
            if (String.IsNullOrEmpty(obj))
                return;

            var consumerMessage = JsonConvert.DeserializeObject<MessageDbo>(obj);

            _ = Task.Run(() =>
            {
                #region STARTED
                var jobStartedModel = new JobStartedModel();
                jobStartedModel.JobMessageId = consumerMessage.Id;
                jobStartedModel.RoutingType = consumerMessage.RoutingType;
                _ = MessageStartedAsync(JsonConvert.SerializeObject(jobStartedModel));
                #endregion

                if (consumerMessage.JobMessageType == MessageTypeEnum.Method)
                {
                    var returnData = MethodHelper.MethodRun(consumerMessage.Message).Result;

                    var jobCompleted = new JobCompletedModel();
                    jobCompleted.JobMessageId = consumerMessage.Id;

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
                    jobCompleted.RoutingType = consumerMessage.RoutingType;

                    _ = MessageCompletedAsync(JsonConvert.SerializeObject(jobCompleted));
                }
                else if (consumerMessage.JobMessageType == MessageTypeEnum.Text)
                {
                    MessageReceive?.Invoke(consumerMessage.Message);
                    if (this.TextMessageReceiveAutoCompleted == true)
                    {
                        var jobCompleted = new JobCompletedModel();
                        jobCompleted.JobMessageId = consumerMessage.Id;
                        jobCompleted.IsError = false;
                        jobCompleted.Message = "";

                        _ = MessageCompletedAsync(JsonConvert.SerializeObject(jobCompleted));
                    }
                }
            });
        }
        private void ReceiveDataActionError(string obj)
        {
            var deserialize = JsonConvert.DeserializeObject<ErrorMessageDbo>(obj);
            MessageReceiveError?.Invoke(deserialize);
        }
        #endregion

        #region DISPOSE
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    connection.Disconnect();
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

        #region HELPER
        private async Task<JobDataAddResponseModel> JobAddAsync(Guid? jobId, string data)
        {
            var result = new JobDataAddResponseModel();

            if (connection.IsConnect == true && IsServerActive == true)
                //result = await connection.PushData<PushDataResponseJobAddModel>(((int)PushDataTypeEnum.JobAdd), data);
                result = await connection.JobDataAdd(data);

            if (result.IsSuccess)
            {
                DataProtectionAdd(PushDataTypeEnum.JobAdd, data);
                return result;
            }

            if (IsOfflineMode)
            {
                var resultLocalData = await OfflineOperation.AddLocalData(PushDataTypeEnum.JobAdd, jobId, data);
                result = new JobDataAddResponseModel
                {
                    //IsServerActive = resultLocalData.IsServerActive,
                    IsOnline = resultLocalData.IsOnline,
                    IsSuccess = resultLocalData.IsSuccess,
                    Message = resultLocalData.Message,
                    JobId = resultLocalData.JobId
                };
            }

            if (result.IsSuccess)
                return result;

            result.IsSuccess = false;
            return result;
        }
        private async Task<ResponseBaseModel> EventSubAsync(DeclareEventTypeEnum declareEventType, string eventName)
        {
            var declareEventModel = new DeclareEventModel();
            declareEventModel.DeclareEventType = declareEventType;
            declareEventModel.EventName = eventName;

            var serialize = JsonConvert.SerializeObject(declareEventModel);

            //var result = await _connection.PushData<PushDataResponseModelBase>(((int)PushDataTypeEnum.EventSub), serialize);
            //return result.IsSuccess;



            var result = new ResponseBaseModel();

            if (connection.IsConnect)
            {
                //result = await connection.PushData<PushDataResponseEventSubModel>(((int)PushDataTypeEnum.EventSub), serialize);
                result = await connection.EventSubscribe(serialize);
                if (result.IsSuccess == false)
                {
                    var resultLocalData = await OfflineOperation.AddLocalData(PushDataTypeEnum.EventSub, null, serialize);
                    result = new ResponseBaseModel
                    {
                        IsOnline = resultLocalData.IsOnline,
                        IsSuccess = resultLocalData.IsSuccess,
                        Message = resultLocalData.Message
                    };
                }
                else
                {
                    DataProtectionAdd(PushDataTypeEnum.EventSub, serialize);
                }
            }
            else if (IsOfflineMode)
            {
                var resultLocalData = await OfflineOperation.AddLocalData(PushDataTypeEnum.EventSub, null, serialize);
                result = new ResponseBaseModel
                {
                    IsOnline = resultLocalData.IsOnline,
                    IsSuccess = resultLocalData.IsSuccess,
                    Message = resultLocalData.Message
                };
            }
            else
            {
                result.IsOnline = false;
                result.IsSuccess = false;
            }

            return result;
        }
        private async Task<ResponseBaseModel> DeclareConsumeAsync(DeclareConsumeTypeEnum declareConsumeType, string queueName = null, string key = null)
        {
            switch (declareConsumeType)
            {
                case DeclareConsumeTypeEnum.SpecialAdd:
                    ConsumeDatas.TryAdd(declareConsumeType.ToString(), new DeclareConsumeModel { DeclareConsumeType = declareConsumeType });
                    break;
                case DeclareConsumeTypeEnum.SpecialRemove:
                    ConsumeDatas.TryRemove(DeclareConsumeTypeEnum.SpecialAdd.ToString(), out var xxxxx);
                    break;
                case DeclareConsumeTypeEnum.GroupAdd:
                    ConsumeDatas.TryAdd(declareConsumeType.ToString(), new DeclareConsumeModel { DeclareConsumeType = declareConsumeType });
                    break;
                case DeclareConsumeTypeEnum.GroupRemove:
                    ConsumeDatas.TryRemove(DeclareConsumeTypeEnum.GroupAdd.ToString(), out var yyyyy);
                    break;
                case DeclareConsumeTypeEnum.QueueAdd:
                    ConsumeDatas.TryAdd(declareConsumeType.ToString(), new DeclareConsumeModel { DeclareConsumeType = declareConsumeType, QueueKey = queueName, Key = key });
                    break;
                case DeclareConsumeTypeEnum.QueueRemove:
                    ConsumeDatas.TryRemove(ConsumeDatas.FirstOrDefault(x => x.Value.QueueKey == queueName && x.Value.Key == key));
                    break;


                case DeclareConsumeTypeEnum.ErrorSpecialAdd:
                    ConsumeDatas.TryAdd(declareConsumeType.ToString(), new DeclareConsumeModel { DeclareConsumeType = declareConsumeType });
                    break;
                case DeclareConsumeTypeEnum.ErrorSpecialRemove:
                    ConsumeDatas.TryRemove(DeclareConsumeTypeEnum.SpecialAdd.ToString(), out var aaaa);
                    break;
                case DeclareConsumeTypeEnum.ErrorGroupAdd:
                    ConsumeDatas.TryAdd(declareConsumeType.ToString(), new DeclareConsumeModel { DeclareConsumeType = declareConsumeType });
                    break;
                case DeclareConsumeTypeEnum.ErrorGroupRemove:
                    ConsumeDatas.TryRemove(DeclareConsumeTypeEnum.GroupAdd.ToString(), out var bbbb);
                    break;
                case DeclareConsumeTypeEnum.ErrorQueueAdd:
                    ConsumeDatas.TryAdd(declareConsumeType.ToString(), new DeclareConsumeModel { DeclareConsumeType = declareConsumeType, QueueKey = queueName, Key = key });
                    break;
                case DeclareConsumeTypeEnum.ErrorQueueRemove:
                    ConsumeDatas.TryRemove(ConsumeDatas.FirstOrDefault(x => x.Value.QueueKey == queueName && x.Value.Key == key));
                    break;
            }

            var declareConsumeModel = new DeclareConsumeModel();
            declareConsumeModel.DeclareConsumeType = declareConsumeType;
            declareConsumeModel.QueueKey = queueName;
            declareConsumeModel.Key = key;

            var serialize = JsonConvert.SerializeObject(declareConsumeModel);

            //var result = await _connection.PushData<PushDataResponseModelBase>(((int)PushDataTypeEnum.DeclareConsume), serialize);
            //return result.IsSuccess;



            var result = new ResponseBaseModel();

            if (connection.IsConnect)
            {
                result = await connection.DeclareConsume(serialize);
                if (result.IsSuccess == false)
                {
                    var resultLocalData = await OfflineOperation.AddLocalData(PushDataTypeEnum.DeclareConsume, null, serialize);
                    result = new ResponseBaseModel
                    {
                        IsOnline = resultLocalData.IsOnline,
                        IsSuccess = resultLocalData.IsSuccess,
                        Message = resultLocalData.Message
                    };
                }
                //else
                //{
                //    DataProtectionAdd(PushDataTypeEnum.DeclareConsume, serialize);
                //}
            }
            else if (IsOfflineMode)
            {
                var resultLocalData = await OfflineOperation.AddLocalData(PushDataTypeEnum.DeclareConsume, null, serialize);
                result = new ResponseBaseModel
                {
                    IsOnline = resultLocalData.IsOnline,
                    IsSuccess = resultLocalData.IsSuccess,
                    Message = resultLocalData.Message
                };
            }
            else
            {
                result.IsOnline = false;
                result.IsSuccess = false;
            }

            return result;
        }
        private async Task<JobDataAddResponseModel> JobAddAsyncOffline(string data)
        {
            if (connection.IsConnect == true && IsServerActive == true)
            {
                return await connection.JobDataAdd(data);
            }
            else
                return new JobDataAddResponseModel
                {
                    IsOnline = false,
                    IsSuccess = false,
                };
        }
        private async Task<ResponseBaseModel> JobGetAsync(Guid jobId)
        {
            if (connection.IsConnect == true)
                //return await connection.PushData<PushDataResponseJobGetModel>(((int)PushDataTypeEnum.JobDataGet), jobId.ToString());
                return await connection.JobDataGet(jobId.ToString());
            else
                return new ResponseBaseModel
                {
                    IsOnline = false,
                    IsSuccess = false,
                    Message = "You are not connected to the server. You cannot create a Trigger Job."
                };
        }
        private async Task<ResponseBaseModel> MessageStartedAsync(string data)
        {
            var result = new ResponseBaseModel();

            if (connection.IsConnect)
            {
                result = await connection.JobMessageStarted(data);
                if (result.IsSuccess == false)
                {
                    var resultLocalData = await OfflineOperation.AddLocalData(PushDataTypeEnum.MessageStarted, null, data);
                    result = new ResponseBaseModel
                    {
                        IsOnline = resultLocalData.IsOnline,
                        IsSuccess = resultLocalData.IsSuccess,
                        Message = resultLocalData.Message
                    };
                }
                else
                {
                    DataProtectionAdd(PushDataTypeEnum.MessageStarted, data);
                }
            }
            else if (IsOfflineMode)
            {
                var resultLocalData = await OfflineOperation.AddLocalData(PushDataTypeEnum.MessageStarted, null, data);
                result = new ResponseBaseModel
                {
                    IsOnline = resultLocalData.IsOnline,
                    IsSuccess = resultLocalData.IsSuccess,
                    Message = resultLocalData.Message
                };
            }
            else
            {
                result.IsOnline = false;
                result.IsSuccess = false;
            }

            return result;
        }
        private async Task<ResponseBaseModel> MessageCompletedAsync(string data)
        {
            var result = new ResponseBaseModel();

            if (connection.IsConnect)
            {
                result = await connection.JobMessageCompleted(data);
                if (result.IsSuccess == false)
                {
                    var resultLocalData = await OfflineOperation.AddLocalData(PushDataTypeEnum.MessageCompleted, null, data);
                    result = new ResponseBaseModel
                    {
                        IsOnline = resultLocalData.IsOnline,
                        IsSuccess = resultLocalData.IsSuccess,
                        Message = resultLocalData.Message
                    };
                }
                else
                {
                    DataProtectionAdd(PushDataTypeEnum.MessageCompleted, data);
                }
            }
            else if (IsOfflineMode)
            {
                var resultLocalData = await OfflineOperation.AddLocalData(PushDataTypeEnum.MessageCompleted, null, data);
                result = new ResponseBaseModel
                {
                    IsOnline = resultLocalData.IsOnline,
                    IsSuccess = resultLocalData.IsSuccess,
                    Message = resultLocalData.Message
                };
            }
            else
            {
                result.IsOnline = false;
                result.IsSuccess = false;
            }

            return result;
        }

        private void DataProtectionAdd(PushDataTypeEnum pushDataType, string data)
        {
            if (protectionCounter == 100)
                protectionCounter = 0;

            var dataProtection = new DataProtectionModel();
            dataProtection.PushDataType = pushDataType;
            dataProtection.Data = data;

            DataProtections[protectionCounter] = dataProtection;

            protectionCounter++;





            //if (DataProtections.Count > 100)
            //    DataProtections.TryDequeue(out var aaaaa);

            //var dataProtection = new DataProtectionModel();
            //dataProtection.PushDataType = pushDataType;
            //dataProtection.Data = data;
            //DataProtections.Enqueue(dataProtection);
        }
        #endregion
    }
}
