using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.ProductDetailServices;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.Catalog.Services.ProductServices;
using MultiShop.Catalog.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// JWT tabanlý kimlik doðrulama servisini ekler (Varsayýlan þema: "Bearer")
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        // Token'ý imzalayan otoritenin URL'si (Identity Server gibi)
        opt.Authority = builder.Configuration["IdentityServerUrl"];

        // Bu API'nin adýdýr, token'daki "aud" deðeriyle eþleþmelidir
        opt.Audience = "ResourceCatalog";

        // HTTPS zorunluluðunu kapatýr (geliþtirme ortamý için true olmalý)
        opt.RequireHttpsMetadata = false;
    });

builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


// appsettings.json dosyasýndaki "DatabaseSettings" bölümünü alýp,
// bu ayarlarý .NET'in IOptions<T> sistemine baðlar.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(nameof(DatabaseSettings)));
// ? "DatabaseSettings" adýndaki konfigürasyon kýsmýný al ve
// DatabaseSettings sýnýfýna map'le (eþleþtir)
// Böylece IOptions<DatabaseSettings> ile bu verilere eriþebilirsin


// IOptions<DatabaseSettings> üzerinden alýnan deðerle
// IDatabaseSettings arayüzünü çözümlenecek (resolve) þekilde register eder
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    // ServiceProvider üzerinden IOptions<DatabaseSettings> alýnýr,
    // .Value ile gerçek DatabaseSettings nesnesine eriþilir ve bu döndürülür.
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
// ? Bu satýr sayesinde, uygulamanýn baþka yerinde IDatabaseSettings
// enjekte edilmek istendiðinde, arka planda
// IOptions<DatabaseSettings>.Value verilir.
// Yani config ayarlarýn nesne olarak kullanýma hazýr olur.



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
