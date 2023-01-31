﻿using MaisSaude.Business.ClinicaBuziness;
using MaisSaude.Business.TitularBuziness;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClinicaController : ControllerBase
    {
        private readonly IClinicaBuziness _ClinicaBuziness;

        public ClinicaController(IClinicaBuziness clinicaBuziness)
        {
            _ClinicaBuziness = clinicaBuziness;
        }

        [HttpGet]
        public async Task<IEnumerable<Clinica>> ListaClinicas()
        {
            return await _ClinicaBuziness.ListaClinicas();
        }


        [HttpPost]
        public async Task IncluirClinica(Clinica clinica)
        {
             await _ClinicaBuziness.IncluirClinica(clinica);
        }
        [HttpPut]
        public async Task EditarClinica(Clinica clinica)
        {
            await _ClinicaBuziness.EditarClinica(clinica);
        }
       
        [HttpGet("DetalhesClinica")]
        public async Task<Clinica> DetalhesClinica([FromQuery] int id)
        {
           return await _ClinicaBuziness.DetalhesClinica(id);
        }


    }
}
