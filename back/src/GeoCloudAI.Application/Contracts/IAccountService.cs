using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    /// <summary>
    /// This interface defines the contract for account-related operations.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Adds the specified account dto.
        /// </summary>
        /// <param name="accountDto">The account dto.</param>
        /// <returns></returns>
        Task<AccountDto> Add(AccountDto accountDto);
        /// <summary>
        /// Updates the specified account dto.
        /// </summary>
        /// <param name="accountDto">The account dto.</param>
        /// <returns></returns>
        Task<AccountDto> Update(AccountDto accountDto);
        /// <summary>
        /// Deletes the specified account identifier.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <returns></returns>
        Task<int>        Delete(int accountId);
        /// <summary>
        /// Gets the specified page parameters.
        /// </summary>
        /// <param name="pageParams">The page parameters.</param>
        /// <returns></returns>
        Task<PageList<AccountDto>> Get(PageParams pageParams);
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <returns></returns>
        Task<AccountDto> GetById(int accountId);
        /// <summary>
        /// Gets the by unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        Task<int> GetByGuid(string guid);
    }
}