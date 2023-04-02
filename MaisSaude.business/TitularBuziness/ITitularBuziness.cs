using MaisSaude.Models;

namespace MaisSaude.Business.TitularBuziness
{
    public interface ITitularBuziness
    {
        Task InsertTitular(Titular titular);
        Task UpdateTitular(Titular titular);
        Task<List<Titular>> GetTitulares();
        Task<List<Dependente>> GetDependentes(string CPF);
        Titular GetTitular(int ID);
    }
}
