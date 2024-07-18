using OrdersManagement.Core.Entities;

namespace OrdersManagement.Core.Repositories.Interfaces
{ 

	public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<Invoice> GetInvoiceByIdAsync(int InvoiceId);
        Task<List<Invoice>> GetAllInvoicesAsync();
    }
}
