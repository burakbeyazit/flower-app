using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Request;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Application.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace CicekApp.API.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        // Constructor Injection for OrderService
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // 1. Create Order Endpoint
        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderRequest request)
        {

            // Sipariş oluşturma işlemi
            var orderId = await _orderService.CreateOrder(request.Username, request.CartId);
            return Ok(orderId); // Başarıyla oluşturulmuş siparişin ID'si döndürülür


        }

        // 2. Get Orders by Username Endpoint
        [HttpGet("user/{username}")]
        public async Task<ActionResult> GetOrdersByUsername(string username)
        {


            var orders = await _orderService.GetOrdersByUsername(username);
            return Ok(orders); // Kullanıcıya ait tüm siparişler döndürülür


        }

        // 3. Get Order by ID Endpoint
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponse>> GetOrderById(int orderId)
        {

            var order = await _orderService.GetOrderById(orderId);
            return Ok(order); // Verilen ID'ye ait sipariş detayları döndürülür
        }

    }
}
