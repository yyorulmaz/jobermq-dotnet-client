using System;

namespace JoberMQ.Common.RoundRobin.Models
{
    public class RoundRobinModel<T>
    {
        public Guid Id { get; set; }
        internal int Weight { get; set; }
        public T Data { get; set; }
    }
}
