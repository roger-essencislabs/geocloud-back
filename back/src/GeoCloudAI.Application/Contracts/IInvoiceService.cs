using GeoCloudAI.Application.Dtos;

namespace GeoCloudAI.Application.Contracts
{
    /// <summary>
    /// Class representing invoice-related operations.
    /// </summary>
    public interface IInvoiceService
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        Task<List<InvoiceDto>> Get();
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Number rows affected</returns>
        Task<int> Delete(int id);
        /// <summary>
        /// Updates the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>The invoice</returns>
        Task<InvoiceDto> Update(InvoiceDto invoice);
    }
}
