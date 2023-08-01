using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Models;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrands(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _productRepo.CountAsync(countSpec);
            
            var response = await _productRepo.ListAsync(spec);

            /* REMOVING FOR HANDLING OF EMPTY RETURN IN CLIENT
             if (!response.Success)
            {
                return NotFound(response);
            }
            */

            // Adding unecessary code in order to continue with examples
            var transformedResponse = new ServiceResponse<List<GetProductDto>>
            {
                Data = response.Data?.Select(p => _mapper.Map<GetProductDto>(p)).ToList()
            };

            transformedResponse.PageIndex = productParams.PageIndex;
            transformedResponse.PageSize = productParams.PageSize;
            transformedResponse.Count = totalItems;

            return Ok(transformedResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> GetProductAsync(int id)
        {
            var spec = new ProductsWithTypesAndBrands(id);
            
            var response = await _productRepo.GetEntityWithSpec(spec);

            if (!response.Success)
            {
                return NotFound(response);
            }

            // Adding unecessary code in order to continue with examples
            var transformedResponse = new ServiceResponse<GetProductDto>
            {
                Data = _mapper.Map<Product, GetProductDto>(response.Data!)
            };

            return Ok(transformedResponse);
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
