# Owl Cafe - Quick Start Guide

## ?? Getting Started in 5 Minutes

### Step 1: Prerequisites ?
Ensure you have installed:
- .NET 8 SDK ([Download](https://dotnet.microsoft.com/download))
- Visual Studio 2022 or VS Code

### Step 2: Clone & Open
```bash
# Clone the repository
git clone <repository-url>
cd OwlCafe

# Open in Visual Studio or VS Code
code .
```

### Step 3: Build & Run
```bash
# Restore packages
dotnet restore

# Build solution
dotnet build

# Run application
dotnet run
```

### Step 4: Access the Application
- **Website**: https://localhost:5001
- **Admin Panel**: https://localhost:5001/admin/dashboard

### Step 5: Login as Admin
```
Email:    admin@owlcafe.com
Password: Admin@123456
```

---

## ?? Key URLs

| Page | URL |
|------|-----|
| Home | / |
| About | /home/about |
| Menu | /menu |
| Gallery | /gallery |
| Reserve Table | /booking/create |
| Contact | /contact/create |
| Admin Dashboard | /admin/dashboard |
| Categories | /admin/categories |
| Menu Items | /admin/menuitems |
| Bookings | /admin/bookings |
| Tables | /admin/tables |

---

## ?? First Tasks to Try

### 1. Add a Menu Item
1. Go to `/admin/menuitems`
2. Click "Add Item"
3. Fill in details:
   - Name: "Biryani"
   - Category: "Indian"
   - Price: $15.99
   - Type: Non-Vegetarian
4. Click Save

### 2. Create a Booking
1. Go to `/booking/create`
2. Fill in:
   - Name: "John Doe"
   - Email: "john@example.com"
   - Phone: "1234567890"
   - Guests: 4
   - Date: Tomorrow
   - Time: 7:00 PM
3. Click "Confirm Booking"

### 3. View Booking in Admin
1. Go to `/admin/bookings`
2. See your booking in the list
3. Click Edit to approve/reject

### 4. Update Website Settings
1. Go to `/admin/settings`
2. Update restaurant info
3. Add social media links
4. Click Save

---

## ?? Project Structure Quick Reference

```
OwlCafe/
??? Controllers/          # Public pages controllers
??? Areas/Admin/          # Admin panel (separate area)
??? Models/              # Database entities
??? Repositories/        # Data access layer
??? Views/               # UI templates
??? Program.cs           # Application startup
??? README.md            # Full documentation
```

---

## ?? Common Modifications

### Change Restaurant Name
Edit `/Areas/Admin/Views/Shared/_Layout.cshtml` line 7:
```html
<h4><i class="fas fa-leaf"></i> Owl Admin</h4>
```

### Modify Color Scheme
Edit `/wwwroot/css/site.css` CSS variables (lines 5-13):
```css
--primary: #0F5C4D;           /* Change primary color */
--accent-gold: #D4A574;       /* Change accent color */
--cream: #F5F3F0;             /* Change background */
```

### Add New Menu Category
1. Go to Admin > Categories
2. Click "Add Category"
3. Enter name, description, display order
4. Click Save

### Customize Admin Sidebar
Edit `/Areas/Admin/Views/Shared/_Layout.cshtml` navigation section to add/remove menu items

---

## ??? Database

### Default Connection
SQLite database file: `owlcafe.db` (created automatically)

### Switch to SQL Server
1. Update `appsettings.json`:
```json
"ConnectionConnection": "Server=.;Database=OwlCafe;Trusted_Connection=true;"
```

2. Update `Program.cs`:
```csharp
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
```

---

## ?? Default Seeded Data

Automatically created on first run:
- ? Admin user: admin@owlcafe.com / Admin@123456
- ? 6 food categories
- ? 6 sample menu items
- ? 9 restaurant tables
- ? Default website settings

---

## ?? Troubleshooting

### "Address already in use"
Port 5001 is in use. Change in `launchSettings.json`:
```json
"applicationUrl": "https://localhost:5002"
```

### "Database locked"
Ensure no other instances are running. Delete `owlcafe.db` and restart.

### "Login fails"
Check database was seeded properly:
1. Delete `owlcafe.db`
2. Restart application
3. Try default credentials again

### "Images not showing"
Add valid image URLs to menu items or replace with placeholder URLs.

---

## ?? Next Steps

1. **Customize Design**
   - Update colors in `site.css`
   - Modify logo/images
   - Change restaurant name

2. **Add Menu Items**
   - Create categories
   - Add 20+ menu items
   - Upload/link item images

3. **Configure Settings**
   - Set opening hours
   - Add social media links
   - Configure contact info

4. **Test Features**
   - Create test bookings
   - Verify email notifications
   - Test all admin functions

5. **Prepare for Production**
   - Switch to SQL Server
   - Enable HTTPS
   - Set up logging
   - Configure backups

---

## ?? Tips & Tricks

- **Bulk Import**: Add menu items via Admin panel one by one
- **Table Management**: Create tables with different capacities (2, 4, 6 persons)
- **Featured Items**: Mark top 6 items as "Featured" to show on home page
- **Booking Workflow**: Bookings start as "Pending" ? Admin approves ? Shows as "Approved"
- **Gallery**: Organize images by category (Restaurant, Dishes, Chef, Events)

---

## ?? Important Notes

?? **Security**
- Change admin password immediately after first login
- Use HTTPS in production
- Keep .NET Framework updated

?? **Performance**
- Use pagination for large datasets
- Optimize images before upload
- Set up caching for frequently accessed data

?? **Maintenance**
- Regular database backups
- Monitor log files
- Clear old contact messages periodically

---

## ?? Need Help?

- Check the full README.md for detailed documentation
- Review inline code comments
- Check Serilog logs in `/logs` directory
- Refer to ASP.NET Core documentation

---

**Happy Building! ??**
