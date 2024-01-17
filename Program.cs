using PIU.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PiuContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("PiuContext"));
});

builder.Services.AddSession(); // Agrega soporte para sesiones.

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        //options.LoginPath = "/Sesion/Ingresar";
        //options.AccessDeniedPath = "/Sesion/Ingresar";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Agrega soporte para sesiones.

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();