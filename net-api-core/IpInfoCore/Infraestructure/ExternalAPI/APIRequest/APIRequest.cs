using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace net_api.Infraestructure.ExternalAPI
{
    public class APIRequest : IAPIRequest
    {
        internal APIRequest()
        {

        }

        internal string BaseUrl { get; init; }

        private static readonly HttpClient HttpClient;

        static APIRequest()
        {
            HttpClient = new HttpClient();
        }

        public async Task<T> GetFromCloudAsync<T>(params string[] urlParams)
        {
            var uri = String.Format(BaseUrl, urlParams);
            HttpResponseMessage response;
            try
            {
                response = await HttpClient.GetAsync(uri);
            }
            catch (Exception e)
            {
                throw new Exception($"Probably invalid URL ({uri})", e);
            }

            CheckResponseStatus(response);

            var data = response.Content.ReadAsStringAsync().Result;

            try
            {
                return JsonSerializer.Deserialize<T>(data);
            }
            catch (Exception e)
            {
                throw new Exception("Unexpected external API response format", e);
            }
        }

        private void CheckResponseStatus(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception($"External API Response Error. Status Code: {response.StatusCode}");
        }
    }
}
