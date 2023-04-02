using MaisSaude.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MaisSaude.Business.MedicoBuziness
{
    public interface IMedicoBuziness
    {
        Task<List<Medico>> GetMedicos();
        Task<Medico> GetMedico(int ID);
        Task InsertMedico(Medico medico);
        Task UpdateMedico(Medico medico);

    }
}
