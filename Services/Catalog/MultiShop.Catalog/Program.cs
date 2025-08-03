using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.ProductDetailServices;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.Catalog.Services.ProductServices;
using MultiShop.Catalog.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// JWT tabanl� kimlik do�rulama servisini ekler (Varsay�lan �ema: "Bearer")
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        // Token'� imzalayan otoritenin URL'si (Identity Server gibi)
        opt.Authority = builder.Configuration["IdentityServerUrl"];

        // Bu API'nin ad�d�r, token'daki "aud" de�eriyle e�le�melidir
        opt.Audience = "ResourceCatalog";

        // HTTPS zorunlulu�unu kapat�r (geli�tirme ortam� i�in true olmal�)
        opt.RequireHttpsMetadata = false;
    });

builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


// appsettings.json dosyas�ndaki "DatabaseSettings" b�l�m�n� al�p,
// bu ayarlar� .NET'in IOptions<T> sistemine ba�lar.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(nameof(DatabaseSettings)));
// ? "DatabaseSettings" ad�ndaki konfig�rasyon k�sm�n� al ve
// DatabaseSettings s�n�f�na map'le (e�le�tir)
// B�ylece IOptions<DatabaseSettings> ile bu verilere eri�ebilirsin


// IOptions<DatabaseSettings> �zerinden al�nan de�erle
// IDatabaseSettings aray�z�n� ��z�mlenecek (resolve) �ekilde register eder
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    // ServiceProvider �zerinden IOptions<DatabaseSettings> al�n�r,
    // .Value ile ger�ek DatabaseSettings nesnesine eri�ilir ve bu d�nd�r�l�r.
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
// ? Bu sat�r sayesinde, uygulaman�n ba�ka yerinde IDatabaseSettings
// enjekte edilmek istendi�inde, arka planda
// IOptions<DatabaseSettings>.Value verilir.
// Yani config ayarlar�n nesne olarak kullan�ma haz�r olur.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
