using JoberMQ.Client.DotNet;
using JoberMQ.Common.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.DotNetClient.Example.Consumer
{

    public interface IHesapla
    {
        //public int Topla(int sayi1, int sayi2);
        public Task<bool> Topla(int sayi1, int sayi2);
    }
    public class Hesapla : IHesapla
    {
        //public int Topla(int sayi1, int sayi2)
        //{
        //    return sayi1 + sayi2;
        //}
        public async Task<bool> Topla(int sayi1, int sayi2)
        {
            return true;
        }
    }


    public interface ITradingTool
    {
        Task<bool> Setup(JoberMQParameterModel joberMQParameterMode);
    }
    public class TradingTool : JoberMQRPCBase<IHesapla>, ITradingTool
    {
        public async Task<bool> Setup(JoberMQParameterModel joberMQParameterModel)
        {
            IHesapla baseClass = new Hesapla();
            return await base.Setup(joberMQParameterModel, baseClass);
        }
    }
}
