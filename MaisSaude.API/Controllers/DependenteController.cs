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


        #region POST
        [HttpPost("InsertDependente")]
        public async Task InsertDependente([FromBody] Dependente dependente)
        {
            await _idependenteBuziness.InsertDependente(dependente);
        }
        #endregion

        #region PUT
        [HttpPut("UpdateDependente")]
        public async Task UpdateDependente(Dependente dependente)
        {
            await _idependenteBuziness.UpdateDependente(dependente);
        }

        #endregion

        #region GET
        [HttpGet("GetDependentes")]
        public async Task<List<Dependente>> GetDependentes()
        {
            return await _idependenteBuziness.GetDependentes();
        }

        [HttpGet("GetDependente")]
        public async Task<Dependente> GetDependente([FromQuery] int ID)
        {
            return await _idependenteBuziness.GetDependente(ID);
        }
        #endregion

    }
}