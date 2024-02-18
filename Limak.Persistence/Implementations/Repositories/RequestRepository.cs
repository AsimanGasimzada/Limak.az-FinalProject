using Limak.Application.Abstractions.Repositories;
using Limak.Domain.Entities;
using Limak.Persistence.DAL;
using Limak.Persistence.Implementations.Repositories.Generic;

namespace Limak.Persistence.Implementations.Repositories;

public class RequestRepository : Repository<Request>, IRequestRepository
{
    public RequestRepository(AppDbContext context) : base(context)
    {

    }
}
