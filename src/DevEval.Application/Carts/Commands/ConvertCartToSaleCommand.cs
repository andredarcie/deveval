﻿using MediatR;
using DevEval.Application.Sales.Dtos;

namespace DevEval.Application.Carts.Commands
{
    public class ConvertCartToSaleCommand : IRequest<SaleDto>
    {
        public int CartId { get; set; }

        public ConvertCartToSaleCommand(int cartId)
        {
            CartId = cartId;
        }
    }
}