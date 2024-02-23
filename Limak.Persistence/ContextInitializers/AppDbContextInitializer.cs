using Limak.Application.Abstractions.Services;
using Limak.Domain.Entities;
using Limak.Domain.Enums;
using Limak.Persistence.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Limak.Persistence.ContextInitializers;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly IWarehouseService _warehouseService;
    private readonly IGenderService _genderService;
    private readonly IUserPositionService _userPositionService;
    private readonly ICitizenshipService _citizenshipService;
    public AppDbContextInitializer(AppDbContext context, RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration, UserManager<AppUser> userManager, IWarehouseService warehouseService, IGenderService genderService, IUserPositionService userPositionService, ICitizenshipService citizenshipService)
    {
        _context = context;
        _roleManager = roleManager;
        _configuration = configuration;
        _userManager = userManager;
        _warehouseService = warehouseService;
        _genderService = genderService;
        _userPositionService = userPositionService;
        _citizenshipService = citizenshipService;
    }

    public async Task InitializeDbContextAsync()
    {
        await _context.Database.MigrateAsync();
    }

    public async Task CreateUserRolesAsync()
    {
        foreach (var role in Enum.GetValues(typeof(IdentityRoles)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
                await _roleManager.CreateAsync(new IdentityRole<int> { Name = role.ToString() });
        }
    }

    public async Task InitializeAdminAsync()
    {
        var gender = await _genderService.FirstOrDefault();
        var warehouse = await _warehouseService.FirstOrDefault();
        var citizenship=await _citizenshipService.FirstOrDefault();
        var userPosition = await _userPositionService.FirstOrDefault();


        AppUser admin = new AppUser
        {
            Name = "Admin",
            Surname = "Admin",
            Email = _configuration["AdminSettings:Email"],
            FinCode= "AAAAAAA",
            SeriaNumber= "AA1234567",
            GenderId=gender.Id,
            WarehouseId=warehouse.Id,
            UserPositionId=userPosition.Id,
            CitizenshipId=citizenship.Id,
            Birtday=DateTime.UtcNow,
            Location="Admin",
            PhoneNumber="+994555555555",
            UserName = _configuration["AdminSettings:UserName"],
        };

        await _userManager.CreateAsync(admin, _configuration["AdminSettings:Password"]);
        await _userManager.AddToRoleAsync(admin, IdentityRoles.Admin.ToString());
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(admin);
        await _userManager.ConfirmEmailAsync(admin, token);
    }
}
