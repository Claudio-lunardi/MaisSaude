
using MaisSaude.Common.Login.ModelLogin;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MaisSaude.Common.Login.ObterToken
{
    public class ApiToken : IApiToken
    {

        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<LoginRespostaModel> _LoginRespostaModel;
        private readonly HttpClient _httpClient;

        public ApiToken(IOptions<DadosBase> urlApi, IOptions<LoginRespostaModel> loginRespostaModel, IHttpClientFactory httpClient)
        {
            _dadosBase = urlApi;
            _LoginRespostaModel = loginRespostaModel;
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task ObterToken()
        {
            LoginRequisicaoModel loginRequisicaoModel = new LoginRequisicaoModel();
            loginRequisicaoModel.Usuario = "UsuarioDevPratica";
            loginRequisicaoModel.Senha = "SenhaDevPratica";

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Login", loginRequisicaoModel);

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                LoginRespostaModel loginRespostaModel = JsonConvert.DeserializeObject<LoginRespostaModel>(conteudo);


                if (loginRespostaModel.Autenticado == true)
                {
                    _LoginRespostaModel.Value.Autenticado = loginRespostaModel.Autenticado;
                    _LoginRespostaModel.Value.Usuario = loginRespostaModel.Usuario;
                    _LoginRespostaModel.Value.DataExpiracao = loginRespostaModel.DataExpiracao;
                    _LoginRespostaModel.Value.Token = loginRespostaModel.Token;
                }
            }

            else
            {
                throw new Exception("DEU ZIKA");
            }

        }
        public async Task<string> Obter()
        {
            if (_LoginRespostaModel.Value.Autenticado == false)
            {
                await ObterToken();
            }
            else
            {
                if (DateTime.Now >= _LoginRespostaModel.Value.DataExpiracao)
                {
                    await ObterToken();
                }
            }
            return _LoginRespostaModel.Value.Token;
        }
    }
}
