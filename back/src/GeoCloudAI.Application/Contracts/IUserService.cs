using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    /// <summary>
    /// This interface defines the contract for user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Adds the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns></returns>
        Task<UserDto> Add(UserDto userDto);
        /// <summary>
        /// Registers the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns></returns>
        Task<string>  Register(UserDto userDto);
        /// <summary>
        /// Updates the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns></returns>
        Task<UserDto> Update(UserDto userDto);
        Task<int>     Delete(int userId);

        Task<PageList<UserDto>> Get(PageParams pageParams);
        /// <summary>
        /// Gets the by account.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="pageParams">The page parameters.</param>
        /// <returns></returns>
        Task<PageList<UserDto>> GetByAccount(int accountId, PageParams pageParams);
        Task<UserDto> GetById(int userId);
        /// <summary>
        /// Gets the by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<UserDto> GetByEmail(string email);
        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<UserDto> Login(string email, string password);
        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns></returns>
        Task<string>  GenerateToken(UserDto userDto);
    }
}