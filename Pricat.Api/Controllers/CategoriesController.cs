using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pricat.Application.Interfaces;
using Pricat.Domain.Dtos;
using Pricat.Domain.Entities;

namespace Pricat.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        // GET: api/<CategorysController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParams queryParams)
        {
            if (queryParams.Page == 0 && queryParams.Limit == 0)
            {
                return Ok(await _categoryService.GetAllAsync());
            }

            var responseData = await _categoryService.GetByQueryParamsAsync(queryParams);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(responseData.XPagination));

            return Ok(responseData.Items);
        }

        // GET api/<CategorysController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _categoryService.GetByIdAsync(id));
        }

        // GET api/<CategorysController>/5/products
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            _ = await _categoryService.GetByIdAsync(id);
            return Ok(await _productService.FindAsync(product => product.CategoryId == id));
        }

        // POST api/<CategorysController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            return Ok(await _categoryService.AddAsync(category));
        }

        // PUT api/<CategorysController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category)
        {
            return Ok(await _categoryService.UpdateAsync(id, category));
        }

        // DELETE api/<CategorysController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.RemoveCategoryAndProductsAsync(id);
            return Ok();
        }
    }
}
