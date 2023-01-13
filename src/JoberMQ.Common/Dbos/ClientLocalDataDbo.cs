using JoberMQ.Common.Enums.Data;
using JoberMQ.Library.Database.Base;
using System;

namespace JoberMQ.Common.Dbos
{
    public class ClientLocalDataDbo : DboPropertyGuidBase, IDboBase
    {
        public virtual PushDataTypeEnum PushDataType { get; set; }
        public virtual Guid? JobId { get; set; }
        public virtual string Data { get; set; }
    }
}
