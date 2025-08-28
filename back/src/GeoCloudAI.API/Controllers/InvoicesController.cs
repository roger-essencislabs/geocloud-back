using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Services;
using GeoCloudAI.Domain.Classes;
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
        /// <returns>InvoiceDto</returns>
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
                   $"Error when trying to recover invoices. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Deletes the invoices.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Number records affected</returns>
        [HttpDelete]
        [Route("DeleteInvoices/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteInvoices(int id)
        {
            try
            {
                var result = await _invoiceService.Delete(id);
                if (result == 0) return NotFound("No invoices found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error when trying to recover invoices. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Updates the invoices.
        /// </summary>
        /// <param name="invoiceDto">The invoice dto.</param>
        /// <returns>The invoice dto</returns>
        [HttpPut]
        [Route("UpdateInvoices")]
        public async Task<IActionResult> UpdateInvoices(InvoiceDto invoiceDto)
        {
            try
            {
                var result = await _invoiceService.Update(invoiceDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Error when trying to update profile. Error: {ex.Message}");
            }
        }
    }
}

