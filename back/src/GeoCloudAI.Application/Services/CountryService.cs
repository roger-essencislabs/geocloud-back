using AutoMapper;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Contracts;

namespace GeoCloudAI.Application.Services
{
    /// <summary>
    /// This class provides country-related services such as retrieving country information.
    /// </summary>
    /// <seealso cref="GeoCloudAI.Application.Contracts.ICountryService" />
    public class CountryService: ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryService"/> class.
        /// </summary>
        /// <param name="countryRepository">The country repository.</param>
        /// <param name="mapper">The mapper.</param>
        public CountryService(ICountryRepository countryRepository,
                              IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets the specified term.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<List<CountryDto>> Get(string term) 
        {
            try
            {
                var countries = await _countryRepository.Get(term);
                if (countries == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<List<CountryDto>>(countries);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<CountryDto> GetById(int countryId) 
        {
            try
            {
                var country = await _countryRepository.GetById(countryId);
                if (country == null) return null;
                //Map Class > Dto
                var result = _mapper.Map<CountryDto>(country);
                return result;         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}