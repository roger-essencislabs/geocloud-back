using Dapper;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Data;

namespace GeoCloudAI.Persistence.Repositories
{
    /// <summary>
    /// Class InvoiceRepository
    /// </summary>
    /// <seealso cref="GeoCloudAI.Persistence.Contracts.IInvoiceRepository" />
    public class InvoiceRepository : IInvoiceRepository
    {
        /// <summary>
        /// The database session to LIMS
        /// </summary>
        private DbSessionLIMS _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
        /// </summary>
        /// <param name="dbSession">The database session.</param>
        public InvoiceRepository(DbSessionLIMS dbSession)
        {
            _db = dbSession;
        }
        /// <summary>
        /// Get Invoice DTO.
        /// </summary>
        /// <returns>Tabela com Lista de Invoices</returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<Invoices>> Get()
        {
            try
            {
                var conn = _db.Connection;

                string query = @"SELECT * FROM INVOICES ";

                var invoices = (await conn.QueryAsync<Invoices>(sql: query)).ToList();

                return invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">Index ID to be removed</param>
        /// <returns>Number rows affected</returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> Delete(int id)
        {
            try
            {
                var conn = _db.Connection;
                string command = @"DELETE FROM INVOICES WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
