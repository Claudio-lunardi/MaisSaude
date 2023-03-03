﻿using MaisSaude.Business.TitularBuziness;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TitularController : ControllerBase
    {
        private readonly ITitularBuziness _TitularBuziness;
        public TitularController(ITitularBuziness clienteBuziness)
        {
            _TitularBuziness = clienteBuziness;
        }

        [HttpPost]
        public async Task<ActionResult> InsertCliente([FromBody] Titular titular)
        {
            titular.TipoPermissao = "titular";
            titular.DataInclusao = DateTime.Now;
            try
            {
              bool EmailExistente = await _TitularBuziness.VerificarEmailExistente(titular.Email, titular.CPFTitular);

                if (EmailExistente)
                {
                    return Ok("O e-mail informado ja é existente!");
                }
                else
                {
                    await _TitularBuziness.InsertTitularAsync(titular);
                    return Ok(); 
                }       
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateTitular")]
        public async Task<ActionResult> UpdateTítular(Titular titular)
        {
            titular.DataAlteracao = DateTime.Now;
            try
            {             
                bool EmailExistente = await _TitularBuziness.VerificarEmailExistente(titular.Email,titular.CPFTitular);

                if (EmailExistente)
                {
                    return Ok("O e-mail informado ja é existente!");
                }
                else
                {
                    var r = _TitularBuziness.UpdateTitularAsync(titular);
                    return Ok(r);
                }


            }
            catch (Exception Z)
            {
                return BadRequest(Z);
            }

        }

        [HttpGet("ObterUmTitular")]
        public async Task<ActionResult<Titular>> DetalhesTitular([FromQuery] string CPF)
        {
            try
            {
                return Ok(_TitularBuziness.DetalhesTitular(CPF));
            }
            catch (Exception Z)
            {
                return BadRequest(Z);
            }
        }


        [HttpGet("ListaTitulares")]
        public async Task<ActionResult<IEnumerable<Titular>>> ListaTitulares()
        {
            try
            {
                return Ok(await _TitularBuziness.ListaTitulares());
            }
            catch (Exception Z)
            {
                return BadRequest(Z);
            }
        }

        [HttpGet("ListaDependente")]
        public async Task<ActionResult<IEnumerable<Dependente>>> ListaDependente( string CPFTitular)
        {
            try
            {
                return Ok(await _TitularBuziness.ListaDependentes(CPFTitular));
            }
            catch (Exception Z)
            {
                return BadRequest(Z);
            }
        }
    
    }
}
