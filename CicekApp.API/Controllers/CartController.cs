using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Request;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Application.Services.CartService;
using Microsoft.AspNetCore.Mvc;

namespace CicekApp.API.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService chartService)
        {
            _cartService = chartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddtoCart(AddtoChartRequest request)
        {
            await _cartService.Add(request);
            return Ok("Başarıyla Eklendi");
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromChart(AddtoChartRequest request)
        {
            await _cartService.Remove(request);
            return Ok("Başarıyla Çıkartıldı");
        }

        [HttpGet("get-by-username")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var response = await _cartService.GetFlowersInCartbyUsername(username);
            return Ok(response);
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(int cartId)
        {
            var response = await _cartService.GetFlowersInCart(cartId);
            return Ok(response);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateCartItemQuantity([FromBody] UpdateCartItemRequest request)
        {
            if (request.Quantity < 1)
            {
                return BadRequest(new { error = "Quantity must be at least 1" });
            }

            await _cartService.UpdateCartItemQuantityAsync(request);

            return Ok("Updated...");
        }
    }
}