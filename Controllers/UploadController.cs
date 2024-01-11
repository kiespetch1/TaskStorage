using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using TaskStorage.Interfaces;

namespace TaskStorage.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }
        
        private static HttpClient client = new()
        {
            BaseAddress = new Uri("https://testjiraintegration.youtrack.cloud/api/issues/"),
            DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer","perm:cm9vdA==.NDktMQ==.SxDRgvTW9ItJ6hGswCqHuMFzFVpYBz")}
        };

        [Route("upload")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Upload()
        {
            var issues = await _uploadService.Upload(client);
            return Ok(issues);
        }
    }
}