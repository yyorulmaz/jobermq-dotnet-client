using JoberMQ.Common.RoundRobin.Abstraction;
using JoberMQ.Common.RoundRobin.Enums;
using JoberMQ.Common.RoundRobin.Implementation.Default;

namespace JoberMQ.Common.RoundRobin.Factories
{
    internal class RoundRobinFactory
    {
        internal static IRoundRobin<T> Create<T>(RoundRobinFactoryEnum factory)
        {
            IRoundRobin<T> roundRobin;

            switch (factory)
            {
                case RoundRobinFactoryEnum.Default:
                    roundRobin = new DfRoundRobin<T>();
                    break;
                default:
                    roundRobin = new DfRoundRobin<T>();
                    break;
            }

            return roundRobin;
        }
    }
}
