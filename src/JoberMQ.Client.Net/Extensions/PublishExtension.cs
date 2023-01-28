using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Dbos;
using JoberMQ.Client.Net.Enums.Declare;
using JoberMQ.Client.Net.Enums.Operation;
using JoberMQ.Client.Net.Models.Builder;
using JoberMQ.Client.Net.Models.DeclareConsume;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Extensions
{
    public static class PublishExtension
    {
        public static async Task<bool> Publish(this IClient client, JobBuilderModel builder)
        {
            bool result = false;

            builder.ClientInfo = client.ClientInfo;
            
            var serialize = "";
            if (builder.Operation.OperationType ==  OperationTypeEnum.Job)
            {
                serialize = JsonConvert.SerializeObject(Job(builder));
                result = await client.HubConnection.InvokeAsync<bool>("Job", serialize);
            }
            else if (builder.Operation.OperationType == OperationTypeEnum.Message)
            {
                serialize = JsonConvert.SerializeObject(Message(builder));
                result = await client.HubConnection.InvokeAsync<bool>("Message", serialize);
            }
            else if (builder.Operation.OperationType == OperationTypeEnum.Rpc)
            {
            }
            return result;
        }
        private static JobDbo Job(JobBuilderModel builder)
        {
            var jobDbo = new JobDbo();
            jobDbo.JobDetails = new List<JobDetailDbo>();

            jobDbo.Id = Guid.NewGuid();
            jobDbo.Operation = builder.Operation;
            jobDbo.ClientInfo = builder.ClientInfo;
            jobDbo.Info = builder.Info;
            jobDbo.Publisher = builder.Publisher;
            jobDbo.Timing = builder.Timing;
            jobDbo.IsResult = builder.IsResult;
            jobDbo.ResultMessage = builder.ResultMessage;

            //todo kontrol
            //jobDbo.Status = ;
            //jobDbo.Version = ;

            foreach (var item in builder.MultipleMessages)
            {
                jobDbo.JobDetails.Add(new JobDetailDbo
                {
                    Id = Guid.NewGuid(),
                    Message = item.Message,
                    IsResult = item.IsResult,
                    ResultMessage = item.ResultMessage
                });
            }

            return jobDbo;
        }
        private static MessageDbo Message(JobBuilderModel builder)
        {
            var messageDbo = new MessageDbo();

            messageDbo.Id = Guid.NewGuid();
            messageDbo.Operation = builder.Operation;
            messageDbo.ClientInfo = builder.ClientInfo;
            messageDbo.Message = builder.MultipleMessages.FirstOrDefault().Message;
            messageDbo.IsResult = builder.IsResult;
            messageDbo.ResultMessage = builder.MultipleMessages.FirstOrDefault().ResultMessage;
            messageDbo.TriggerGroupsId = builder.Timing.TriggerGroupsId;


            //toto kontrol
            //messageDbo.Consuming = ;


            return messageDbo;
        }



        public static async Task<bool> Publish(this IClient client, DeclareConsumeBuilderModel declareConsumeBuilder)
        {
            var declareConsume = new DeclareConsumeModel();
            bool isOperation = false;

            switch (declareConsumeBuilder.DeclareConsumeOperationType)
            {
                case DeclareConsumeOperationTypeEnum.SpecialAdd:
                    var specialAdd = client.DeclareConsuming.Where(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Special);
                    if (specialAdd == null || specialAdd.Count() == 0)
                    {
                        declareConsume.DeclareConsumeType = DeclareConsumeTypeEnum.Special;
                        client.DeclareConsuming.TryAdd(Guid.NewGuid(), declareConsume);
                        isOperation = true;
                    }
                    break;
                case DeclareConsumeOperationTypeEnum.SpecialRemove:
                    var specialRemove = client.DeclareConsuming.Where(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Special);
                    if (specialRemove != null || specialRemove.Count() == 0)
                    {
                        var specialValue = client.DeclareConsuming.FirstOrDefault(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Special);
                        client.DeclareConsuming.TryRemove(specialValue.Key, out var xxxx);
                        isOperation = true;
                    }
                    break;
                case DeclareConsumeOperationTypeEnum.GroupAdd:
                    var groupAdd = client.DeclareConsuming.Where(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Group);
                    if (groupAdd == null || groupAdd.Count() == 0)
                    {
                        declareConsume.DeclareConsumeType = DeclareConsumeTypeEnum.Group;
                        client.DeclareConsuming.TryAdd(Guid.NewGuid(), declareConsume);
                        isOperation = true;
                    }
                    break;
                case DeclareConsumeOperationTypeEnum.GroupRemove:
                    var groupRemove = client.DeclareConsuming.Where(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Group);
                    if (groupRemove != null || groupRemove.Count() == 0)
                    {
                        var groupValue = client.DeclareConsuming.FirstOrDefault(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Group);
                        client.DeclareConsuming.TryRemove(groupValue.Key, out var xxxx);
                        isOperation = true;
                    }
                    break;
                case DeclareConsumeOperationTypeEnum.QueueAdd:
                    var queueAdd = client.DeclareConsuming.Where(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Queue && x.Value.DeclareKey == declareConsumeBuilder.DeclareKey);
                    if (queueAdd == null || queueAdd.Count() == 0)
                    {
                        declareConsume.DeclareConsumeType = DeclareConsumeTypeEnum.Queue;
                        declareConsume.DeclareKey = declareConsumeBuilder.DeclareKey;
                        client.DeclareConsuming.TryAdd(Guid.NewGuid(), declareConsume);
                        isOperation = true;
                    }
                    break;
                case DeclareConsumeOperationTypeEnum.QueueRemove:
                    var queueRemove = client.DeclareConsuming.Where(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Queue && x.Value.DeclareKey == declareConsumeBuilder.DeclareKey);
                    if (queueRemove != null || queueRemove.Count() == 0)
                    {
                        var queueValue = client.DeclareConsuming.FirstOrDefault(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Queue && x.Value.DeclareKey == declareConsumeBuilder.DeclareKey);
                        client.DeclareConsuming.TryRemove(queueValue.Key, out var xxx);
                        isOperation = true;
                    }
                    break;
                case DeclareConsumeOperationTypeEnum.EventSubscript:
                    var eventSubscript = client.DeclareConsuming.Where(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Event && x.Value.DeclareKey == declareConsumeBuilder.DeclareKey);
                    if (eventSubscript == null || eventSubscript.Count() == 0)
                    {
                        declareConsume.DeclareConsumeType = DeclareConsumeTypeEnum.Event;
                        declareConsume.DeclareKey = declareConsumeBuilder.DeclareKey;
                        client.DeclareConsuming.TryAdd(Guid.NewGuid(), declareConsume);
                        isOperation = true;
                    }
                    break;
                case DeclareConsumeOperationTypeEnum.EventUnSubscript:
                    var eventUnSubscript = client.DeclareConsuming.Where(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Event && x.Value.DeclareKey == declareConsumeBuilder.DeclareKey);
                    if (eventUnSubscript != null || eventUnSubscript.Count() == 0)
                    {
                        var eventValue = client.DeclareConsuming.FirstOrDefault(x => x.Value.DeclareConsumeType == DeclareConsumeTypeEnum.Event && x.Value.DeclareKey == declareConsumeBuilder.DeclareKey);
                        client.DeclareConsuming.TryRemove(eventValue.Key, out var xxx);
                        isOperation = true;
                    }
                    break;
            }

            if (isOperation == true)
            {
                var serialize = JsonConvert.SerializeObject(client.DeclareConsuming);
                return await client.HubConnection.InvokeAsync<bool>("DeclareConsume", serialize);
            }
            else
            {
                return true;
            }
        }
    }
}
