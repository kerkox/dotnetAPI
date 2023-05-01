using Pricat.Domain.Dtos;
using Pricat.Domain.Entities;
using System.Linq.Expressions;

namespace Pricat.Application.Interfaces;

public interface ICategoryService
{
    public Task<Category> AddAsync(Category entity);

    public Task<IEnumerable<Category>> GetAllAsync();

    public Task<Category> GetByIdAsync(int id);

    public Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate);

    public Task<Category> UpdateAsync(int id, Category entity);

    public Task RemoveAsync(int id);

    public Task RemoveCategoryAndProductsAsync(int id);

    public Task<ResponseData<Category>> GetByQueryParamsAsync(QueryParams queryParams);
}