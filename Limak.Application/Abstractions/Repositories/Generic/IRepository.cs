using Limak.Domain.Entities.Common;
using System.Linq.Expressions;

namespace Limak.Application.Abstractions.Repositories.Generic;

public interface IRepository<T> where T : BaseEntity, new()
{
    IQueryable<T> GetAll(bool ignoreFilter = false, params string[] includes);
    IQueryable<T> OrderBy(IQueryable<T> query, Expression<Func<T, object>> expression);
    IQueryable<T> Paginate(IQueryable<T> query, int limit, int page = 1);
    IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, bool ignoreFilter = false, params string[] includes);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, bool ignoreFilter = false, params string[] includes);

    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, bool ignoreFilter = false);
    Task CreateAsync(T entity);
    void Update(T entity);
    void HardDelete(T entity);
    void SoftDelete(T entity);
    void Repair(T entity);
    Task<int> SaveAsync();
}
