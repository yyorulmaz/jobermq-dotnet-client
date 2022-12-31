using System;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Common.Timers
{
    internal class TimerInstance
    {
        internal static ConcurrentDictionary<Guid, ITimer> Instances = new ConcurrentDictionary<Guid, ITimer>();
    }
}
