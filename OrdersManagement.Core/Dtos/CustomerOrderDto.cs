using OrdersManagement.Core.Enums;

namespace OrdersManagement.Core.Dtos
{
    public class CustomerOrderDto
	{
	public int OrderId { get; set; }
	public DateTime OrderDate { get; set; }
	public decimal TotalAmount { get; set; }
	public PaymentMethodsEnum PaymentMethod { get; set; }
	public OrderStatusEnum Status { get; set; }
	public List<OrderItemDto> OrderItems { get; set; }
}

}
   