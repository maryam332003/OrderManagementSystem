using OrdersManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Core.Dtos
{
	public class InvoiceDto
	{
		public Entities.Order Order { get; set; }
		public int OrderId { get; set; }
		public DateTime InvoiceDate { get; set; }

		public decimal TotalAmount { get; set; }

	}
}
