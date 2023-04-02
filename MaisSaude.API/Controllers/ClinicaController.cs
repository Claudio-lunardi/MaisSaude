using MaisSaude.Business.ClinicaBuziness;
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

        #region POST
        [HttpPost("InsertClinica")]
        public async Task InsertClinica([FromBody] Clinica clinica)
        {
            await _ClinicaBuziness.InsertClinica(clinica);
        }
        #endregion

        #region UPDATE
        [HttpPut("UpdateClinica")]
        public async Task UpdateClinica([FromBody] Clinica clinica)
        {
            await _ClinicaBuziness.UpdateClinica(clinica);
        }
        #endregion

        #region GET
        [HttpGet("GetClinica")]
        public async Task<Clinica> GetClinica([FromQuery] int id)
        {
            return await _ClinicaBuziness.GetClinica(id);
        }

        [HttpGet("GetClinicas")]
        public async Task<List<Clinica>> GetClinicas()
        {
            return await _ClinicaBuziness.GetClinicas();
        }
        #endregion

    }
}
