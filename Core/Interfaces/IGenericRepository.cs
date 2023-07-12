using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        public Task<ServiceResponse<List<T>>> GetListAsync();
        public Task<ServiceResponse<T>> GetByIdAsync(int id);
        public Task<ServiceResponse<T>> GetEntityWithSpec(ISpecification<T> spec);
        Task<ServiceResponse<List<T>>> ListAsync(ISpecification<T> spec);
    }
}
