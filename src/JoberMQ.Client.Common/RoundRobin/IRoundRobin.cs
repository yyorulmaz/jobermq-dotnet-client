using System;
using System.Collections.Generic;

namespace JoberMQ.Client.Common.RoundRobin
{
    public interface IRoundRobin<T>
    {
        public RoundRobinModel<T> Get(Guid id);
        public List<RoundRobinModel<T>> GetAll();
        public RoundRobinModel<T> GetEndRoundRobin();
        public T GetEndRoundRobinData();
        public RoundRobinModel<T> Add(T value, int weight);
        public RoundRobinModel<T> Remove(Guid id);
        public T Next { get; }
    }
}
