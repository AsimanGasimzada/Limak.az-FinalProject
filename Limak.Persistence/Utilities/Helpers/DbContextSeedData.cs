using Limak.Domain.Entities;
using Limak.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Utilities.Helpers;

public static class DbContextSeedData
{
    public static void AddSeedData(this ModelBuilder builder)
    {
        AddStatuses(builder);
        AddCountries(builder);
        AddUserPositions(builder);
        AddCitizenships(builder);

    }

    private static void AddCountries(ModelBuilder builder)
    {
        builder.Entity<Country>().HasData(
          new Country { Id = 1, Name = CountryNames.Turkey},
          new Country { Id = 2, Name = CountryNames.America}
          );
    }

    private static void AddUserPositions(ModelBuilder builder)
    {
        builder.Entity<UserPosition>().HasData(
         new UserPosition { Id = 1, Name = UserPositionNames.Individual },
         new UserPosition { Id = 2, Name = UserPositionNames.Civilian }
         );
    }

    private static void AddCitizenships(ModelBuilder builder)
    {
        builder.Entity<Citizenship>().HasData(
                 new Citizenship { Id = 1, Name = CitizenshipNames.Azerbaijani },
                 new Citizenship { Id = 2, Name = CitizenshipNames.Other }
                 );
    }

    private static void AddStatuses(ModelBuilder builder)
    {
        builder.Entity<Status>().HasData(
                  new Status { Id = 1, Name = StatusNames.Paid },
                  new Status { Id = 2, Name = StatusNames.Ordered },
                  new Status { Id = 3, Name = StatusNames.NotOrdered },
                  new Status { Id = 4, Name = StatusNames.ForeignWarehouse },
                  new Status { Id = 5, Name = StatusNames.Customs },
                  new Status { Id = 6, Name = StatusNames.OnTheWay },
                  new Status { Id = 7, Name = StatusNames.LocalWarehouse },
                  new Status { Id = 8, Name = StatusNames.Delivery },
                  new Status { Id = 9, Name = StatusNames.Kargomat },
                  new Status { Id = 10, Name = StatusNames.OrderIsDone },
                  new Status { Id = 11, Name = StatusNames.IsCanceled }
                  );
    }
}
