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

        public async Task<Clinica> DetalhesClinica(int Id)
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();

                var TitularReturn = connection.QueryFirst<Clinica>("SELECT * FROM Clinica WHERE Id = @Id", param: new { Id });

                return TitularReturn;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task EditarClinica(Clinica clinica)
        {
            throw new NotImplementedException();
        }

        public async Task IncluirClinica(Clinica clinica)
        {
            try
            {
                clinica.DataInclusao = DateTime.Now;
                var connection = _connectionDapper.connectionString();
                connection.Open();
                string query = @"INSERT INTO Clinica
                                                   (NomeClinica
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
                                                   (@NomeClinica, 
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
