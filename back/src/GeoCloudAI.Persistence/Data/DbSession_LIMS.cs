using System;
using System.Data;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

namespace GeoCloudAI.Persistence.Data
{
    public class DbSessionLIMS : IDisposable
        {
            public IDbConnection Connection { get; }

            public DbSessionLIMS(IConfiguration configuration)
            {
                Connection = new MySqlConnection(configuration.GetConnectionString("LIMSConnection"));

                Connection.Open();
            }
            public void Dispose() => Connection?.Dispose();
        }
}