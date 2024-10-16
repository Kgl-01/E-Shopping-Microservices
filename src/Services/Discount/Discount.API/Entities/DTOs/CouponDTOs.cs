namespace Discount.API.Entities.DTOs
{

    public record CouponDto(int Amount);

    public record CreateCouponDto(string ProductName, string Description, int Amount);

    public record UpdateCouponDto(string ProductName, string Description, int Amount);


}
