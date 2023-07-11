using Core.Models;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> Get()
        {
            var response = await _productService.GetProducts();

            if (!response.Success)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductAsync(int id)
        {
            var response = await _productService.GetProductById(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ServiceResponse<ProductBrand>>> GetBrandsAsync()
        {
            var response = await _productService.GetProductBrands();

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("types")]
        public async Task<ActionResult<ServiceResponse<ProductBrand>>> GetTypesAsync()
        {
            var response = await _productService.GetProductTypes();

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
