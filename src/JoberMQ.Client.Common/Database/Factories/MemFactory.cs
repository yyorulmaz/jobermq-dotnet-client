using JoberMQ.Client.Common.Database.Enums;
using JoberMQ.Client.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Database.Repository.Implementation.Mem.Default;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Common.Database.Factories
{
    internal class MemFactory
    {
        internal static IMemRepository<TKey, TValue> CreateMem<TKey, TValue>(MemFactoryEnum memFactory, MemDataFactoryEnum memDataFactory, ConcurrentDictionary<TKey, TValue> masterData)
        {
            IMemRepository<TKey, TValue> memRepository;

            switch (memFactory)
            {
                case MemFactoryEnum.Default:
                    switch (memDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            memRepository = new DfMemRepository<TKey, TValue>(masterData);
                            break;
                        default:
                            memRepository = new DfMemRepository<TKey, TValue>(masterData);
                            break;
                    }
                    break;
                default:
                    switch (memDataFactory)
                    {
                        case MemDataFactoryEnum.Data:
                            memRepository = new DfMemRepository<TKey, TValue>(masterData);
                            break;
                        default:
                            memRepository = new DfMemRepository<TKey, TValue>(masterData);
                            break;
                    }
                    break;
            }

            return memRepository;
        }
    }
}
