using MaisSaude.Business.TitularBuziness;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TitularController : ControllerBase
    {
        private readonly ITitularBuziness _TitularBuziness;
        public TitularController(ITitularBuziness clienteBuziness)
        {
            _TitularBuziness = clienteBuziness;
        }


        #region POST

        [HttpPost("InsertTitular")]
        public async Task<ActionResult> InsertTitular([FromBody] Titular titular)
        {
            titular.TipoPermissao = "titular";
            titular.DataInclusao = DateTime.Now;
            try
            {

                await _TitularBuziness.InsertTitular(titular);
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        #region PUT 
        [HttpPut("UpdateTitular")]
        public async Task<ActionResult> UpdateTitular(Titular titular)
        {
            titular.DataAlteracao = DateTime.Now;
            try
            {

                var r = _TitularBuziness.UpdateTitular(titular);
                return Ok(r);



            }
            catch (Exception Z)
            {
                return BadRequest(Z);
            }

        }

        #endregion

        #region GET
        [HttpGet("GetTitular")]
        public async Task<ActionResult<Titular>> GetTitular([FromQuery] int ID)
        {
            try
            {
                return Ok(_TitularBuziness.GetTitular(ID));
            }
            catch (Exception Z)
            {
                return BadRequest(Z);
            }
        }

        [HttpGet("GetTitulares")]
        public async Task<ActionResult<List<Titular>>> GetTitulares()
        {
            try
            {
                return Ok(await _TitularBuziness.GetTitulares());
            }
            catch (Exception Z)
            {
                return BadRequest(Z);
            }
        }

        [HttpGet("GetDependentes")]
        public async Task<ActionResult<List<Dependente>>> GetDependentes(string CPF)
        {
            try
            {
                return Ok(await _TitularBuziness.GetDependentes(CPF));
            }
            catch (Exception x)
            {
                return BadRequest(x);
            }
        }
        #endregion

    }
}
