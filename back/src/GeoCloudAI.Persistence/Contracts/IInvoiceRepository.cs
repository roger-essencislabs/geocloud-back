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
    }
}
