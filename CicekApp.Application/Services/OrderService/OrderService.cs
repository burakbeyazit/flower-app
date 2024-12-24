using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Application.Persistence;
using CicekApp.Application.Services.UserService;
using CicekApp.Domain.Entities;
using CicekApp.Domain.Enums;
using Dapper;
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
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            // Sepeti al ve doğrula
            var cart = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<Cart>(
                "SELECT * FROM carts WHERE id = @CartId AND userid = @UserId AND orderid IS NULL",
                new { CartId = cartId, UserId = user.UserId });

            if (cart == null)
            {
                throw new Exception("Geçerli bir sepet bulunamadı.");
            }

            // Sipariş oluşturma
            var orderId = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO orders (userid, orderdate, totalprice, status, cartid) 
          VALUES (@UserId, CURRENT_TIMESTAMP, @TotalPrice, @Status, @CartId) 
          RETURNING orderid",
                new
                {
                    UserId = user.UserId,
                    TotalPrice = cart.TotalAmount,
                    Status = OrderStatus.Pending, // Varsayılan olarak "Bekliyor" durumu
                    CartId = cartId
                });

            // Sepeti siparişle ilişkilendir
            var updateCartQuery = "UPDATE carts SET orderid = @OrderId WHERE id = @CartId";
            await _context.Database.GetDbConnection().ExecuteAsync(updateCartQuery, new { OrderId = orderId, CartId = cartId });

            return orderId; // Oluşturulan siparişin ID'si
        }

        public async Task<List<OrderResponse>> GetOrdersByUsername(string username)
        {
            var user = await _userService.GetByEmailAsync(username);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            // Kullanıcının siparişlerini al
            var orders = await _context.Database.GetDbConnection().QueryAsync<OrderResponse>(
                            @"SELECT o.orderid, 
                            o.orderdate, 
                            o.totalprice, 
                            o.status, 
                            c.id AS cartid,
                            u.email AS username
                    FROM orders o
                    JOIN carts c ON o.cartid = c.id
                    JOIN users u ON o.userid = u.userid
                    WHERE o.userid = @UserId",
                new { UserId = user.UserId });

            foreach (var order in orders)
            {
                // Her sipariş için ilişkili çiçekleri al
                var flowers = await _context.Database.GetDbConnection().QueryAsync<FlowerResponse>(
                    @"SELECT f.flowerid, 
                     f.flowername, 
                     f.price, 
                     f.stockquantity,
                     f.description,
                     f.imageurl,
                     f.categoryid,
                     c.categoryname AS category
                        FROM cart_flowers cf
                        JOIN flowers f ON cf.flowerid = f.flowerid
                        LEFT JOIN categories c ON f.categoryid = c.categoryid
                        WHERE cf.cartid = @CartId",
                    new { CartId = order.CartId });

                order.Flowers = flowers.ToList();  // Çiçekleri sipariş detaylarına ekle
            }

            return orders.ToList(); // Siparişlerin tamamını döndür
        }
        public async Task<OrderResponse> GetOrderById(int orderId)
        {
            // Sipariş detayını al
            var order = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<OrderResponse>(
            @"SELECT o.orderid, 
                        o.orderdate, 
                        o.totalprice, 
                        o.status, 
                        c.id AS cartid, 
                        u.email AS username
                FROM orders o
                JOIN carts c ON o.cartid = c.id
                JOIN users u ON o.userid = u.userid
                WHERE o.orderid = @OrderId",
     new { OrderId = orderId });

            if (order == null)
            {
                throw new Exception("Sipariş bulunamadı.");
            }

            // Siparişe ait çiçekleri getir
            var flowers = await _context.Database.GetDbConnection().QueryAsync<FlowerResponse>(
                @"SELECT f.flowerid, 
                 f.flowername, 
                 f.price, 
                 f.stockquantity,
                 f.description,
                 f.imageurl,
                 f.categoryid,
                 c.categoryname AS category
          FROM cart_flowers cf
          JOIN flowers f ON cf.flowerid = f.flowerid
          LEFT JOIN categories c ON f.categoryid = c.categoryid
          WHERE cf.cartid = @CartId",
                new { CartId = order.CartId });

            order.Flowers = flowers.ToList();  // Çiçekleri sipariş detaylarına ekle

            return order; // Sipariş detayını döndür
        }


    }
}