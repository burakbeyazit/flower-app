using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Request;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Application.Persistence;
using CicekApp.Application.Services.FlowerService;
using CicekApp.Application.Services.UserService;
using CicekApp.Domain.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace CicekApp.Application.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IFlowerService _flowerService;


        public CartService(AppDbContext context, IFlowerService flowerService, IUserService userService)
        {
            _context = context;
            _flowerService = flowerService;
            _userService = userService;
        }

        public async Task Add(AddtoChartRequest request)
        {
            var user = await _userService.GetByEmailAsync(request.Username);

            // Kullanıcının aktif sepetini al (sipariş verilmemiş)
            var cart = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<Cart>(
                "SELECT * FROM carts WHERE userid = @UserId AND orderid IS NULL",
                new { user.UserId });

            // Eğer sepet yoksa, yeni bir sepet oluştur
            if (cart == null)
            {
                var insertCartQuery = "INSERT INTO carts (userid, totalamount, createddate) " +
                                      "VALUES (@UserId, 0, CURRENT_TIMESTAMP) RETURNING id";

                // Yeni oluşturulan sepetin ID'sini almak için RETURNING kullanılır
                cart = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<Cart>(insertCartQuery, new { user.UserId });
            }

            // Çiçeği al
            var flower = await _flowerService.GetByIdAsync(request.FlowerId);

            // Çiçek miktarını 1 olarak ayarla
            var totalPrice = flower.Price * 1; // Quantity is set to 1

            // Sepetin toplam tutarını güncelle
            var updateCartQuery = "UPDATE Carts SET TotalAmount = TotalAmount + @TotalPrice WHERE Id = @CartId";
            await _context.Database.GetDbConnection().ExecuteAsync(updateCartQuery, new { TotalPrice = totalPrice, CartId = cart.Id });

            // Sepete çiçek ve 1 miktar ilişkisini ekle
            var insertCartFlowerQuery = "INSERT INTO cart_flowers (cartid, flowerid, quantity) VALUES (@CartId, @FlowerId, 1)";
            await _context.Database.GetDbConnection().ExecuteAsync(insertCartFlowerQuery, new { CartId = cart.Id, FlowerId = request.FlowerId });
        }



        public async Task Remove(AddtoChartRequest request)
        {
            var user = await _userService.GetByEmailAsync(request.Username);

            // Kullanıcının aktif sepetini al (sipariş verilmemiş)
            var cart = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<Cart>(
                "SELECT * FROM carts WHERE userid = @UserId AND orderid IS NULL",
                new { user.UserId });

            if (cart == null)
            {
                throw new Exception("Aktif bir sepet bulunamadı.");
            }

            // Sepetteki çiçeği al
            var cartFlower = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<dynamic>(
                "SELECT * FROM cart_flowers WHERE cartid = @CartId AND flowerid = @FlowerId",
                new { CartId = cart.Id, FlowerId = request.FlowerId });

            if (cartFlower == null)
            {
                throw new Exception("Çiçek bu sepette bulunamadı.");
            }

            // Çiçeği tamamen kaldır
            var deleteFlowerQuery = "DELETE FROM cart_flowers WHERE cartid = @CartId AND flowerid = @FlowerId";
            await _context.Database.GetDbConnection().ExecuteAsync(deleteFlowerQuery, new { CartId = cart.Id, FlowerId = request.FlowerId });

            // Çiçeğin fiyatını al
            var flower = await _flowerService.GetByIdAsync(request.FlowerId);

            // Sepetin toplam tutarını güncelle
            var totalPriceReduction = flower.Price * cartFlower.quantity;
            var updateCartQuery = "UPDATE carts SET totalamount = totalamount - @TotalPriceReduction WHERE id = @CartId";
            await _context.Database.GetDbConnection().ExecuteAsync(updateCartQuery, new { TotalPriceReduction = totalPriceReduction, CartId = cart.Id });
        }

        public async Task<List<CartViewResponse>> GetFlowersInCartbyUsername(string username)
        {
            // Kullanıcıyı e-posta adresiyle al
            var user = await _userService.GetByEmailAsync(username);

            // Kullanıcının aktif sepetini al (sipariş verilmemiş)
            var cart = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<Cart>(
                "SELECT * FROM carts WHERE userid = @UserId AND orderid IS NULL",
                new { user.UserId });

            if (cart == null)
            {
                return new List<CartViewResponse>(); // Sepet yoksa boş bir liste döndür
            }

            // Kullanıcının sepetindeki çiçekleri al
            var flowersInCart = await _context.Database.GetDbConnection().QueryAsync<CartViewResponse>(
                @"SELECT f.flowerid, 
                f.flowername AS flowername, 
                f.price, 
                f.stockquantity, 
                f.description, 
                f.imageurl, 
                f.categoryid, 
                c.categoryname AS category,
                cf.quantity,  -- Cart flower quantity ekleniyor
                @CartId AS CartId -- Sepet ID'si her çiçekle birlikte dönüyor
                FROM flowers f
                JOIN cart_flowers cf ON cf.flowerid = f.flowerid
                LEFT JOIN categories c ON f.categoryid = c.categoryid  -- LEFT JOIN kullanarak kategori verisi olmayan çiçekleri de al
                WHERE cf.cartid = @CartId",
                        new { CartId = cart.Id });

            return flowersInCart.ToList();
        }

        public async Task<List<CartViewResponse>> GetFlowersInCart(int cartId)
        {
            var flowersInCart = await _context.Database.GetDbConnection().QueryAsync<CartViewResponse>(
                            @"SELECT f.flowerid, 
                f.flowername AS flowername, 
                f.price, 
                f.stockquantity, 
                f.description, 
                f.imageurl, 
                f.categoryid, 
                c.categoryname AS category,
                cf.quantity,  -- Cart flower quantity ekleniyor
                @CartId AS CartId -- Sepet ID'si her çiçekle birlikte dönüyor
                FROM flowers f
                JOIN cart_flowers cf ON cf.flowerid = f.flowerid
                LEFT JOIN categories c ON f.categoryid = c.categoryid  -- LEFT JOIN kullanarak kategori verisi olmayan çiçekleri de al
                WHERE cf.cartid = @CartId",
                                    new { CartId = cartId });

            return flowersInCart.ToList();
        }

        public async Task UpdateCartItemQuantityAsync(UpdateCartItemRequest request)
        {
            // Prepare the query to update the cart flower quantity
            var result = await _context.Database.GetDbConnection().ExecuteAsync(
                @"UPDATE cart_flowers
            SET quantity = @Quantity
        WHERE cartid = @CartId
            AND flowerid = @FlowerId",
                new
                {
                    request.Quantity, // Updated quantity
                    request.CartId,   // Cart ID to specify which cart is being updated
                    request.FlowerId  // Flower ID to specify which flower's quantity to update
                });

            // Check if any row was updated
        }

    }
}