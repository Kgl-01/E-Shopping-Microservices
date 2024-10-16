using Discount.API.Entities;
using Discount.API.Entities.DTOs;

namespace Discount.API
{
    public static class Extensions
    {
        public static CouponDto asDTO(this Coupon coupon)
        {
            return new CouponDto(Amount: coupon.Amount);

        }
    }
}
