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
            /*
            var invoices = new List<InvoiceData>
            {
                new InvoiceData { Id = "01", Invoice = "Basic Plan", Amount = "$860", Date = "Nov 22, 2021", Status = "Subscribed" },
                new InvoiceData { Id = "02", Invoice = "Premium Plan", Amount = "$1200", Date = "Nov 10, 2021", Status = "Unsubscribed" },
                new InvoiceData { Id = "03", Invoice = "Basic Plan", Amount = "$860", Date = "Nov 19, 2021", Status = "Subscribed" },
                new InvoiceData { Id = "04", Invoice = "Corporate Plan", Amount = "$1599", Date = "Nov 22, 2021", Status = "Subscribed" },
                new InvoiceData { Id = "05", Invoice = "Teste Plan", Amount = "$1000", Date = "Nov 22, 2025", Status = "Subscribed" }
            };

            return Ok(invoices);
            */

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

