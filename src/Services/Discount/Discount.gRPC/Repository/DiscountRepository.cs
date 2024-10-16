using Discount.gRPC.Data;
using Discount.gRPC.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountContext _db;

        public DiscountRepository(DiscountContext db)
        {
            _db = db;
        }

        public async Task<Coupon> CreateDiscount(Coupon coupon)
        {
            await _db.Coupons.AddAsync(coupon);
            await _db.SaveChangesAsync();
            return coupon;
        }

        public async Task<Coupon> DeleteDiscount(string productName)
        {
            var deleted = await _db.Coupons.FirstOrDefaultAsync(x => x.ProductName == productName);

            _db.Coupons.Remove(deleted);
            await _db.SaveChangesAsync();

            return deleted;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var coupon = await _db.Coupons.FirstOrDefaultAsync(c => c.ProductName.ToLower().Equals(productName.ToLower()));

            if (coupon == null)
            {
                return null;
            }

            return coupon;
        }

        public async Task<Coupon> UpdateDiscount(Coupon coupon)
        {
            _db.Coupons.Update(coupon);
            await _db.SaveChangesAsync();

            return coupon;
        }
    }
}
