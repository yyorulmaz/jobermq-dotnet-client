//using JoberMQ.Client.Net;
//using JoberMQ.Client.Net.Abs;
//using JoberMQ.Common.Models.General;
//using System.Dynamic;

//namespace JoberMQ.Client.Example.Consumer
//{
//    public abstract class JoberMQRPCBase
//    {
//        protected IClient joberMQClient;
//        JoberMQParameterModel joberMQParameter;
//        public JoberMQRPCBase(JoberMQParameterModel joberMQParameter)
//        {
//            this.joberMQParameter = joberMQParameter;
//            Setup();
//        }
//        async void Setup()
//        {
//            var result = await JoberMQClient.CreateClientAndConnectAsync(joberMQParameter);
//            if (result.connect == true)
//            {
//                joberMQClient = result.client;
//                joberMQClient.ReceiveMessage += JoberMQClient_ReceiveMessage;
//            }
//            else
//                throw new Exception("JoberMQ connect error");

//            joberMQRPCClient.GetTTTTTTTTT();
//            denemeBase.METSSS();


//        }

//        protected abstract void JoberMQClient_ReceiveMessage(string obj);

//        IJoberMQRPCClient joberMQRPCClient;
//        DenemeBase denemeBase;

//    }

   

//    public interface IJoberMQRPCClient
//    {
//        public void GetTTTTTTTTT();
//    }
//    public abstract class DenemeBase
//    {
//        public abstract void METSSS();  
//    }
  

//}
