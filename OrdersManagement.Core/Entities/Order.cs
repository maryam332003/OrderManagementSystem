using OrdersManagement.Core.Enums;

namespace OrdersManagement.Core.Entities
{
    public class Order : BaseEntity
	{
		public Customer Customer { get; set; }
		public int CustomerId { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.Now;
		public decimal TotalAmount { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; } 
		public PaymentMethodsEnum PaymentMethod { get; set; }
		public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;

	}
}