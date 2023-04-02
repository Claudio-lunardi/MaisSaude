using MaisSaude.Business.MedicoBuziness;
using MaisSaude.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoBuziness _MedicoBuziness;

        public MedicoController(IMedicoBuziness MedicoBuziness)
        {
            _MedicoBuziness = MedicoBuziness;
        }

        #region PUT
        [HttpPut("UpdateMedico")]
        public async Task UpdateMedico(Medico medico)
        {
            medico.DataAlteracao = DateTime.Now;
            await _MedicoBuziness.UpdateMedico(medico);
        }
        #endregion

        #region POST
        [HttpPost("InsertMedico")]
        public async Task InsertMedico(Medico medico)
        {
            medico.DataInclusao = DateTime.Now;
            await _MedicoBuziness.InsertMedico(medico);
        }
        #endregion

        #region GET


        [HttpGet("GetMedicos")]
        public async Task<List<Medico>> GetMedicos()
        {
            return await _MedicoBuziness.GetMedicos();
        }

        [HttpGet("GetMedico")]
        public async Task<Medico> GetMedico(int ID)
        {
            return await _MedicoBuziness.GetMedico(ID);
        }
        #endregion

    }
}
