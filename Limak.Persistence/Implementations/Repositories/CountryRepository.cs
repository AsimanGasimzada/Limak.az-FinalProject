using Limak.Application.Abstractions.Repositories;
using Limak.Domain.Entities;
using Limak.Persistence.DAL;
using Limak.Persistence.Implementations.Repositories.Generic;

namespace Limak.Persistence.Implementations.Repositories;

public class CountryRepository:Repository<Country>,ICountryRepository
{
    public CountryRepository(AppDbContext context):base(context)
    {
        
    }
}
