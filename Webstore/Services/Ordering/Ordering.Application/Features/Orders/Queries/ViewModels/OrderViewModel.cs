using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.ViewModels
{
    public class OrderViewModel
    {
        // Relevant information from EntityBase
        public int Id { get; set; }

        // Relevant information from Address
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }

        // Relevant information from Order
        public string BuyerId { get; set; }
        public string BuyerUsername { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
    }
}
