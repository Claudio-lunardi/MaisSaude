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

        Task IncluirDependenteAsync(Dependente dependente);

        Task<IEnumerable<Dependente>> ListaDependentes();

        Task<IEnumerable<Dependente>> DetalhesDependente(string CPF);
        Task EditarDependente(Dependente dependente);



    }
}
