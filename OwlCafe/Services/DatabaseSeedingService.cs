using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OwlCafe.Data;
using OwlCafe.Models;

namespace OwlCafe.Services
{
    public class DatabaseSeedingService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DatabaseSeedingService> _logger;

        public DatabaseSeedingService(IServiceProvider serviceProvider, ILogger<DatabaseSeedingService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task InitializeDatabaseAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            try
            {
                // Apply migrations
                await context.Database.EnsureCreatedAsync();

                // Seed roles
                await SeedRolesAsync(roleManager);

                // Seed admin user
                await SeedAdminUserAsync(userManager);

                // Seed categories
                await SeedCategoriesAsync(context);

                // Seed menu items
                await SeedMenuItemsAsync(context);

                // Seed tables
                await SeedTablesAsync(context);

                // Seed website settings
                await SeedWebsiteSettingsAsync(context);

                _logger.LogInformation("Database seeding completed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while seeding the database");
            }
        }

        private async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    _logger.LogInformation($"Role '{role}' created");
                }
            }
        }

        private async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            const string adminEmail = "admin@owlcafe.com";
            const string adminPassword = "Admin@123456";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User"
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    _logger.LogInformation("Admin user created successfully");
                }
                else
                {
                    _logger.LogWarning($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        private async Task SeedCategoriesAsync(ApplicationDbContext context)
        {
            if (context.Categories.Any())
                return;

            var categories = new[]
            {
                new Category { Name = "Chinese", Description = "Authentic Chinese cuisine", DisplayOrder = 1, Status = true },
                new Category { Name = "Indian", Description = "Traditional Indian dishes", DisplayOrder = 2, Status = true },
                new Category { Name = "Italian", Description = "Classic Italian specialties", DisplayOrder = 3, Status = true },
                new Category { Name = "Pizza", Description = "Wood-fired pizzas", DisplayOrder = 4, Status = true },
                new Category { Name = "Desserts", Description = "Sweet delicacies", DisplayOrder = 5, Status = true },
                new Category { Name = "Beverages", Description = "Hot and cold drinks", DisplayOrder = 6, Status = true }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
            _logger.LogInformation($"Seeded {categories.Length} categories");
        }

        private async Task SeedMenuItemsAsync(ApplicationDbContext context)
        {
            if (context.MenuItems.Any())
                return;

            var categories = await context.Categories.ToListAsync();
            var chineseId = categories.FirstOrDefault(c => c.Name == "Chinese")?.Id ?? 1;
            var indianId = categories.FirstOrDefault(c => c.Name == "Indian")?.Id ?? 2;
            var pizzaId = categories.FirstOrDefault(c => c.Name == "Pizza")?.Id ?? 4;

            var menuItems = new[]
            {
                new MenuItem { CategoryId = chineseId, Name = "Fried Rice", Description = "Aromatic fried rice with vegetables and proteins", Price = 12.99m, IsVeg = true, IsAvailable = true, IsFeatured = true, PreparationTime = 15, Status = true, DisplayOrder = 1 },
                new MenuItem { CategoryId = chineseId, Name = "Hakka Noodles", Description = "Stir-fried noodles with fresh vegetables", Price = 10.99m, IsVeg = true, IsAvailable = true, IsFeatured = false, PreparationTime = 12, Status = true, DisplayOrder = 2 },
                new MenuItem { CategoryId = indianId, Name = "Butter Chicken", Description = "Creamy tomato-based chicken curry", Price = 14.99m, IsVeg = false, IsAvailable = true, IsFeatured = true, PreparationTime = 20, Status = true, DisplayOrder = 3 },
                new MenuItem { CategoryId = indianId, Name = "Paneer Tikka", Description = "Grilled cottage cheese with Indian spices", Price = 11.99m, IsVeg = true, IsAvailable = true, IsFeatured = true, PreparationTime = 18, Status = true, DisplayOrder = 4 },
                new MenuItem { CategoryId = pizzaId, Name = "Margherita Pizza", Description = "Fresh mozzarella, tomato, and basil", Price = 13.99m, IsVeg = true, IsAvailable = true, IsFeatured = true, PreparationTime = 15, Status = true, DisplayOrder = 5 },
                new MenuItem { CategoryId = pizzaId, Name = "Pepperoni Pizza", Description = "Loaded with pepperoni and cheese", Price = 14.99m, IsVeg = false, IsAvailable = true, IsFeatured = false, PreparationTime = 15, Status = true, DisplayOrder = 6 }
            };

            await context.MenuItems.AddRangeAsync(menuItems);
            await context.SaveChangesAsync();
            _logger.LogInformation($"Seeded {menuItems.Length} menu items");
        }

        private async Task SeedTablesAsync(ApplicationDbContext context)
        {
            if (context.RestaurantTables.Any())
                return;

            var tables = new List<RestaurantTable>();

            // Create tables for 2, 4, 6 persons
            int tableId = 1;
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    tables.Add(new RestaurantTable
                    {
                        TableNumber = tableId++,
                        Capacity = i * 2,
                        Location = $"Section {(char)(64 + j)}",
                        Status = TableStatus.Available
                    });
                }
            }

            await context.RestaurantTables.AddRangeAsync(tables);
            await context.SaveChangesAsync();
            _logger.LogInformation($"Seeded {tables.Count} restaurant tables");
        }

        private async Task SeedWebsiteSettingsAsync(ApplicationDbContext context)
        {
            if (context.WebsiteSettings.Any())
                return;

            var settings = new WebsiteSetting
            {
                RestaurantName = "Owl Cafe",
                Phone = "+1 (555) 123-4567",
                Email = "info@owlcafe.com",
                Address = "123 Main Street, City, State 12345",
                OpeningHours = "11:00 AM - 11:00 PM",
                Instagram = "https://instagram.com/owlcafe",
                Facebook = "https://facebook.com/owlcafe",
                WhatsApp = "+1 (555) 123-4567",
                SEODescription = "Premium restaurant offering Chinese, Indian, Italian, and Pizza cuisine",
                SEOKeywords = "restaurant, dining, food, cafe"
            };

            await context.WebsiteSettings.AddAsync(settings);
            await context.SaveChangesAsync();
            _logger.LogInformation("Website settings seeded");
        }
    }
}
