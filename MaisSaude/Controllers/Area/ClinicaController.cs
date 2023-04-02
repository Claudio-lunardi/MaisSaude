using MaisSaude.Common;
using MaisSaude.Common.Login.ObterToken;
using MaisSaude.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MaisSaude.Controllers.Area
{
    public class ClinicaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IApiToken _IApiToken;

        public ClinicaController(IHttpClientFactory httpClient, IOptions<DadosBase> dadosBase, IApiToken iApiToken)
        {
            _httpClient = httpClient.CreateClient();
            _dadosBase = dadosBase;
            _IApiToken = iApiToken;
        }

        #region GET
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Clinica/GetClinica?Id={id}");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<Clinica>(await r.Content.ReadAsStringAsync()));
                }
                else
                {
                    throw new Exception("Erro ao tentar listar dependentes");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

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
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Clinica/GetClinicas");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<List<Clinica>>(await r.Content.ReadAsStringAsync()));
                }
                else
                {
                    throw new Exception("Erro ao tentar listar dependentes");

                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        #endregion

        #region EDIT


        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Clinica/GetClinica?Id={id}");
                if (r.IsSuccessStatusCode)
                {
                    return View(JsonConvert.DeserializeObject<Clinica>(await r.Content.ReadAsStringAsync()));
                }
                else
                {
                    throw new Exception("Erro ao tentar listar clinica");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Clinica clinica)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                    HttpResponseMessage r = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Clinica/UpdateClinica", clinica);

                    if (r.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro Editado!", sucesso = true });
                    else
                        throw new Exception("Erro ao tentar Editar uma clinica!");

                }
                else
                {
                    TempData["erro"] = "Algum campo deve estar faltando preenchimento";
                    return View();
                }
            }
            catch
            {
                return View();
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
        public async Task<ActionResult> Create(Clinica clinica)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _IApiToken.Obter());
                HttpResponseMessage r = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Clinica/InsertClinica", clinica);
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
