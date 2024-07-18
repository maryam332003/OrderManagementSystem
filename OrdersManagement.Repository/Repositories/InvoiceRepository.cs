using Microsoft.EntityFrameworkCore;
using OrdersManagement.Core;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Repository.Data;

namespace OrdersManagement.Repository.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly OrdersDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		public InvoiceRepository(OrdersDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
		{
			_context = context;
			_unitOfWork = unitOfWork;
		}
		public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            var Invoices = await _context.Invoices
                                         .Include(I => I.Order)
                                              .ThenInclude(I=>I.Customer)
                                         .Include(I=>I.Order)
                                               .ThenInclude(I=>I.OrderItems)
                                          .ToListAsync();
            return Invoices;
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int InvoiceId)
        {
            var Invoice = await _context.Invoices.Where(I => I.Id == InvoiceId)
                                          .Include(I => I.Order)
                                              .ThenInclude(I => I.Customer)
                                         .Include(I => I.Order)
                                               .ThenInclude(I => I.OrderItems)
                                        .FirstOrDefaultAsync();

            if(Invoice == null)
                    throw new ArgumentException("Invoice With This Id not Found");

            return Invoice;

        }
    }
}
