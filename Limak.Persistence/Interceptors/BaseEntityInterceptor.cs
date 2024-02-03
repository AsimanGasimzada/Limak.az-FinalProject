using Limak.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Limak.Persistence.Interceptors;

public class BaseEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntity(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntity(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntity(DbContext context)
    {
        if (context is null) return;
        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State is EntityState.Added)
            {
                entry.Entity.CreatedTime = DateTime.UtcNow;
                entry.Entity.ModifiedTime = DateTime.UtcNow;
                entry.Entity.CreatedBy = "Asiman";
                entry.Entity.ModifiedBy = "Asiman";
                entry.Entity.IsDeleted = false;
            }
            if (entry.State is EntityState.Modified)
            {
                entry.Entity.ModifiedBy = "Asiman";
                entry.Entity.ModifiedTime = DateTime.UtcNow;
            }
        }
    }
}
