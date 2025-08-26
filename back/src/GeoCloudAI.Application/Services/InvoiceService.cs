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
        private readonly IInvoiceRepository _invoiceRepository;

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
    }
}
