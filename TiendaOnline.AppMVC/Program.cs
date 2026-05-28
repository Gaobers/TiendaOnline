using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TiendaOnlineZapContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de autenticación con cookies
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "TiendaOnline.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;

        // Para una app MVC normal suele ser mejor Lax
        options.Cookie.SameSite = SameSiteMode.Lax;

        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;

        // Ajusta estas rutas según el nombre real de tu controlador
        options.LoginPath = "/Usuarios/Login";
        options.AccessDeniedPath = "/Usuarios/AccessDenied";
        options.LogoutPath = "/Usuarios/Logout";
    });

// Autorización
builder.Services.AddAuthorization();

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

// Primero autenticación
app.UseAuthentication();

// Luego autorización
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PaginaPrincipal}/{action=Index}/{id?}");

app.Run();