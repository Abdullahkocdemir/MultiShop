using Microsoft.Extensions.DependencyInjection;
using MultiShop.Cargo.BussinessLayer.Absract;
using MultiShop.Cargo.BussinessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Absract;
using MultiShop.Cargo.DataAccessLayer.EntityFrameWork;

namespace MultiShop.Cargo.BussinessLayer.Container
{
    public static class Extension
    {
        public static void ConteinerDependencies(this IServiceCollection Services)
        {
            Services.AddScoped<ICargoCompanyDal, EfCargoCompanyDal>();
            Services.AddScoped<ICargoCompanyService, CargoCompanyManager>();

            Services.AddScoped<ICargoCustomerDal, EfCargoCustomerDal>();
            Services.AddScoped<ICargoCustomerService, CargoCustomerManager>();

            Services.AddScoped<ICargoDetailService, CargoDetailManager>();
            Services.AddScoped<ICargoDetailDal, EfCargoDetailDal>();

            Services.AddScoped<ICargoOperationDal, EfCargoOperationDal>();
            Services.AddScoped<ICargoOperationService, CargoOperationManager>();
        }
    }
}
