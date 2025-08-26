using Dapper;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Data;

namespace GeoCloudAI.Persistence.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private DbSessionLIMS _db;

        public InvoiceRepository(DbSessionLIMS dbSession)
        {
            _db = dbSession;
        }
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
    }
}
