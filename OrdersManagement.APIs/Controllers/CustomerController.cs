
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.APIs.Errors;
using OrdersManagement.Core;
using OrdersManagement.Core.Dtos;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Repository;

namespace OrdersManagement. APIs.Controllers
{
    /// <summary>
    /// API Controller to manage Customers 
    /// </summary>
    public class CustomerController : BaseApiController
    {
        private readonly ICustomerRepository _customerRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CustomerController(ICustomerRepository customerRepository , IUnitOfWork  unitOfWork)
        {
            _customerRepository = customerRepository;
			_unitOfWork = unitOfWork;
		}

        #region AddCustomer
        /// <summary>
        /// Add New Customer.
        /// </summary>
        /// <param name="customerDto">Customer details.</param>
        /// <returns>Created Customer Details.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> AddCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var CreatedCustomer = new Customer()
            {
                Email = customerDto.Email,
                Name = customerDto.Name,
            };

            await _customerRepository.Add(CreatedCustomer);

            return Ok(customerDto);

        }
        #endregion

        #region GetCustomerOrders
        /// <summary>
        /// Retrieves Customer Orders
        /// </summary>
        /// <param name="customerId">Customer ID.</param>
        /// <returns>List of orders.</returns>
        [HttpGet("{customerId}/orders")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {

            var orders = await _unitOfWork.Repository<Customer>().GetByIdAsync(customerId);

            if (orders == null)
                return NotFound(new ApiResponse(statusCode: 404, message: "Customer with This Id Was not Found"));

            return Ok(orders);
        } 
        #endregion
    }
}