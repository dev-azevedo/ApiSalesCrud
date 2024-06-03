using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesCrud.Services.Interfaces;
using SalesCrud.ViewModel;

namespace SalesCrud.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet]
    public IActionResult Get(string fileName)
    {
        var fileBytes = _fileService.GetFile(fileName);

        if (fileBytes == null)
        {
            return NotFound();
        }

        var fileType = Path.GetExtension(fileName).ToLowerInvariant();
        var mimeType = _fileService.GetMimeType(fileType);

        return File(fileBytes, mimeType, fileName);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] FilePostViewModel fileViewModel)
    {
        if (fileViewModel.File == null || fileViewModel.File.Length == 0)
        {
            return BadRequest("Invalid file");
        }

        FileViewModel detail = await _fileService.SaveFile(fileViewModel.File, fileViewModel.ProductId);
        return Ok(detail);
    }
}
