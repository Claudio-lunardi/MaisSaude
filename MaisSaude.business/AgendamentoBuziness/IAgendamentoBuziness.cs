using MaisSaude.Models;

namespace MaisSaude.Business.AgendamentoBuziness
{
    public interface IAgendamentoBuziness
    {
        Task Insert(Agendamento agendamento);
        Task<List<Agendamento>> SelectAgendamento(string usuario);

        Task<Medico> GetMedico(string especialidade);

    }
}
