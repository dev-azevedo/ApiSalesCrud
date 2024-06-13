using SalesCrud.Exceptions;
using SalesCrud.Services.Interfaces;
using SalesCrud.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SalesCrud.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var (sales, totalItems) = await _saleService.FindAll(pageNumber, pageSize);
            var response = new
            {
                PageNumber = pageNumber,
                pageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = sales
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
            var sale = _saleService.FindById(id);
            return Ok(sale);
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
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
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }


    [HttpPost]
    public IActionResult Post([FromBody] SalePostViewModel saleViewModel)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => new ValidationError(x.ErrorMessage))).ToList();

            return BadRequest(new ValidationResultModel(400, errors));
        }
        
        try
        {
            var sale = _saleService.Created(saleViewModel);
            return Created($"/sale/{sale.Id}", sale);
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
    [Authorize(Roles = "Manager")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        try
        {
            _saleService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }
}
