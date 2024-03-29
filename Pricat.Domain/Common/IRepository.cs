﻿using Pricat.Domain.Dtos;
using System.Linq.Expressions;

namespace Pricat.Domain.Common;

public interface IRepository<T> where T : EntityBase
{
    public Task<T> AddAsync(T entity);

    public Task<IEnumerable<T>> GetAllAsync();

    public Task<T?> GetByIdAsync(int id);

    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    public Task<T> UpdateAsync(T entity);

    public Task RemoveAsync(T entity);

    public Task<ResponseData<T>> GetByQueryParamsAsync(QueryParams queryParams);
}