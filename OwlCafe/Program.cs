using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using OwlCafe.Data;
using OwlCafe.Models;
using OwlCafe.Repositories;
using OwlCafe.Repositories.Interfaces;
using OwlCafe.Services;
using OwlCafe.Validators;
using OwlCafe.DTOs;
using FluentValidation;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/owl-cafe-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add Serilog
    builder.Host.UseSerilog();

    // Add DbContext
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Add Identity
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

    // Add AutoMapper
    builder.Services.AddAutoMapper(typeof(OwlCafe.Mappings.MappingProfile));

    // Add FluentValidation
    builder.Services.AddScoped<IValidator<BookingDto>, BookingDtoValidator>();

    // Add Repositories
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
    builder.Services.AddScoped<IBookingRepository, BookingRepository>();
    builder.Services.AddScoped<IRestaurantTableRepository, RestaurantTableRepository>();
    builder.Services.AddScoped<IGalleryImageRepository, GalleryImageRepository>();
    builder.Services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
    builder.Services.AddScoped<IWebsiteSettingRepository, WebsiteSettingRepository>();

    // Add Services
    builder.Services.AddScoped<DatabaseSeedingService>();

    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Seed database
    using (var scope = app.Services.CreateScope())
    {
        var seedingService = scope.ServiceProvider.GetRequiredService<DatabaseSeedingService>();
        await seedingService.InitializeDatabaseAsync();
    }

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "admin",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
