using Limak.Application.Abstractions.Repositories;
using Limak.Domain.Entities;
using Limak.Persistence.DAL;
using Limak.Persistence.Implementations.Repositories.Generic;

namespace Limak.Persistence.Implementations.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }
}
