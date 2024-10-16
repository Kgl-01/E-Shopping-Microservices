


using Discount.gRPC.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) { }


        public DbSet<Coupon> Coupons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            List<Coupon> coupons = new List<Coupon>
            {
                new Coupon{Id=1 ,Amount=150,Description="IPhone Discount", ProductName="IPhone X"},
                new Coupon{Id=2 ,Amount=100,Description="Samsung Discount", ProductName="Samsung 10"}

            };
            modelBuilder.Entity<Coupon>().HasData(coupons);
        }

    }
}
