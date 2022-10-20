using LabPortugal_Intranet.Commons;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;



// Add services to the container.
services.AddControllersWithViews();
/*services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(e =>
    {
        e.LoginPath = "";
        e.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        e.AccessDeniedPath = "";
    }
    );*/

services.AddAuthentication()
    .AddGoogle(go =>
    {
        go.ClientId = configuration["Authentication:Google:ClientId"];
        go.ClientSecret = configuration["Authentication:Google:ClientSecret"];

        Debug.WriteLine(go.ClientId + " -->  " + go.ClientSecret);
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Farmacia}/{action=Index}/{id?}");

app.Run();
