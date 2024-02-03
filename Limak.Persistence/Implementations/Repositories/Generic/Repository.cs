using Limak.Application.Abstractions.Repositories.Generic;
using Limak.Domain.Entities.Common;
using Limak.Persistence.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Limak.Persistence.Implementations.Repositories.Generic;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }


    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void HardDelete(T entity)
    {

        _context.Set<T>().Remove(entity);

    }
    public void SoftDelete(T entity)
    {

        if (entity is BaseAuditableEntity auditableEntity)
        {
            auditableEntity.IsDeleted = true;
        }
    }
    public void Repair(T entity)
    {

        if (entity is BaseAuditableEntity auditableEntity)
        {
            auditableEntity.IsDeleted = false;
        }
    }

    public IQueryable<T> GetAll(bool ignoreFilter = false, params string[] includes)
    {
        var query = _context.Set<T>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        if (ignoreFilter)
            query = query.IgnoreQueryFilters();

        return query;
    }



    public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, bool ignoreFilter = false, params string[] includes)
    {
        var query = _context.Set<T>().Where(expression).AsQueryable();
        foreach (string include in includes)
        {
            query = query.Include(include);
        }
        if (ignoreFilter)
            query = query.IgnoreQueryFilters();
        return query;
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, bool ignoreFilter = false)
    {
        if (ignoreFilter)
            return await _context.Set<T>().IgnoreQueryFilters().AnyAsync(expression);
        return await _context.Set<T>().AnyAsync(expression);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, bool ignoreFilter = false, params string[] includes)
    {
        var query = _context.Set<T>().AsQueryable<T>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        if (ignoreFilter)
            query = query.IgnoreQueryFilters();
        T? entity = await query.FirstOrDefaultAsync(expression);
        return entity;
    }

    public IQueryable<T> OrderBy(IQueryable<T> query, Expression<Func<T, object>> expression)
    {
        IQueryable<T> result = query.OrderBy(expression);
        return result;
    }

    public IQueryable<T> Paginate(IQueryable<T> query, int limit, int page = 1)
    {
        IQueryable<T> result = query.Skip((page - 1) * limit).Take(limit);
        return result;
    }
}
