using JoberMQ.Common.Models.Response;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace JoberMQ.Client.Net.Operations
{
    internal class LoginOperation
    {
        internal readonly Uri BaseAddress;
        internal readonly HttpClient HttpClient;
        internal string ConnectionToken;

        //public string User { get; private set; }

        //public LoginOperation(string baseAddress)
        //{
        //    BaseAddress = new Uri(baseAddress);
        //    HttpClient = new HttpClient
        //    {
        //        BaseAddress = BaseAddress
        //    };
        //}

        public async Task<ResponseLoginModel> AuthenticateAsync(string endpoint, string user, string pass, string clientId)
        {
            HttpClient HttpClient = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, new Uri(BaseAddress, "account/login"));
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(endpoint));
            var encoding = Encoding.GetEncoding("iso-8859-1");
            HttpClient.DefaultRequestHeaders.Add("clientId", clientId);
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(encoding.GetBytes($"{user}:{pass}")));

            try
            {
                var response = await HttpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ResponseLoginModel>(content);
            }
            catch (Exception)
            {
                return new ResponseLoginModel
                {
                    IsSuccess = false,
                    StatusCode = "0.0.12",
                    Message = JoberMQClient.StatusCode.GetStatusMessage("0.0.12"),
                    Token = null
                };
            }
        }
        //public async Task<string> AuthenticateAsync(string endpoint, string user, string pass, string clientId)
        //{
        //    //var request = new HttpRequestMessage(HttpMethod.Post, new Uri(BaseAddress, "account/login"));
        //    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(endpoint));
        //    var encoding = Encoding.GetEncoding("iso-8859-1");
        //    HttpClient.DefaultRequestHeaders.Add("clientId", clientId);
        //    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(encoding.GetBytes($"{user}:{pass}")));

        //    try
        //    {
        //        var response = await HttpClient.SendAsync(request);
        //        var content = await response.Content.ReadAsStringAsync();

        //        if (response?.IsSuccessStatusCode == true)
        //        {
        //            ConnectionToken = await response.Content.ReadAsStringAsync();
        //        }

        //        if (string.IsNullOrWhiteSpace(ConnectionToken)) throw new InvalidOperationException("Token missing from authentication response.");

        //        User = user;
        //        Console.WriteLine("Logged in.");

        //        return ConnectionToken;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}
