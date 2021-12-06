using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class BasketItem
    {
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Units { get; set; }
    }

    public class BasketCheckoutEvent : IntegrationBaseEvent
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
        public IEnumerable<BasketItem> OrderItems { get; set; }
    }
}
