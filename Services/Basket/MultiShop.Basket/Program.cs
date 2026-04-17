using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MultiShop.Basket.LoginService;
using MultiShop.Basket.Services;
using MultiShop.Basket.Settings;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
// 1. Controller ve Swagger Konfigürasyonu
//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MultiShop Basket API", Version = "v1" });
});

// 2. HttpContext ve Kullanýcý Bilgisi Servisleri
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginService, LoginService>();

// 3. Redis Ayarlarýnýn Baðlanmasý (IOptions deseni)
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));

// 4. RedisService Kaydý (Singleton)
// Hatayý düzelttik: Baðlantý Lazy olduðu için burada metot çaðýrmaya gerek yok.
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    return new RedisService(redisSettings.Host, redisSettings.Port);
});

// 5. Sepet Ýþ Mantýðý Servisi
builder.Services.AddScoped<IBasketService, BasketService>();

// 6. Kimlik Doðrulama (IdentityServer JWT)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerURL"];
    opt.Audience = "ResourceBasket";
    opt.RequireHttpsMetadata = false; // Geliþtirme ortamý için false
});
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
var app = builder.Build();

// --- Middleware Pipeline (Sýralama Önemlidir) ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Önce kimlik doðrulanýr (Kimsin?), sonra yetki kontrol edilir (Neye yetkin var?)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();