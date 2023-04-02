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

        Task<List<Clinica>> GetClinicas();
        Task<Clinica> GetClinica(int Id);
        Task InsertClinica(Clinica clinica);
        Task UpdateClinica(Clinica clinica);



    }
}
