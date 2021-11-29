using Microsoft.Extensions.Logging;
using Ordering.Domain.Aggregates;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seeding database associated with context {DbContextName}", nameof(OrderContext));
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            var order1 = new Order("1", "rs2", new Address("Ulica 1", "Beograd", "Beograd", "Srbija", "11000", "razvoj.softvera.matf@gmail.com"));
            order1.AddOrderItem("IPhone X", "602d2149e773f2a3990b47f5", "https://fdn2.gsmarena.com/vv/pics/apple/apple-iphone-x-new-1.jpg", 800, 2);
            order1.AddOrderItem("HTC U11+ Plus", "602d2149e773f2a3990b47f9", "https://fdn2.gsmarena.com/vv/pics/htc/htc-u11-plus-1.jpg", 380, 1);

            return new List<Order> { order1 };
        }
    }
}
