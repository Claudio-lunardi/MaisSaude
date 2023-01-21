namespace MaisSaude.Common.Login.ObterToken
{
    public interface IApiToken
    {
        Task<string> Obter();
    }
}
