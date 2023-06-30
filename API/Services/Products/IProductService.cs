using Core.Models;

namespace API.Services.Products
{
    public interface IProductService
    {
        public Task<ServiceResponse<List<Product>>> GetProducts();
        public Task<ServiceResponse<Product>> GetProductById(int id);
    }
}
