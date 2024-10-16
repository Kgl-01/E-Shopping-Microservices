using Discount.gRPC.Entities;
using Discount.gRPC.Protos;

namespace Discount.gRPC
{
    public static class Extensions
    {
        public static ApplyDiscount asDTO(this CouponModel couponModel)
        {
            return new ApplyDiscount { Amount = couponModel.Amount };
        }

        public static Coupon toCoupon(this CouponModel couponModel)
        {
            return new Coupon { Amount = couponModel.Amount, Description = couponModel.Description, ProductName = couponModel.ProductName };
        }

        public static CouponModel asCouponModel(this Coupon coupon)
        {
            return new CouponModel { Id = coupon.Id, Amount = coupon.Amount, Description = coupon.Description, ProductName = coupon.ProductName };
        }
    }
}