using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MysteryCaseInfrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseInfrastructure.Services
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection string not found.");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
