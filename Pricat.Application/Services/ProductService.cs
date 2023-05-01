using Pricat.Application.Interfaces;
using Pricat.Domain.Dtos;
using Pricat.Domain.Entities;
using Pricat.Domain.Exceptions;
using Pricat.Domain.Interfaces.Repositories;
using Pricat.Utilities;
using System.Linq.Expressions;

namespace Pricat.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryService _categoryService;

    public ProductService(IProductRepository productRepository, ICategoryService categoryService)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await ValidateCategoryIdIsValid(entity);
        ValidateEanCodeIsValid(entity);
        
        return await _productRepository.AddAsync(entity);
    }

    public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _productRepository.FindAsync(predicate);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new NotFoundException($"Product [{id}] Not Found");
        }

        return product;
    }

    public async Task<ResponseData<Product>> GetByQueryParamsAsync(QueryParams queryParams)
    {
        return await _productRepository.GetByQueryParamsAsync(queryParams);
    }

    public async Task RemoveAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            throw new NotFoundException($"Product [{id}] Not Found");
        }
        await _productRepository.RemoveAsync(product);
    }

    public async Task<Product> UpdateAsync(int id, Product entity)
    {
        if (id != entity.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Product.Id [{entity.Id}]");
        }

        var product = await _productRepository.GetByIdAsync(id);
        ValidateProductIsNull(id, product);
        await ValidateCategoryIdIsValid(entity);
        ValidateEanCodeIsValid(entity);

        return (await _productRepository.UpdateAsync(entity));
    }

    private void ValidateProductIsNull(int id, Product? product)
    {
        if (product is null)
        {
            throw new NotFoundException($"Product [{id}] Not Found");
        }
    }

    private void ValidateEanCodeIsValid(Product entity)
    {
        if (!Ean13Calculator.IsValid(entity.EanCode))
        {
            throw new BadRequestException($"EAN Code [{entity.EanCode}] is Not Valid");
        }
    }

    private async Task ValidateCategoryIdIsValid(Product entity)
    {
        _ = await _categoryService.GetByIdAsync(entity.CategoryId);
    }
}