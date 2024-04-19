using CamposDealerCrud.Exceptions;
using CamposDealerCrud.Services;
using CamposDealerCrud.Services.Interfaces;
using CamposDealerCrud.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CamposDealerCrud.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var sales = _saleService.FindAll();
            return Ok(sales);

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
            var sale = _saleService.FindById(id);
            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{nameOrDescription}")]
    public IActionResult Get([FromRoute] string nameOrDescription)
    {
        try
        {
            var sale = _saleService.FindAllByNameOrDescription(nameOrDescription);
            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] SalePostViewModel saleViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(saleViewModel);
        }
        
        try
        {
            var sale = _saleService.Created(saleViewModel);
            return Created($"/sale/{sale.Id}", sale);
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
    public IActionResult Put([FromBody] SalePutViewModel saleViewModel)
    {
        try
        {
            _saleService.Updated(saleViewModel);
            return Ok(saleViewModel);
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
            _saleService.Delete(id);
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
