using MaisSaude.Common;
using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MaisSaude.Controllers.Area
{
    public class MedicoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IApiToken _IApiToken;

        public MedicoController(IHttpClientFactory httpClient, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
        {
            _httpClient = httpClient.CreateClient();
            _dadosBase = dadosBase;
            _IApiToken = iApiToken;
        }

        #region INDEX

        public async Task<ActionResult> Index(string mensagem = null, bool sucesso = true)
        {
            try
            {
                if (sucesso)
                    TempData["sucesso"] = mensagem;
                else
                    TempData["erro"] = mensagem;

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Medico/GetMedicos");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<IEnumerable<Medico>>(await r.Content.ReadAsStringAsync()));
                }
                else
                {
                    throw new Exception("Erro ao tentar listar dependentes");

                }
            }
            catch (Exception x)
            {

                throw x;
            }

        }
        #endregion

        #region POST
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Medico medico)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Medico/InsertMedico", medico);
                if (r.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), new { mensagem = "Registro Salvo!", sucesso = true });
                }
                else
                {

                    throw new Exception("Erro ao tentar listar dependentes");
                };
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region EDIT
        public async Task<ActionResult> Edit(int ID)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Medico/GetMedico?ID={ID}");

                if (r.IsSuccessStatusCode)
                    return View(JsonConvert.DeserializeObject<Medico>(await r.Content.ReadAsStringAsync()));
                else
                    throw new Exception("Erro ao tentar mostrar um medico!");

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Medico medico)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Medico/UpdateMedico", medico);
                if (r.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), new { mensagem = "Registro salvo!", sucesso = true });
                }
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

        #endregion

    }
}
