using MaisSaude.Business.AgendamentoBuziness;
using MaisSaude.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoBuziness _IAgendamentoBuziness;
        public AgendamentoController(IAgendamentoBuziness iAgendamentoBuziness)
        {
            _IAgendamentoBuziness = iAgendamentoBuziness;
        }

        #region Post
        [HttpPost("InsertAgendamento")]
        public async Task InsertAgendamento(Agendamento agendamento)
        {
            await _IAgendamentoBuziness.InsertAgendamento(agendamento);
        }

        #endregion

        #region GET
        [HttpGet("GetMedico")]
        public async Task<Medico> GetMedico(string especialidade)
        {
            return await _IAgendamentoBuziness.GetMedico(especialidade);
        }

        [HttpGet("GetAgendamentos")]
        public async Task<List<Agendamento>> GetAgendamentos(string usuario)
        {
            return await _IAgendamentoBuziness.GetAgendamentos(usuario);
        }
        #endregion

    }
}
