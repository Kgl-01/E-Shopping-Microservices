using Discount.API.Entities;
using Discount.API.Entities.DTOs;
using Discount.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public CouponController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(_discountRepository));
        }

        [HttpGet]
        [Route("{productName}")]
        public async Task<IActionResult> GetCoupon([FromRoute] string productName)
        {
            var coupon = await _discountRepository.GetDiscount(productName);

            if (coupon == null)
            {
                return NotFound("Coupon Not Available");
            }

            return Ok(coupon.asDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponDto createCouponDto)
        {

            var coupon = new Coupon
            {
                Amount = createCouponDto.Amount,
                Description = createCouponDto.Description,
                ProductName = createCouponDto.ProductName
            };

            var createdItem = await _discountRepository.CreateDiscount(coupon);

            if (createdItem == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetCoupon), new { productName = createdItem.ProductName.ToLower() }, createdItem.asDTO());
        }

        [HttpPut]
        [Route("{productName}")]
        public async Task<IActionResult> UpdateCoupon([FromRoute] string productName)
        {
            var existingCoupon = await _discountRepository.GetDiscount(productName);

            if (existingCoupon == null)
            {
                return NotFound();
            }

            var updatedCoupon = await _discountRepository.UpdateDiscount(existingCoupon);

            return Ok(updatedCoupon.asDTO());

        }

        [HttpDelete]
        [Route("{productName}")]
        public async Task<IActionResult> DeleteCoupon([FromRoute] string productName)
        {
            var existingCoupon = await _discountRepository.GetDiscount(productName);

            if (existingCoupon == null)
            {
                return NotFound();
            }

            await _discountRepository.DeleteDiscount(existingCoupon);

            return Ok();

        }
    }
}
