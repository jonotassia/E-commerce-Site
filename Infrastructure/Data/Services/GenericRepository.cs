using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        
        public async Task<ServiceResponse<T>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<T>();

            var data = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                response.Success = false;
                response.Message = "Product does not exist.";
                return response;
            }
            response.Data = data;
            response.Message = "Product found.";
            return response;
        }

        public async Task<ServiceResponse<List<T>>> GetListAsync()
        {
            var response = new ServiceResponse<List<T>>();

            var data = await _context.Set<T>().ToListAsync();

            if (!data.Any())
            {
                response.Success = false;
                response.Message = "There are no products in the database.";
                return response;
            }
            response.Data = data;
            response.Message = "Product found.";
            return response;
        }

        public async Task<ServiceResponse<T>> GetEntityWithSpec(ISpecification<T> spec)
        {
            var response = new ServiceResponse<T>();

            var data = await ApplySpecification(spec).FirstOrDefaultAsync();

            if (data == null)
            {
                response.Success = false;
                response.Message = "There are no products in the database.";
                return response;
            }
            response.Data = data;
            response.Message = "Product found.";
            return response;
        }

        public async Task<ServiceResponse<List<T>>> ListAsync(ISpecification<T> spec)
        {
            var response = new ServiceResponse<List<T>>();

            var data = await ApplySpecification(spec).ToListAsync();

            if (!data.Any())
            {
                response.Success = false;
                response.Message = "There are no products in the database.";
                return response;
            }
            response.Data = data;
            response.Message = "Product found.";
            return response;
        }
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            // GetQuery will use the generic type to convert the desired entity to a queryable
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
