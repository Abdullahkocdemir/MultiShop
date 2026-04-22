using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.Services.BrandService;
using MultiShop.Catalog.Services.CategoryService;
using MultiShop.Catalog.Services.FeatureService;
using MultiShop.Catalog.Services.FeatureSliderService;
using MultiShop.Catalog.Services.OfferService;
using MultiShop.Catalog.Services.ProductDetailDetailService;
using MultiShop.Catalog.Services.ProductDetailService;
using MultiShop.Catalog.Services.ProductImageService;
using MultiShop.Catalog.Services.ProductService;
using MultiShop.Catalog.Services.SpecialOfferService;
using MultiShop.Catalog.Settings;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerURL"];
    opt.Audience = "ResourceCatalog";
    opt.RequireHttpsMetadata = false;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductDetailDetailService, ProductDetailService>();
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>();
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
