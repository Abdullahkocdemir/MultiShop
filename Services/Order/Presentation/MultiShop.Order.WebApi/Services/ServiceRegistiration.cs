using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;

namespace MultiShop.Order.WebApi.Services
{
    public static class ServiceRegistiration
    {
        public static void ConteinerDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly));

            // Address Handlers
            services.AddScoped<GetAddressQueryHandler>();
            services.AddScoped<GetAddressByIdQueryHandler>();
            services.AddScoped<CreateAddressCommandHandler>();
            services.AddScoped<UpdateAddressCommandHandler>();
            services.AddScoped<RemoveAddressCommandHandler>();

            // Order Detail Handlers
            services.AddScoped<GetOrderDetailQueryHandler>();
            services.AddScoped<GetOrderDetailByIdQueryHandler>();
            services.AddScoped<CreateOrderDetailCommandHandler>();
            services.AddScoped<UpdateOrderDetailQueryHandler>();
            services.AddScoped<RemoveOrderDetailQueryHandler>();


            //Ordering Handlers
            services.AddScoped<GetOrderingByIdQueryHandler>();
            services.AddScoped<GetOrderingQueryHandler>();
            services.AddScoped<CreateOrderingCommandHandler>();
            services.AddScoped<UpdateOrderingCommandHandler>();
            services.AddScoped<RemoveOrderingCommadHandler>();
        }
    }
}
