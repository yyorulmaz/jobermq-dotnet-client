using System;

namespace JoberMQ.Client.Common.Timers
{

    internal interface ITimer
    {
        public Guid InstanceId { get; }
        public event Action<TimerModel> Receive;

        public TimerModel Get(Guid timerId);
        public bool Add(TimerModel timer);
        public bool Remove(Guid timerId);
        public bool Remove(TimerModel timer);
        public bool Update(TimerModel timer);
        public void SetReceive(TimerModel timer);
    }
}
