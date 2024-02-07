using Limak.Domain.Entities;
using Limak.Domain.Enums;
using Limak.Persistence.Interceptors;
using Limak.Persistence.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Limak.Persistence.DAL;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    private readonly BaseEntityInterceptor _interceptor;
    public AppDbContext(DbContextOptions<AppDbContext> options, BaseEntityInterceptor interceptor) : base(options)
    {
        _interceptor = interceptor;
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.AddSeedData();
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(_interceptor);
    }


    public DbSet<Company> Company { get; set; } //only one data
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ShopCategory> ShopCategories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Kargomat> Kargomats { get; set; }
    public DbSet<DeliveryArea> DeliveryAreas { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Citizenship> Citizenships { get; set; }
    public DbSet<UserPosition> UserPositions { get; set; }




}
