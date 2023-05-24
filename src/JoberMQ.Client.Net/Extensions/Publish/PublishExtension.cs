using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Constants;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Consume;
using JoberMQ.Library.Models.Consume;
using JoberMQ.Library.Models.Distributor;
using JoberMQ.Library.Models.Queue;
using JoberMQ.Library.Models.Response;
using JoberMQ.Library.Models.Rpc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Extensions.Publish
{
    public static class PublishExtension
    {
        public static async Task<ResponseModel> PublishAsync(this IClient client, DistributorModel distributor)
        {
            var serialize = JsonConvert.SerializeObject(distributor);

            if (client.Connect.IsConnect)
                return await client.Connect.InvokeAsync<ResponseModel>("Distributor", serialize);
            else
                return new ResponseModel { IsOnline = false, IsSucces = false, Message = "Distributor operation error" };
        }

        public static async Task<ResponseModel> PublishAsync(this IClient client, QueueModel queue)
        {
            var serialize = JsonConvert.SerializeObject(queue);

            if (client.Connect.IsConnect)
                return await client.Connect.InvokeAsync<ResponseModel>("Queue", serialize);
            else
                return new ResponseModel { IsOnline = false, IsSucces = false, Message = "Queue operation error" };
        }

        public static async Task<ResponseModel> PublishAsync(this IClient client, ConsumeTransportModel consumeTransport)
        {
            var result = new ResponseModel();
            result.IsOnline = false;
            result.IsSucces = false;

            var consumeRequest = new ConsumeRequestModel();
            var consume = new ConsumeModel();
            bool isOperation = false;

            //switch (consumeTransport.ConsumeOperationType)
            //{
            //    case ConsumeOperationTypeEnum.SpecialAdd:
            //        var specialAdd = client.Consuming.Where(x => x.Value.ConsumeType == ConsumeTypeEnum.Special);
            //        if (specialAdd == null || specialAdd.Count() == 0)
            //        {
            //            consume.ConsumeType = ConsumeTypeEnum.Special;
            //            consume.DeclareKey = consumeTransport.DeclareKey;
            //            client.Consuming.TryAdd(Guid.NewGuid(), consume);
            //            isOperation = true;

            //            #region ConsumeRequest
            //            consumeRequest.ConsumeOperationType = ConsumeOperationTypeEnum.SpecialAdd;
            //            consumeRequest.ConsumeType = ConsumeTypeEnum.Special;
            //            consumeRequest.DeclareKey = consumeTransport.DeclareKey;
            //            #endregion
            //        }
            //        break;
            //    case ConsumeOperationTypeEnum.SpecialRemove:
            //        var specialRemove = client.Consuming.Where(x => x.Value.ConsumeType == ConsumeTypeEnum.Special);
            //        if (specialRemove != null || specialRemove.Count() == 0)
            //        {
            //            var specialValue = client.Consuming.FirstOrDefault(x => x.Value.ConsumeType == ConsumeTypeEnum.Special);
            //            client.Consuming.TryRemove(specialValue.Key, out var xxxx);
            //            isOperation = true;

            //            #region ConsumeRequest
            //            consumeRequest.ConsumeOperationType = ConsumeOperationTypeEnum.SpecialRemove;
            //            consumeRequest.ConsumeType = ConsumeTypeEnum.Special;
            //            consumeRequest.DeclareKey = consumeTransport.DeclareKey;
            //            #endregion
            //        }
            //        break;
            //    case ConsumeOperationTypeEnum.GroupAdd:
            //        var groupAdd = client.Consuming.Where(x => x.Value.ConsumeType == ConsumeTypeEnum.Group);
            //        if (groupAdd == null || groupAdd.Count() == 0)
            //        {
            //            consume.ConsumeType = ConsumeTypeEnum.Group;
            //            client.Consuming.TryAdd(Guid.NewGuid(), consume);
            //            isOperation = true;

            //            #region ConsumeRequest
            //            consumeRequest.ConsumeOperationType = ConsumeOperationTypeEnum.GroupAdd;
            //            consumeRequest.ConsumeType = ConsumeTypeEnum.Group;
            //            consumeRequest.DeclareKey = consumeTransport.DeclareKey;
            //            #endregion
            //        }
            //        break;
            //    case ConsumeOperationTypeEnum.GroupRemove:
            //        var groupRemove = client.Consuming.Where(x => x.Value.ConsumeType == ConsumeTypeEnum.Group);
            //        if (groupRemove != null || groupRemove.Count() == 0)
            //        {
            //            var groupValue = client.Consuming.FirstOrDefault(x => x.Value.ConsumeType == ConsumeTypeEnum.Group);
            //            client.Consuming.TryRemove(groupValue.Key, out var yyyy);
            //            isOperation = true;

            //            #region ConsumeRequest
            //            consumeRequest.ConsumeOperationType = ConsumeOperationTypeEnum.GroupRemove;
            //            consumeRequest.ConsumeType = ConsumeTypeEnum.Group;
            //            consumeRequest.DeclareKey = consumeTransport.DeclareKey;
            //            #endregion
            //        }
            //        break;
            //    case ConsumeOperationTypeEnum.QueueAdd:
            //        var queueAdd = client.Consuming.Where(x => x.Value.ConsumeType == ConsumeTypeEnum.Queue && x.Value.DeclareKey == consumeTransport.DeclareKey);
            //        if (queueAdd == null || queueAdd.Count() == 0)
            //        {
            //            consume.ConsumeType = ConsumeTypeEnum.Queue;
            //            consume.DeclareKey = consumeTransport.DeclareKey;
            //            client.Consuming.TryAdd(Guid.NewGuid(), consume);
            //            isOperation = true;

            //            #region ConsumeRequest
            //            consumeRequest.ConsumeOperationType = ConsumeOperationTypeEnum.QueueAdd;
            //            consumeRequest.ConsumeType = ConsumeTypeEnum.Queue;
            //            consumeRequest.DeclareKey = consumeTransport.DeclareKey;
            //            #endregion
            //        }
            //        break;
            //    case ConsumeOperationTypeEnum.QueueRemove:
            //        var queueRemove = client.Consuming.Where(x => x.Value.ConsumeType == ConsumeTypeEnum.Queue && x.Value.DeclareKey == consumeTransport.DeclareKey);
            //        if (queueRemove != null || queueRemove.Count() == 0)
            //        {
            //            var queueValue = client.Consuming.FirstOrDefault(x => x.Value.ConsumeType == ConsumeTypeEnum.Queue && x.Value.DeclareKey == consumeTransport.DeclareKey);
            //            client.Consuming.TryRemove(queueValue.Key, out var zzzz);
            //            isOperation = true;

            //            #region ConsumeRequest
            //            consumeRequest.ConsumeOperationType = ConsumeOperationTypeEnum.QueueRemove;
            //            consumeRequest.ConsumeType = ConsumeTypeEnum.Queue;
            //            consumeRequest.DeclareKey = consumeTransport.DeclareKey;
            //            #endregion
            //        }
            //        break;
            //    case ConsumeOperationTypeEnum.EventSubscript:
            //        var eventSubscript = client.Consuming.Where(x => x.Value.ConsumeType == ConsumeTypeEnum.Event && x.Value.DeclareKey == consumeTransport.DeclareKey);
            //        if (eventSubscript == null || eventSubscript.Count() == 0)
            //        {
            //            consume.ConsumeType = ConsumeTypeEnum.Event;
            //            consume.DeclareKey = consumeTransport.DeclareKey;
            //            client.Consuming.TryAdd(Guid.NewGuid(), consume);
            //            isOperation = true;

            //            #region ConsumeRequest
            //            consumeRequest.ConsumeOperationType = ConsumeOperationTypeEnum.EventSubscript;
            //            consumeRequest.ConsumeType = ConsumeTypeEnum.Event;
            //            consumeRequest.DeclareKey = consumeTransport.DeclareKey;
            //            #endregion
            //        }
            //        break;
            //    case ConsumeOperationTypeEnum.EventUnSubscript:
            //        var eventUnSubscript = client.Consuming.Where(x => x.Value.ConsumeType == ConsumeTypeEnum.Event && x.Value.DeclareKey == consumeTransport.DeclareKey);
            //        if (eventUnSubscript != null && eventUnSubscript.Count() != 0)
            //        {
            //            var eventValue = client.Consuming.FirstOrDefault(x => x.Value.ConsumeType == ConsumeTypeEnum.Event && x.Value.DeclareKey == consumeTransport.DeclareKey);
            //            client.Consuming.TryRemove(eventValue.Key, out var wwww);
            //            isOperation = true;

            //            #region ConsumeRequest
            //            consumeRequest.ConsumeOperationType = ConsumeOperationTypeEnum.EventUnSubscript;
            //            consumeRequest.ConsumeType = ConsumeTypeEnum.Event;
            //            consumeRequest.DeclareKey = consumeTransport.DeclareKey;
            //            #endregion
            //        }
            //        break;
            //}

            //if (isOperation == true)
            //{
            //    consumeRequest.Consuming = client.Consuming;
                var serialize = JsonConvert.SerializeObject(consume);
                return await client.Connect.InvokeAsync<ResponseModel>("Consume", serialize);
            //}
            //else
            //{
            //    return result;
            //}
        }

        public static async Task<ResponseModel> PublishAsync(this IClient client, JobDbo job)
            => await client.Connect.InvokeAsync<ResponseModel>("Job", JsonConvert.SerializeObject(job));
        public static async Task<ResponseModel> PublishAsync(this IClient client, MessageDbo message)
            => await client.Connect.InvokeAsync<ResponseModel>("Message", JsonConvert.SerializeObject(message));
        public static async Task<RpcResponseModel> PublishAsync(this IClient client, RpcRequestModel message)
            => await client.Connect.InvokeAsync<RpcResponseModel>("Rpc", JsonConvert.SerializeObject(message));

    }
}

