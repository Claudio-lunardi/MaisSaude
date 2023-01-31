using MaisSaude.Business.TitularBuziness;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly ITitularBuziness _clienteBuziness;

        public ClienteController(ITitularBuziness clienteBuziness)
        {
            _clienteBuziness = clienteBuziness;
        }

        [HttpPost]
        public async Task InsertCliente([FromBody] Titular titular)
        {
            titular.TipoPermissao = "usuario";
            titular.DataInclusao = DateTime.Now;
            await _clienteBuziness.InsertTitularAsync(titular);
        }

        [HttpPut("UpdateTitular")]
        public async Task UpdateTítular(Titular titular)
        {
            titular.DataAlteracao = DateTime.Now;
            await _clienteBuziness.UpdateTitularAsync(titular);
        }


        [HttpGet("ObterUmTitular")]
        public async Task<Titular> DetalhesTitular(string CPF)
        {
            return _clienteBuziness.DetalhesTitular(CPF);
        }


        [HttpGet("ListaTitulares")]
        public async Task<IEnumerable<Titular>> ListaTitulares()
        {
            return await _clienteBuziness.ListaTitulares();
        } 

        [HttpGet("ListaDependente")]
        public async Task<IEnumerable<Dependente>> ListaDependente(string CPFTitular)
        {
            return await _clienteBuziness.ListaDependentes(CPFTitular);
        }

    }
}
