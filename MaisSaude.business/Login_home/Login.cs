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

            //Verifica se o usuario é títular ou dependente TRUE/FALSE
            var TitularOuDependente = connection.Query<UsuarioAutenticado>("autenticar", commandType: CommandType.StoredProcedure, param: new { Usuario, Senha }).FirstOrDefault();

            if (TitularOuDependente.Titular)
            {
                var TitularReturn = connection.QueryFirst<UsuarioAutenticado>("SELECT Nome,Usuario,Email,TipoPermissao FROM Titular WHERE Usuario = @Usuario AND Senha = @Senha", param: new { Usuario, Senha });

                UsuarioAutenticado usuarsio = new UsuarioAutenticado()
                {
                    Dependente = TitularOuDependente.Dependente,
                    Titular = TitularOuDependente.Titular,
                    Nome = TitularReturn.Nome,
                    Email = TitularReturn.Email,
                    Usuario = TitularReturn.Usuario,
                    TipoPermissao = TitularReturn.TipoPermissao
                };
                return usuarsio;
            }
            else if (TitularOuDependente.Dependente)
            {
                var TitularReturn = connection.QueryFirst<UsuarioAutenticado>("SELECT Nome,Usuario,Email,TipoPermissao FROM Dependente WHERE Usuario = @Usuario AND Senha = @Senha", param: new { Usuario, Senha });

                UsuarioAutenticado usuarsio = new UsuarioAutenticado()
                {
                    Dependente = TitularOuDependente.Dependente,
                    Titular = TitularOuDependente.Titular,
                    Nome = TitularReturn.Nome,
                    Email = TitularReturn.Email,
                    Usuario = TitularReturn.Usuario,
                    TipoPermissao = TitularReturn.TipoPermissao
                };
                return usuarsio;
            }
            else if (TitularOuDependente.Clinica)
            {
                var TitularReturn = connection.QueryFirst<UsuarioAutenticado>("SELECT Nome,Usuario,Email,TipoPermissao FROM Clinica WHERE Usuario = @Usuario AND Senha = @Senha", param: new { Usuario, Senha });

                UsuarioAutenticado usuarsio = new UsuarioAutenticado()
                {
                    Dependente = TitularOuDependente.Dependente,
                    Titular = TitularOuDependente.Titular,
                    Nome = TitularReturn.Nome,
                    Email = TitularReturn.Email,
                    Usuario = TitularReturn.Usuario,
                    TipoPermissao = TitularReturn.TipoPermissao
                };
                return usuarsio;
            }

            return usuario;

        }
    }
}
