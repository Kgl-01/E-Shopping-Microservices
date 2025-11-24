using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                await orderContext.Orders.AddRangeAsync(GetPreConfigOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreConfigOrders()
        {
            return new List<Order> {
            new Order(){UserName="Karthik",FirstName="Yogesh",LastName="Akshay",EmailAddress="killerkarti98@gmail.com",AddressLine="Shakambari Nagara",Country="India",TotalPrice=10000}
            };
        }
    }
}
