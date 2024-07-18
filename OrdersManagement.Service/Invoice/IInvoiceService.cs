using OrdersManagement.Core.Entities;

namespace OrdersManagement.Service.Invoices
{
		public	interface  IInvoiceService
	{

		public void CreateInvoice(Order order);
	}
}