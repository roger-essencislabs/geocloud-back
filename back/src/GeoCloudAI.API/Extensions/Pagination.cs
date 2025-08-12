using System.Text.Json;
using GeoCloudAI.API.Models;

namespace GeoCloudAI.API.Extensions
{
    /// <summary>
    /// This static class provides an extension method to add pagination headers to an HTTP response.
    /// </summary>
    public static class Pagination
    {
        /// <summary>
        /// Adds the pagination.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">The total pages.</param>
        public static void AddPagination(this HttpResponse response,
            int totalCount, int currentPage, int pageSize, int totalPages)
        {
            var pagination = new PaginationHeaders(totalCount, currentPage, pageSize, totalPages);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(pagination, options));
            
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}