using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Core.Repositories.Interfaces
{
    public interface IOrderRepository 
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> GetDetailsForSpecificOrderAsync(int OrderId);
        Task<List<Order>> GetOrdersAsync();
        Task<Order> UpdateOrderStatusAsync(int orderId, OrderStatusEnum status);

    }
}
