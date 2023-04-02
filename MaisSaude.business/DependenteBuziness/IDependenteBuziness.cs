using MaisSaude.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MaisSaude.Business.DependenteBuziness
{
    public interface IDependenteBuziness
    {

        Task InsertDependente(Dependente dependente);

        Task<List<Dependente>> GetDependentes();

        Task<Dependente> GetDependente(int ID);
        Task UpdateDependente(Dependente dependente);



    }
}
