using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Publisher;
using JoberMQ.Library.Models;
using JoberMQ.Library.Models.Job;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Client.Net.Extensions.Job
{
    public static class JobPublisherExtension
    {
        public static JobBuilderPublisherExtensionModel Publisher(this JobBuilderModel jobBuilder, PublisherTypeEnum publisherType = PublisherTypeEnum.Standart)
            => Add(jobBuilder.Job, publisherType);

        private static JobBuilderPublisherExtensionModel Add(JobDbo builder, PublisherTypeEnum publisherType)
        {
            var jJobBuilderPublisherExtension = new JobBuilderPublisherExtensionModel();
            jJobBuilderPublisherExtension.Job = builder;
            jJobBuilderPublisherExtension.Job.Publisher.PublisherType = publisherType;
            return jJobBuilderPublisherExtension;
        }
    }
}
