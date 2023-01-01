using JoberMQ.Common.Enums.Data;

namespace JoberMQ.Common.Models.DataProtection
{
    internal class DataProtectionModel
    {
        public PushDataTypeEnum PushDataType { get; set; }
        public string Data { get; set; }
    }
}
