using Microsoft.AspNetCore.Mvc;
using TaskStorage.Interfaces;

namespace TaskStorage.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UploadController : ControllerBase
    {
        ///<summary/>
        private readonly IUploadService _uploadService;
        
        ///<summary/>
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }
        /// <summary>
        /// Выгружает с YouTrack все задачи.
        /// </summary>
        /// <returns>Все задачи с YouTrack.</returns>
        [Route("upload")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadAll()
        {
            var issues = await _uploadService.UploadAll();
            return Ok(issues);
        }
        
        /// <summary>
        /// Выгружает с YouTrack только новые задачи.
        /// </summary>
        /// <returns>Новые задачи с YouTrack.</returns>
        [Route("uploadNew")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadNew()
        {
            var issues = await _uploadService.UploadNew();
            return Ok(issues);
        }
    }
}