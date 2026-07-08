# Owl Cafe - API & Routes Documentation

## ?? Complete Route Map

### ?? Public Routes (No Authentication Required)

#### Home & Pages
```
GET     /                          HomeController.Index
GET     /home/about                HomeController.About
GET     /home/privacy              HomeController.Privacy
GET     /home/error                HomeController.Error
```

#### Menu
```
GET     /menu                      MenuController.Index
GET     /menu?categoryId=1         MenuController.Index (filtered by category)
GET     /menu/{id}                 MenuController.Details
```

#### Gallery
```
GET     /gallery                   GalleryController.Index
GET     /gallery?category=Food     GalleryController.Index (filtered by category)
```

#### Bookings
```
GET     /booking/create            BookingController.Create (form)
POST    /booking/create            BookingController.Create (submit)
GET     /booking/confirmation/{id} BookingController.Confirmation
GET     /api/available-tables      BookingController.GetAvailableTables (AJAX)
```

Query Parameters for GetAvailableTables:
```
date=2024-01-15
time=19:00:00
guests=4
```

#### Contact
```
GET     /contact/create            ContactController.Create (form)
POST    /contact/create            ContactController.Create (submit)
```

---

### ?? Admin Routes (Authentication Required - Admin Role)

#### Dashboard
```
GET     /admin/dashboard           DashboardController.Index
```

#### Categories Management
```
GET     /admin/categories                          CategoriesController.Index
GET     /admin/categories/create                   CategoriesController.Create (form)
POST    /admin/categories/create                   CategoriesController.Create (submit)
GET     /admin/categories/edit/{id}                CategoriesController.Edit (form)
POST    /admin/categories/edit/{id}                CategoriesController.Edit (submit)
POST    /admin/categories/delete?id={id}           CategoriesController.Delete
```

#### Menu Items Management
```
GET     /admin/menuitems                           MenuItemsController.Index
GET     /admin/menuitems?page=1                    MenuItemsController.Index (paginated)
GET     /admin/menuitems/create                    MenuItemsController.Create (form)
POST    /admin/menuitems/create                    MenuItemsController.Create (submit)
GET     /admin/menuitems/edit/{id}                 MenuItemsController.Edit (form)
POST    /admin/menuitems/edit/{id}                 MenuItemsController.Edit (submit)
POST    /admin/menuitems/delete?id={id}            MenuItemsController.Delete
```

#### Tables Management
```
GET     /admin/tables                              TablesController.Index
GET     /admin/tables/create                       TablesController.Create (form)
POST    /admin/tables/create                       TablesController.Create (submit)
GET     /admin/tables/edit/{id}                    TablesController.Edit (form)
POST    /admin/tables/edit/{id}                    TablesController.Edit (submit)
POST    /admin/tables/delete?id={id}               TablesController.Delete
```

#### Bookings Management
```
GET     /admin/bookings                            BookingsController.Index
GET     /admin/bookings?status=Pending             BookingsController.Index (filtered)
GET     /admin/bookings?status=Approved            BookingsController.Index (filtered)
GET     /admin/bookings/edit/{id}                  BookingsController.Edit (form)
POST    /admin/bookings/edit/{id}                  BookingsController.Edit (submit)
POST    /admin/bookings/changeStatus?id=1&status=Approved   BookingsController.ChangeStatus
POST    /admin/bookings/delete?id={id}             BookingsController.Delete
```

Status Options: `Pending`, `Approved`, `Rejected`, `Completed`, `Cancelled`

#### Gallery Management
```
GET     /admin/gallery                             GalleryController.Index
GET     /admin/gallery/create                      GalleryController.Create (form)
POST    /admin/gallery/create                      GalleryController.Create (submit)
GET     /admin/gallery/edit/{id}                   GalleryController.Edit (form)
POST    /admin/gallery/edit/{id}                   GalleryController.Edit (submit)
POST    /admin/gallery/delete?id={id}              GalleryController.Delete
```

#### Contact Messages
```
GET     /admin/contactmessages                     ContactMessagesController.Index
GET     /admin/contactmessages?page=1              ContactMessagesController.Index (paginated)
GET     /admin/contactmessages/details/{id}        ContactMessagesController.Details
POST    /admin/contactmessages/markasread/{id}     ContactMessagesController.MarkAsRead (AJAX)
POST    /admin/contactmessages/delete?id={id}      ContactMessagesController.Delete
```

#### Settings
```
GET     /admin/settings                            SettingsController.Index
POST    /admin/settings/update                     SettingsController.Update
```

---

## ?? Request/Response Examples

### Example 1: Create Booking
**Request:**
```http
POST /booking/create
Content-Type: application/x-www-form-urlencoded

CustomerName=John Doe&Email=john@example.com&Phone=1234567890&BookingDate=2024-01-15&BookingTime=19:00&Guests=4&SpecialRequest=Window+seat+please
```

**Response:** Redirect to `/booking/confirmation/1`

