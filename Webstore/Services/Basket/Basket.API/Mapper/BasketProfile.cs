using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Mapper
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Entities.BasketCheckout, EventBus.Messages.Events.BasketCheckoutEvent>().ReverseMap();
            CreateMap<Entities.BasketItem, EventBus.Messages.Events.BasketItem>().ReverseMap();
        }
    }
}
