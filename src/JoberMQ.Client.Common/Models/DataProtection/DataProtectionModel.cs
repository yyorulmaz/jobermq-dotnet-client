using JoberMQ.Client.Common.Enums.Data;

namespace JoberMQ.Client.Common.Models.DataProtection
{
    internal class DataProtectionModel
    {
        public PushDataTypeEnum PushDataType { get; set; }
        public string Data { get; set; }
    }
}
