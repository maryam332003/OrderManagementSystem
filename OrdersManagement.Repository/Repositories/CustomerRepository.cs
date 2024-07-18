

using Microsoft.EntityFrameworkCore;
using OrdersManagement.Core;
using OrdersManagement.Core.Dtos;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Repository.Data;

namespace OrdersManagement.Repository.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>,ICustomerRepository
    {
        private readonly OrdersDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		public CustomerRepository(OrdersDbContext context , IUnitOfWork unitOfWork):base(context, unitOfWork)
        {
            _context = context;
	     	_unitOfWork = unitOfWork;
		}
        public async Task<Customer> CreateCustomer(CustomerDto customrDto )
        {
            var customer = new Customer()
            {
                Name = customrDto.Name,
                Email = customrDto.Email
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        

        public async Task<List<CustomerOrderDto>> GetAllOrderSForCustomer(int CustomerId)
        {
            var orders = await _context.Orders
             .Where(O => O.CustomerId == CustomerId)
             .Include(O => O.OrderItems)
             .Select(O => new CustomerOrderDto
             {
                 OrderId = O.Id,
                 OrderDate = O.OrderDate,
                 TotalAmount = O.TotalAmount,
                 PaymentMethod = O.PaymentMethod,
                 Status = O.Status,
                 OrderItems = O.OrderItems.Select(OI => new OrderItemDto
                 {
                     OrderItemId = OI.Id,
                     ProductId = OI.ProductId,
                     Quantity = OI.Quantity,
                     UnitPrice = OI.UnitPrice,
                     Discount = OI.Discount
                 }).ToList()
             })
             .ToListAsync();

            return orders;


        }

      
    }
}
