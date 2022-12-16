using System.Data.SqlClient;

namespace MaisSaude.API.Extencoes
{
    public static class ServicoExtencoes
    {
        public static void ConfigurarServicos(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<SqlConnection>( item => item. (configuration.GetConnectionString("DefaultConnection")));

        }
    }
}