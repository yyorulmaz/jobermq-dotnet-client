using System;

namespace JoberMQ.Client.Common.RoundRobin
{
    public class RoundRobinModel<T>
    {
        public Guid Id { get; set; }
        internal int Weight { get; set; }
        public T Data { get; set; }
    }
}