---

### Example 2: Get Available Tables (AJAX)
**Request:**
```http
GET /api/available-tables?date=2024-01-15&time=19:00:00&guests=4
```

**Response:**
```json
[
  {
    "id": 5,
    "tableNumber": 5,
    "capacity": 4,
    "location": "Section B",
    "status": 0
  },
  {
    "id": 7,
    "tableNumber": 7,
    "capacity": 6,
    "location": "Window View",
    "status": 0
  }
]
```

---

### Example 3: Create Menu Item
**Request:**
```http
POST /admin/menuitems/create
Content-Type: application/x-www-form-urlencoded

Name=Butter+Chicken&CategoryId=2&Description=Creamy+tomato+curry&Price=14.99&IsVeg=false&IsAvailable=true&IsFeatured=true&PreparationTime=20&DisplayOrder=1&Status=true
```

**Response:** Redirect to `/admin/menuitems` with success message

---

### Example 4: Update Booking Status
**Request:**
```http
POST /admin/bookings/changeStatus?id=1&status=Approved
Content-Type: application/x-www-form-urlencoded

__RequestVerificationToken=...
```

**Response:** Redirect to `/admin/bookings` with success message

---

## ?? Data Flow Diagrams

### Booking Flow
```
Customer fills form ? 
  ?
Validate input (FluentValidation) ? 
  ?
Check table availability ? 
  ?
Create booking (Pending status) ? 
  ?
Show confirmation page ? 
  ?
Admin approves/rejects ? 
  ?
Booking status updated
```

### Menu Item Flow
```
Admin creates category ? 
  ?
Admin adds menu items ? 
  ?
Mark as featured (optional) ? 
  ?
Items appear on homepage ? 
  ?
Customers browse menu ? 
  ?
Admin manages/updates items
```

---

## ?? Authentication Flow

```
1. User visits /admin/dashboard
   ?
2. Not authenticated ? Redirect to login
   ?
3. ASP.NET Identity login page
   ?
4. Submit credentials
   ?
5. Validate against database
   ?
6. Create authentication cookie
   ?
7. Redirect to /admin/dashboard
   ?
8. Check authorization (Admin role)
   ?
9. Access granted ? Display dashboard
```

---

## ?? Status Enums

### BookingStatus
```csharp
0 = Pending
1 = Approved
2 = Rejected
3 = Completed
4 = Cancelled
```

### TableStatus
```csharp
0 = Available
1 = Reserved
2 = Maintenance
3 = Occupied
```

---

## ?? Query Parameters

### Pagination
```
?page=1&pageSize=10
```

### Filtering
```
/admin/bookings?status=Approved
/gallery?category=Food
/menu?categoryId=1
```

### Search
```
// Built into table views as POST forms
// Uses server-side filtering
```

---

## ?? AJAX Endpoints

### Get Available Tables
- **Method**: GET
- **URL**: `/api/available-tables`
- **Parameters**: `date`, `time`, `guests`
- **Response**: JSON array of tables

### Mark Message as Read
- **Method**: POST
- **URL**: `/admin/contactmessages/markasread/{id}`
- **Response**: 200 OK

---

## ?? HTTP Status Codes

| Code | Scenario |
|------|----------|
| 200 | Success |
| 301 | Redirect (after POST) |
| 400 | Bad Request |
| 401 | Unauthorized |
| 403 | Forbidden (not authorized) |
| 404 | Not Found |
| 500 | Server Error |

---

## ??? CSRF Protection

All POST requests require CSRF tokens:

```html
@Html.AntiForgeryToken()
```

This is automatically included in all forms.

---

## ?? Form Model Binding

### Category Model
```csharp
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
    public int DisplayOrder { get; set; }
    public bool Status { get; set; }
}
```

### MenuItem Model
```csharp
public class MenuItem
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public string? Image { get; set; }
    public bool IsVeg { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsFeatured { get; set; }
    public int PreparationTime { get; set; }
    public bool Status { get; set; }
    public int DisplayOrder { get; set; }
}
```

---

## ?? Validation Rules

### Booking Validation
- `CustomerName`: Required, 3-100 chars
- `Email`: Required, valid email format
- `Phone`: Required, 10+ digits
- `BookingDate`: Today or future date
- `BookingTime`: Valid time format
- `Guests`: 1-20 range

---

## ?? Response Headers

All responses include:
```
Content-Type: text/html; charset=utf-8
X-Content-Type-Options: nosniff
X-Frame-Options: SAMEORIGIN
Strict-Transport-Security: max-age=31536000
```

---

## ?? Rate Limiting

Currently: No rate limiting implemented
(Can be added via middleware if needed)

---

## ?? Error Handling

Errors are displayed via:
1. **Admin Views**: TempData["Error"] messages
2. **Public Views**: ModelState.AddModelError()
3. **Logs**: Serilog (`/logs/` directory)

---

**Last Updated**: Phase 3 Complete
