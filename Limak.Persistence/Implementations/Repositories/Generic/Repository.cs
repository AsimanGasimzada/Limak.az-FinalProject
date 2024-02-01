using Limak.Application.Abstractions.Repositories.Generic;
using Limak.Domain.Entities.Common;
using System.Linq.Expressions;

namespace Limak.Persistence.Implementations.Repositories.Generic;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    public Task CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAll(bool ignoreFilter = false, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, bool ignoreFilter = false, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, bool ignoreFilter = false, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public void HardDelete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, bool ignoreFilter = false)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> OrderBy(IQueryable<T> query, Expression<Func<T, object>> expression)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Paginate(IQueryable<T> query, int limit, int page = 1)
    {
        throw new NotImplementedException();
    }

    public void Repair(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync()
    {
        throw new NotImplementedException();
    }

    public void SoftDelete(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}
