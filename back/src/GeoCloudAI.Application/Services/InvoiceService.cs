using AutoMapper;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Repositories;
using Microsoft.AspNetCore.Http;

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
        /// <summary>
        /// Updates the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>
        /// The invoice
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<InvoiceDto> Update(InvoiceDto invoice)
        {
            try
            {
                //Check if exist Profile
                var existInvoice = await _invoiceRepository.GetById(invoice.Id);
                if (existInvoice == null) 
                    return null;
                //Map Dto > Class
                var updateInvoice = _mapper.Map<Domain.Classes.Invoices>(invoice);
                //Update Profile
               
                var resultCode = await _invoiceRepository.Update(updateInvoice); // resultCode = "0" or "1"
                if (resultCode == 0) 
                    return null;
                //Get Updated Invoice
                var result = await _invoiceRepository.GetById(updateInvoice.Id);
                if (result == null) 
                    return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<InvoiceDto>(result);
                
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Add/ Creates the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>
        /// The invoice dto
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<InvoiceDto> Add(InvoiceDto invoice)
        {
            try
            {
                //Map Dto > Class
                var addInvoice = _mapper.Map<Invoices>(invoice);
                //Add Invoice
                var resultCode = await _invoiceRepository.Add(addInvoice); // resultCode = "0" or "new Id"
                if (resultCode == 0) 
                    return null;
                //Get New Invoice
                var result = await _invoiceRepository.GetById(resultCode);
                if (result == null) 
                    return null;
                //Map Class > Dto
                var resultDto = _mapper.Map<InvoiceDto>(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
