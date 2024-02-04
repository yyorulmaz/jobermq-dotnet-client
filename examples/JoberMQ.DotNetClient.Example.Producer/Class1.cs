using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.DotNetClient.Example.Producer
{
    public interface IRPCICINDENEME
    {
        public int Topla(int sayi1, int sayi2);
    }
    public class DENEME
    {
        public static IRPCICINDENEME MyProperty { get; set; }
        public static ITradingToolEventAction TradingToolEventAction { get; set; }
    }
}
