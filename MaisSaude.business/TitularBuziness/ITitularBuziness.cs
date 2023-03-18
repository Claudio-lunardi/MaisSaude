using MaisSaude.Models;

namespace MaisSaude.Business.TitularBuziness
{
    public interface ITitularBuziness
    {
        Task InsertTitularAsync(Titular titular);
        Task UpdateTitularAsync(Titular titular);

        Task<IEnumerable<Titular>> ListaTitulares();
        Task<IEnumerable<Dependente>> ListaDependentes(string CPF);
        Titular DetalhesTitular(int ID);

        Task<bool> VerificarEmailExistente(string email, string CPFTitular);
    }
}
