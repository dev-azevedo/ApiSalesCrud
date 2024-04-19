using CamposDealerCrud.Exceptions;
using CamposDealerCrud.Services;
using CamposDealerCrud.Services.Interfaces;
using CamposDealerCrud.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CamposDealerCrud.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var clients = _clientService.FindAll();
            return Ok(clients);

        }
        catch
        {
            return BadRequest();
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
            return BadRequest(ex.Message);
        }
    }

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
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] ClientPostViewModel clientViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(clientViewModel);
        }

        try
        {
            var client = _clientService.Created(clientViewModel);
            return Created($"/client/{client.Id}", client);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public IActionResult Put(ClientPutViewModel clientViewModel)
    {
        try
        {
            _clientService.Updated(clientViewModel);
            return Ok(clientViewModel);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest();
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
        catch
        {
            return BadRequest();
        }
    }
}
