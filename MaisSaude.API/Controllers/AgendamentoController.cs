using MaisSaude.Business.AgendamentoBuziness;
using MaisSaude.Models;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("insert")]
        public async Task DetalhesClinica(Agendamento agendamento)
        {
            await _IAgendamentoBuziness.Insert(agendamento);
        }

        [HttpGet()]
        public async Task<List<Agendamento>> SelectAgendamento(string usuario)
        {
          return  await _IAgendamentoBuziness.SelectAgendamento(usuario);
        }

    }
}
