using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server
{
    public class ServerController
    {
        private const string URL = "https://wadahub.manerai.com/api/inventory/status";
        private const string Token = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";

        public async Task<T> SendRequest<T>(string data)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, URL);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.SendAsync(request);

            if (!responseMessage.IsSuccessStatusCode) throw new Exception(responseMessage.StatusCode.ToString());

            var response = await Deserialize<T>(responseMessage);
            return response;
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage message)
        {
            var str = await message.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<T>(str);
            return response;
        }
    }
}