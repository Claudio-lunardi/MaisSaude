using Dapper;
using MaisSaude.Common;
using MaisSaude.Infra.Dapper;
using MaisSaude.Models;
using System.Data;

namespace MaisSaude.Business.Login_home
{
    public class Login : ILogin
    {
        private readonly ConnectionDapper _connectionDapper;

        public Login(ConnectionDapper connectionDapper)
        {
            _connectionDapper = connectionDapper;
        }

        public UsuarioAutenticado LoginHome(string Usuario, string Senha)
        {
            var connection = _connectionDapper.connectionString();
            connection.Open();


            UsuarioAutenticado result = connection.QueryFirstOrDefault<UsuarioAutenticado>("autenticar", commandType: CommandType.StoredProcedure, param: new { Usuario, Senha });

            return result;

        }
    }
}
