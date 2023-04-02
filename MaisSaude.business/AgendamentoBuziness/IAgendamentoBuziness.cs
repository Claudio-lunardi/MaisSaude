using MaisSaude.Models;

namespace MaisSaude.Business.AgendamentoBuziness
{
    public interface IAgendamentoBuziness
    {
        Task InsertAgendamento(Agendamento agendamento);
        Task<List<Agendamento>> GetAgendamentos(string usuario);
        Task<Medico> GetMedico(string especialidade);

    }
}
