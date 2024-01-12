using System.Net.Http.Headers;

namespace TaskStorage.Services
{
    public class YouTrackHttpClient : HttpClient
    {
        public YouTrackHttpClient()
        {
            BaseAddress = new Uri("https://testjiraintegration.youtrack.cloud/api/issues/");
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", "perm:cm9vdA==.NDktMQ==.SxDRgvTW9ItJ6hGswCqHuMFzFVpYBz");
        }
    }
}