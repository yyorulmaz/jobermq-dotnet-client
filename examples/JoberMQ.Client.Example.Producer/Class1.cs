using JoberMQ.Client.Net;
using JoberMQ.Client.Net.Constant;
using JoberMQ.Common.Enums.Endpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Client.Example.Producer
{
    internal class Class1
    {
    }

    public interface ITest
    {
        void Islem();
    }
    public class Test : ITest
    {
        public async void Islem()
        {
            var client = JoberMQClient.CreateClient("testClient1", UrlProtocolEnum.http, 5000);
            var connect = client.ConnectAsync().Result;
            client.ReceiveMessageText += Client_ReceiveMessageText;


            var result1 = await client.Consume().SubAsync(ClientConst.Default_1m_Queue, true);
            var result2 = await client.Consume().SubAsync(ClientConst.Default_5m_Queue, true);
        }

        private void Client_ReceiveMessageText(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}
