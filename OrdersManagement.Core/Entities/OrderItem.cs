using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersManagement.Core.Entities
{
	public class OrderItem : BaseEntity
	{
		public  Order Order { get; set; }
		public int OrderId { get; set; }
		public Product Product { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Discount { get; set; }

	}
}