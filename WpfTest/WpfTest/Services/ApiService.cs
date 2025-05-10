using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using WpfTest.Models.Dtos;
using WpfTest.Models.Entities;

namespace WpfTest
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:44331/api/"; // Cambiar por tu URL

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                Console.WriteLine($"Solicitando: {_httpClient.BaseAddress}{endpoint}");
                var response = await _httpClient.GetAsync(endpoint);
                var rawContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Respuesta: {rawContent}");  // Ver contenido crudo
                response.EnsureSuccessStatusCode();

                return JsonSerializer.Deserialize<T>(rawContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR CRÍTICO: {ex}");
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint, Dictionary<string, string> queryParams = null)
        {
            var uriBuilder = new UriBuilder(_httpClient.BaseAddress + endpoint);
            if (queryParams != null)
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                foreach (var param in queryParams)
                {
                    query[param.Key] = param.Value;
                }
                uriBuilder.Query = query.ToString();
            }
            return await _httpClient.GetAsync(uriBuilder.Uri);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
        {
            return await _httpClient.PostAsJsonAsync(endpoint, data);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            return await _httpClient.DeleteAsync(endpoint);
        }
    }
}
