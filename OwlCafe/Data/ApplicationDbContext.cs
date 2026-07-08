using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OwlCafe.Models;

namespace OwlCafe.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<MenuItem> MenuItems { get; set; } = null!;
        public DbSet<RestaurantTable> RestaurantTables { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<GalleryImage> GalleryImages { get; set; } = null!;
        public DbSet<ContactMessage> ContactMessages { get; set; } = null!;
        public DbSet<WebsiteSetting> WebsiteSettings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Category
            builder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Image).HasMaxLength(255);
            });

            // Configure MenuItem
            builder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Image).HasMaxLength(255);
                entity.Property(e => e.Price).HasPrecision(10, 2);
                entity.Property(e => e.DiscountPrice).HasPrecision(10, 2);
                entity.HasOne(e => e.Category)
                    .WithMany(c => c.MenuItems)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure RestaurantTable
            builder.Entity<RestaurantTable>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TableNumber).IsRequired();
                entity.Property(e => e.Location).IsRequired().HasMaxLength(100);
            });

            // Configure Booking
            builder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SpecialRequest).HasMaxLength(500);
                entity.HasOne(e => e.Table)
                    .WithMany(t => t.Bookings)
                    .HasForeignKey(e => e.TableId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure GalleryImage
            builder.Entity<GalleryImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Image).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Category).HasMaxLength(50);
            });

            // Configure ContactMessage
            builder.Entity<ContactMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Message).IsRequired();
            });

            // Configure WebsiteSetting
            builder.Entity<WebsiteSetting>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RestaurantName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Logo).HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.OpeningHours).HasMaxLength(200);
            });
        }
    }
}
