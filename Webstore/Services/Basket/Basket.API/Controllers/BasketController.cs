using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using Grpc.Core;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Authorize(Roles = "Buyer")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly CouponGrpcService _couponGrpcService;
        private readonly ILogger<BasketController> _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository repository, CouponGrpcService couponGrpcService, ILogger<BasketController> logger, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _couponGrpcService = couponGrpcService ?? throw new ArgumentNullException(nameof(couponGrpcService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != username)
            {
                return Forbid();
            }

            var basket = await _repository.GetBasket(username);
            return Ok(basket ?? new ShoppingCart(username));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != basket.Username)
            {
                return Forbid();
            }

            foreach (var item in basket.Items)
            {
                try
                {
                    var coupon = await _couponGrpcService.GetDiscount(item.ProductName);
                    item.Price -= coupon.Amount;
                }
                catch (RpcException e)
                {
                    _logger.LogInformation("Error while retrieving coupon for item {ProductName}: {message}", item.ProductName, e.Message);
                }
            }

            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != username)
            {
                return Forbid();
            }

            await _repository.DeleteBasket(username);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != basketCheckout.BuyerUsername)
            {
                return Forbid();
            }

            // Get existing basket
            var basket = await _repository.GetBasket(basketCheckout.BuyerUsername);
            if (basket == null)
            {
                return BadRequest();
            }

            // Send checkout event
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            await _publishEndpoint.Publish(eventMessage);

            // Remove the basket
            await _repository.DeleteBasket(basketCheckout.BuyerUsername);

            return Accepted();
        }
    }
}
