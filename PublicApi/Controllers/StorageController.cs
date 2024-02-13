using Microsoft.AspNetCore.Mvc;
using TaskStorage.Interfaces;

namespace TaskStorage.Controllers;


[ApiController]
[Route("api/")]
public class StorageController : ControllerBase
{
    
    ///<summary/>
    private readonly IStorageService _storageService;

    ///<summary/>
    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    /// <summary>
    /// Добавляет новые задачи в базу данных.
    /// </summary>
    /// <returns>Статус код 200 (ОК)</returns>
    [Route("store")]
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> StoreNewIssues()
    {
        await _storageService.StoreNewIssues();
        return Ok();
    }
}