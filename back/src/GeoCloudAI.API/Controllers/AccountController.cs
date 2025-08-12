using Microsoft.AspNetCore.Mvc;
using GeoCloudAI.Application.Dtos;
using GeoCloudAI.Application.Contracts;
using GeoCloudAI.Persistence.Models;
using GeoCloudAI.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GeoCloudAI.API.Controllers
{
    /// <summary>
    /// This controller is responsible for managing accounts.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {  
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Adds the specified account dto.
        /// </summary>
        /// <param name="accountDto">The account dto.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(AccountDto accountDto)
        {
            try
            {
                var result = await _accountService.Add(accountDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to add account. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Updates the specified account dto.
        /// </summary>
        /// <param name="accountDto">The account dto.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(AccountDto accountDto)
        {
            try
            {
                var result = await _accountService.Update(accountDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to update account. Error: {ex.Message}");
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
                var result = await _accountService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to delete account. Error: {ex.Message}");
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
                var result = await _accountService.Get(pageParams);
                if(result == null) return NotFound("No accounts found");

                Response.AddPagination(result.TotalCount, result.CurrentPage, result.PageSize, result.TotalPages);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover accounts. Error: {ex.Message}");
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
                var result = await _accountService.GetById(id);
                if(result == null) return NotFound("No account found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover account. Error: {ex.Message}");
            }
        }
    }
}