using MaisSaude.Business.ClinicaBuziness;
using MaisSaude.Business.DependenteBuziness;
using MaisSaude.Business.MedicoBuzines;
using MaisSaude.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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


        [HttpGet]
        public async Task<List<Medico>> GetMedico()
        {
            return await _MedicoBuziness.ListaMedico();
        }

        [HttpGet("DetailsMedico")]
        public async Task<Medico> DetailsMedico(int ID)
        {
          return await _MedicoBuziness.DetailsMedico(ID);
        }

        [HttpPut("PutMedico")]
        public async Task PutMedico(Medico medico)
        {
            medico.DataAlteracao = DateTime.Now;
            await _MedicoBuziness.PutMedico(medico);
        }

        [HttpPost]
        public async Task PostMedico(Medico medico)
        {
            medico.DataInclusao = DateTime.Now;
            await _MedicoBuziness.PostMedico(medico);
        }


    }
}
