using JoberMQ.Common.Enums.Declare;
using JoberMQ.Common.Models.Declare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Client.Net.Extensions
{
    public static class DeclareConsumeExtension
    {
        public static DeclareConsumeBuilder SpecialAdd(this DeclareConsumeModel declare)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.SpecialAdd } };
        public static DeclareConsumeBuilder SpecialRemove(this DeclareConsumeModel declare)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.SpecialRemove } };
        public static DeclareConsumeBuilder GroupAdd(this DeclareConsumeModel declare)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.GroupAdd } };
        public static DeclareConsumeBuilder GroupRemove(this DeclareConsumeModel declare)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.GroupRemove } };
        public static DeclareConsumeBuilder QueueAdd(this DeclareConsumeModel declare, string queueKey)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.QueueAdd, QueueKey = queueKey } };
        public static DeclareConsumeBuilder QueueRemove(this DeclareConsumeModel declare, string queueKey)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.QueueRemove, QueueKey = queueKey } };


        public static DeclareConsumeBuilder ErrorSpecialAdd(this DeclareConsumeModel declare)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.ErrorSpecialAdd } };
        public static DeclareConsumeBuilder ErrorSpecialRemove(this DeclareConsumeModel declare)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.ErrorSpecialRemove } };
        public static DeclareConsumeBuilder ErrorGroupAdd(this DeclareConsumeModel declare)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.ErrorGroupAdd } };
        public static DeclareConsumeBuilder ErrorGroupRemove(this DeclareConsumeModel declare)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.ErrorGroupRemove } };
        public static DeclareConsumeBuilder ErrorQueueAdd(this DeclareConsumeModel declare, string queueKey)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.ErrorQueueAdd, QueueKey = queueKey } };
        public static DeclareConsumeBuilder ErrorQueueRemove(this DeclareConsumeModel declare, string queueKey)
            => new DeclareConsumeBuilder { DeclareConsumeModel = new DeclareConsumeModel { DeclareConsumeType = DeclareConsumeTypeEnum.ErrorQueueRemove, QueueKey = queueKey } };




        public static bool Publish(this DeclareConsumeBuilder builder)
        {


            return true;
        }
    }
}
