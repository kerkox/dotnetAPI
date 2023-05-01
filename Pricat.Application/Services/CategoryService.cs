using Pricat.Application.Interfaces;
using Pricat.Domain.Dtos;
using Pricat.Domain.Entities;
using Pricat.Domain.Exceptions;
using Pricat.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Pricat.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> AddAsync(Category entity)
    {
        return await _categoryRepository.AddAsync(entity);
    }

    public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
    {
        return await _categoryRepository.FindAsync(predicate);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            throw new NotFoundException($"Category [{id}] Not Found");
        }

        return category;
    }

    public async Task<ResponseData<Category>> GetByQueryParamsAsync(QueryParams queryParams)
    {
        return await _categoryRepository.GetByQueryParamsAsync(queryParams);
    }

    public async Task RemoveAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            throw new NotFoundException($"Category [{id}] Not Found");
        }

        await _categoryRepository.RemoveAsync(category);
    }

    public async Task RemoveCategoryAndProductsAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            throw new NotFoundException($"Category [{id}] Not Found");
        }

        await _categoryRepository.RemoveCategoryAndProductsAsync(category);
    }


    public async Task<Category> UpdateAsync(int id, Category entity)
    {
        if (id != entity.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Category.Id [{entity.Id}]");
        }

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            throw new NotFoundException($"Category [{id}] Not Found");
        }

        return (await _categoryRepository.UpdateAsync(entity));
    }
}