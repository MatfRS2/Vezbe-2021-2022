using MediatR;
using Ordering.Application.Features.Orders.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetListOfOrders
{
    public class GetListOfOrdersQuery : IRequest<List<OrderViewModel>>
    {
        public string Username { get; set; }

        public GetListOfOrdersQuery(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
