﻿using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class UpdateOrderDetailQueryHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public UpdateOrderDetailQueryHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateOrderDetailCommand command)
        {
            var value = await _repository.GetByIdAsync(command.OrderDetailId);
            value.OrderingId = command.OrderingId;
            value.ProductName = command.ProductName;
            value.ProductId = command.ProductId;
            value.ProductPrice = command.ProductPrice;
            value.ProductTotalPrice = command.ProductTotalPrice;
            value.ProductAmount = command.ProductAmount;
            await _repository.UpdateAsync(value);
        }
    }
}
