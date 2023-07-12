using Core.Models;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo)
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> Get()
        {
            var spec = new ProductsWithTypesAndBrands();
            
            var response = await _productRepo.ListAsync(spec);

            if (!response.Success)
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductAsync(int id)
        {
            var response = await _productRepo.GetByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ServiceResponse<ProductBrand>>> GetBrandsAsync()
        {
            var response = await _productBrandRepo.GetListAsync();

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("types")]
        public async Task<ActionResult<ServiceResponse<ProductBrand>>> GetTypesAsync()
        {
            var response = await _productTypeRepo.GetListAsync();

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
