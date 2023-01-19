namespace JoberMQ.Client.Net.Models.Login
{
    public class ResponseLoginModel
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
