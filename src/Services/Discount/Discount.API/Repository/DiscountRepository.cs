using Discount.API.Data;
using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Repository
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

        public async Task<Coupon> DeleteDiscount(Coupon coupon)
        {


            _db.Coupons.Remove(coupon);
            await _db.SaveChangesAsync();

            return coupon;
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
