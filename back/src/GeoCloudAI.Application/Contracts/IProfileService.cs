using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Application.Contracts
{
    /// <summary>
    /// This interface defines the contract for profile-related operations.
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Adds the specified profile dto.
        /// </summary>
        /// <param name="profileDto">The profile dto.</param>
        /// <returns></returns>
        Task<ProfileDto> Add(ProfileDto profileDto);
        /// <summary>
        /// Updates the specified profile dto.
        /// </summary>
        /// <param name="profileDto">The profile dto.</param>
        /// <returns></returns>
        Task<ProfileDto> Update(ProfileDto profileDto);
        /// <summary>
        /// Deletes the specified profile identifier.
        /// </summary>
        /// <param name="profileId">The profile identifier.</param>
        /// <returns></returns>
        Task<int>        Delete(int profileId);
        /// <summary>
        /// Gets the specified page parameters.
        /// </summary>
        /// <param name="pageParams">The page parameters.</param>
        /// <returns></returns>
        Task<PageList<ProfileDto>> Get(PageParams pageParams);
        /// <summary>
        /// Gets the by account.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="pageParams">The page parameters.</param>
        /// <returns></returns>
        Task<PageList<ProfileDto>> GetByAccount(int accountId, PageParams pageParams);
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="profileId">The profile identifier.</param>
        /// <returns></returns>
        Task<ProfileDto> GetById(int profileId);
    }
}