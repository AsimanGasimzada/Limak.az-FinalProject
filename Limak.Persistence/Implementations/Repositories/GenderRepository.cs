using Limak.Application.Abstractions.Repositories;
using Limak.Domain.Entities;
using Limak.Persistence.DAL;
using Limak.Persistence.Implementations.Repositories.Generic;

namespace Limak.Persistence.Implementations.Repositories;

public class GenderRepository:Repository<Gender>,IGenderRepository
{
    public GenderRepository(AppDbContext context):base(context)
    {
        
    }
}
