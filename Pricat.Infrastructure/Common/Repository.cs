﻿using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Common;
using Pricat.Domain.Dtos;
using Pricat.Domain.Exceptions;
using Pricat.Infrastructure.Context;
using System.Linq.Expressions;

namespace Pricat.Infrastructure.Common;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    private readonly AppDbContext _appDbContext;

    public Repository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        _appDbContext.Set<T>().Add(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _appDbContext.Set<T>().Where(predicate).ToListAsync<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _appDbContext.Set<T>().ToListAsync<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _appDbContext.Set<T>().FindAsync(id);
    }

    public async Task<ResponseData<T>> GetByQueryParamsAsync(QueryParams queryParams)
    {
        var entityData = _appDbContext.Set<T>().OrderBy(x => x.Id);

        var totalCount = entityData.Count();

        PaginationData xPagination = new PaginationData(totalCount, queryParams.Page, queryParams.Limit);

        var items = await entityData.Skip((queryParams.Page - 1) * queryParams.Limit)
                                    .Take(queryParams.Limit)
                                    .ToListAsync();

        var responseData = new ResponseData<T>() { Items = items, XPagination = xPagination };

        return responseData;
    }

    public async Task RemoveAsync(T entity)
    {
        if(entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        var id = entity.Id;
        _ = await _appDbContext.Set<T>().FindAsync(id) ?? throw new NotFoundException($"Person with Id={id} Not Found");
        
        _appDbContext.Set<T>().Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        var id = entity.Id;
        var original = await _appDbContext.Set<T>().FindAsync(id) ?? throw new NotFoundException($"Person with Id={id} Not Found");
        _appDbContext.Entry(original).CurrentValues.SetValues(entity);
        await _appDbContext.SaveChangesAsync();

        return entity;
    }
}