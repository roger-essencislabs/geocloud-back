using Dapper;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Data;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Invoice
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<Invoices> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;

                string query = @"SELECT * FROM INVOICES WHERE ID = " + id;

                var invoices = (await conn.QueryAsync<Invoices>(sql: query));

                if (invoices.Count() == 0) return null;
                return invoices.First();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Updates the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>Number columns affected</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<int> Update(Invoices invoice)
        {
            try
            {
                var conn = _db.Connection;
                if (invoice.Id == 0) { return 0; }
                string command = @"UPDATE INVOICES SET 
                                    id      = @id,
                                    invoice = @invoice,
                                    amount  = @amount,
                                    date    = @date,
                                    status  = @status
                                    WHERE id= @id";
                var result = await conn.ExecuteAsync(sql: command, param: invoice);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Add(Invoices invoice)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                //using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled, TransactionScopeOption.Required))
                {
                    string command = @" INSERT INTO INVOICES(
                                            invoice, 
                                            amount,
                                            date,  
                                            status
                                        )
                                        VALUES(@invoice, @amount,@date, @status);
                                        SELECT LAST_INSERT_ID();";
                    var result = await conn.ExecuteScalarAsync<int>(sql: command, param: invoice);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
