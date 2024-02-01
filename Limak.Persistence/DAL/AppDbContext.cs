using Limak.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Limak.Persistence.DAL;

public class AppDbContext:IdentityDbContext<AppUser,IdentityRole<int>,int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }


    public DbSet<Shop> Shops { get; set; }
    public DbSet<Category> Categories { get; set; }
}
