using System.ComponentModel.DataAnnotations;

namespace OrdersManagement.Core.Dtos
{
	public class CustomerDto
	{
		[Required]
		public string Name { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
