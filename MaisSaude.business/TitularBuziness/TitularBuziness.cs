using Dapper;
using MaisSaude.Infra.Dapper;
using MaisSaude.Models;

namespace MaisSaude.Business.TitularBuziness
{

    public class TitularBuziness : ITitularBuziness
    {
        private readonly ConnectionDapper _connectionDapper;

        public TitularBuziness(ConnectionDapper connectionDapper)
        {
            _connectionDapper = connectionDapper;
        }
        public Titular GetTitular(int ID)
        {

            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();

                var TitularReturn = connection.QueryFirst<Titular>("SELECT * FROM Titular WHERE ID = @ID", param: new { ID });


                return TitularReturn;
            }
            catch (Exception x)
            {

                throw x;
            }


        }
        public async Task UpdateTitular(Titular titular)
        {
            try
            {
                titular.DataAlteracao = DateTime.Now;
                var connection = _connectionDapper.connectionString();
                connection.Open();
                string query = @"UPDATE Titular SET RG = @RG, Nome = @Nome, DataNascimento = @DataNascimento, Telefone = @Telefone, Celular = @Celular, Ativo = @Ativo, Email = @Email, Cep = @Cep, Cidade = @Cidade, Estado = @Estado, Complemento = @Complemento, Numero = @Numero, Logradouro = @Logradouro, DataAlteracao = @DataAlteracao,TipoPermissao = @TipoPermissao, Usuario = @Usuario, Senha = @Senha WHERE ID = @ID";

                connection.ExecuteScalar(query, titular);

            }
            catch (Exception x)
            {

                throw x;
            }


        }
        public async Task InsertTitular(Titular titular)
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();
                titular.TipoPermissao = "titular";
                titular.DataInclusao = DateTime.Now;
                string query = @"INSERT INTO Titular (CPF_Titular , RG , Nome , DataNascimento , Telefone , Celular , Ativo , Email , Cep , Cidade , Estado , Complemento , Numero , Logradouro , DataInclusao ,TipoPermissao,Usuario,Senha ) VALUES (@CPF_Titular,@RG,@Nome,@DataNascimento,@Telefone,@Celular,@Ativo,@Email,@Cep,@Cidade,@Estado,@Complemento,@Numero,@Logradouro,@DataInclusao,@TipoPermissao,@Usuario,@Senha)";
                connection.ExecuteScalar(query, titular);
                connection.Close();

            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public async Task<List<Titular>> GetTitulares()
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();
                var TitularReturn = connection.Query<Titular>("SELECT * FROM Titular").ToList();
                connection.Close();
                return TitularReturn;

            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public async Task<List<Dependente>> GetDependentes(string CPF)
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();

                var TitularReturn = connection.Query<Dependente>("SELECT * FROM Dependente WHERE CPF_titular = @CPF", param: new { CPF }).ToList();

                return TitularReturn;

            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
