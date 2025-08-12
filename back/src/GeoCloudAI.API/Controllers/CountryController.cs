using Microsoft.AspNetCore.Mvc;
using GeoCloudAI.Application.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace GeoCloudAI.API.Controllers
{
    /// <summary>
    /// This controller is responsible for managing countries.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {  
        private readonly ICountryService _countryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryController"/> class.
        /// </summary>
        /// <param name="countryService">The country service.</param>
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        /// <summary>
        /// Gets the specified term.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns>OK[200](with result or Exception message)</returns>
        [HttpGet]
        [Route("get")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string? term)
        {
            try
            {
                var result = await _countryService.Get(term!);
                if(result == null) return NotFound("No countries found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover countries. Error: {ex.Message}");
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
                var result = await _countryService.GetById(id);
                if(result == null) return NotFound("No country found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                   $"Error when trying to recover country. Error: {ex.Message}");
            }
        }
    }
}