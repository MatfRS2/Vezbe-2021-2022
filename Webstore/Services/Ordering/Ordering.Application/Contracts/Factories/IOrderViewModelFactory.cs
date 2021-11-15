using Ordering.Application.Features.Orders.Queries.ViewModels;
using Ordering.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Factories
{
    public interface IOrderViewModelFactory
    {
        OrderViewModel CreateViewModel(Order order);
    }
}
