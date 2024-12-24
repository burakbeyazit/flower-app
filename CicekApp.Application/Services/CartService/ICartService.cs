using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Request;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Domain.Entities;

namespace CicekApp.Application.Services.CartService
{
    public interface ICartService
    {
        Task Add(AddtoChartRequest request);

        Task Remove(AddtoChartRequest request);
        Task<List<CartViewResponse>> GetFlowersInCartbyUsername(string username);
        Task<List<CartViewResponse>> GetFlowersInCart(int cartId);
        Task UpdateCartItemQuantityAsync(UpdateCartItemRequest request);
    }
}