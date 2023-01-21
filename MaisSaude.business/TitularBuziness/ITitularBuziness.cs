using MaisSaude.Models;

namespace MaisSaude.Business.TitularBuziness
{
    public interface ITitularBuziness
    {
        Task InsertTitularAsync(Titular titular);
        Task UpdateTitularAsync(Titular titular);

        Task<IEnumerable<Titular>> ListaTitulares();

        Titular DetalhesTitular(string CPF);
    }
}
