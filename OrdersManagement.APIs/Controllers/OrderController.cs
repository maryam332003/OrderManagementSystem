
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.Core.Dtos;
using OrdersManagement.APIs.Errors;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;
using AutoMapper;
using OrdersManagement.Core.Enums;
using OrdersManagement.Service.Product.Contract;
using Microsoft.AspNetCore.Authorization;
using OrdersManagement.Service.Invoices;
using Microsoft.AspNetCore.SignalR;
using OrdersManagement.APIs.Helpers;
using OrdersManagement.Service;

namespace OrdersManagement.APIs.Controllers
{
    [Authorize]
    public class OrderController : BaseApiController
	{
        private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
        private readonly IValidateTheOrderServices _validateTheOrder;
        private readonly ILogger<OrderController> _logger;

        private readonly IInvoiceService _invoiceService;
        //private readonly IEmailService _emailService;
        IHubContext<NotificationHub> _hubContext;

        public OrderController(IOrderRepository orderRepository
                              , IMapper mapper
                              , IInvoiceService invoiceService
                              //, IEmailService emailService  
                              , IHubContext<NotificationHub> hubContext,
                              IValidateTheOrderServices validateTheOrder
                              ,ILogger<OrderController> logger)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_invoiceService = invoiceService;
            //_emailService = emailService;
            _validateTheOrder = validateTheOrder;
            _logger= logger;
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext)); 
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="orderDto">The order data transfer object containing order details.</param>
        /// <returns>The created order.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderDto orderDto)
        {
            if (orderDto == null)
                return BadRequest(new ApiResponse(400, "Order data is null"));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (orderDto.OrderItems == null || !orderDto.OrderItems.Any())
                return BadRequest(new ApiResponse(400, "Order items are required"));

            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.OrderItems.Sum(i => (i.UnitPrice - i.Discount) * i.Quantity),
                PaymentMethod = orderDto.PaymentMethod,
                Status = orderDto.Status,
                OrderItems = orderDto.OrderItems.Select(OI => new OrderItem
                {
                    ProductId = OI.ProductId,
                    Quantity = OI.Quantity,
                    UnitPrice = OI.UnitPrice,
                    Discount = OI.Discount
                }).ToList()
            };

            order.TotalAmount -= _validateTheOrder.Discount(order.TotalAmount);

            foreach (var item in orderDto.OrderItems)
            {
                if (!_validateTheOrder.Validate(item.Quantity, item.ProductId))
                {
                    return BadRequest(new ApiResponse(400, "The quantity isn't in stock"));
                }
            }

            var createdOrder = await _orderRepository.CreateOrderAsync(order);
            if (createdOrder != null)
            {
                _invoiceService.CreateInvoice(createdOrder);
            }

            var mappedOrder = _mapper.Map<OrderDto>(createdOrder);

            return Ok(mappedOrder);
        }


        #region GetAllOrders

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAllOrders()
		{
			var orders = await _orderRepository.GetOrdersAsync();

			if (orders == null)
				return NotFound(new ApiResponse(statusCode: 404, message: "There is no Orders :("));

			return Ok(orders);
		}
        #endregion

        #region GetOrderById
        /// <summary>
        /// Gets an order by ID.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>The order details.</returns>
        [HttpGet("{orderId}")]
        public async Task<ActionResult> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetDetailsForSpecificOrderAsync(orderId);

            if (order == null)
                return NotFound();

            return Ok(order);
        }


        #endregion

        #region UpdateOrderStatus
        /// <summary>
        /// Updates the status of an order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <param name="status">The new status of the order.</param>
        /// <returns>Action result indicating the outcome of the update operation.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] OrderStatusEnum status)
        {

       

            try
            {
                var order = await _orderRepository.GetDetailsForSpecificOrderAsync(orderId);
                if (order == null)
                {
                    _logger.LogError("Order not found. OrderId: {OrderId}", orderId);
                    return NotFound(new ApiResponse(statusCode: 404, message: "Order not found"));
                }

                await _orderRepository.UpdateOrderStatusAsync(orderId, status);

                var subject = "Your order status has been updated";
                var message = $"Dear {order.Customer?.Name},<br><br>Your order with ID {order.Id} has been updated to {status}.<br><br>Thank you for shopping with us.";

                var email = new Email
                {
                    Subject = subject,
                    Recipients = order?.Customer?.Email,
                    Body = message
                };

                if (string.IsNullOrEmpty(email.Recipients))
                {
                    _logger.LogError("Customer email is null or empty. OrderId: {OrderId}", orderId);
                    return StatusCode(500, new ApiResponse(statusCode: 500, message: "Customer email is not available"));
                }

                try
                {
                     EmailService.SendEmail(email);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send email. OrderId: {OrderId}", orderId);
                    return StatusCode(500, new ApiResponse(statusCode: 500, message: $"Failed to send email: {ex.Message}"));
                }

                var notificationMessage = $"Order ID {order.Id} status has been updated to {status}.";

                if (_hubContext == null)
                {
                    _logger.LogError("HubContext is null. OrderId: {OrderId}", orderId);
                    return StatusCode(500, new ApiResponse(statusCode: 500, message: "HubContext is not available"));
                }

                await _hubContext.Clients.All.SendAsync("ReceiveNotification", notificationMessage);

                return Ok(new ApiResponse(statusCode:200,message:$"Please Check Your Email{order?.Customer?.Email}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating order status. OrderId: {OrderId}", orderId);
                return StatusCode(500, new ApiResponse(statusCode: 500, message: "An error occurred while updating order status"));
            }

        } 

        #endregion
    }
}
