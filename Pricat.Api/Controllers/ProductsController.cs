using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pricat.Application.Interfaces;
using Pricat.Domain.Dtos;
using Pricat.Domain.Entities;
using Pricat.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pricat.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryParams queryParams)
    {
        if (queryParams.Page == 0 && queryParams.Limit == 0)
        {
            return Ok(await _productService.GetAllAsync());
        }

        var responseData = await _productService.GetByQueryParamsAsync(queryParams);

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(responseData.XPagination));

        return Ok(responseData.Items);
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _productService.GetByIdAsync(id));
    }

    // POST api/<ProductsController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        return Ok(await _productService.AddAsync(product));
    }

    // PUT api/<ProductsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Product product)
    {
        return Ok(await _productService.UpdateAsync(id, product));
    }

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.RemoveAsync(id);
        return Ok();
    }
}