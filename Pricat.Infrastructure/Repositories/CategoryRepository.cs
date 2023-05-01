using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces.Repositories;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Context;

namespace Pricat.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AppDbContext _appDbContext;
    public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task RemoveCategoryAndProductsAsync(Category category)
    {
        _appDbContext.Set<Product>().RemoveRange(_appDbContext.Products.Where(x => x.CategoryId == category.Id));
        await _appDbContext.SaveChangesAsync();
        _appDbContext.Set<Category>().Remove(category);
        await _appDbContext.SaveChangesAsync();

    }
}