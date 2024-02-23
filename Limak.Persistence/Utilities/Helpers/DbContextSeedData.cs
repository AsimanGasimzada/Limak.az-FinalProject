using Limak.Domain.Entities;
using Limak.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Utilities.Helpers;

public static class DbContextSeedData
{
    public static void AddSeedData(this ModelBuilder builder)
    {
        InitCompany(builder);
        AddStatuses(builder);
        AddCountries(builder);
        AddUserPositions(builder);
        AddCitizenships(builder);
        AddSettings(builder);
        AddDefaultWarehouse(builder);


    }

    private static void AddSettings(ModelBuilder builder)
    {
        builder.Entity<Setting>().HasData(
         new Setting { Id = 1, Key = "HeaderLogo", Value = "Image" },
         new Setting { Id = 2, Key = "FooterLogo", Value = "Image" },
         new Setting { Id = 3, Key = "PhoneNumber", Value = "number" },

        //turkey
         new Setting { Id = 4, Key = "XaricdekiUnvanlar-AdSoyad", Value = "0256069 - LİMAK TAŞIMACILIK DIŞ TİCARET LİMİTED ŞİRKETİ" },
         new Setting { Id = 5, Key = "XaricdekiUnvanlar-AdressBasligi", Value = "LIMAK" },
         new Setting { Id = 6, Key = "XaricdekiUnvanlar-AdressSatir", Value = "Gürsel mah.Bahçeler cad. Erdoğan iş merkezi 37-39B" },
         new Setting { Id = 7, Key = "XaricdekiUnvanlar-IlSehir", Value = "Istanbul" },
         new Setting { Id = 8, Key = "XaricdekiUnvanlar-Semt", Value = "Gürsel" },
         new Setting { Id = 9, Key = "XaricdekiUnvanlar-TCKimlik", Value = "35650276048" },
         new Setting { Id = 10, Key = "XaricdekiUnvanlar-İlce", Value = "Kağıthane" },
         new Setting { Id = 11, Key = "XaricdekiUnvanlar-PostKodu", Value = "34400" },
         new Setting { Id = 12, Key = "XaricdekiUnvanlar-Telefon", Value = "05364700021" },
         new Setting { Id = 13, Key = "XaricdekiUnvanlar-VergiDairesi", Value = "Şişli" },
         new Setting { Id = 14, Key = "XaricdekiUnvanlar-Ulke", Value = "Türkiye" },
         new Setting { Id = 15, Key = "XaricdekiUnvanlar-VergiNo", Value = "6081089593" },
         new Setting { Id = 16, Key = "XaricdekiUnvanlar-TrendyolSMS", Value = "05356191259" },

         //America
         new Setting { Id = 17, Key = "XaricdekiUnvanlar-AddressLine1", Value = "320 Cornell drive C1" },
         new Setting { Id = 18, Key = "XaricdekiUnvanlar-State", Value = "Delaware (DE) " },
         new Setting { Id = 19, Key = "XaricdekiUnvanlar-City", Value = "Wilmington " },
         new Setting { Id = 20, Key = "XaricdekiUnvanlar-Postalcode", Value = "19801" },
         new Setting { Id = 21, Key = "XaricdekiUnvanlar-Country", Value = "United States" },
         new Setting { Id = 22, Key = "XaricdekiUnvanlar-PhoneNumber", Value = "862-5718476" }

         );
    }

    private static void AddDefaultWarehouse(ModelBuilder builder)
    {
        builder.Entity<Warehouse>().HasData(
            new Warehouse
            {
                Id = 1,
                CreatedBy = "Admin",
                ModifiedBy = "Admin",
                WorkingHours = "Default warehouse",
                CreatedTime = DateTime.MinValue,
                ModifiedTime = DateTime.MinValue,
                DeliveryAreas = new List<DeliveryArea>() { },
                Email = "Default@gmail.com",
                IsDeleted = true,
                Location = "Default",
                Name = "Default",
                Orders = new List<Order>(),
                PhoneNumber = "Default",
                Position = "Default"
            });
    }

    private static void InitCompany(ModelBuilder builder)
    {
        builder.Entity<Company>().HasData(
          new Company { Id = 1, TRYBalance = 0, AZNBalance = 0, USDBalance = 0 }
          );
    }

    private static void AddCountries(ModelBuilder builder)
    {
        builder.Entity<Country>().HasData(
          new Country { Id = 1, Name = CountryNames.Turkey },
          new Country { Id = 2, Name = CountryNames.America }
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
