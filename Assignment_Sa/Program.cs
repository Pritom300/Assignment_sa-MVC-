using Assignment_Sa.Data;
using Assignment_Sa.Interfaces;
using Assignment_Sa.Models;
using Assignment_Sa.Repositories;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using System.Security.Cryptography;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add MVC controllers and views
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISalesMasterRepository, SalesMasterRepository>();

//QuestPDF
QuestPDF.Settings.License = LicenseType.Community;


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Error Handling
    app.UseHsts();
}




// Seed Data product and customer
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    
    db.Database.Migrate();

    // ===== Seed Customers =====
    if (!db.Customers.Any())
    {
        db.Customers.AddRange(
            new Customer { Name = "Rahim Khan", Email = "rahim@example.com", Phone = "01712345678", Address = "Dhaka, Bangladesh" },
            new Customer { Name = "Karim Ali", Email = "karim@example.com", Phone = "01887654321", Address = "Sylhet, Bangladesh" },
            new Customer{ Name = "Matiur Khan", Email = "miami@example.com", Phone = "0173445678", Address = "Khulna, Bangladesh" },
            new Customer { Name = "Salman Ali", Email = "salman@example.com", Phone = "01834321", Address = "Chittagong, Bangladesh" }
        );
        db.SaveChanges();
    }

    // ===== Seed Products =====
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product { Name = "Laptop", UnitPrice = 60000, Stock = 10 },
            new Product { Name = "Mouse", UnitPrice = 500, Stock = 50 },
            new Product { Name = "Keyboard", UnitPrice = 1500, Stock = 30 },
            new Product { Name = "Ac", UnitPrice = 4000, Stock = 180 },
            new Product { Name = "Monitor", UnitPrice = 1500, Stock = 250 },
            new Product { Name = "Speaker", UnitPrice = 1500, Stock = 530 }
        );
        db.SaveChanges();
    }

    if (!db.Users.Any(u => u.Role == "Admin"))
    {
        db.Users.Add(new User
        {
            Username = "admin",
            FullName = "Admin User",
            Role = "Admin",
            PasswordHash = ComputeSha256Hash("123456") 
        });
        db.SaveChanges();
     }

}
//end seed data

// extract password
string ComputeSha256Hash(string rawData)
{
    using (SHA256 sha256 = SHA256.Create())
    {
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        StringBuilder builder = new StringBuilder();
        foreach (var b in bytes)
            builder.Append(b.ToString("x2"));
        return builder.ToString();
    }
}

//extract password end





// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  // <-- Add this if you use wwwroot
app.UseSession();    // <-- Add this if you use Session

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

