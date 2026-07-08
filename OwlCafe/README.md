# Owl Cafe - Restaurant Management Website
## Production-Grade ASP.NET Core MVC (.NET 8)

---

## ?? Project Overview

**Owl Cafe** is a complete, production-ready restaurant management website built with **ASP.NET Core MVC (.NET 8)**. It features a premium design with dark green and gold accents, a comprehensive admin panel, and a full-featured booking system.

### Key Features
? Premium public website  
? Complete admin dashboard  
? Menu management system  
? Table reservation system  
? Admin user authentication & authorization  
? Contact message management  
? Gallery management  
? Responsive design  
? Clean architecture  
? Repository pattern implementation  

---

## ??? Project Architecture

### Technology Stack
- **Backend**: ASP.NET Core MVC (.NET 8)
- **Database**: SQLite (easily switchable to SQL Server/PostgreSQL)
- **ORM**: Entity Framework Core 8
- **Authentication**: ASP.NET Identity
- **Mapping**: AutoMapper
- **Validation**: FluentValidation
- **Logging**: Serilog
- **Frontend**: HTML5, Bootstrap 5, Vanilla JavaScript, Font Awesome

### Project Structure

```
OwlCafe/
??? Models/                          # Entity Models
?   ??? Category.cs
?   ??? MenuItem.cs
?   ??? RestaurantTable.cs
?   ??? Booking.cs
?   ??? GalleryImage.cs
?   ??? ContactMessage.cs
?   ??? WebsiteSetting.cs
?   ??? ApplicationUser.cs
??? Data/
?   ??? ApplicationDbContext.cs      # EF Core DbContext
??? Repositories/                    # Repository Pattern
?   ??? Interfaces/
?   ?   ??? IGenericRepository.cs
?   ?   ??? ICategoryRepository.cs
?   ?   ??? IMenuItemRepository.cs
?   ?   ??? IBookingRepository.cs
?   ?   ??? [others...]
?   ??? Implementations/
?       ??? GenericRepository.cs
?       ??? CategoryRepository.cs
?       ??? [others...]
??? Controllers/                     # Public Controllers
?   ??? HomeController.cs
?   ??? MenuController.cs
?   ??? BookingController.cs
?   ??? ContactController.cs
?   ??? GalleryController.cs
??? Areas/Admin/                     # Admin Panel
?   ??? Controllers/
?   ?   ??? DashboardController.cs
?   ?   ??? CategoriesController.cs
?   ?   ??? MenuItemsController.cs
?   ?   ??? TablesController.cs
?   ?   ??? BookingsController.cs
?   ?   ??? GalleryController.cs
?   ?   ??? ContactMessagesController.cs
?   ?   ??? SettingsController.cs
?   ??? Views/
?       ??? Dashboard/
?       ??? Categories/
?       ??? [others...]
??? DTOs/                            # Data Transfer Objects
?   ??? CategoryDto.cs
?   ??? MenuItemDto.cs
?   ??? BookingDto.cs
??? Validators/                      # Fluent Validators
?   ??? BookingDtoValidator.cs
??? Mappings/                        # AutoMapper Profiles
?   ??? MappingProfile.cs
??? Services/                        # Business Logic
?   ??? DatabaseSeedingService.cs
??? Views/                           # Public Views
?   ??? Home/
?   ?   ??? Index.cshtml
?   ?   ??? About.cshtml
?   ??? Menu/
?   ??? Booking/
?   ??? Gallery/
?   ??? Contact/
?   ??? Shared/
??? wwwroot/                         # Static Files
?   ??? css/
?   ?   ??? site.css                # Premium Theme Styles
?   ??? js/
?   ?   ??? site.js
?   ??? images/
??? Program.cs                       # Startup Configuration
??? appsettings.json                # Configuration

```

---

## ?? Design System

### Color Scheme
```css
Primary: #0F5C4D (Dark Green)
Primary Dark: #0D4A3D
Primary Light: #1A7A6E
Accent Gold: #D4A574
Cream Background: #F5F3F0
Dark Text: #1F1F1F
```

### Typography
- **Headlines**: Playfair Display (serif)
- **Body**: Poppins (sans-serif)
- **Accents**: Lora (serif)

### Components
- Glass morphism cards
- Smooth animations
- Responsive grid layouts
- Premium shadows and effects

---

## ?? Database Models

### Category
- `Id` (Primary Key)
- `Name` (string, max 100)
- `Description` (string)
- `Image` (URL)
- `DisplayOrder` (int)
- `Status` (bool)
- `CreatedDate` (DateTime)
- `UpdatedDate` (DateTime)

