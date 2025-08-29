using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Persistence.Contracts
{
    /// <summary>
    /// Invoice Repository Interface
    /// </summary>
    public interface IInvoiceRepository
    {
        /// <summary>
        /// Gets Invoice DTO.
        /// </summary>
        /// <returns></returns>
        Task<List<Invoices>> Get();
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Number rows affected</returns>
        Task<int> Delete(int id);
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Invoice dto</returns>
        Task<Invoices> GetById(int id);

        /// <summary>
        /// Updates the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>Number columns affected</returns>
        Task<int> Update(Invoices invoice);

        /// <summary>
        /// Adds the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>Invoice dto</returns>
        Task<int> Add(Invoices invoice);
    }
}
