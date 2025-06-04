using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Application.Persistence;
using CicekApp.Application.Services.UserService;
using CicekApp.Domain.Entities;
using CicekApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CicekApp.Application.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public OrderService(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<int> CreateOrder(string username, int cartId)
        {
            // Kullanıcıyı al
            var user = await _userService.GetByEmailAsync(username);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            // Sepeti al ve doğrula
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.Id == cartId && c.UserId == user.UserId && c.OrderId == null);

            if (cart == null)
                throw new Exception("Geçerli bir sepet bulunamadı.");

            // Sipariş oluşturma
            var order = new Order
            {
                UserId = user.UserId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = cart.TotalAmount,
                Status = OrderStatus.Pending,
                CartId = cartId
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Sepeti siparişle ilişkilendir
            cart.OrderId = order.OrderId;
            await _context.SaveChangesAsync();

            return order.OrderId; // Oluşturulan siparişin ID'si
        }

        public async Task<List<OrderResponse>> GetOrdersByUsername(string username)
        {
            var user = await _userService.GetByEmailAsync(username);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            var orders = await _context.Orders
                .Include(o => o.Cart)
                .Where(o => o.UserId == user.UserId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var responses = new List<OrderResponse>();
            foreach (var order in orders)
            {
                var cartFlowers = await _context.CartFlowers
                    .Include(cf => cf.Flower)
                        .ThenInclude(f => f.Category)
                    .Where(cf => cf.CartId == order.CartId)
                    .ToListAsync();

                var flowerResponses = cartFlowers.Select(cf => new FlowerResponse
                {
                    FlowerId = cf.Flower.FlowerId,
                    FlowerName = cf.Flower.FlowerName,
                    Price = cf.Flower.Price,
                    StockQuantity = cf.Flower.StockQuantity,
                    Description = cf.Flower.Description,
                    ImageUrl = cf.Flower.ImageUrl,
                    CategoryId = cf.Flower.CategoryId,
                    Category = cf.Flower.Category != null ? cf.Flower.Category.CategoryName : null,
                }).ToList();

                responses.Add(new OrderResponse
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice,
                    Status = order.Status,
                    CartId = order.CartId,
                    Username = user.Email,
                    Flowers = flowerResponses
                });
            }

            return responses;
        }

        public async Task<OrderResponse> GetOrderById(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Cart)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                throw new Exception("Sipariş bulunamadı.");

            var cartFlowers = await _context.CartFlowers
                .Include(cf => cf.Flower)
                    .ThenInclude(f => f.Category)
                .Where(cf => cf.CartId == order.CartId)
                .ToListAsync();

            var flowerResponses = cartFlowers.Select(cf => new FlowerResponse
            {
                FlowerId = cf.Flower.FlowerId,
                FlowerName = cf.Flower.FlowerName,
                Price = cf.Flower.Price,
                StockQuantity = cf.Flower.StockQuantity,
                Description = cf.Flower.Description,
                ImageUrl = cf.Flower.ImageUrl,
                CategoryId = cf.Flower.CategoryId,
                Category = cf.Flower.Category != null ? cf.Flower.Category.CategoryName : null,
            }).ToList();

            var orderResponse = new OrderResponse
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                CartId = order.CartId,
                Username = order.User?.Email,
                Flowers = flowerResponses
            };

            return orderResponse;
        }
    }
}
