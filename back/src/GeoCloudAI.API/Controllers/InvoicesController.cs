using GeoCloudAI.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoCloudAI.API.Controllers
{
    /// <summary>
    /// Class InvoicesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoicesController"/> class.
        /// </summary>
        /// <param name="invoiceService">The invoice service.</param>
        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInvoices")]
        [AllowAnonymous]
        public async Task<IActionResult> GetInvoices()
        {
            try
            {
                var result = await _invoiceService.Get();
                if (result == null) return NotFound("No invoices found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error when trying to recover countries. Error: {ex.Message}");
            }
        }
    }
}

