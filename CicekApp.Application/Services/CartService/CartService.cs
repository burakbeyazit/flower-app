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

            var cart = await _context.Carts
                .FirstOrDefaultAsync(x => x.UserId == user.UserId && x.OrderId == null);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = user.UserId,
                    TotalAmount = 0,
                    CreatedDate = DateTime.UtcNow, // <-- Dikkat!
                    CartFlowers = new List<CartFlowers>()
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var flower = await _flowerService.GetByIdAsync(request.FlowerId);
            var totalPrice = flower.Price * 1;

            // Sepette o çiçek zaten var mı kontrol et
            var existingCartFlower = await _context.CartFlowers
                .FirstOrDefaultAsync(cf => cf.CartId == cart.Id && cf.FlowerId == request.FlowerId);

            if (existingCartFlower != null)
            {
                existingCartFlower.Quantity += 1;
            }
            else
            {
                var cartFlower = new CartFlowers
                {
                    CartId = cart.Id,
                    FlowerId = request.FlowerId,
                    Quantity = 1
                };
                _context.Set<CartFlowers>().Add(cartFlower);
            }

            cart.TotalAmount += totalPrice;
            await _context.SaveChangesAsync();
        }




        public async Task Remove(AddtoChartRequest request)
        {
            var user = await _userService.GetByEmailAsync(request.Username);

            var cart = await _context.Carts
                .FirstOrDefaultAsync(x => x.UserId == user.UserId && x.OrderId == null);

            if (cart == null)
                throw new Exception("Aktif bir sepet bulunamadı.");

            var cartFlower = await _context.CartFlowers
                .FirstOrDefaultAsync(cf => cf.CartId == cart.Id && cf.FlowerId == request.FlowerId);

            if (cartFlower == null)
                throw new Exception("Çiçek bu sepette bulunamadı.");

            var flower = await _flowerService.GetByIdAsync(request.FlowerId);

            // Her seferinde sadece 1 adet çıkar
            cart.TotalAmount -= flower.Price;

            if (cartFlower.Quantity > 1)
            {
                cartFlower.Quantity -= 1;
            }
            else
            {
                _context.CartFlowers.Remove(cartFlower);
            }

            await _context.SaveChangesAsync();
        }


        public async Task<List<CartViewResponse>> GetFlowersInCartbyUsername(string username)
        {
            var user = await _userService.GetByEmailAsync(username);

            var cart = await _context.Carts
                .FirstOrDefaultAsync(x => x.UserId == user.UserId && x.OrderId == null);

            if (cart == null)
                return new List<CartViewResponse>();

            var flowersInCart = await _context.Set<CartFlowers>()
                .Where(cf => cf.CartId == cart.Id)
                .Select(cf => new CartViewResponse
                {
                    FlowerId = cf.FlowerId,
                    FlowerName = cf.Flower.FlowerName,
                    Price = cf.Flower.Price,
                    StockQuantity = cf.Flower.StockQuantity,
                    Description = cf.Flower.Description,
                    ImageUrl = cf.Flower.ImageUrl,
                    CategoryId = cf.Flower.CategoryId,
                    Category = cf.Flower.Category.CategoryName,
                    Quantity = cf.Quantity,
                    CartId = cart.Id
                })
                .ToListAsync();

            return flowersInCart;
        }


        public async Task<List<CartViewResponse>> GetFlowersInCart(int cartId)
        {
            var flowersInCart = await _context.Set<CartFlowers>()
                .Where(cf => cf.CartId == cartId)
                .Select(cf => new CartViewResponse
                {
                    FlowerId = cf.FlowerId,
                    FlowerName = cf.Flower.FlowerName,
                    Price = cf.Flower.Price,
                    StockQuantity = cf.Flower.StockQuantity,
                    Description = cf.Flower.Description,
                    ImageUrl = cf.Flower.ImageUrl,
                    CategoryId = cf.Flower.CategoryId,
                    Category = cf.Flower.Category.CategoryName,
                    Quantity = cf.Quantity,
                    CartId = cartId
                })
                .ToListAsync();

            return flowersInCart;
        }


        public async Task UpdateCartItemQuantityAsync(UpdateCartItemRequest request)
        {
            var cartFlower = await _context.Set<CartFlowers>()
                .FirstOrDefaultAsync(cf => cf.CartId == request.CartId && cf.FlowerId == request.FlowerId);

            if (cartFlower != null)
            {
                cartFlower.Quantity = request.Quantity;
                await _context.SaveChangesAsync();
            }
        }


    }
}