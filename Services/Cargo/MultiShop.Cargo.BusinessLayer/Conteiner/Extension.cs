using Microsoft.Extensions.DependencyInjection;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.BusinessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Abstruct;
using MultiShop.Cargo.DataAccessLayer.EntityFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Conteiner
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
