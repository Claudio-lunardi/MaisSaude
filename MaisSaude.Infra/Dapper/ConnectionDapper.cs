using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace MaisSaude.Infra.Dapper
{
    public class ConnectionDapper
    {
        private readonly IConfiguration _configuration;

        public ConnectionDapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection connectionString()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }

}