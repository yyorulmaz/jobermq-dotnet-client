using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.DotNetClient.Example.Producer
{
    public interface ITradingToolEventAction
    {
        public Task<bool> TradingToolAdd(TradingToolDbo tradingToolDbo);
        public Task<bool> TradingToolUpdate(TradingToolDbo tradingToolDbo);

        public Task<bool> TradingToolOptionAdd(TradingToolOptionDbo tradingToolOptionDbo);
        public Task<bool> TradingToolOptionUpdate(TradingToolOptionDbo tradingToolOptionDbo);

        public Task<bool> TradingToolCandleAdd(TradingToolCandleDbo tradingToolCandleDbo);
        public Task<bool> TradingToolCandleUpdate(TradingToolCandleDbo tradingToolCandleDbo);
    }
    public class TradingToolEventAction : ITradingToolEventAction
    {
        public async Task<bool> TradingToolAdd(TradingToolDbo tradingToolDbo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> TradingToolCandleAdd(TradingToolCandleDbo tradingToolCandleDbo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> TradingToolCandleUpdate(TradingToolCandleDbo tradingToolCandleDbo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> TradingToolOptionAdd(TradingToolOptionDbo tradingToolOptionDbo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> TradingToolOptionUpdate(TradingToolOptionDbo tradingToolOptionDbo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> TradingToolUpdate(TradingToolDbo tradingToolDbo)
        {
            throw new NotImplementedException();
        }
    }



    public class TradingToolDbo
    {
        public int Id { get; set; }
    }



    public class TradingToolCandleDbo
    {


    }
    public class TradingToolOptionDbo
    {

    }
}
