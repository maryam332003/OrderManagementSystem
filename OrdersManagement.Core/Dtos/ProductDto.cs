using OrdersManagement.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrdersManagement.Core.Dtos
{
	public class ProductDto
	{
	
		public int Id { get; set; }
		
		public string Name { get; set; }
		
		public decimal Price { get; set; }
		public int Stock { get; set; }



	}
}
