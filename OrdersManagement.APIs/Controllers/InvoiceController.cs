using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.APIs.Errors;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;

namespace OrdersManagement.APIs.Controllers
{
    /// <summary>
    /// API Controller to Manage Invoices.
    /// </summary>
    [Authorize(Roles = "Admin")]
	public class InvoiceController : BaseApiController
	{
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        #region GetAllInvoices
        /// <summary>
        /// Retrieves all Invoices.
        /// </summary>
        /// <returns>List of Invoices.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Invoice>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<Invoice>>> GetAll()
        {
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();
            if (invoices == null)
                return NotFound(new ApiResponse(statusCode: 404, message: "There is No Invoice With This Id"));
            return Ok(invoices);
        }

        #endregion


        #region GetInvoiceById
        /// <summary>
        /// Retrieves an invoice by ID.
        /// </summary>
        /// <param name="InvoiceId">Invoice ID.</param>
        /// <returns>Invoice Details.</returns>
        [HttpGet("{InvoiceId}")]
        [ProducesResponseType(typeof(Invoice), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Invoice>> GetInvoiceById(int InvoiceId)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(InvoiceId);
            if (invoice == null)
                return NotFound();
            return Ok(invoice);
        } 
        #endregion
    }
}
