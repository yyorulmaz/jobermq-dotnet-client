namespace JoberMQ.Client.Common.Models.Method
{
    internal class MethodReturnDataModel<T>
    {
        public bool IsOperationSuccess { get; set; } = true;
        public string StatusCode { get; set; } = "0";
        public string Message { get; set; } = "";
        public string TypeFullName { get; set; }
        public T Data { get; set; }
    }
}
