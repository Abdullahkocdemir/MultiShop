using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.BusinessLayer.Conteiner;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.WebApi.Mapping;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Entity Framework DbContext'i ekle - BU ÇOK ÖNEMLÝ!
builder.Services.AddDbContext<CargoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceCargo";
    opt.RequireHttpsMetadata = false;
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper'ý GeneralMapping sýnýfýný kullanarak ekle
builder.Services.AddAutoMapper(typeof(GeneralMapping));
// BusinessLayer'daki baðýmlýlýklarý eklemek için geniþletme metodunu çaðýr
builder.Services.ConteinerDependencies();

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
