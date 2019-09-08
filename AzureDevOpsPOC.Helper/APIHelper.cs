using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AzureDevOpsPOC.Helper
{
    public class APIHelper
    {
        private readonly HttpClient _client = new HttpClient();

        public APIHelper(string url, string accessToken)
        {
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($" :{accessToken}")));
        }

        public async Task<T> Get<T>(string method)
        {
            try
            {
                HttpResponseMessage responseMessage = await _client.GetAsync(method);

                if (responseMessage.IsSuccessStatusCode)
                {
                    string response = await responseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(response);
                }

                return default;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Post(string method, object obj)
        {
            try
            {
                return (await _client.PostAsync(method, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"))).IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Put(string method, object obj)
        {
            try
            {
                return (await _client.PutAsync(method, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"))).IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(string method)
        {
            try
            {
                return (await _client.DeleteAsync(method)).IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}