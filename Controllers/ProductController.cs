using CamposDealerCrud.Exceptions;
using CamposDealerCrud.Model;
using CamposDealerCrud.Services;
using CamposDealerCrud.Services.Interfaces;
using CamposDealerCrud.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CamposDealerCrud.Controllers;
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
    public IActionResult Get()
    {
        try
        {
            var products = _productService.FindAll();
            return Ok(products);

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
            var product = _productService.FindById(id);
            return Ok(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Post(ProductPostViewModel productViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(productViewModel);
        }

        try
        {
            var product = _productService.Created(productViewModel);
            return Created($"/product/{product.Id}", product);
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
    public IActionResult Put(ProductPutViewModel productViewModel)
    {
        try
        {
            _productService.Updated(productViewModel);
            return Ok(productViewModel);
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
            _productService.Delete(id);
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
