using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Dbos;
using JoberMQ.Client.Net.Enums.Operation;
using JoberMQ.Client.Net.Models.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Extensions
{
    public static class PublishExtension
    {
        public static async Task<bool> Publish(this IClient client, BuilderModel builder)
        {
            bool result = false;

            builder.Producer = client.Producer;
            
            var serialize = "";
            if (builder.Operation.OperationType ==  OperationTypeEnum.Job)
            {
                serialize = JsonConvert.SerializeObject(Job(builder, client));
                result = await client.HubConnection.InvokeAsync<bool>("Job", serialize);
            }
            else if (builder.Operation.OperationType == OperationTypeEnum.Message)
            {
                serialize = JsonConvert.SerializeObject(Message(builder, client));
                result = await client.HubConnection.InvokeAsync<bool>("Message", serialize);
            }
            else if (builder.Operation.OperationType == OperationTypeEnum.Rpc)
            {

            }

            return result;
        }

        private static JobDbo Job(BuilderModel builder, IClient client)
        {
            var jobDbo = new JobDbo();
            jobDbo.JobDetails = new List<JobDetailDbo>();

            jobDbo.Id = Guid.NewGuid();
            jobDbo.Operation = builder.Operation;
            jobDbo.Producer = builder.Producer;
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
        private static JobDbo Message(BuilderModel builder, IClient client)
            => Job(builder, client);
    }
}
