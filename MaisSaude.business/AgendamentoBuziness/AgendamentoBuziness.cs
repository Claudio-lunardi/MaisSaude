using Dapper;
using MaisSaude.Infra.Dapper;
using MaisSaude.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.AgendamentoBuziness
{
    public class AgendamentoBuziness : IAgendamentoBuziness
    {
        private readonly ConnectionDapper _connectionDapper;

        public AgendamentoBuziness(ConnectionDapper connectionDapper)
        {
            _connectionDapper = connectionDapper;
        }
        public async Task Insert(Agendamento agendamento)
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();
                string query = @"INSERT INTO  Agendamento 
                               (Clinica 
                               , Medico 
                               , Especialidade 
                               , DataConsulta 
                               , Paciente 
                               , UsuarioPaciente 
                               , DataInclusao 
                               , DataAlteracao)
                         VALUES
                               ( @Clinica, 
                                 @Medico, 
                                 @Especialidade, 
                                 @DataConsulta, 
                                 @Paciente, 
                                 @UsuarioPaciente, 
                                 @DataInclusao, 
                                 @DataAlteracao)";
                connection.ExecuteScalar(query, agendamento);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Agendamento>> SelectAgendamento(string usuario)
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();
                var TitularReturn = connection.Query<Agendamento>("SELECT * FROM Agendamento WHERE UsuarioPaciente = @usuario", param: new { usuario }).ToList();
                return TitularReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
