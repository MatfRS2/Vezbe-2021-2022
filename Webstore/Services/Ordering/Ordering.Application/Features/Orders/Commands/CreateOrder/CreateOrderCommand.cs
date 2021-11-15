using MediatR;
using Ordering.Application.Features.Orders.Commands.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        // Address
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }

        // Order
        public string BuyerId { get; set; }
        public string BuyerUsername { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}
