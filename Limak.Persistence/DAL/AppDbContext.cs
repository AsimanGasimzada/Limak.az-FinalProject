using Limak.Domain.Entities;
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

    public DbSet<Company> Company { get; set; } = null!; //only one data
    public DbSet<Shop> Shops { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<ShopCategory> ShopCategories { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Delivery> Deliveries { get; set; } = null!;
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    public DbSet<Kargomat> Kargomats { get; set; } = null!;
    public DbSet<DeliveryArea> DeliveryAreas { get; set; } = null!;
    public DbSet<Gender> Genders { get; set; } = null!;
    public DbSet<Citizenship> Citizenships { get; set; } = null!;
    public DbSet<UserPosition> UserPositions { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<News> News { get; set; } = null!;
    public DbSet<Tariff> Tariffs { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<Request> Requests { get; set; } = null!;
    public DbSet<RequestMessage> RequestMessages { get; set; } = null!;
    public DbSet<RequestSubject> RequestSubjects { get; set; } = null!;

}
