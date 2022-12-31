using JoberMQ.Client.Common.Database.Base;
using JoberMQ.Client.Common.Enums.Data;

namespace JoberMQ.Client.Common.Dbos
{
    public class ClientLocalDataDbo : DboPropertyGuidBase, IDboBase
    {
        public virtual PushDataTypeEnum PushDataType { get; set; }
        public virtual Guid? JobId { get; set; }
        public virtual string Data { get; set; }
    }
}
