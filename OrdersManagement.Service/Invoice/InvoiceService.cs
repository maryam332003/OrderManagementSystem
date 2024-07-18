using OrdersManagement.Core.Dtos;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Service.Invoices
{
	public class InvoiceService : IInvoiceService
	{
		private readonly IInvoiceRepository invoiceRepository;
		public InvoiceService(IInvoiceRepository invoiceRepository)
		{
			this.invoiceRepository = invoiceRepository;
		}

		public void CreateInvoice(Order order)
		{
			Invoice invoice = new Invoice();

			invoice.OrderId = order.Id;
			invoice.TotalAmount = order.TotalAmount;
			invoice.InvoiceDate = DateTime.Now;

			invoiceRepository.Add(invoice);
			invoiceRepository.Save();

		}
	}
}
