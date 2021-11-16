using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Factories
{
    public interface IOrderFactory
    {
        Order Create(CreateOrderCommand command);
    }
}
