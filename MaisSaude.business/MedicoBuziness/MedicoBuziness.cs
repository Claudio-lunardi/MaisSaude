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

namespace MaisSaude.Business.MedicoBuziness
{


    public class MedicoBuziness : IMedicoBuziness
    {
        private readonly ConnectionDapper _connectionDapper;

        public MedicoBuziness(ConnectionDapper connectionDapper)
        {
            _connectionDapper = connectionDapper;
        }

        public async Task<Medico> GetMedico(int ID)
        {
            var connection = _connectionDapper.connectionString();
            connection.Open();

            var List = connection.QueryFirstOrDefault<Medico>("SELECT * FROM Medico WHERE ID = @ID", param: new { ID });
            return List;
        }

        public async Task<List<Medico>> GetMedicos()
        {
            var connection = _connectionDapper.connectionString();
            connection.Open();

            var List = connection.Query<Medico>("SELECT * FROM Medico").ToList();
            return List;
        }

        public async Task InsertMedico(Medico medico)
        {

            var connection = _connectionDapper.connectionString();
            connection.Open();
            connection.ExecuteScalar(@"INSERT INTO [dbo].[Medico]
                                               ([Nome]
                                               ,[Especialidade]
                                               ,[DataInclusao]
                                               ,[Email])
                                         VALUES
                                               (@Nome,
                                                @Especialidade,
                                                @DataInclusao,
                                                @Email)", medico);


        }

        public async Task UpdateMedico(Medico medico)
        {
            var connection = _connectionDapper.connectionString();
            connection.Open();

            string sql = @"UPDATE [dbo].[Medico]
                               SET [Nome] = @Nome
                                  ,[Especialidade] = @Especialidade
                                  ,[DataAlteracao] = @DataAlteracao
                                  ,[Email] = @Email
	                               WHERE ID = @ID";

            connection.ExecuteScalar(sql, medico);


        }
    }
}
