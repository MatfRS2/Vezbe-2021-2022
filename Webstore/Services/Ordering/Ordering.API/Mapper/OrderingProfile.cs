using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Mapper
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CreateOrderCommand, EventBus.Messages.Events.BasketCheckoutEvent>().ReverseMap();
            CreateMap<OrderItemDTO, EventBus.Messages.Events.BasketItem>().ReverseMap();
        }
    }
}
