using Microsoft.EntityFrameworkCore;
using OrdersManagement.Core;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Enums;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Repository.Repositories
{
    public class OrderRepository :  IOrderRepository
    {
        private readonly OrdersDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		public OrderRepository(OrdersDbContext context, IUnitOfWork unitOfWork) 
		{
			_context = context;
			_unitOfWork = unitOfWork;
		}
		public async Task<Order?> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
            
        }

        public async Task<Order> GetDetailsForSpecificOrderAsync(int OrderId)
        {
            var order = await _context.Orders
                                      .Where(O => O.Id == OrderId)
                                       .Include(O=>O.Customer)
                                       .Include(O=>O.OrderItems)
                                      .FirstOrDefaultAsync();
            return order;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var orders = await _context.Orders.Include(O => O.Customer).Include(O=>O.OrderItems).ThenInclude(O=>O.Product).ToListAsync();
            return orders;
        }
        public async Task<Order> UpdateOrderStatusAsync(int orderId, OrderStatusEnum status)
        {
            var order = await _context.Orders.FindAsync(orderId);

            order.Status = status;
            await _context.SaveChangesAsync();
            return order;

        }
    }
}
