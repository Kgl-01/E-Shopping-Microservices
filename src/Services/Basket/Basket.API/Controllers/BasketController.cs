using AutoMapper;
using Basket.API.Entities.Domain;
using Basket.API.gRPCServices;
using Basket.API.Repository;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountgRPCService _discountgRPCService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndPoint;
        public BasketController(IBasketRepository basketRepository, DiscountgRPCService discountgRPCService, IMapper mapper, IPublishEndpoint publishEndPoint)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _discountgRPCService = discountgRPCService ?? throw new ArgumentNullException(nameof(discountgRPCService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndPoint = publishEndPoint ?? throw new ArgumentNullException(nameof(publishEndPoint));
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
            foreach (var item in cart.Items)
            {
                var coupon = await _discountgRPCService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _basketRepository.UpdateBasket(cart));
        }

        [HttpDelete]
        [Route("{userName}")]
        public async Task<IActionResult> DeleteBasket([FromRoute] string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            //get existing basket with total price with given username
            var basket = await _basketRepository.GetBasket(basketCheckout.UserName);

            if (basket == null)
            {
                return BadRequest();
            }

            //Create basketCheckoutEvent -- Set Total Price on basketCheckout eventMessage
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);

            eventMessage.TotalPrice = basket.TotalPrice;


            //send checkout event to rabbitmq
            await _publishEndPoint.Publish(eventMessage);

            //remove basket
            await _basketRepository.DeleteBasket(basket.UserName);

            return Accepted();

        }
    }
}
