using System.Net.Http.Headers;

namespace TaskStorage.Services
{
    public class YouTrackHttpClient
    {
        private readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://testjiraintegration.youtrack.cloud/api/"),
            DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", "perm:cm9vdA==.NDktMQ==.SxDRgvTW9ItJ6hGswCqHuMFzFVpYBz") }
        };

        /// <summary>
        /// Возвращает HTTP-клиент для запросов к YouTrack. 
        /// </summary>
        /// <returns></returns>
        public HttpClient GetClient()
        {
            return _httpClient;
        }
    }
}