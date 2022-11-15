using LabPortugal_Intranet.Commons;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Diagnostics;
//agregue
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.DependencyInjection.Extensions;





var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;



// Add services to the container.
services.AddControllersWithViews();
//services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(e =>
//    {
//        e.LoginPath = "";
//        e.ExpireTimeSpan = TimeSpan.FromMinutes(5);
//        e.AccessDeniedPath = "";
//    }
//    );


//--------------------------------------------------------------------------
services.AddDistributedMemoryCache();
services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddSession(o =>
{
    o.IdleTimeout = TimeSpan.FromMinutes(20);
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});
//--------------------------------------------------------------------------





services.AddAuthentication(o =>
    {
        o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(GoogleDefaults.AuthenticationScheme, go =>
    {
        go.ClientId = configuration["Authentication:Google:ClientId"];
        go.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        go.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();//--------------------
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
