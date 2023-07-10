using Core.Models;

namespace Core.Interfaces
{
    public interface IProductService
    {
        public Task<ServiceResponse<List<Product>>> GetProducts();
        public Task<ServiceResponse<Product>> GetProductById(int id);
    }
}
