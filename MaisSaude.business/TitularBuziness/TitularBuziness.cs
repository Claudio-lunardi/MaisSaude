using Dapper;
using MaisSaude.Infra.Dapper;
using MaisSaude.Models;

namespace MaisSaude.Business.TitularBuziness
{

    public partial class TitularBuziness : ITitularBuziness
    {
        private readonly ConnectionDapper _connectionDapper;

        public TitularBuziness(ConnectionDapper connectionDapper)
        {
            _connectionDapper = connectionDapper;
        }

        public Titular DetalhesTitular(string CPFTitular)
        {

            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();

                var TitularReturn = connection.QueryFirst<Titular>("SELECT * FROM Titular WHERE CPFTitular = @CPFTitular", param: new { CPFTitular });


                return TitularReturn;
            }
            catch (Exception)
            {

                throw;
            }


        }
        public async Task UpdateTitularAsync(Titular titular)
        {
            try
            {
                titular.DataAlteracao = DateTime.Now;
                var connection = _connectionDapper.connectionString();
                connection.Open();
                string query = @"UPDATE Titular SET RG = @RG, Nome = @Nome, DataNascimento = @DataNascimento, Telefone = @Telefone, Celular = @Celular, Ativo = @Ativo, Email = @Email, Cep = @Cep, Cidade = @Cidade, Estado = @Estado, Complemento = @Complemento, Numero = @Numero, Logradouro = @Logradouro, DataAlteracao = @DataAlteracao,TipoPermissao = @TipoPermissao, Usuario = @Usuario, Senha = @Senha WHERE CPFTitular = @CPFTitular";

                var a = connection.ExecuteScalar(query, titular);




            }
            catch (Exception)
            {

                throw;
            }


        }
        public async Task InsertTitularAsync(Titular titular)
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();
                titular.TipoPermissao = "titular";
                titular.DataInclusao = DateTime.Now;
                string query = @"INSERT INTO Titular (CPFTitular , RG , Nome , DataNascimento , Telefone , Celular , Ativo , Email , Cep , Cidade , Estado , Complemento , Numero , Logradouro , DataInclusao ,TipoPermissao,Usuario,Senha ) VALUES (@CPFTitular,@RG,@Nome,@DataNascimento,@Telefone,@Celular,@Ativo,@Email,@Cep,@Cidade,@Estado,@Complemento,@Numero,@Logradouro,@DataInclusao,@TipoPermissao,@Usuario,@Senha)";
                connection.ExecuteScalar(query, titular);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Titular>> ListaTitulares()
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();

                var TitularReturn = connection.Query<Titular>("SELECT * FROM Titular");

                return TitularReturn;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Dependente>> ListaDependentes(string CPFTitular)
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();

                var TitularReturn = connection.Query<Dependente>("SELECT * FROM Dependente WHERE CPFTitular = @CPFTitular", new { CPFTitular });

                return TitularReturn;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
