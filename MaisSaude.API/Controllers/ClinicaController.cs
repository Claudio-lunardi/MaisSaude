﻿using MaisSaude.Business.ClinicaBuziness;
using MaisSaude.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task IncluirClinica([FromBody] Clinica clinica)
        {
            await _ClinicaBuziness.IncluirClinica(clinica);
        }
        [HttpPut]
        public async Task EditarClinica([FromBody] Clinica clinica)
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
