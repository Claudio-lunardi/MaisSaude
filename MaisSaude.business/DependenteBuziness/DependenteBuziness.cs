using Dapper;
using Dapper.Contrib.Extensions;
using MaisSaude.Infra.Dapper;
using MaisSaude.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.DependenteBuziness
{


    public class DependenteBuziness : IDependenteBuziness
    {
        private readonly ConnectionDapper _connectionDapper;

        public DependenteBuziness(ConnectionDapper connectionDapper)
        {
            _connectionDapper = connectionDapper;
        }

        public async Task<IEnumerable<Dependente>> DetalhesDependente(string CPFDependente)
        {
            var connection = _connectionDapper.connectionString();
            connection.Open();

            var retorno = connection.QueryAsync<Dependente, Titular, Dependente>(
                    sql: "SELECT * FROM Dependente INNER JOIN Titular on Titular.CPFTitular = Dependente.CPFTitular WHERE CPFDependente = @CPFDependente",
                    map: (invoice, titular) =>
                    {
                        invoice.Titular = titular;
                        return invoice;
                    },
                    param: new { CPFDependente },
                    splitOn: "CPFTitular");

            return await retorno;
        }

        public async Task EditarDependente(Dependente dependente)
        {
            dependente.DataAlteracao = DateTime.Now;    
            var connection = _connectionDapper.connectionString();
            connection.Open();
            connection.ExecuteScalar(@"UPDATE Dependente SET CPFTitular = @CPFTitular, RG = @RG, Nome = @Nome, DataNascimento = @DataNascimento, Email = @Email,Telefone = @Telefone,Celular = @Celular,DataInclusao = @DataInclusao, DataAlteracao = @DataAlteracao,TipoPermissao = @TipoPermissao,Usuario = @Usuario,Senha = @Senha WHERE CPFDependente = @CPFDependente", dependente);
        }

        public async Task IncluirDependenteAsync(Dependente dependente)
        {

            dependente.DataInclusao = DateTime.Now;
            dependente.TipoPermissao = "dependente";
            var connection = _connectionDapper.connectionString();
            connection.Open();
            connection.ExecuteScalar("INSERT INTO Dependente (CPFDependente ,CPFTitular ,RG ,Nome ,DataNascimento ,Email ,Telefone ,Celular ,DataInclusao,TipoPermissao ,Usuario ,Senha) VALUES (@CPFDependente,@CPFTitular, @RG,@Nome,@DataNascimento, @Email,@Telefone,@Celular,@DataInclusao, @TipoPermissao,@Usuario,@Senha)", dependente);

        }

        public async Task<IEnumerable<Dependente>> ListaDependentes()
        {
            var connection = _connectionDapper.connectionString();
            connection.Open();

            var List = connection.Query<Dependente>("SELECT * FROM Dependente");
            return List;
        }


    }
}
