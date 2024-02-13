using System.Net.Http.Headers;

public class YouTrackHttpClient
{
    private readonly HttpClient _client;

    public YouTrackHttpClient()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("https://testjiraintegration.youtrack.cloud/api/")
        };

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "perm:cm9vdA==.NDktMQ==.SxDRgvTW9ItJ6hGswCqHuMFzFVpYBz");
    }

    public HttpClient GetClient => _client;
}