### MenuItem
- `Id` (Primary Key)
- `CategoryId` (Foreign Key)
- `Name` (string, max 100)
- `Description` (string)
- `Price` (decimal)
- `DiscountPrice` (decimal, nullable)
- `Image` (URL)
- `IsVeg` (bool)
- `IsAvailable` (bool)
- `IsFeatured` (bool)
- `PreparationTime` (int - minutes)
- `Status` (bool)
- `DisplayOrder` (int)
- `CreatedDate` (DateTime)
- `UpdatedDate` (DateTime)

### RestaurantTable
- `Id` (Primary Key)
- `TableNumber` (int)
- `Capacity` (int)
- `Location` (string)
- `Status` (TableStatus enum)
- `CreatedDate` (DateTime)

### Booking
- `Id` (Primary Key)
- `CustomerName` (string)
- `Phone` (string)
- `Email` (string)
- `BookingDate` (DateTime)
- `BookingTime` (TimeSpan)
- `Guests` (int)
- `TableId` (Foreign Key, nullable)
- `SpecialRequest` (string, nullable)
- `Status` (BookingStatus enum)
- `CreatedDate` (DateTime)
- `UpdatedDate` (DateTime, nullable)

### GalleryImage
- `Id` (Primary Key)
- `Title` (string)
- `Image` (URL)
- `Category` (string)
- `DisplayOrder` (int)
- `Status` (bool)
- `CreatedDate` (DateTime)

### ContactMessage
- `Id` (Primary Key)
- `Name` (string)
- `Email` (string)
- `Phone` (string)
- `Subject` (string)
- `Message` (string)
- `CreatedDate` (DateTime)
- `IsRead` (bool)

### WebsiteSetting
- `Id` (Primary Key)
- `RestaurantName` (string)
- `Logo` (URL, nullable)
- `Phone` (string)
- `Email` (string)
- `Address` (string)
- `OpeningHours` (string)
- `Facebook` (URL, nullable)
- `Instagram` (URL, nullable)
- `WhatsApp` (string, nullable)
- `GoogleMap` (iframe embed, nullable)
- `SEODescription` (string, nullable)
- `SEOKeywords` (string, nullable)
- `UpdatedDate` (DateTime)

---

## ?? Public Website Pages

### Home Page
- Hero section with CTA
- Featured menu items showcase
- Why choose us section
- Call to action

### About Page
- Restaurant story
- Mission & Vision
- Chef information
- Statistics & achievements
- Core values

### Menu Page
- Dynamic menu from database
- Filter by category
- Search functionality
- Veg/Non-veg indicators
- Price display

### Gallery Page
- Image grid
- Category filtering
- Lightbox viewer
- Responsive layout

### Reservation Page
- Booking form
- Date/time picker
- Guest selection
- Special requests
- Table availability check

### Contact Page
- Contact form
- Location information
- Google Map embed
- Social media links
- Opening hours

---

## ?? Admin Panel

### Authentication
- Admin login required
- Role-based authorization
- Secure password hashing
- Session management

### Dashboard
- Summary statistics
- Today's bookings
- Recent activity
- Quick action buttons

### Admin Modules

#### 1. Category Management
- ? Create/Read/Update/Delete categories
- Display order management
- Status toggle
- Bulk operations ready

#### 2. Menu Item Management
- ? Full CRUD operations
- Category assignment
- Price management (regular + discount)
- Image URL upload
- Veg/Non-veg classification
- Featured item flagging
- Preparation time tracking
- Pagination & sorting

#### 3. Table Management
- ? Create/Read/Update/Delete tables
- Capacity management
- Location tracking
- Status management (Available/Reserved/Maintenance)

#### 4. Booking Management
- ? View all bookings
- Approve/Reject bookings
- Complete/Cancel bookings
- Assign tables
- Filter by status
- Search functionality

#### 5. Gallery Management
- ? Upload images
- Category organization
- Display order management
- Status toggle

#### 6. Contact Messages
- ? View customer messages
- Mark as read/unread
- Reply management
- Delete messages

#### 7. Website Settings
- Restaurant name & logo
- Contact information
- Address & location
- Opening hours
- Social media links
- Google Map embed
- SEO metadata

---

## ?? Getting Started

