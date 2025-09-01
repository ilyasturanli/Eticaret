using Eticaret.Data;
using Eticaret.Service.Abstract;
using Eticaret.Service.Concrete;
using Eticaret.WebUI.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Session configuration
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Eticaret.Session";       // Session cookie ismi
    options.Cookie.HttpOnly = true;                 // JS erişimini engelle
    options.Cookie.IsEssential = true;              // GDPR uyumu
    options.IdleTimeout = TimeSpan.FromMinutes(30);// Session timeout
    options.Cookie.SameSite = SameSiteMode.None;    // 3D Secure redirect için
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS zorunlu
});

// DbContext
builder.Services.AddDbContext<DatabaseContext>();

// Scoped services
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICartService, CartService>();

// Authentication configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Account/SignIn";
        x.AccessDeniedPath = "/AccessDenied";
        x.Cookie.Name = "Account";
        x.Cookie.MaxAge = TimeSpan.FromHours(5);
        x.Cookie.IsEssential = true;

        x.Cookie.SameSite = SameSiteMode.None;          // 3D Secure redirect için
        x.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS zorunlu
    });

// Authorization policies
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User", "Customer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// CookiePolicy ve Session middleware (sıralama önemli)
app.UseCookiePolicy(); // SameSite cookie için
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Registering areas
app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
