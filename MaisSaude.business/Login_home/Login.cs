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
            UsuarioAutenticado usuario = new UsuarioAutenticado();
            var connection = _connectionDapper.connectionString();
            connection.Open();

            //Verifica se o usuario é títular , dependente TRUE/FALSE
            UsuarioAutenticado TitularOuDependente = connection.QueryFirstOrDefault<UsuarioAutenticado>("autenticar", commandType: CommandType.StoredProcedure, param: new { Usuario, Senha });

            if (TitularOuDependente == null)
                return null;


            if (TitularOuDependente.Titular)
            {
                var TitularReturn = connection.QuerySingle<UsuarioAutenticado>("SELECT Nome,Usuario,Email,TipoPermissao FROM Titular WHERE Usuario = @Usuario AND Senha = @Senha", param: new { Usuario, Senha });

                UsuarioAutenticado usuarsio = new UsuarioAutenticado()
                {
                    Dependente = TitularOuDependente.Dependente,
                    Titular = TitularOuDependente.Titular,
                    Nome = TitularReturn.Nome,
                    Email = TitularReturn.Email,
                    Usuario = TitularReturn.Usuario,
                    TipoPermissao = TitularReturn.TipoPermissao,
                    Clinica = TitularOuDependente.Clinica
                };
                return usuarsio;
            }
            else if (TitularOuDependente.Dependente)
            {
                var TitularReturn = connection.QuerySingle<UsuarioAutenticado>("SELECT Nome,Usuario,Email,TipoPermissao FROM Dependente WHERE Usuario = @Usuario AND Senha = @Senha", param: new { Usuario, Senha });

                UsuarioAutenticado usuarsio = new UsuarioAutenticado()
                {
                    Dependente = TitularOuDependente.Dependente,
                    Titular = TitularOuDependente.Titular,
                    Nome = TitularReturn.Nome,
                    Email = TitularReturn.Email,
                    Usuario = TitularReturn.Usuario,
                    TipoPermissao = TitularReturn.TipoPermissao,
                    Clinica = TitularOuDependente.Clinica
                };
                return usuarsio;
            }
            else if (TitularOuDependente.Clinica)
            {
                var TitularReturn = connection.QuerySingle<UsuarioAutenticado>("SELECT NomeClinica as Nome,Usuario,TipoPermissao FROM Clinica WHERE Usuario = @Usuario AND Senha = @Senha", param: new { Usuario, Senha });

                UsuarioAutenticado usuarsio = new UsuarioAutenticado()
                {
                    Dependente = TitularOuDependente.Dependente,
                    Titular = TitularOuDependente.Titular,
                    Nome = TitularReturn.Nome,
                    Email = TitularReturn.Email,
                    Usuario = TitularReturn.Usuario,
                    TipoPermissao = TitularReturn.TipoPermissao,
                    Clinica = TitularOuDependente.Clinica
                };
                return usuarsio;
            }

            return usuario;

        }
    }
}