### Prerequisites
- .NET 8 SDK or higher
- Visual Studio 2022 / Visual Studio Code
- SQLite (included with EF Core)

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/OwlCafe.git
cd OwlCafe
```

2. **Restore dependencies**
```bash
dotnet restore
```

3. **Build the solution**
```bash
dotnet build
```

4. **Run the application**
```bash
dotnet run
```

The application will be available at `https://localhost:5001`

### Default Credentials
- **Email**: admin@owlcafe.com
- **Password**: Admin@123456

---

## ?? API Endpoints

### Public Endpoints
```
GET  /                              # Home page
GET  /home/about                    # About page
GET  /menu                          # Menu page
GET  /gallery                       # Gallery page
GET  /booking/create                # Booking form
POST /booking/create                # Submit booking
GET  /contact/create                # Contact form
POST /contact/create                # Submit message
```

### Admin Endpoints (Requires Admin Role)
```
GET  /admin/dashboard               # Dashboard
GET  /admin/categories              # Categories list
GET  /admin/menuItems               # Menu items list
GET  /admin/tables                  # Tables list
GET  /admin/bookings                # Bookings list
GET  /admin/gallery                 # Gallery list
GET  /admin/contactMessages         # Messages list
GET  /admin/settings                # Settings
```

---

## ??? Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=owlcafe.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Database Connection
The application uses SQLite by default. To switch to SQL Server:

```csharp
// In Program.cs, replace:
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))

// With:
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
```

---

## ?? Dependency Injection

All repositories and services are registered using dependency injection:

```csharp
// Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
// ... and more
```

---

## ??? Repository Pattern

The repository pattern abstracts data access logic:

```csharp
public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task<bool> SaveChangesAsync();
    IQueryable<T> GetQueryable();
}
```

---

## ? Data Validation

### Booking Validation (FluentValidation)
```
- Customer name: Required, minimum 3 characters
- Email: Required, valid format
- Phone: Required, minimum 10 digits
- Booking date: Today or in future
- Guests: 1-20 maximum
```

---

## ?? Seeded Data

The application automatically seeds the database on first run with:
- 2 admin roles (Admin, Customer)
- 1 admin user
- 6 food categories (Chinese, Indian, Italian, Pizza, Desserts, Beverages)
- 6 sample menu items
- 9 restaurant tables
- Default website settings

---

## ?? Security Features

? **Authentication & Authorization**
- ASP.NET Identity integration
- Role-based access control
- Secure password hashing

? **Input Validation**
- Server-side validation
- Client-side validation
- Anti-forgery tokens

? **Data Protection**
- HTTPS enforced
- HSTS headers
- Secure cookie handling

? **Logging & Monitoring**
- Serilog integration
- Request/response logging
- Error tracking

---

## ?? Responsive Design

The website is fully responsive across all devices:
- Desktop (1200px+)
- Tablet (768px - 1199px)
- Mobile (< 768px)

Media queries are built into the CSS framework for automatic adaptation.

---

## ?? Performance Optimization

- Async/await throughout
- Database query optimization
- Lazy loading with Include()
- Pagination for large datasets
- Caching where appropriate
- Minimal JavaScript
- Optimized CSS

---

## ?? Deployment

### Local Development
```bash
dotnet run
```

### Production Build
```bash
dotnet publish -c Release -o ./publish
```

### Docker Support (Coming Soon)
Dockerfile configuration can be added for containerization.

---

## ?? Logging

Logs are stored in the `/logs` directory with daily rotation:
```
logs/
??? owl-cafe-20240101.txt
??? owl-cafe-20240102.txt
??? ...
```

---

## ?? Troubleshooting

### Issue: Database not created
**Solution**: Run `dotnet build` to ensure the database is created automatically on first run.

### Issue: Admin login fails
**Solution**: Verify credentials (admin@owlcafe.com / Admin@123456) or check the database seeding.

### Issue: Images not displaying
**Solution**: Update image URLs in the database to valid image sources.

---

## ?? Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Bootstrap Documentation](https://getbootstrap.com/docs/)
- [Font Awesome Icons](https://fontawesome.com/icons/)

---

## ?? License

This project is provided as-is for educational and commercial purposes.

---

## ????? Project Status

**Current Phase**: Phase 3 Complete ?
- ? Phase 1: Project Setup & Configuration
- ? Phase 2: Public Website UI
- ? Phase 3: Admin Panel & CRUD Operations
- ? Phase 4: Advanced Features & Refinements
- ? Phase 5: Testing & Deployment

---

## ?? Contributing

To contribute to this project:
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

---

## ?? Support

For issues, questions, or suggestions, please create an issue in the repository.

---

**Built with ?? using ASP.NET Core MVC**
