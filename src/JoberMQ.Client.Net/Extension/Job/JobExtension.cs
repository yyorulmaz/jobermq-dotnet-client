using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Operation;
using JoberMQ.Common.Enums.Publisher;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Enums.Timing;
using JoberMQ.Common.Models.Info;
using JoberMQ.Common.Models.Job;
using JoberMQ.Common.Models.Operation;
using JoberMQ.Common.Models.Producer;
using JoberMQ.Common.Models.Publisher;
using JoberMQ.Common.Models.Status;
using JoberMQ.Common.Models.Timing;
using System;
using System.Collections.Generic;

public static class JobExtension
{
    public static JobBuilderModel JobBuilder(this IClient client, string name = null, string description = null)
            => JobBuilderDefault(client.ClientInfo.ClientKey, name, description);
    private static JobBuilderModel JobBuilderDefault(string clientKey, string name, string description)
        => new JobBuilderModel
        {
            Job = new JobDbo
            {
                Id = Guid.NewGuid(),
                Operation = new OperationModel
                {
                    Version = 0,
                    OperationType = OperationTypeEnum.Job
                },
                Producer = new ProducerModel
                {
                    ClientKey = clientKey
                },
                Info = new InfoModel
                {
                    Name = name,
                    Description = description
                },
                Publisher = new PublisherModel
                {
                    PublisherType = PublisherTypeEnum.Standart
                },
                Timing = new TimingModel
                {
                    TimingType = TimingTypeEnum.Now
                },
                JobDetails = new List<JobDetailDbo>(),
                IsJobResultMessage = false,
                JobResultMessage = null,
                JobResultMessageConsuming = null,
                Status = new StatusModel
                {
                    IsCompleted = false,
                    IsError = false,
                    StatusTypeMessage = StatusTypeMessageEnum.None,
                    TempAgainDate = null
                }
            }
        };

}
