using GeoCloudAI.Application.Dtos;

namespace GeoCloudAI.Application.Contracts
{
    /// <summary>
    /// This interface defines the contract for country-related operations.
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Gets the specified term.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        Task<List<CountryDto>> Get(string term);
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        Task<CountryDto> GetById(int countryId);
    }
}