using AutoMapper;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Repositories;

namespace GeoCloudAI.Application.Services
{
    /// <summary>
    /// Class InvoiceService
    /// </summary>
    /// <seealso cref="GeoCloudAI.Application.Contracts.IInvoiceService" />
    public class InvoiceService : IInvoiceService
    {
        /// <summary>
        /// The invoice repository
        /// </summary>
        private readonly IInvoiceRepository _invoiceRepository;

        /// <summary>
        /// The mapper. Map Class (Invoices) > Dto (InvoiceDto)
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceService"/> class.
        /// </summary>
        /// <param name="invoiceyRepository">The invoicey repository.</param>
        /// <param name="mapper">The mapper.</param>
        public InvoiceService(IInvoiceRepository invoiceyRepository,
                              IMapper mapper)
        {
            _invoiceRepository = invoiceyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets Invoice DTO.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<List<InvoiceDto>> Get()
        {
            try
            {
                var invoices = await _invoiceRepository.Get();
                if (invoices == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<List<InvoiceDto>>(invoices);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Number os row affected</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<int> Delete(int id)
        {
            try
            {
                var numRegAffected = await _invoiceRepository.Delete(id);
                return numRegAffected;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
