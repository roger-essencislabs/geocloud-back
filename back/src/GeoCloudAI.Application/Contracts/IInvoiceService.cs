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
    }
}
