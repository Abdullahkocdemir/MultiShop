using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Services;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Login/Index/";
    options.LogoutPath = "/Login/Logout/";
    options.AccessDeniedPath = "/Login/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.Name = "MultiShopJwt";

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, (options =>
{
    options.LoginPath = "/Login/Index";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
    options.LogoutPath = "/Login/Logout";
    options.AccessDeniedPath = "/Login/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.Name = "MultiShopCookie";
}));


builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
//builder.Services.AddHttpClient<IIdentityService, IdentityService>(opt =>
//{
//    opt.BaseAddress = new Uri("https://localhost:5001");
//}).ConfigurePrimaryHttpMessageHandler(() =>
//{
//    return new HttpClientHandler
//    {
//        // Geliþtirme ortamýnda SSL sertifika hatalarýný görmezden gel
//        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
//    };
//});

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
