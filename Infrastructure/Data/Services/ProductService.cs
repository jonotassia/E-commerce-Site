using Infrastructure.Data;
using Core.Models;
using AutoMapper;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public ProductService(StoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ProductBrand>>> GetProductBrands()
        {
            var response = new ServiceResponse<List<ProductBrand>>();

            var brands = await _context.ProductBrands.ToListAsync();

            if (!brands.Any())
            {
                response.Success = false;
                response.Message = "Product does not exist.";
                return response;
            }
            response.Data = brands;
            response.Message = "Product found.";
            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductById(int id)
        {
            var response = new ServiceResponse<Product>();

            var product = await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Product does not exist.";
                return response;
            }
            response.Data = product;
            response.Message = "Product found.";
            return response;

        }

        public async Task<ServiceResponse<List<Product>>> GetProducts()
        {
            var response = new ServiceResponse<List<Product>>();

            var products = await _context.Products.ToListAsync();
            
            if (!products.Any())
            {
                response.Success = false;
                response.Message = "There are no products in the database.";
                return response;
            }
            response.Data = products;
            response.Message = "Product found.";
            return response;
        }

        public async Task<ServiceResponse<List<ProductType>>> GetProductTypes()
        {
            var response = new ServiceResponse<List<ProductType>>();

            var types = await _context.ProductTypes.ToListAsync();

            if (!types.Any())
            {
                response.Success = false;
                response.Message = "Product does not exist.";
                return response;
            }
            response.Data = types;
            response.Message = "Product found.";
            return response;
        }
    }
}
