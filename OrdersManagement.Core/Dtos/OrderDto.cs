using OrdersManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Core.Dtos
{
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public PaymentMethodsEnum PaymentMethod { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}
