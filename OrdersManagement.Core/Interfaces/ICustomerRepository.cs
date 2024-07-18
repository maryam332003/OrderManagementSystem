
using OrdersManagement.Core.Dtos;
using OrdersManagement.Core.Entities;

namespace OrdersManagement.Core.Repositories.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> CreateCustomer(CustomerDto customerDto);
        Task<List<CustomerOrderDto>> GetAllOrderSForCustomer(int CustomerId);
    }
}
