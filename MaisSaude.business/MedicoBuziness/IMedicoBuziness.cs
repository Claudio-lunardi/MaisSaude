using MaisSaude.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MaisSaude.Business.MedicoBuzines
{
    public interface IMedicoBuziness
    {
        Task<List<Medico>> ListaMedico();
        Task<Medico> DetailsMedico(int ID);
        Task PostMedico(Medico medico);
        Task PutMedico(Medico medico);

    }
}
