﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesCrud.Enums;
using SalesCrud.Exceptions;
using SalesCrud.Services.Interfaces;
using SalesCrud.ViewModel;

namespace SalesCrud.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IFileService _fileService;

    public ClientController(IClientService clientService, IFileService fileService)
    {
        _clientService = clientService;
        _fileService = fileService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        try
        {
            var (clients, totalItems) = await _clientService.FindAll(pageNumber, pageSize);
            var response = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = clients
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get([FromRoute] Guid id)
    {
        try
        {
            var product = _clientService.FindById(id);
            return Ok(product);
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [AllowAnonymous]
    [HttpGet("{name}")]
    public IActionResult Get([FromRoute] string name)
    {
        try
        {
            var product = _clientService.FindAllByName(name);
            return Ok(product);
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [AllowAnonymous]
    [HttpGet("bestSeller")]
    public async Task<IActionResult> GetBestSeller()
    {
        try
        {
            var topThreeClients = await _clientService.FindBestSeller();
            return Ok(
                topThreeClients.Select(c => new { Client = c.Item1, SaleCount = c.Item2, }).ToList()
            );
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] ClientPostViewModel clientViewModel)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(x.ErrorMessage))).ToList();

            return BadRequest(new ValidationResultModel(400, errors));
        }

        try
        {
            var client = _clientService.Created(clientViewModel);
            return Created($"/client/{client.Id}", client);
        }
        catch (DomainException ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [HttpPost("file")]
    public async Task<IActionResult> PostFile([FromForm] FilePostViewModel fileViewModel)
    {
        if (fileViewModel.File == null || fileViewModel.File.Length == 0)
        {
            return BadRequest("Invalid file");
        }

        FileViewModel detail = await _fileService.SaveFile(
            fileViewModel.File,
            fileViewModel.Id,
            EDestinationFile.Client
        );

        var client = _clientService.FindById(fileViewModel.Id);

        ClientPutViewModel updateClient = new ClientPutViewModel
        {
            Id = fileViewModel.Id,
            Name = client.Name,
            Email = client.Email,
            City = client.City,
            PathImage = detail.Url
        };

        Put(updateClient);

        return Ok(detail);
    }

    [HttpPut]
    public IActionResult Put([FromBody] ClientPutViewModel clientViewModel)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(x.ErrorMessage))).ToList();

            return BadRequest(new ValidationResultModel(400, errors));
        }

        try
        {
            _clientService.Updated(clientViewModel);
            return Ok(clientViewModel);
        }
        catch (DomainException ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        try
        {
            _clientService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [HttpDelete("file/{id:guid}")]
    [Authorize(Roles = "Manager")]
    public IActionResult DeleteFile([FromRoute] Guid id)
    {
        try
        {
            _fileService.DeleteFile(id, EDestinationFile.Client);

            var client = _clientService.FindById(id);

            ClientPutViewModel updateClient = new ClientPutViewModel
            {
                Id = id,
                Name = client.Name,
                Email = client.Email,
                City = client.City,
                PathImage = null
            };

            _clientService.Updated(updateClient);
            return NoContent();
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }
}
