using SalesCrud.Exceptions;
using SalesCrud.Services.Interfaces;
using SalesCrud.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace SalesCrud.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{

    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var (products, totalItems) = await _productService.FindAll(pageNumber, pageSize);
            var response = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = products
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
            var product = _productService.FindById(id);
            return Ok(product);
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [HttpGet("{description}")]
    public IActionResult Get([FromRoute] string description)
    {
        try
        {
            var product = _productService.FindAllByDescription(description);
            return Ok(product);
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProductPostViewModel productViewModel)
    {
        try
        {
            var product = _productService.Created(productViewModel);
            return Created($"/product/{product.Id}", product);
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
    public IActionResult Put([FromBody] ProductPutViewModel productViewModel)
    {
        try
        {
            _productService.Updated(productViewModel);
            return Ok(productViewModel);
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
            _productService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };

            return BadRequest(new ValidationResultModel(400, errors));
        }
    }
}
