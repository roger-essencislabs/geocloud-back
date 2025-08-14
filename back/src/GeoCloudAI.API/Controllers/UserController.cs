using Microsoft.AspNetCore.Mvc;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GeoCloudAI.API.Controllers
{
    /// <summary>
    /// This class is responsible for handling user-related operations.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {  
        private readonly IUserService _userService;

        private readonly IWebHostEnvironment _hostEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="hostEnvironment">The host environment.</param>
        public UserController(IUserService userService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _userService = userService;
        }
        /// <summary>
        /// Adds the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            try
            {
                var result = await _userService.Add(userDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add user. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Registers the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                var result = await _userService.Register(userDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to register user. Error: {ex.Message}");
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
        /// Updates the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            try
            {
                var result = await _userService.Update(userDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update user. Error: {ex.Message}");
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
                var result = await _userService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete user. Error: {ex.Message}");
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
                var result = await _userService.Get(pageParams);
                if(result == null) return NotFound("No users found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover users. Error: {ex.Message}");
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
                var result = await _userService.GetByAccount(accountId, pageParams);
                if(result == null) return NotFound("No users found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover users. Error: {ex.Message}");
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
                var result = await _userService.GetById(id);
                if(result == null) return NotFound("No user found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover user. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Gets the by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpGet]
        [Route("getByEmail")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var result = await _userService.GetByEmail(email);
                if(result == null) return NotFound("No user found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover user. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Logins the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest login)
        {
            try
            {
                var result = await _userService.Login(login.Email, login.Password);
                if(result == null) 
                    return NotFound("No user found");

                result.Token = _userService.GenerateToken(result).Result;
                result.Password = "";
                return Ok(result);
            }
            catch (Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Error when trying to recover user. Error: {ex.Message}");
            }
        }
    }
}