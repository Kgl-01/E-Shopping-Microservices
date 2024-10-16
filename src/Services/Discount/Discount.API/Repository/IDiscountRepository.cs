using Discount.API.Entities;

namespace Discount.API.Repository
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<Coupon> CreateDiscount(Coupon coupon);
        Task<Coupon> UpdateDiscount(Coupon coupon);
        Task<Coupon> DeleteDiscount(Coupon coupon);
    }
}
