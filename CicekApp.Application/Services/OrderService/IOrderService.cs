using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Response.FlowerResponse;

namespace CicekApp.Application.Services.OrderService
{
    public interface IOrderService
    {
        Task<int> CreateOrder(string username, int cartId);
        Task<OrderResponse> GetOrderById(int orderId);
        Task<List<OrderResponse>> GetOrdersByUsername(string username);
    }
}