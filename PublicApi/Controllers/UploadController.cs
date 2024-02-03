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
        public async Task<IActionResult> UploadAll()
        {
            var issues = await _uploadService.UploadAll(new YouTrackHttpClient());
            return Ok(issues);
        }
        
        [Route("uploadNew")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadNew()
        {
            var issues = await _uploadService.UploadNew(new YouTrackHttpClient());
            return Ok(issues);
        }
    }
}