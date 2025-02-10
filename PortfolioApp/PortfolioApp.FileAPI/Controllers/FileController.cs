using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.FileAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        var result = await _fileService.UploadFileAsync(file);
        
        return Ok(result);
    }

    [HttpGet("delete/{fileName}")]
    public IActionResult Delete([FromRoute] string fileName)
    {
        var result = _fileService.DeleteFile(fileName);

        if(result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}
