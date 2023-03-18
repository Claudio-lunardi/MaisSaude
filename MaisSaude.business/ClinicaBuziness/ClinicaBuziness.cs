using Dapper;
using MaisSaude.Infra.Dapper;
using MaisSaude.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.ClinicaBuziness
{
    public class ClinicaBuziness : IClinicaBuziness
    {
        private readonly ConnectionDapper _connectionDapper;

        public ClinicaBuziness(ConnectionDapper connectionDapper)
        {
            _connectionDapper = connectionDapper;
        }

        public async Task<Clinica> DetalhesClinica(int ID)
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();

                var TitularReturn = connection.QueryFirst<Clinica>("SELECT * FROM Clinica WHERE ID = @Id", param: new { ID });

                return TitularReturn;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EditarClinica(Clinica clinica)
        {
            clinica.DataAlteracao = DateTime.Now;
            clinica.TipoPermissao = "clinica";
            var connection = _connectionDapper.connectionString();
            connection.Open();
            string query = @"UPDATE Clinica 
                                SET Nome = @Nome, 
                                CNPJ = @CNPJ, 
                                Cep = @Cep,
                                Telefone = @Telefone, 
                                Cidade = @Cidade,
                                Estado = @Estado,
                                Complemento = @Complemento,
                                Numero = @Numero,
                                Logradouro = @Logradouro, 
                                DataAlteracao = @DataAlteracao,
                                TipoPermissao = @TipoPermissao,
                                Usuario = @Usuario,
                                Senha = @Senha,
                                Ativo = @Ativo WHERE ID = @ID";
            connection.ExecuteScalar(query, clinica);
        }

        public async Task IncluirClinica(Clinica clinica)
        {
            try
            {
                clinica.DataInclusao = DateTime.Now;
                clinica.TipoPermissao = "clinica";
                var connection = _connectionDapper.connectionString();
                connection.Open();
                string query = @"INSERT INTO Clinica
                                                   (Nome
                                                   ,CNPJ
                                                   ,Cep
                                                   ,Telefone
                                                   ,Cidade
                                                   ,Estado
                                                   ,Complemento
                                                   ,Numero
                                                   ,Logradouro
                                                   ,DataInclusao
                                                   ,TipoPermissao
                                                   ,Usuario
                                                   ,Senha
                                                   ,Ativo)
                                             VALUES
                                                   (@Nome, 
                                                    @CNPJ, 
                                                    @Cep, 
                                                    @Telefone, 
                                                    @Cidade, 
                                                    @Estado, 
                                                    @Complemento, 
                                                    @Numero,
                                                    @Logradouro,
                                                    @DataInclusao, 
                                                    @TipoPermissao, 
                                                    @Usuario,
                                                    @Senha,
                                                    @Ativo)";
                connection.ExecuteScalar(query, clinica);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Clinica>> ListaClinicas()
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();

                var TitularReturn = connection.Query<Clinica>("SELECT * FROM Clinica");

                return TitularReturn;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
