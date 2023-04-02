using Dapper;
using MaisSaude.Business.Rabbit;
using MaisSaude.Common;
using MaisSaude.Infra.Dapper;
using MaisSaude.Models;
using System.Data;

namespace MaisSaude.Business.Login_home
{
    public class Login : ILogin
    {
        private readonly ConnectionDapper _connectionDapper;
        private readonly IMensageria _mensageria;
        public Login(ConnectionDapper connectionDapper, IMensageria mensageria)
        {
            _connectionDapper = connectionDapper;
            _mensageria = mensageria;
        }

        public UsuarioAutenticado LoginHome(string Usuario, string Senha)
        {
            var connection = _connectionDapper.connectionString();
            connection.Open();


            UsuarioAutenticado result = connection.QueryFirstOrDefault<UsuarioAutenticado>("autenticar", commandType: CommandType.StoredProcedure, param: new { Usuario, Senha });

            return result;

        }
        public void RestaurarSenha(RestaurarSenha redefinirSenha)
        {
            var connection = _connectionDapper.connectionString();
            connection.Open();

            RestaurarSenha result = connection.QueryFirstOrDefault<RestaurarSenha>("RestaurarSenha", commandType: CommandType.StoredProcedure, param: new { redefinirSenha.CPF, redefinirSenha.Email, redefinirSenha.Usuario });
            if (result != null)
            {
                _mensageria.EnviarMensagemRabbit(result, "", "Restaurar senha");
            }
          
        }

    }
}
