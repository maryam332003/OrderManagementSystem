using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Core.Entities
{
	public class Invoice : BaseEntity
	{
		public Order Order { get; set; }
		public int OrderId { get; set; }
		public DateTime InvoiceDate { get; set; }

		public decimal TotalAmount { get; set; }

	}
}
