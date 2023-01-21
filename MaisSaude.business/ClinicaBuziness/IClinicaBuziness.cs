using MaisSaude.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.ClinicaBuziness
{
    public interface IClinicaBuziness
    {

        Task<IEnumerable<Clinica>> ListaClinicas();
        Task<Clinica> DetalhesClinica(int Id);
        Task IncluirClinica(Clinica clinica);
        Task EditarClinica(Clinica clinica);



    }
}
