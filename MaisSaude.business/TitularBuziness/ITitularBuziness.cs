using MaisSaude.Models;

namespace MaisSaude.Business.TitularBuziness
{
    public interface ITitularBuziness
    {
        Task InsertTitularAsync(Titular titular);
        Task UpdateTitularAsync(Titular titular);

        Task<IEnumerable<Titular>> ListaTitulares();
        Task<IEnumerable<Dependente>> ListaDependentes(string CPFTitular);
        Titular DetalhesTitular(string CPF);
    }
}
