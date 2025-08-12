using Microsoft.AspNetCore.Mvc;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GeoCloudAI.API.Controllers
{
    /// <summary>
    /// This controller is responsible for managing user profiles.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {  
        private readonly IProfileService _profileService;

        private readonly IWebHostEnvironment _hostEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="profileService">The profile service.</param>
        /// <param name="hostEnvironment">The host environment.</param>
        public ProfileController(IProfileService profileService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _profileService = profileService;
        }
        /// <summary>
        /// Adds the specified profile dto.
        /// </summary>
        /// <param name="profileDto">The profile dto.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(ProfileDto profileDto)
        {
            try
            {
                var result = await _profileService.Add(profileDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add profile. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="pathName">Name of the path.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpPost]
        [Route("uploadImage")]
        public async Task<IActionResult> UploadImage(string pathName)
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0) {
                    var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, pathName);
                    //Create directory (if necessary)
                    FileInfo finfo = new FileInfo(pathName);
                    if (!Directory.Exists(finfo.DirectoryName)) {
                        Directory.CreateDirectory(finfo.DirectoryName!);
                    };
                    using ( var fileStream = new FileStream(imagePath, FileMode.Create)) {
                        await file.CopyToAsync(fileStream);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to upload image. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Updates the specified profile dto.
        /// </summary>
        /// <param name="profileDto">The profile dto.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(ProfileDto profileDto)
        {
            try
            {
                var result = await _profileService.Update(profileDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update profile. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _profileService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete profile. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Gets the specified page parameters.
        /// </summary>
        /// <param name="pageParams">The page parameters.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _profileService.Get(pageParams);
                if(result == null) return NotFound("No profiles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover profiles. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Gets the by account.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="pageParams">The page parameters.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpGet]
        [Route("getByAccount")]
        public async Task<IActionResult> GetByAccount(int accountId, [FromQuery]PageParams pageParams)
        {
            try
            {
                var result = await _profileService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No profiles found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover profiles. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _profileService.GetById(id);
                if(result == null) return NotFound("No profile found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover profile. Error: {ex.Message}");
            }
        }
    }
}