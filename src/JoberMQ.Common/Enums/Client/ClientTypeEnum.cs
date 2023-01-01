using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Common.Enums.Client
{
    public enum ClientTypeEnum
    {
        Normal = 1,
        LoadBalancing = 2,
        HighAvailability = 3,
        LoadBalancingANDHighAvailability = 4
    }
}
