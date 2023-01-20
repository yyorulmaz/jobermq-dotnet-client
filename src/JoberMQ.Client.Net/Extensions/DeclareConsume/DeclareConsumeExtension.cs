using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Models.DeclareConsumeBuilder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeExtension
    {
        public static DeclareConsumeBuilderModel DeclareConsumeBuilder(this IClient client)
          => Add(client);

        private static DeclareConsumeBuilderModel Add(IClient client)
        {
            var declareConsumeBuilder = new DeclareConsumeBuilderModel();
           


            return declareConsumeBuilder;
        }
    }
}
