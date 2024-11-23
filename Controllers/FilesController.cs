﻿using Microsoft.AspNetCore.Mvc;
using SalesCrud.Enums;
using SalesCrud.Services.Interfaces;

namespace SalesCrud.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;
    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet("{destinationFile}/{id:guid}")]
    public IActionResult Get([FromRoute] EDestinationFile destinationFile, [FromRoute] Guid id)
    {
        var fileResult = _fileService.GetFile(destinationFile, id);

        if (fileResult == null)
        {
            return NotFound();
        }

        var fileType = Path.GetExtension(fileResult.FileName).ToLowerInvariant();
        var mimeType = _fileService.GetMimeType(fileType);

        return File(fileResult.FileBytes, mimeType, fileResult.FileName);
    }

    //[HttpPost]
    //public async Task<IActionResult> Post([FromForm] FilePostViewModel fileViewModel)
    //{
    //    if (fileViewModel.File == null || fileViewModel.File.Length == 0)
    //    {
    //        return BadRequest("Invalid file");
    //    }

    //    FileViewModel detail = await _fileService.SaveFile(fileViewModel.File, fileViewModel.ProductId, fileViewModel.DestinationFile);
    //    return Ok(detail);
    //}
}
