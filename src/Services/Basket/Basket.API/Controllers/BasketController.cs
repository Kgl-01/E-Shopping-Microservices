using Basket.API.Entities.Domain;
using Basket.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket([FromQuery] string userName)
        {

            var basket = await _basketRepository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));

        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart cart)
        {
            return Ok(await _basketRepository.UpdateBasket(cart));
        }

        [HttpDelete]
        [Route("{userName}")]
        public async Task<IActionResult> DeleteBasket([FromRoute] string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}
