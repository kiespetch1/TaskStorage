using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Controllers
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

        [Route("upload")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Upload()
        {
            var issues = await _uploadService.Upload(new YouTrackHttpClient());
            return Ok(issues);
        }
    }
}