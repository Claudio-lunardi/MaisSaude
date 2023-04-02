using Dapper;
using MaisSaude.Business.Rabbit;
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
        private readonly IMensageria _mensageria;
        public AgendamentoBuziness(ConnectionDapper connectionDapper, IMensageria mensageria)
        {
            _connectionDapper = connectionDapper;
            _mensageria = mensageria;
        }

        public async Task InsertAgendamento(Agendamento agendamento)
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
                               , DataAlteracao
                               , Ativo
                               , Email )
                         VALUES
                               ( @Clinica, 
                                 @Medico, 
                                 @Especialidade, 
                                 @DataConsulta, 
                                 @Paciente, 
                                 @UsuarioPaciente, 
                                 @DataInclusao, 
                                 @DataAlteracao,
                                 @Ativo, 
                                 @Email)";
                connection.ExecuteScalar(query, agendamento);
                

                _mensageria.EnviarMensagemRabbit(agendamento, "", "Email");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Agendamento>> GetAgendamentos(string usuario)
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
        public async Task<Medico> GetMedico(string Especialidade)
        {
            try
            {
                var connection = _connectionDapper.connectionString();
                connection.Open();
                var TitularReturn = connection.QueryFirstOrDefault<Medico>("SELECT Nome FROM Medico WHERE Especialidade = @Especialidade", param: new { Especialidade });
                return TitularReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
