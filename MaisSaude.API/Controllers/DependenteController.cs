using MaisSaude.Business.DependenteBuziness;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DependenteController : ControllerBase
    {
        private readonly IDependenteBuziness _idependenteBuziness;


        public DependenteController(IDependenteBuziness idependenteBuziness)
        {
            _idependenteBuziness = idependenteBuziness;
        }

        [HttpPost("InserirDependente")]
        public async Task IncluirDependente([FromBody] Dependente dependente)
        {

            await _idependenteBuziness.IncluirDependenteAsync(dependente);
        }
        [HttpGet]
        public async Task<IEnumerable<Dependente>> ListaDependentes()
        {
            return await _idependenteBuziness.ListaDependentes();
        }

        [HttpGet("DetalhesDependente")]
        public async Task<IEnumerable<Dependente>> DetalhesDependente([FromQuery] string CPF)
        {
            return await _idependenteBuziness.DetalhesDependente(CPF);
        }

        [HttpPut("UpdateDependente")]
        public async Task EditarDependente(Dependente dependente)
        {
            await _idependenteBuziness.EditarDependente(dependente);
        }
    }
}