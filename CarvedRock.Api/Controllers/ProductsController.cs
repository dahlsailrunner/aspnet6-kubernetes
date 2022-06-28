using CarvedRock.Api.DomainLogic;
using CarvedRock.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarvedRock.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly IProductLogic _productLogic;
    private readonly ILogger<ProductsController> _logger;


    public ProductsController(IProductLogic productLogic, ILogger<ProductsController> logger)
    {
        _productLogic = productLogic;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductModel>>> Get()
    {
        return await _productLogic.GetAllProducts();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductModel>> GetById(int id)
    {
        var product = await _productLogic.GetProductById(id);
        if (product == null)
        {
            _logger.LogInformation("No product found for {id}.", id);
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(ProductModel product)
    {
        await _productLogic.AddNewProduct(product);
        var uri = Request.Path.Value + $"/{product.Id}";
        return Created(uri, product.Id);
    }

    [HttpPut]
    public async Task<ActionResult> Edit(int id, ProductModel product)
    {
        if (id != product.Id) return BadRequest();
        await _productLogic.UpdateProduct(product);
        return Ok(product);
    }


    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _productLogic.RemoveProduct(id);
        return Ok();
    }
}
