using GeoCloudAI.Domain.Classes;

namespace GeoCloudAI.Persistence.Contracts
{
    public interface IInvoiceRepository
    {
        Task<List<Invoices>> Get();
    }
}
